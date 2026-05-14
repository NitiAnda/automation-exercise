using FluentAssertions;
using Microsoft.Playwright;

namespace AutomationExercise.Tests.Pages;

public class ProductsPage(IPage page)
{
    private ILocator AllProductsHeading => page.Locator(".features_items h2.title");
    private ILocator SearchInput => page.Locator("input#search_product");
    private ILocator SearchButton => page.Locator("button#submit_search");
    private ILocator SearchedProductsHeading => page.Locator("h2:has-text('SEARCHED PRODUCTS')");
    private ILocator ProductCards => page.Locator(".product-image-wrapper");
    private ILocator FirstViewProductLink => page.Locator(".choose a").First;
    private ILocator AddToCartModal => page.Locator("#cartModal");
    private ILocator ModalViewCartButton => page.Locator("#cartModal a[href='/view_cart']");
    private ILocator ModalContinueButton => page.Locator("#cartModal button:has-text('Continue Shopping')");

    public async Task NavigateAsync() => await page.GotoAsync("/products");

    public async Task VerifyAllProductsHeadingVisibleAsync()
        => await Assertions.Expect(AllProductsHeading).ToBeVisibleAsync();

    public async Task SearchProductAsync(string term)
    {
        await SearchInput.FillAsync(term);
        await SearchButton.ClickAsync(new() { Force = true });
        await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
    }

    public async Task VerifySearchedProductsHeadingVisibleAsync()
        => await Assertions.Expect(SearchedProductsHeading).ToBeVisibleAsync(new() { Timeout = 15_000 });

    public async Task VerifyProductsListNotEmptyAsync()
    {
        var count = await ProductCards.CountAsync();
        count.Should().BeGreaterThan(0, "search should return at least one product");
    }

    private async Task HoverAndAddFirstToCartAsync()
    {
        await ProductCards.First.HoverAsync();
        await ProductCards.First.Locator("a.add-to-cart").First.ClickAsync();
        await Assertions.Expect(AddToCartModal).ToBeVisibleAsync(new() { Timeout = 10_000 });
    }

    public async Task AddFirstProductToCartAsync()
    {
        await HoverAndAddFirstToCartAsync();
        await ModalContinueButton.ClickAsync(new LocatorClickOptions { Force = true });
    }

    public async Task AddFirstProductToCartAndViewCartAsync()
    {
        await HoverAndAddFirstToCartAsync();
        await ModalViewCartButton.ClickAsync();
    }

    public async Task AddFirstProductToCartAndContinueAsync()
    {
        await HoverAndAddFirstToCartAsync();
        await ModalContinueButton.ClickAsync();
    }

    public async Task ClickFirstViewProductAsync()
        => await FirstViewProductLink.ClickAsync(new LocatorClickOptions { Force = true });
}
