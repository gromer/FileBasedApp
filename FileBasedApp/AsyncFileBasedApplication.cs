namespace FileBasedApp;

public abstract class AsyncFileBasedApplication : FileBasedApplicationBase
{
    public AsyncFileBasedApplication Build()
    {
        this.SetupBuild();
        this.BuildConfiguration();
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
