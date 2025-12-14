namespace OrderProcessor.Producer.Entities;
public class Address(
    string line1,
    string? line2 = null,
    string city = "",
    string stateOrProvince = "",
    string postalCode = "",
    string country = ""
)
{
    public string Line1 { get; set; } = line1;
    public string? Line2 { get; set; } = line2;
    public string City { get; set; } = city;
    public string StateOrProvince { get; set; } = stateOrProvince;
    public string PostalCode { get; set; } = postalCode;
    public string Country { get; set; } = country;
}

