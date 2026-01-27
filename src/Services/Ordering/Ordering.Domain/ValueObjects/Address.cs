namespace Ordering.Domain.ValueObjects;

public record class Address
{
    public string FirstName { get; } = default!;
    public string LastName { get; } = default!;
    public string? EmailAddress { get; } = default!;
    public string AddressLine { get; } = default!;
    public string Country { get; } = default!;
    public string Province { get; } = default!;
    public string PostalCode { get; } = default!;

    protected Address(
        string firstName,
        string lastName,
        string? emailAddress,
        string addressLine,
        string country,
        string province,
        string postalCode)
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        AddressLine = addressLine;
        Country = country;
        Province = province;
        PostalCode = postalCode;
    }

    public static Address Of(
        string firstName,
        string lastName,
        string? emailAddress,
        string addressLine,
        string country,
        string province,
        string postalCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(nameof(firstName));
        ArgumentException.ThrowIfNullOrWhiteSpace(nameof(lastName));
        ArgumentException.ThrowIfNullOrWhiteSpace(nameof(emailAddress));
        ArgumentException.ThrowIfNullOrWhiteSpace(nameof(addressLine));
        ArgumentException.ThrowIfNullOrWhiteSpace(nameof(country));
        ArgumentException.ThrowIfNullOrWhiteSpace(nameof(province));
        ArgumentException.ThrowIfNullOrWhiteSpace(nameof(postalCode));

        return new Address(
            firstName,
            lastName,
            emailAddress,
            addressLine,
            country,
            province,
            postalCode);
    }
}