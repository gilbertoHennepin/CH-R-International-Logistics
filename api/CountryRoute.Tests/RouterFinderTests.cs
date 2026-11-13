using CountryRoute.Api.Services;

namespace CountryRoute.Tests;

public class RouteFinderTests
{
    [Fact]
    public void PanamaRoute()
    {
        var route = new RouteFinder().FindRoute("PAN");
        Assert.Equal(new[] { "USA", "MEX", "GTM", "HND", "NIC", "CRI", "PAN" }, route);
    }
    
    //

        [Fact]
    public void BelizeRoute() =>
        Assert.Equal(new[] { "USA", "MEX", "BLZ" }, new RouteFinder().FindRoute("BLZ"));

    [Fact]
    public void LowercaseWorks() =>
        Assert.Equal(new[] { "USA", "MEX", "BLZ" }, new RouteFinder().FindRoute("blz"));

    [Fact]
    public void UsaIsJustUsa() =>
        Assert.Equal(new[] { "USA" }, new RouteFinder().FindRoute("USA"));

    [Fact]
    public void UnknownCodeReturnsNull() =>
        Assert.Null(new RouteFinder().FindRoute("ZZZ"));
}