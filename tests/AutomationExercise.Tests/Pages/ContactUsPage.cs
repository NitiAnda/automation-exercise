using Microsoft.Playwright;

namespace AutomationExercise.Tests.Pages;

public class ContactUsPage(IPage page)
{
    private ILocator GetInTouchHeading => page.Locator("h2:has-text('GET IN TOUCH')");
    private ILocator NameInput => page.Locator("input[data-qa='name']");
    private ILocator EmailInput => page.Locator("input[data-qa='email']");
    private ILocator SubjectInput => page.Locator("input[data-qa='subject']");
    private ILocator MessageTextArea => page.Locator("textarea[data-qa='message']");
    private ILocator FileUploadInput => page.Locator("input[name='upload_file']");
    private ILocator SubmitButton => page.Locator("input[data-qa='submit-button']");
    private ILocator SuccessMessage => page.Locator("#contact-page .status.alert-success");

    public async Task NavigateAsync() => await page.GotoAsync("/contact_us");

    public async Task VerifyGetInTouchVisibleAsync()
        => await Assertions.Expect(GetInTouchHeading).ToBeVisibleAsync();

    public async Task FillContactFormAsync(string name, string email, string subject, string message)
    {
        await NameInput.FillAsync(name);
        await EmailInput.FillAsync(email);
        await SubjectInput.FillAsync(subject);
        await MessageTextArea.FillAsync(message);
    }

    public async Task UploadFileAsync(string filePath)
        => await FileUploadInput.SetInputFilesAsync(filePath);

    public async Task SubmitFormAsync()
    {
        EventHandler<IDialog> handler = async (_, d) => await d.AcceptAsync();
        page.Dialog += handler;
        try
        {
            await SubmitButton.ClickAsync();
        }
        finally
        {
            page.Dialog -= handler;
        }
    }

    public async Task VerifySuccessMessageVisibleAsync()
        => await Assertions.Expect(SuccessMessage).ToBeVisibleAsync();
}
