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

Save this to `HelloWorld.cs`.

```csharp
#:package FileBasedApp@0.1.0

using FileBasedApp;

new HelloWorld()
    .Build()
    .ConfigureServices()
    .Run();

class HelloWorld : FileBasedApplication
{
    public override void Run()
    {
        Console.WriteLine("Hello, World!");
    }
}
```

Run with `dotnet HelloWorld.cs`.

### Asynchronous Application

Save this to `CurrentWeatherApplication.cs`.

```csharp
#:package FileBasedApp@0.1.0

using FileBasedApp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

await new CurrentWeatherApplication()
    .Build()
    .ConfigureServices()
    .Run();

class CurrentWeatherApplication : AsyncFileBasedApplication
{
    public override void SetupServices()
    {
        // Configure options
        this._serviceCollection.Configure<CurrentWeatherApplicationOptions>(this._configuration.GetSection(nameof(CurrentWeatherApplicationOptions)));

        // Register HttpClient factory
        this._serviceCollection.AddHttpClient();
    }

    public override async Task Run()
    {
        var httpClientFactory = this._serviceProvider.GetRequiredService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient();
        var logger = this._serviceProvider.GetRequiredService<ILogger<CurrentWeatherApplication>>();
        var options = this._serviceProvider.GetRequiredService<IOptions<CurrentWeatherApplicationOptions>>();

        logger.LogInformation("Application is running.");
        logger.LogInformation("Using Latitude: {Latitude}, Longitude: {Longitude}", options.Value.Latitude, options.Value.Longitude);

        var response = await httpClient.GetAsync($"{options.Value.WeatherApiUrl}?latitude={options.Value.Latitude}&longitude={options.Value.Longitude}&current_weather=true");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            logger.LogInformation("Received weather data: {@content}", content);
        }
        else
        {
            logger.LogError("Failed to fetch weather data. Status Code: {@statusCode}", response.StatusCode);
        }
    }
}

class CurrentWeatherApplicationOptions
{
    public string Latitude { get; set; } = string.Empty;

    public string Longitude { get; set; } = string.Empty;

    public string WeatherApiUrl { get; set; } = string.Empty;
}
```

Save this to `appsettings.CurrentWeatherApplication.json`

```json
{
  "CurrentWeatherApplicationOptions": {
    "Latitude": "40.589169",
    "Longitude": "-111.638812",
    "WeatherApiUrl": "https://api.open-meteo.com/v1/forecast"
  }
}
```

## Building Locally

```bash
dotnet restore FileBasedApp/FileBasedApp.csproj
dotnet build FileBasedApp/FileBasedApp.csproj -c Release
dotnet pack FileBasedApp/FileBasedApp.csproj -c Release -o ./artifacts
```

## License

MIT