using AutomationExercise.Tests.Fixtures;
using AutomationExercise.Tests.Helpers;
using AutomationExercise.Tests.Pages;
using NUnit.Framework;

namespace AutomationExercise.Tests.Tests;

[TestFixture]
[Category("Regression")]
public class TC10_SubscriptionHome : BaseTest
{
    [Test]
    [Description("TC10: Subscribing to the newsletter on the home page shows a success message")]
    public async Task HomePageSubscription_ShouldShowSuccessMessage()
    {
        var subscriptionEmail = TestDataFactory.GenerateUser().Email;

        var home = new HomePage(Page);

        await home.NavigateAsync();
        await home.VerifyHomePageVisibleAsync();
        await home.SubscribeWithEmailAsync(subscriptionEmail);
    }
}
