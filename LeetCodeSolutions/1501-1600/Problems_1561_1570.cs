namespace LeetCodeSolutions._1501_1600;

public class Problems_1561_1570
{
    /// <summary>
    /// Problem 1568
    /// </summary>
    /// <param name="grid"></param>
    /// <returns></returns>
    public static int MinDays(int[][] grid)
    {
        // Check if the grid is already disconnected
        if (CountIslands(grid) != 1)
        {
            return 0;
        }

        // Try changing each land cell to water to see if it disconnects the grid
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[0].Length; j++)
            {
                if (grid[i][j] == 1)
                {
                    grid[i][j] = 0; // Change land to water
                    if (CountIslands(grid) != 1)
                    {
                        return 1;
                    }
                    grid[i][j] = 1; // Revert back to land
                }
            }
        }

        // If changing one cell didn't work, at least 2 days are needed
        return 2;

        // Method to count the number of islands in the grid
        static int CountIslands(int[][] grid)
        {
            int count = 0;
            bool[,] visited = new bool[grid.Length, grid[0].Length];

            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[0].Length; j++)
                {
                    if (grid[i][j] == 1 && !visited[i, j])
                    {
                        Dfs(grid, visited, i, j);
                        count++;
                    }
                }
            }

            return count;

            // Depth-First Search to mark all connected land cells
            static void Dfs(int[][] grid, bool[,] visited, int i, int j)
            {
                if (i < 0 || i >= grid.Length || j < 0 || j >= grid[0].Length || grid[i][j] == 0 || visited[i, j])
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
}