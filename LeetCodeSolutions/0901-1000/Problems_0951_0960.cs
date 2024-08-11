namespace LeetCodeSolutions._0901_1000;

public class Problems_0951_0960
{
    /// <summary>
    /// Problem 959
    /// </summary>
    /// <param name="grid"></param>
    /// <returns></returns>
    public static int RegionsBySlashes(string[] grid)
    {
        int n = grid.Length;
        int size = n * 3;

        // Create a 3x3 expanded grid
        int[][] expandedGrid = new int[size][];
        for (int i = 0; i < size; i++)
        {
            expandedGrid[i] = new int[size];
        }

        // Expand the grid based on the slashes
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (grid[i][j] == '/')
                {
                    expandedGrid[i * 3][j * 3 + 2] = 1;
                    expandedGrid[i * 3 + 1][j * 3 + 1] = 1;
                    expandedGrid[i * 3 + 2][j * 3] = 1;
                }
                else if (grid[i][j] == '\\')
                {
                    expandedGrid[i * 3][j * 3] = 1;
                    expandedGrid[i * 3 + 1][j * 3 + 1] = 1;
                    expandedGrid[i * 3 + 2][j * 3 + 2] = 1;
                }
            }
        }

        // Count regions in the expanded grid using DFS
        int regionCount = 0;
        bool[,] visited = new bool[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (expandedGrid[i][j] == 0 && !visited[i, j])
                {
                    Dfs(expandedGrid, visited, i, j);
                    regionCount++;
                }
            }
        }

        return regionCount;

        // Depth-First Search to explore the region
        static void Dfs(int[][] grid, bool[,] visited, int i, int j)
        {
            int size = grid.Length;
            if (i < 0 || i >= size || j < 0 || j >= size || grid[i][j] == 1 || visited[i, j])
            {
                return;
            }

            visited[i, j] = true;

            Dfs(grid, visited, i + 1, j);
            Dfs(grid, visited, i - 1, j);
            Dfs(grid, visited, i, j + 1);
            Dfs(grid, visited, i, j - 1);
        }
    }
}
