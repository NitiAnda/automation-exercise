using Bogus;

namespace AutomationExercise.Tests.Helpers;

public record UserInfo(string Name, string Email, string Password, string FirstName, string LastName);

public record AddressInfo(
    string FirstName,
    string LastName,
    string Company,
    string Address1,
    string Address2,
    string Country,
    string State,
    string City,
    string Zipcode,
    string Phone);

public static class TestDataFactory
{
    private static Faker F => new("en");

    public static UserInfo GenerateUser()
    {
        var f = F;
        var firstName = f.Name.FirstName();
        var lastName = f.Name.LastName();
        var ticks = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        var email = $"qa_{firstName.ToLower()}_{ticks}@mailtest.dev";
        return new UserInfo(
            Name: $"{firstName} {lastName}",
            Email: email,
            Password: "Test@1234",
            FirstName: firstName,
            LastName: lastName);
    }

    public static AddressInfo GenerateAddress()
    {
        var f = F;
        return new AddressInfo(
            FirstName: f.Name.FirstName(),
            LastName: f.Name.LastName(),
            Company: f.Company.CompanyName(),
            Address1: f.Address.StreetAddress(),
            Address2: f.Address.SecondaryAddress(),
            Country: "United States",
            State: f.Address.State(),
            City: f.Address.City(),
            Zipcode: f.Address.ZipCode("#####"),
            Phone: f.Phone.PhoneNumber("##########"));
    }
}
