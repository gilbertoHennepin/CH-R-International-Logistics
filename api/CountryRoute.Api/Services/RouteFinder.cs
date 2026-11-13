namespace CountryRoute.Api.Services;

public class RouteFinder
{
    private const string Start = "USA";

    // Returns the countries from USA to the destination (fewest border crossings),
    // or null if the code isn't on the map.
    public List<string>? FindRoute(string destination)
    {
        // 1. Unknown code -> no route
        if (!BorderMap.Neighbors.ContainsKey(destination))
            return null;

        // 2. BFS setup
        var queue = new Queue<string>();
        var visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var parent = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        queue.Enqueue(Start);
        visited.Add(Start);

        // 3. Explore ring by ring
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (string.Equals(current, destination, StringComparison.OrdinalIgnoreCase))
                break;

            foreach (var neighbor in BorderMap.Neighbors[current])
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    parent[neighbor] = current;   // "I reached neighbor from current"
                    queue.Enqueue(neighbor);
                }
            }
        }

        // 4. Walk the parent notes back from the destination to USA, then flip
        var step = destination.ToUpperInvariant();
        var path = new List<string> { step };
        while (step != Start)
        {
            step = parent[step];
            path.Add(step);
        }
        path.Reverse();
        return path;
    }
}