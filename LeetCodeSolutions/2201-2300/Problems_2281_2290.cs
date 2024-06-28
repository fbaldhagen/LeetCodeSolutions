namespace LeetCodeSolutions._2201_2300;

public class Problems_2281_2290
{
    /// <summary>
    /// Problem 2285
    /// </summary>
    /// <param name="n"></param>
    /// <param name="roads"></param>
    /// <returns></returns>
    public static long MaximumImportance(int n, int[][] roads)
    {
        // Step 1: Compute degrees of each city
        int[] degree = new int[n];
        foreach (int[] road in roads)
        {
            degree[road[0]]++;
            degree[road[1]]++;
        }

        // Step 2: Sort cities by their degrees in descending order
        int[] sortedCities = degree
            .Select((deg, city) => new { City = city, Degree = deg })
            .OrderByDescending(x => x.Degree)
            .Select(x => x.City)
            .ToArray();

        // Step 3: Assign values from n to 1 to cities based on sorted order
        int[] values = new int[n];
        for (int i = 0; i < n; i++)
        {
            values[sortedCities[i]] = n - i;
        }

        // Step 4: Compute the total importance
        long totalImportance = 0;
        foreach (var road in roads)
        {
            totalImportance += values[road[0]] + values[road[1]];
        }

        return totalImportance;
    }
}
