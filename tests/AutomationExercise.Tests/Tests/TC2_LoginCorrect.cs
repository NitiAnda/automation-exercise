using AutomationExercise.Tests.Fixtures;
using AutomationExercise.Tests.Helpers;
using AutomationExercise.Tests.Pages;
using NUnit.Framework;

namespace AutomationExercise.Tests.Tests;

[TestFixture]
[Category("Smoke")]
public class TC2_LoginCorrect : BaseTest
{
    [Test]
    [Description("TC2: Login with correct credentials verifies the user is logged in")]
    public async Task LoginWithCorrectCredentials_ShouldSucceed()
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
        await loginSignup.VerifyLoginToAccountVisibleAsync();
        await loginSignup.LoginAsync(user.Email, user.Password);

        await home.VerifyLoggedInAsAsync(user.Name);

        await home.DeleteAccountAsync();
    }
}
