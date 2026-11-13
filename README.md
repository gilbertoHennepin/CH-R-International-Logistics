# Country Route Finder

Given a three-letter North American country code, returns every country a truck must drive through to get from the United States to that destination, using the fewest border crossings.

I built **both options**: a C# Web API (Option 1) and a React web app (Option 2) that uses it.

## Tech stack

- .NET 11
- C#
- ASP.NET Core Web API
- React
- Azure Container Apps
- Azure Static Web Apps
- GitHub Actions
- xUnit

## Live links

| | URL |
|---|---|
| **Option 1 – Web API** | https://country-route-api.whitesmoke-24b2183e.eastus.azurecontainerapps.io/PAN |
| **Option 2 – Web app** | https://agreeable-cliff-01ac38a10.4.azurestaticapps.net |

Try `/PAN`, `/BLZ`, `/blz` (lowercase works) and `/ZZZ` (unknown code → 404).

```
GET /PAN
{ "destination": "PAN", "list": ["USA", "MEX", "GTM", "HND", "NIC", "CRI", "PAN"] }
```

## Demo

![Customs Route Finder demo: entering PAN, BLZ, CAN and ZZZ](docs/route-finder-demo.gif)

## Example codes

These are the results from the live web app (Option 2). The API returns the same `list` for each code.

| Input | Route (`list`) | Border crossings | Notes |
|---|---|---|---|
| `PAN` | USA → MEX → GTM → HND → NIC → CRI → PAN | 6 | Full route through Central America |
| `BLZ` | USA → MEX → BLZ | 2 | `blz` in lowercase gives the same result |
| `CAN` | USA → CAN | 1 | Direct neighbour of the USA |
| `ZZZ` | none | none | Not a supported code, so the API returns 404 and the app shows an error |

## How it works

The map is a **graph**: each country is a node and each shared border is an edge, stored as an adjacency list (`BorderMap.cs`). Finding the route with the fewest border crossings is a shortest-path problem on an unweighted graph, so I use **breadth-first search** from USA (`RouteFinder.cs`). BFS explores countries in rings by distance, so the first time it reaches the destination is guaranteed to be by the fewest crossings. While searching, it records which country each one was reached from; once it finds the destination it walks those links back to USA and reverses the list.


## Architecture

```
React (Vite) on Azure Static Web Apps
        │  fetch GET /{code}
        ▼
ASP.NET Core Web API (.NET 11) in a container on Azure Container Apps
        │
        └── RouteFinder (BFS) ── BorderMap (adjacency list)
```

- The **route logic lives only in the API**. The web app just calls it and displays the result, so there is one source of truth.
- Both apps deploy automatically from `main` with **GitHub Actions**.


## Tests

```
cd api
dotnet test
```
Covers: border map symmetry and size, routes for PAN, BLZ and USA, lowercase input, and unknown codes.

## Project structure

```
api/
  CountryRoute.Api/       ASP.NET Core Web API
    Controllers/          RouteController (GET /{code})
    Services/             BorderMap, RouteFinder (BFS)
  CountryRoute.Tests/     xUnit tests
  Dockerfile
web/                      React + TypeScript (Vite)
.github/workflows/        CI/CD for the API and the web app
```

