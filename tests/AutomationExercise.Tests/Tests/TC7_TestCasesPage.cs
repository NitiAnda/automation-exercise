using AutomationExercise.Tests.Fixtures;
using AutomationExercise.Tests.Pages;
using NUnit.Framework;

namespace AutomationExercise.Tests.Tests;

[TestFixture]
[Category("Smoke")]
public class TC7_TestCasesPage : BaseTest
{
    [Test]
    [Description("TC7: Navigating to Test Cases page shows the test cases list")]
    public async Task TestCasesPage_ShouldBeVisible()
    {
        var home = new HomePage(Page);
        var testCasesPage = new TestCasesPage(Page);

        await home.NavigateAsync();
        await home.VerifyHomePageVisibleAsync();

        await testCasesPage.NavigateAsync();
        await testCasesPage.VerifyTestCasesPageLoadedAsync();
    }
}
