using AutomationExercise.Tests.Fixtures;
using AutomationExercise.Tests.Pages;
using NUnit.Framework;

namespace AutomationExercise.Tests.Tests;

[TestFixture]
[Category("Smoke")]
public class TC9_SearchProduct : BaseTest
{
    [Test]
    [Description("TC9: Searching for a product returns relevant results")]
    public async Task SearchProduct_ShouldReturnResults()
    {
        var home = new HomePage(Page);
        var products = new ProductsPage(Page);

        await home.NavigateAsync();
        await products.NavigateAsync();
        await products.VerifyAllProductsHeadingVisibleAsync();

        await products.SearchProductAsync("top");
        await products.VerifySearchedProductsHeadingVisibleAsync();
        await products.VerifyProductsListNotEmptyAsync();
    }
}
