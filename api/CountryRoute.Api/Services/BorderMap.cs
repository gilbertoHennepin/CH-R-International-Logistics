namespace CountryRoute.Api.Services;

// The simplified North America map
// Each key is a country code; its value is the list of countries it shares a border with.
public static class BorderMap
{
    public static IReadOnlyDictionary<string, string[]> Neighbors { get; } =
        new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase)
        {
            ["CAN"] = new[] { "USA" },
            ["USA"] = new[] { "CAN", "MEX" },
            ["MEX"] = new[] { "USA", "GTM", "BLZ" },
            ["BLZ"] = new[] { "MEX", "GTM" },
            ["GTM"] = new[] { "MEX", "BLZ", "SLV", "HND" },
            ["SLV"] = new[] { "GTM", "HND" },
            ["HND"] = new[] { "GTM", "SLV", "NIC" },
            ["NIC"] = new[] { "HND", "CRI" },
            ["CRI"] = new[] { "NIC", "PAN" },
            ["PAN"] = new[] { "CRI" },
        };
}