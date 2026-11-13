using CountryRoute.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CountryRoute.Api.Controllers;

[ApiController]
[Route("")]                                  // routes start at the site root: /PAN, not /api/route/PAN
public class RouteController : ControllerBase
{
    private readonly RouteFinder _routeFinder;

    // ASP.NET hands us the RouteFinder (dependency injection, see Program.cs)
    public RouteController(RouteFinder routeFinder) => _routeFinder = routeFinder;

    // Only 3 letters match this route: /PAN works, /12 or /PANAMA never reach this code
    [HttpGet("{code:alpha:length(3)}")]
    public ActionResult<RouteResponse> Get(string code)
    {
        var route = _routeFinder.FindRoute(code);

        if (route is null)
            return Problem(statusCode: 404,
                           title: "Unknown country code",
                           detail: $"'{code}' is not a supported/North American country.");

        return Ok(new RouteResponse(code.ToUpperInvariant(), route));
    }
}

// The JSON shape. ASP.NET turns Destination/List into "destination"/"list" automatically.
public record RouteResponse(string Destination, IReadOnlyList<string> List);