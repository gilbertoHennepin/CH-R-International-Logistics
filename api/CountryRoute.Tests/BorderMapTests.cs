using CountryRoute.Api.Services;   

namespace CountryRoute.Tests;

public class BorderMapTests
{
    [Fact]   // marks this method as a test
    public void EveryBorderGoesBothWays()
    {
        // loop over every country and its neighbor list
        foreach (var (country, neighbors) in BorderMap.Neighbors)
        {
            foreach (var neighbor in neighbors)
            {
                // if USA lists MEX, then MEX's list must contain USA
                Assert.Contains(country, BorderMap.Neighbors[neighbor]);
            }
        }
        
    }
        [Fact]
    public void MapHasTenCountries()
    {
        // the assignment lists exactly 10 countries
        Assert.Equal(10, BorderMap.Neighbors.Count);
    }
}