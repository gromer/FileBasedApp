using Microsoft.Extensions.DependencyInjection;

namespace FileBasedApp;

public abstract class FileBasedApplication : FileBasedApplicationBase
{
    public FileBasedApplication Build()
    {
        this.SetupBuild();
        this._configuration = this._configurationBuilder.Build();
        return this;
    }

    public FileBasedApplication ConfigureServices()
    {
        this.SetupServices();
        this.BuildServiceProvider();
        return this;
    }

    public abstract void Run();
}