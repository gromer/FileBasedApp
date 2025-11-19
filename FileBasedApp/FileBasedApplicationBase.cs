using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FileBasedApp;

public abstract class FileBasedApplicationBase
{
    private readonly ServiceProviderOptions _serviceProviderOptions;

    protected IConfiguration? Configuration { get; private set; }

    // ReSharper disable MemberCanBePrivate.Global
    protected IConfigurationBuilder ConfigurationBuilder { get; }
    // ReSharper restore MemberCanBePrivate.Global

    // ReSharper disable MemberCanBePrivate.Global
    protected IServiceCollection ServiceCollection { get; }
    // ReSharper restore MemberCanBePrivate.Global

    protected IServiceProvider? ServiceProvider { get; private set; }

    protected FileBasedApplicationBase()
    {
        this.ConfigurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.{this.GetType().Name}.json",
                optional: true,
                reloadOnChange: false);

        this.ServiceCollection = new ServiceCollection()
            .AddLogging(builder => builder.AddSimpleConsole());

        this._serviceProviderOptions = new ServiceProviderOptions
        {
            ValidateScopes = true,
            ValidateOnBuild = true
        };
    }

    protected virtual void SetupBuild() { }

    protected virtual void SetupServices() { }

    internal void BuildConfiguration() => this.Configuration = this.ConfigurationBuilder.Build();

    internal void BuildServiceProvider() => this.ServiceProvider = this.ServiceCollection.BuildServiceProvider(this._serviceProviderOptions);
}
