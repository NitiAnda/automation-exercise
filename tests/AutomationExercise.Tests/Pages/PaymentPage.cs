using Microsoft.Playwright;

namespace AutomationExercise.Tests.Pages;

public class PaymentPage(IPage page)
{
    private ILocator CardNameInput => page.Locator("input[data-qa='name-on-card']");
    private ILocator CardNumberInput => page.Locator("input[data-qa='card-number']");
    private ILocator CvcInput => page.Locator("input[data-qa='cvc']");
    private ILocator ExpiryMonthInput => page.Locator("input[data-qa='expiry-month']");
    private ILocator ExpiryYearInput => page.Locator("input[data-qa='expiry-year']");
    private ILocator PayButton => page.Locator("button[data-qa='pay-button']");

    public async Task FillPaymentDetailsAsync(
        string cardName,
        string cardNumber = "4111111111111111",
        string cvc = "123",
        string expiryMonth = "12",
        string expiryYear = "2027")
    {
        await CardNameInput.FillAsync(cardName);
        await CardNumberInput.FillAsync(cardNumber);
        await CvcInput.FillAsync(cvc);
        await ExpiryMonthInput.FillAsync(expiryMonth);
        await ExpiryYearInput.FillAsync(expiryYear);
    }

    public async Task ClickPayAndConfirmAsync() => await PayButton.ClickAsync();

    public async Task VerifyOrderPlacedSuccessfullyAsync()
        => await Assertions.Expect(page).ToHaveURLAsync(
            new System.Text.RegularExpressions.Regex("payment_done"),
            new PageAssertionsToHaveURLOptions { Timeout = 30000 });
}
