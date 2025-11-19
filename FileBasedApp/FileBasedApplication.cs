namespace FileBasedApp;

public abstract class FileBasedApplication : FileBasedApplicationBase
{
    public FileBasedApplication Build()
    {
        this.SetupBuild();
        this.BuildConfiguration();
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
