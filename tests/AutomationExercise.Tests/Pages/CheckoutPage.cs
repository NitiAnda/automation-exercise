using Microsoft.Playwright;

namespace AutomationExercise.Tests.Pages;

public class CheckoutPage(IPage page)
{
    private ILocator DeliveryAddressSection => page.Locator("#address_delivery");
    private ILocator CommentTextArea => page.Locator("textarea.form-control");
    private ILocator PlaceOrderButton => page.Locator("a.btn.btn-default.check_out");

    public async Task VerifyCheckoutPageVisibleAsync()
        => await Assertions.Expect(DeliveryAddressSection).ToBeVisibleAsync();

    public async Task AddCommentAndPlaceOrderAsync(string comment = "Automated test order")
    {
        await CommentTextArea.FillAsync(comment);
        await PlaceOrderButton.ClickAsync(new() { Force = true });
    }
}
