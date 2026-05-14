using AutomationExercise.Tests.Fixtures;
using AutomationExercise.Tests.Helpers;
using AutomationExercise.Tests.Pages;
using NUnit.Framework;

namespace AutomationExercise.Tests.Tests;

[TestFixture]
[Category("Regression")]
public class TC21_AddReview : BaseTest
{
    [Test]
    [Description("TC21: Submitting a product review shows a thank-you success message")]
    public async Task AddProductReview_ShouldShowSuccessMessage()
    {
        var user = TestDataFactory.GenerateUser();

        var home = new HomePage(Page);
        var products = new ProductsPage(Page);
        var details = new ProductDetailsPage(Page);

        await home.NavigateAsync();
        await products.NavigateAsync();
        await products.VerifyAllProductsHeadingVisibleAsync();

        await details.NavigateToFirstProductAsync();
        await details.VerifyWriteReviewSectionVisibleAsync();

        await details.SubmitReviewAsync(
            name: user.Name,
            email: user.Email,
            reviewText: "Great product! Automated review from the QA suite.");

        await details.VerifyReviewSuccessVisibleAsync();
    }
}
