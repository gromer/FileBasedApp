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

    protected ServiceProviderOptions _serviceProviderOptions = new ServiceProviderOptions { ValidateScopes = true, ValidateOnBuild = true };

    public FileBasedApplicationBase()
    {
        this._configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.{this.GetType().Name}.json",
                optional: true,
                reloadOnChange: true);

        this._serviceCollection = new ServiceCollection();
        this._serviceCollection.AddLogging(builder =>
        {
            builder.AddSimpleConsole();
        });
    }

    public virtual void SetupBuild() { }

    public virtual void SetupServices() { }
}