using AutomationExercise.Tests.Fixtures;
using AutomationExercise.Tests.Helpers;
using AutomationExercise.Tests.Pages;
using NUnit.Framework;

namespace AutomationExercise.Tests.Tests;

[TestFixture]
[Category("Smoke")]
public class TC14_PlaceOrderRegisterWhileCheckout : BaseTest
{
    [Test]
    [Description("TC14: Place Order — register a new user during the checkout flow")]
    public async Task PlaceOrder_RegisterWhileCheckout_ShouldSucceed()
    {
        var user = TestDataFactory.GenerateUser();
        var address = TestDataFactory.GenerateAddress();

        var home = new HomePage(Page);
        var products = new ProductsPage(Page);
        var cart = new CartPage(Page);
        var loginSignup = new LoginSignupPage(Page);
        var accountCreated = new AccountCreatedPage(Page);
        var checkout = new CheckoutPage(Page);
        var payment = new PaymentPage(Page);

        await home.NavigateAsync();
        await products.NavigateAsync();
        await products.AddFirstProductToCartAsync();
        await cart.NavigateAsync();

        await cart.ClickProceedToCheckoutAsync();
        await cart.ClickRegisterLoginInModalAsync();

        await loginSignup.SignupAsync(user.Name, user.Email);
        await accountCreated.FillRegistrationFormAsync(user, address);
        await accountCreated.ClickCreateAccountAsync();
        await accountCreated.VerifyAccountCreatedVisibleAsync();
        await accountCreated.ClickContinueAsync();

        await cart.NavigateAsync();
        await cart.ClickProceedToCheckoutAsync();
        await checkout.VerifyCheckoutPageVisibleAsync();

        await checkout.AddCommentAndPlaceOrderAsync("Automated test order — please ignore");

        await payment.FillPaymentDetailsAsync(cardName: user.Name);
        await payment.ClickPayAndConfirmAsync();
        await payment.VerifyOrderPlacedSuccessfullyAsync();

        await home.DeleteAccountAsync();
    }
}
