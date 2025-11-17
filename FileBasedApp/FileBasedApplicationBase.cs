using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FileBasedApp;

public abstract class FileBasedApplicationBase
{
    protected IConfiguration? _configuration;

    protected readonly IConfigurationBuilder _configurationBuilder;

    protected readonly IServiceCollection _serviceCollection;

    protected IServiceProvider? _serviceProvider;

    private readonly ServiceProviderOptions _serviceProviderOptions;
    
    public FileBasedApplicationBase()
    {
        this._configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.{this.GetType().Name}.json",
                optional: true,
                reloadOnChange: false);

        this._serviceCollection = new IServiceCollection()
            .AddLogging(builder =>
            {
                builder.AddSimpleConsole();
            });

        this._serviceProviderOptions = new ServiceProviderOptions
        {
            ValidateScopes = true,
            ValidateOnBuild = true
        };
    }

    public virtual void SetupBuild() { }

    public virtual void SetupServices() { }

    internal void BuildServiceProvider()
    {
        this._serviceProvider = this._serviceCollection.BuildServiceProvider(this._serviceProviderOptions);
    }
}