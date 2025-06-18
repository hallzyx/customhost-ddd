using System.Text.RegularExpressions;

namespace customhost_backend.crm.Domain.Models.ValueObjects;

/// <summary>
/// Email Address Value Object
/// </summary>
/// <remarks>
/// This class represents an email address value object.
/// It ensures that the email address is valid.
/// </remarks>
public record EmailAddress
{
    public string Address { get; init; }

    public EmailAddress()
    {
        Address = string.Empty;
    }

    public EmailAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Email address cannot be null or empty", nameof(address));

        if (!IsValidEmail(address))
            throw new ArgumentException("Email address is not valid", nameof(address));

        Address = address.ToLowerInvariant();
    }

    private static bool IsValidEmail(string email)
    {
        const string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, emailPattern);
    }

    public override string ToString() => Address;
}
