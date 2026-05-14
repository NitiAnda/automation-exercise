using Microsoft.Playwright;

namespace AutomationExercise.Tests.Pages;

public class TestCasesPage(IPage page)
{
    private ILocator TestCasesHeading => page.Locator("h2.title.text-center b");

    public async Task NavigateAsync() => await page.GotoAsync("/test_cases");

    public async Task VerifyTestCasesPageLoadedAsync()
    {
        await Assertions.Expect(page).ToHaveURLAsync(new System.Text.RegularExpressions.Regex("/test_cases"));
        await Assertions.Expect(TestCasesHeading).ToBeVisibleAsync();
    }
}
