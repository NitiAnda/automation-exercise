using Microsoft.Extensions.Configuration;

namespace AutomationExercise.Tests.Helpers;

public sealed class ConfigLoader
{
    private static readonly Lazy<ConfigLoader> _instance = new(() => new ConfigLoader());
    public static ConfigLoader Instance => _instance.Value;

    private readonly IConfiguration _config;

    private ConfigLoader()
    {
        _config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables()
            .Build();
    }

    public string BaseUrl => _config["BaseUrl"] ?? "https://automationexercise.com";

    public string Browser => _config["Browser"] ?? "chromium";

    public bool IsHeadless =>
        Environment.GetEnvironmentVariable("HEADED") == "1"
            ? false
            : bool.TryParse(_config["Headless"], out var h) ? h : true;

    public int DefaultTimeoutMs =>
        int.TryParse(_config["DefaultTimeoutMs"], out var ms) ? ms : 30_000;
}
