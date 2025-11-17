using Microsoft.Extensions.DependencyInjection;

namespace FileBasedApp;

public abstract class FileBasedApplication : FileBasedApplicationBase
{
    public FileBasedApplication Build()
    {
        Console.WriteLine("Build");
        this.SetupBuild();
        
        this._configuration = this._configurationBuilder.Build();
        return this;
    }

    public FileBasedApplication ConfigureServices()
    {
        Console.WriteLine("ConfigureServices");
        this.SetupServices();

        this._serviceProvider = this._serviceCollection.BuildServiceProvider();
        return this;
    }

    public abstract void Run();
}