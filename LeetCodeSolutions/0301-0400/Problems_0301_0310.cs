namespace LeetCodeSolutions._0301_0400;

public class Problems_0301_0310
{
    /// <summary>
    /// Problem 300
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int LengthOfLIS(int[] nums)
    {
        int len = 0;
        int[] tails = new int[nums.Length];

        foreach (int num in nums)
        {
            int left = 0;
            int right = len;

            while (left < right)
            {
                int mid = left + (right - left) / 2;
                if (tails[mid] < num)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid;
                }
            }

            tails[left] = num;
            if (left == len)
            {
                len++;
            }
        }

        return len;
    }

    /// <summary>
    /// Problem 301
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public IList<string> RemoveInvalidParentheses(string s)
    {
        IList<string> result = [];

        if (string.IsNullOrEmpty(s))
        {
            return result;
        }

        Queue<string> queue = [];
        HashSet<string> visited = [];

        queue.Enqueue(s);
        visited.Add(s);

        bool found = false;

        while (queue.Count > 0)
        {
            int size = queue.Count;

            for (int i = 0; i < size; i++)
            {
                string curr = queue.Dequeue();

                if (IsValid(curr))
                {
                    result.Add(curr);
                    found = true;
                }

                if (found)
                {
                    continue;
                }

                for (int j = 0; j < curr.Length; j++)
                {
                    string next = string.Concat(curr.AsSpan(0, j), curr.AsSpan(j + 1));

                    if (!visited.Contains(next))
                    {
                        queue.Enqueue(next);
                        visited.Add(next);
                    }
                }
            }

            if (found)
            {
                break;
            }
        }

        return result.Count == 0 ? [""] : result;

        static bool IsValid(string s)
        {
            int count = 0;
            foreach (char c in s)
            {
                if (c == '(')
                {
                    count++;
                }
                if (c == ')')
                {
                    count--;
                    if (count < 0)
                    {
                        return false;
                    }
                }
            }

            return count == 0;
        }
    }

    /// <summary>
    /// Problem 310
    /// </summary>
    /// <param name="n"></param>
    /// <param name="edges"></param>
    /// <returns></returns>
    public static IList<int> FindMinHeightTrees(int n, int[][] edges)
    {
        // Special case when there's only one node
        if (n == 1)
        {
            return [0];
        }

        // Construct adjacency list
        List<IList<int>> adjList = new(n);

        for (int i = 0; i < n; i++)
        {
            adjList.Add([]);
        }

        foreach (int[] edge in edges)
        {
            int u = edge[0];
            int v = edge[1];
            adjList[u].Add(v);
            adjList[v].Add(u);
        }

        // Initialize leaves queue
        Queue<int> leaves = new();
        for (int i = 0; i < n; i++)
        {
            // If node has only one neighbor, it's a leaf
            if (adjList[i].Count == 1)
            {
                leaves.Enqueue(i);
            }
        }

        // Remove leaves level by level until 1 or 2 nodes left
        while (n > 2)
        {
            int leavesCount = leaves.Count;
            n -= leavesCount;

            for (int i = 0; i < leavesCount; i++)
            {
                int leaf = leaves.Dequeue();

                foreach (int neighbor in adjList[leaf])
                {
                    // Remove leaf from its neighbor's adjacency list
                    adjList[neighbor].Remove(leaf);

                    // If neighbor becomes a leaf
                    if (adjList[neighbor].Count == 1)
                    {
                        leaves.Enqueue(neighbor);
                    }
                }
            }
        }

        // Remaining nodes in 'leaves' are the roots of MHTs
        var result = new List<int>(leaves);
        return result;
    }
}
