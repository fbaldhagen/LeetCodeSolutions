namespace LeetCodeSolutions._2101_2200;

public class Problems_2191_2200
{
    /// <summary>
    /// Problem 2192
    /// </summary>
    /// <param name="n"></param>
    /// <param name="edges"></param>
    /// <returns></returns>
    public static IList<IList<int>> GetAncestors(int n, int[][] edges)
    {
        // Dictionary to store the reversed graph (child -> parents)
        Dictionary<int, List<int>> adj = [];

        // Reverse edges and build the adjacency list
        foreach (int[] e in edges)
        {
            if (!adj.TryGetValue(e[1], out List<int>? value))
            {
                value = [];
                adj[e[1]] = value;
            }
                
            value.Add(e[0]);
        }

        IList<IList<int>> ans = [];
        for (int i = 0; i < n; i++)
        {
            HashSet<int> vis = [];
            List<int> cur = [];
            DFS(i, adj, vis, cur);
            cur.Sort();
            ans.Add(new List<int>(cur));
        }

        return ans;

        static void DFS(int node, Dictionary<int, List<int>> map, HashSet<int> vis, List<int> cur)
        {
            vis.Add(node);
            if (!map.TryGetValue(node, out List<int>? value))
            {
                return;
            }

            foreach (int x in value)
            {
                if (vis.Contains(x))
                {
                    continue;
                }
                cur.Add(x);
                DFS(x, map, vis, cur);
            }
        }
    }
}
