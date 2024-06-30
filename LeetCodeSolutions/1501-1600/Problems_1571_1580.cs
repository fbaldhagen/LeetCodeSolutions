namespace LeetCodeSolutions._1501_1600;

public class Problems_1571_1580
{
    /// <summary>
    /// Problem 1579
    /// </summary>
    /// <param name="n"></param>
    /// <param name="edges"></param>
    /// <returns></returns>
    public static int MaxNumEdgesToRemove(int n, int[][] edges)
    {
        int[] parentAlice = new int[n + 1];
        int[] parentBob = new int[n + 1];

        for (int i = 0; i <= n; i++)
        {
            parentAlice[i] = i;
            parentBob[i] = i;
        }

        int usedEdges = 0;

        // Type 3 edges first
        foreach (int[] edge in edges)
        {
            if (edge[0] == 3)
            {
                if (Union(parentAlice, edge[1], edge[2]))
                {
                    Union(parentBob, edge[1], edge[2]);
                    usedEdges++;
                }
            }
        }

        // Type 1 edges for Alice
        foreach (int[] edge in edges)
        {
            if (edge[0] == 1)
            {
                if (Union(parentAlice, edge[1], edge[2]))
                {
                    usedEdges++;
                }
            }
        }

        // Type 2 edges for Bob
        foreach (int[] edge in edges)
        {
            if (edge[0] == 2)
            {
                if (Union(parentBob, edge[1], edge[2]))
                {
                    usedEdges++;
                }
            }
        }

        // Check if both Alice and Bob can traverse the entire graph
        int rootAlice = Find(parentAlice, 1);
        int rootBob = Find(parentBob, 1);
        for (int i = 2; i <= n; i++)
        {
            if (Find(parentAlice, i) != rootAlice || Find(parentBob, i) != rootBob)
            {
                return -1;
            }
        }

        return edges.Length - usedEdges;

        int Find(int[] parent, int x)
        {
            if (parent[x] != x)
            {
                parent[x] = Find(parent, parent[x]);
            }

            return parent[x];
        }

        bool Union(int[] parent, int x, int y)
        {
            int rootX = Find(parent, x);
            int rootY = Find(parent, y);
            if (rootX != rootY)
            {
                parent[rootX] = rootY;
                return true;
            }

            return false;
        }
    }
}
