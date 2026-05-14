using Microsoft.Playwright;

namespace AutomationExercise.Tests.Pages;

public class ProductDetailsPage(IPage page)
{
    private ILocator WriteReviewHeading => page.Locator("a[href='#reviews']:has-text('Write Your Review')");
    private ILocator ReviewNameInput => page.Locator("input#name");
    private ILocator ReviewEmailInput => page.Locator("input#email");
    private ILocator ReviewTextArea => page.Locator("textarea#review");
    private ILocator SubmitReviewButton => page.Locator("button#button-review");
    private ILocator ReviewSuccessMessage => page.Locator("div#review-section .alert-success span");

    public async Task NavigateToFirstProductAsync() => await page.GotoAsync("/product_details/1");

    public async Task VerifyWriteReviewSectionVisibleAsync()
        => await Assertions.Expect(WriteReviewHeading).ToBeVisibleAsync();

    public async Task SubmitReviewAsync(string name, string email, string reviewText)
    {
        await ReviewNameInput.FillAsync(name);
        await ReviewEmailInput.FillAsync(email);
        await ReviewTextArea.FillAsync(reviewText);
        await SubmitReviewButton.ClickAsync(new() { Force = true });
    }

    public async Task VerifyReviewSuccessVisibleAsync()
        => await Assertions.Expect(ReviewSuccessMessage).ToContainTextAsync("Thank you for your review");
}
