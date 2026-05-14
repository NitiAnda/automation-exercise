using Microsoft.Playwright;

namespace AutomationExercise.Tests.Pages;

public class HomePage(IPage page)
{
    private ILocator NavSignupLogin => page.Locator("#header a[href='/login']");
    private ILocator NavProducts => page.Locator("#header a[href='/products']");
    private ILocator NavTestCases => page.Locator("#header a[href='/test_cases']");
    private ILocator NavCart => page.Locator("#header a[href='/view_cart']");
    private ILocator NavDeleteAccount => page.Locator("#header a[href='/delete_account']");
    private ILocator NavLogout => page.Locator("#header a[href='/logout']");
    private ILocator LoggedInLabel => page.Locator("#header li:has-text('Logged in as')");
    private ILocator AccountDeletedHeading => page.Locator("b:has-text('ACCOUNT DELETED!')");
    private ILocator ContinueAfterDeleteButton => page.Locator("a[data-qa='continue-button']");
    private ILocator SubscriptionEmailInput => page.Locator("#susbscribe_email");
    private ILocator SubscribeButton => page.Locator("#subscribe");
    private ILocator SubscriptionSuccessAlert => page.Locator("#success-subscribe");
    private ILocator SubscriptionHeading => page.Locator("h2:has-text('SUBSCRIPTION')");

    private ILocator WomenCategoryToggle => page.Locator("a[href='#Women']");
    private ILocator WomenDressLink => page.Locator("#Women a[href='/category_products/1']");
    private ILocator MenCategoryToggle => page.Locator("a[href='#Men']");
    private ILocator MenTshirtsLink => page.Locator("#Men a[href='/category_products/3']");
    private ILocator CategoryPageHeading => page.Locator(".features_items h2.title");

    public async Task NavigateAsync() => await page.GotoAsync("/");
    public async Task LogoutAsync() => await page.GotoAsync("/logout");

    public async Task ClickSignupLoginAsync() => await NavSignupLogin.ClickAsync(new() { Force = true });
    public async Task ClickProductsAsync() => await NavProducts.ClickAsync(new() { Force = true });
    public async Task ClickTestCasesAsync() => await NavTestCases.ClickAsync(new() { Force = true });
    public async Task ClickCartAsync() => await NavCart.ClickAsync(new() { Force = true });
    public async Task ClickLogoutAsync() => await NavLogout.ClickAsync(new() { Force = true });

    public async Task VerifyHomePageVisibleAsync()
        => await Assertions.Expect(page).ToHaveURLAsync(new System.Text.RegularExpressions.Regex("automationexercise\\.com"));

    public async Task VerifyLoggedInAsAsync(string name)
        => await Assertions.Expect(LoggedInLabel).ToContainTextAsync(name);

    public async Task DeleteAccountAsync()
    {
        await page.GotoAsync("/delete_account");
        await Assertions.Expect(AccountDeletedHeading).ToBeVisibleAsync();
    }

    public async Task SubscribeWithEmailAsync(string email)
    {
        await page.EvaluateAsync("window.scrollTo(0, document.body.scrollHeight)");
        await Assertions.Expect(SubscriptionHeading).ToBeVisibleAsync();
        await SubscriptionEmailInput.FillAsync(email);
        await SubscribeButton.ClickAsync(new() { Force = true });
        await Assertions.Expect(SubscriptionSuccessAlert).ToBeVisibleAsync();
    }

    public async Task ClickWomenCategoryAndDressAsync()
    {
        await WomenCategoryToggle.ClickAsync(new() { Force = true });
        await Assertions.Expect(WomenDressLink).ToBeVisibleAsync(new() { Timeout = 10_000 });
        await WomenDressLink.ClickAsync(new() { Force = true });
    }

    public async Task ClickMenCategoryAndTshirtsAsync()
    {
        await MenCategoryToggle.ClickAsync(new() { Force = true });
        await Assertions.Expect(MenTshirtsLink).ToBeVisibleAsync(new() { Timeout = 10_000 });
        await MenTshirtsLink.ClickAsync(new() { Force = true });
    }

    public async Task VerifyCategoryHeadingContainsAsync(string text)
        => await Assertions.Expect(CategoryPageHeading).ToContainTextAsync(text, new() { IgnoreCase = true, Timeout = 15_000 });
}
