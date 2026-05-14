using Microsoft.Playwright;
using AutomationExercise.Tests.Helpers;

namespace AutomationExercise.Tests.Pages;

public class AccountCreatedPage(IPage page)
{
    private ILocator EnterAccountInfoHeading => page.Locator("b:has-text('ENTER ACCOUNT INFORMATION')");
    private ILocator TitleMrRadio => page.Locator("input#id_gender1");
    private ILocator PasswordInput => page.Locator("input[data-qa='password']");
    private ILocator DaysSelect => page.Locator("select[data-qa='days']");
    private ILocator MonthsSelect => page.Locator("select[data-qa='months']");
    private ILocator YearsSelect => page.Locator("select[data-qa='years']");
    private ILocator NewsletterCheckbox => page.Locator("input#newsletter");
    private ILocator OptinCheckbox => page.Locator("input#optin");
    private ILocator FirstNameInput => page.Locator("input[data-qa='first_name']");
    private ILocator LastNameInput => page.Locator("input[data-qa='last_name']");
    private ILocator CompanyInput => page.Locator("input[data-qa='company']");
    private ILocator Address1Input => page.Locator("input[data-qa='address']");
    private ILocator Address2Input => page.Locator("input[data-qa='address2']");
    private ILocator CountrySelect => page.Locator("select[data-qa='country']");
    private ILocator StateInput => page.Locator("input[data-qa='state']");
    private ILocator CityInput => page.Locator("input[data-qa='city']");
    private ILocator ZipcodeInput => page.Locator("input[data-qa='zipcode']");
    private ILocator MobileInput => page.Locator("input[data-qa='mobile_number']");
    private ILocator CreateAccountButton => page.Locator("button[data-qa='create-account']");
    private ILocator AccountCreatedHeading => page.Locator("b:has-text('ACCOUNT CREATED!')");
    private ILocator ContinueButton => page.Locator("a[data-qa='continue-button']");

    public async Task VerifyEnterAccountInfoVisibleAsync()
        => await Assertions.Expect(EnterAccountInfoHeading).ToBeVisibleAsync();

    public async Task FillRegistrationFormAsync(UserInfo user, AddressInfo address)
    {
        await TitleMrRadio.CheckAsync();
        await PasswordInput.FillAsync(user.Password);
        await DaysSelect.SelectOptionAsync("1");
        await MonthsSelect.SelectOptionAsync("January");
        await YearsSelect.SelectOptionAsync("2000");
        await NewsletterCheckbox.CheckAsync();
        await OptinCheckbox.CheckAsync();
        await FirstNameInput.FillAsync(address.FirstName);
        await LastNameInput.FillAsync(address.LastName);
        await CompanyInput.FillAsync(address.Company);
        await Address1Input.FillAsync(address.Address1);
        await Address2Input.FillAsync(address.Address2);
        await CountrySelect.SelectOptionAsync(address.Country);
        await StateInput.FillAsync(address.State);
        await CityInput.FillAsync(address.City);
        await ZipcodeInput.FillAsync(address.Zipcode);
        await MobileInput.FillAsync(address.Phone);
    }

    public async Task ClickCreateAccountAsync()
    {
        await CreateAccountButton.ClickAsync(new() { Force = true });
        await page.WaitForURLAsync("**/account_created", new() { Timeout = 30_000, WaitUntil = WaitUntilState.DOMContentLoaded });
    }

    public async Task VerifyAccountCreatedVisibleAsync()
        => await Assertions.Expect(AccountCreatedHeading).ToBeVisibleAsync(new() { Timeout = 15_000 });

    public async Task ClickContinueAsync()
    {
        await ContinueButton.ClickAsync(new() { Force = true });
        await page.WaitForURLAsync(url => !url.Contains("account_created"), new() { Timeout = 30_000 });
    }
}
