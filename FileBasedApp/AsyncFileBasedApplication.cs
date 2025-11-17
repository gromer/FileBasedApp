using Microsoft.Extensions.DependencyInjection;

namespace FileBasedApp;

public abstract class AsyncFileBasedApplication : FileBasedApplicationBase
{
    public AsyncFileBasedApplication Build()
    {
        Console.WriteLine("Build");
        this.SetupBuild();
        
        this._configuration = this._configurationBuilder.Build();
        return this;
    }

    public AsyncFileBasedApplication ConfigureServices()
    {
        Console.WriteLine("ConfigureServices");
        this.SetupServices();

        this._serviceProvider = this._serviceCollection.BuildServiceProvider();
        return this;
    }

    public abstract Task Run();
}