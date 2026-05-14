using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace AutomationExercise.Tests.Fixtures;

[Parallelizable(ParallelScope.Self)]
public class BaseTest : PageTest
{
    protected string BaseUrl => Helpers.ConfigLoader.Instance.BaseUrl;

    public override BrowserNewContextOptions ContextOptions() => new()
    {
        BaseURL = BaseUrl,
        ViewportSize = new ViewportSize { Width = 1280, Height = 720 },
        IgnoreHTTPSErrors = true
    };

    [SetUp]
    public async Task StartTraceAsync()
    {
        Page.SetDefaultTimeout(Helpers.ConfigLoader.Instance.DefaultTimeoutMs);

        await Context.Tracing.StartAsync(new TracingStartOptions
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true,
            Title = TestContext.CurrentContext.Test.FullName
        });

        await Page.GotoAsync("/");
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        try
        {
            await Page.GetByRole(AriaRole.Button, new() { Name = "Consent" })
                .ClickAsync(new() { Timeout = 5_000 });
        }
        catch (Exception ex) when (ex is TimeoutException or PlaywrightException)
        {
        }
    }

    [TearDown]
    public async Task StopTraceAsync()
    {
        var status = TestContext.CurrentContext.Result.Outcome.Status;
        var tracesDir = Path.Combine(AppContext.BaseDirectory, "playwright-traces");

        if (status == TestStatus.Failed)
        {
            Directory.CreateDirectory(tracesDir);
            var safeName = TestContext.CurrentContext.Test.Name
                .Replace(' ', '_')
                .Replace('/', '_');

            await Context.Tracing.StopAsync(new TracingStopOptions
            {
                Path = Path.Combine(tracesDir, $"{safeName}.zip")
            });

            await Page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = Path.Combine(tracesDir, $"{safeName}.png"),
                FullPage = true
            });
        }
        else
        {
            await Context.Tracing.StopAsync(new TracingStopOptions());
        }
    }
}
