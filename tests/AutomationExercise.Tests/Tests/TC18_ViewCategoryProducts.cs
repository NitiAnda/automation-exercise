using AutomationExercise.Tests.Fixtures;
using AutomationExercise.Tests.Pages;
using NUnit.Framework;

namespace AutomationExercise.Tests.Tests;

[TestFixture]
[Category("Regression")]
public class TC18_ViewCategoryProducts : BaseTest
{
    [Test]
    [Description("TC18: Clicking category links in the sidebar loads the correct category product pages")]
    public async Task ViewCategoryProducts_ShouldDisplayCorrectCategory()
    {
        var home = new HomePage(Page);

        await home.NavigateAsync();
        await home.VerifyHomePageVisibleAsync();

        await home.ClickWomenCategoryAndDressAsync();
        await home.VerifyCategoryHeadingContainsAsync("WOMEN");

        await home.NavigateAsync();

        await home.ClickMenCategoryAndTshirtsAsync();
        await home.VerifyCategoryHeadingContainsAsync("MEN");
    }
}
