using Microsoft.Extensions.DependencyInjection;

namespace FileBasedApp;

public abstract class AsyncFileBasedApplication : FileBasedApplicationBase
{
    public AsyncFileBasedApplication Build()
    {
        this.SetupBuild();
        this._configuration = this._configurationBuilder.Build();
        return this;
    }

    public AsyncFileBasedApplication ConfigureServices()
    {
        this.SetupServices();
        this.BuildServiceProvider();
        return this;
    }

    public abstract Task Run();
}