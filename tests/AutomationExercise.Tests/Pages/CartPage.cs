using Microsoft.Playwright;

namespace AutomationExercise.Tests.Pages;

public class CartPage(IPage page)
{
    private ILocator ProceedToCheckoutButton => page.Locator("a.btn.btn-default.check_out");
    private ILocator CheckoutModalRegisterLink => page.Locator(".modal-body a[href='/login']");

    public async Task NavigateAsync() => await page.GotoAsync("/view_cart");

    public async Task VerifyCartPageVisibleAsync()
        => await Assertions.Expect(page).ToHaveURLAsync(new System.Text.RegularExpressions.Regex("/view_cart"));

    public async Task ClickProceedToCheckoutAsync() => await ProceedToCheckoutButton.ClickAsync(new() { Force = true });

    public async Task ClickRegisterLoginInModalAsync()
    {
        await Assertions.Expect(CheckoutModalRegisterLink).ToBeVisibleAsync();
        await CheckoutModalRegisterLink.ClickAsync(new() { Force = true });
    }
}
