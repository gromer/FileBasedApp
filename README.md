# FileBasedApp

A base library for building file-based applications with dependency injection and configuration support.

## Features

- Base classes for building file-based applications
- Integrated dependency injection using Microsoft.Extensions.DependencyInjection
- Configuration support with JSON files
- Built-in logging with console output
- Support for both synchronous and asynchronous applications

## Installation

Install the NuGet package:

```bash
dotnet add package FileBasedApp
```

## Usage

### Synchronous Application

```csharp
using FileBasedApp;

public class MyApp : FileBasedApplication
{
    public override void SetupServices()
    {
        // Configure your services here
        base.SetupServices();
    }

    public override void Run()
    {
        // Your application logic here
    }
}

// Usage example:
public static void Main(string[] args)
{
    var app = new MyApp();
    app.Build().ConfigureServices();
    app.Run();
}
```

### Asynchronous Application

```csharp
using FileBasedApp;

public class MyAsyncApp : AsyncFileBasedApplication
{
    public override void SetupServices()
    {
        // Configure your services here
        base.SetupServices();
    }

    public override async Task Run()
    {
        // Your asynchronous application logic here
    }
}

// Usage example:
public static async Task Main(string[] args)
{
    var app = new MyAsyncApp();
    app.Build().ConfigureServices();
    await app.Run();
}
```

## Publishing to NuGet

This project is configured to automatically publish to NuGet.org using GitHub Actions.

### Prerequisites

1. A NuGet.org API key stored as a GitHub secret named `NUGET_API_KEY`

### Publishing a New Version

#### Option 1: Create a GitHub Release (Recommended)

1. Create a new tag with the version number (e.g., `v1.0.0`)
2. Push the tag to GitHub
3. Create a GitHub release from the tag
4. The package will be automatically built and published to NuGet.org

```bash
git tag v1.0.0
git push origin v1.0.0
# Then create a release on GitHub
```

#### Option 2: Manual Workflow Dispatch

1. Go to the Actions tab in GitHub
2. Select the "Publish to NuGet" workflow
3. Click "Run workflow"
4. Enter the version number (e.g., `1.0.0`)
5. Click "Run workflow"

## Building Locally

```bash
dotnet restore FileBasedApp/FileBasedApp.csproj
dotnet build FileBasedApp/FileBasedApp.csproj -c Release
dotnet pack FileBasedApp/FileBasedApp.csproj -c Release -o ./artifacts
```

## License

MIT