using Microsoft.Playwright;

namespace AutomationExercise.Tests.Pages;

public class LoginSignupPage(IPage page)
{
    private ILocator NewUserSignupHeading => page.Locator("h2:has-text('New User Signup!')");
    private ILocator SignupNameInput => page.Locator("input[data-qa='signup-name']");
    private ILocator SignupEmailInput => page.Locator("input[data-qa='signup-email']");
    private ILocator SignupButton => page.Locator("button[data-qa='signup-button']");
    private ILocator SignupEmailExistsError => page.Locator("p:has-text('Email Address already exist!')");

    private ILocator LoginHeading => page.Locator("h2:has-text('Login to your account')");
    private ILocator LoginEmailInput => page.Locator("input[data-qa='login-email']");
    private ILocator LoginPasswordInput => page.Locator("input[data-qa='login-password']");
    private ILocator LoginButton => page.Locator("button[data-qa='login-button']");
    private ILocator LoginError => page.Locator("p:has-text('Your email or password is incorrect!')");

    public async Task NavigateAsync() => await page.GotoAsync("/login");

    public async Task VerifyNewUserSignupVisibleAsync()
        => await Assertions.Expect(NewUserSignupHeading).ToBeVisibleAsync();

    public async Task VerifyLoginToAccountVisibleAsync()
        => await Assertions.Expect(LoginHeading).ToBeVisibleAsync();

    public async Task SignupAsync(string name, string email)
    {
        await SignupNameInput.FillAsync(name);
        await SignupEmailInput.FillAsync(email);
        await SignupButton.ClickAsync(new() { Force = true });
        await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
    }

    public async Task LoginAsync(string email, string password)
    {
        await LoginEmailInput.FillAsync(email);
        await LoginPasswordInput.FillAsync(password);
        await LoginButton.ClickAsync(new() { Force = true });
        await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
    }

    public async Task VerifyLoginErrorVisibleAsync()
        => await Assertions.Expect(LoginError).ToBeVisibleAsync();

    public async Task VerifyEmailExistsErrorVisibleAsync()
        => await Assertions.Expect(SignupEmailExistsError).ToBeVisibleAsync();

    public async Task VerifyLoginFormElementsVisibleAsync()
    {
        await Assertions.Expect(LoginEmailInput).ToBeVisibleAsync();
        await Assertions.Expect(LoginPasswordInput).ToBeVisibleAsync();
        await Assertions.Expect(LoginButton).ToBeVisibleAsync();
    }
}
