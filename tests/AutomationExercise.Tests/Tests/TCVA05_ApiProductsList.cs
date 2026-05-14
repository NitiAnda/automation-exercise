using System.Text.Json;
using AutomationExercise.Tests.Fixtures;
using Microsoft.Playwright;
using NUnit.Framework;

namespace AutomationExercise.Tests.Tests;

[TestFixture]
[Category("Smoke")]
public class TCVA05_ApiProductsList : BaseTest
{
    [Test]
    [Description("TC-VA-05: API Contract Smoke — GET /api/productsList returns HTTP 200 with a non-empty products array")]
    public async Task GetProductsList_ShouldReturn200WithNonEmptyProducts()
    {
        await using var apiContext = await Playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions
        {
            BaseURL = BaseUrl
        });

        var response = await apiContext.GetAsync("/api/productsList");

        Assert.That(response.Status, Is.EqualTo(200), "Expected HTTP 200 from /api/productsList");

        var body = await response.JsonAsync();
        Assert.That(body.HasValue, Is.True, "Response body must be valid JSON");

        var root = body!.Value;
        Assert.That(root.GetProperty("responseCode").GetInt32(), Is.EqualTo(200),
            "JSON responseCode should be 200");

        var products = root.GetProperty("products").EnumerateArray().ToList();
        Assert.That(products, Is.Not.Empty, "Products array must not be empty");

        var first = products[0];
        Assert.Multiple(() =>
        {
            Assert.That(first.TryGetProperty("id", out _), Is.True, "Product must have 'id'");
            Assert.That(first.TryGetProperty("name", out _), Is.True, "Product must have 'name'");
            Assert.That(first.TryGetProperty("price", out _), Is.True, "Product must have 'price'");
            Assert.That(first.TryGetProperty("brand", out _), Is.True, "Product must have 'brand'");
            Assert.That(first.TryGetProperty("category", out _), Is.True, "Product must have 'category'");
        });
    }
}
