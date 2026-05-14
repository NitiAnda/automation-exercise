using AutomationExercise.Tests.Fixtures;
using AutomationExercise.Tests.Helpers;
using AutomationExercise.Tests.Pages;
using NUnit.Framework;

namespace AutomationExercise.Tests.Tests;

[TestFixture]
[Category("Regression")]
public class TC3_LoginIncorrect : BaseTest
{
    private const string InvalidEmail = "invalid_user@notexist.dev";
    private const string InvalidPassword = "WrongPassword999!";

    [Test]
    [Description("TC3: Login with incorrect credentials shows an error message")]
    public async Task LoginWithIncorrectCredentials_ShouldShowError()
    {
        var home = new HomePage(Page);
        var loginSignup = new LoginSignupPage(Page);

        await home.NavigateAsync();
        await loginSignup.NavigateAsync();
        await loginSignup.VerifyLoginToAccountVisibleAsync();

        await loginSignup.LoginAsync(InvalidEmail, InvalidPassword);
        await loginSignup.VerifyLoginErrorVisibleAsync();
    }
}
