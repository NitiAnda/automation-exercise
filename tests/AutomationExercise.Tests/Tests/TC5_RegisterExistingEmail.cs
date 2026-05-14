using AutomationExercise.Tests.Fixtures;
using AutomationExercise.Tests.Helpers;
using AutomationExercise.Tests.Pages;
using NUnit.Framework;

namespace AutomationExercise.Tests.Tests;

[TestFixture]
[Category("Regression")]
public class TC5_RegisterExistingEmail : BaseTest
{
    [Test]
    [Description("TC5: Attempting to register with an already-registered email shows an error")]
    public async Task RegisterWithExistingEmail_ShouldShowError()
    {
        var user = TestDataFactory.GenerateUser();
        var address = TestDataFactory.GenerateAddress();

        var home = new HomePage(Page);
        var loginSignup = new LoginSignupPage(Page);
        var accountCreated = new AccountCreatedPage(Page);

        await home.NavigateAsync();
        await loginSignup.NavigateAsync();
        await loginSignup.SignupAsync(user.Name, user.Email);
        await accountCreated.FillRegistrationFormAsync(user, address);
        await accountCreated.ClickCreateAccountAsync();
        await accountCreated.ClickContinueAsync();

        await home.LogoutAsync();

        await loginSignup.NavigateAsync();
        await loginSignup.SignupAsync(user.Name, user.Email);
        await loginSignup.VerifyEmailExistsErrorVisibleAsync();

        await loginSignup.LoginAsync(user.Email, user.Password);
        await home.DeleteAccountAsync();
    }
}
