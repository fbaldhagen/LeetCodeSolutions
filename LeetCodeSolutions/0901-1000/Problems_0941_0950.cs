namespace LeetCodeSolutions._0901_1000;

public class Problems_0941_0950
{
    /// <summary>
    /// Problem 945
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int MinIncrementForUnique(int[] nums)
    {
        // Sort in non-descending order
        Array.Sort(nums);

        // Start counting
        int minIncrement = 0;
        for (int i = 1; i < nums.Length; i++)
        {
            // Compare with previous element
            if (nums[i] <= nums[i - 1])
            {
                // Calculate the increment. Previous value - current value + 1
                int increment = nums[i - 1] + 1 - nums[i];
                nums[i] = nums[i - 1] + 1;
                minIncrement += increment;
            }
        }

        return minIncrement;
    }

    /// <summary>
    /// Problem 947
    /// </summary>
    /// <param name="stones"></param>
    /// <returns></returns>
    public static int RemoveStones(int[][] stones)
    {
        // A stone can be removed if it shares a row or a column with a stone that hasn't been removed.
        // Think of this as a graph where stones in same rows and columns are connected.
        // A stone can be removed if it has neighbors in the graph. Maximize the number of stones that can be removed.
        // Any group of connected stones can be left with just 1 stone, so the number that can be left are the same as the number of connected groups.
        // Therefore the number of stones that can be removed are (total number of stones - number of connected groups).

        // Graph represented by a dictionary
        Dictionary<int, List<int>> graph = [];
        // Build the graph by checking the neighbors of each stone
        int n = stones.Length;
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                // Check if the stones are neighbors (same row or column). If they are - connect them.
                if (stones[i][0] == stones[j][0] || stones[i][1] == stones[j][1])
                {
                    // Check if the stones already have lists of neighbors. If not, create them.
                    // Either way, add the stones as connected.
                    if (graph.TryGetValue(i, out List<int>? iList))
                    {
                        iList.Add(j);
                    }
                    else
                    {
                        graph[i] = [];
                        graph[i].Add(j);
                    }

                    if (graph.TryGetValue(j, out List<int>? jList))
                    {
                        jList.Add(i);
                    }
                    else
                    {
                        graph[j] = [];
                        graph[j].Add(i);
                    }
                }
            }
        }

        // Keep track of visited stones (for DFS)
        HashSet<int> visited = [];
        // Count connected groups, DFS to find them
        int connectedGroups = 0;
        for (int i = 0; i < n; i++)
        {
            if (!visited.Contains(i))
            {
                connectedGroups++;
                DFS(i);
            }
        }

        return n - connectedGroups;

        void DFS(int node)
        {
            // Already visited this node, move on
            if (visited.Contains(node))
            {
                return;
            }

            visited.Add(node);

            if (graph.TryGetValue(node, out List<int>? value))
            {
                foreach (int neighbor in value)
                {
                    DFS(neighbor);
                }
            }
        }
    }
}