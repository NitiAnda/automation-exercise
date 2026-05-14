using AutomationExercise.Tests.Fixtures;
using AutomationExercise.Tests.Helpers;
using AutomationExercise.Tests.Pages;
using NUnit.Framework;

namespace AutomationExercise.Tests.Tests;

[TestFixture]
[Category("Smoke")]
public class TC1_RegisterUser : BaseTest
{
    [Test]
    [Description("TC1: Register a new user and verify account creation and deletion")]
    public async Task RegisterUser_ShouldCreateAndDeleteAccount()
    {
        var user = TestDataFactory.GenerateUser();
        var address = TestDataFactory.GenerateAddress();

        var home = new HomePage(Page);
        var loginSignup = new LoginSignupPage(Page);
        var accountCreated = new AccountCreatedPage(Page);

        await home.NavigateAsync();
        await home.VerifyHomePageVisibleAsync();

        await loginSignup.NavigateAsync();
        await loginSignup.VerifyNewUserSignupVisibleAsync();

        await loginSignup.SignupAsync(user.Name, user.Email);
        await accountCreated.VerifyEnterAccountInfoVisibleAsync();

        await accountCreated.FillRegistrationFormAsync(user, address);
        await accountCreated.ClickCreateAccountAsync();
        await accountCreated.VerifyAccountCreatedVisibleAsync();

        await accountCreated.ClickContinueAsync();
        await home.VerifyLoggedInAsAsync(user.Name);

        await home.DeleteAccountAsync();
    }
}
