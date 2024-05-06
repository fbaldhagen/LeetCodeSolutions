namespace LeetCodeSolutions._0401_0500;

public class Problems_0461_0470
{
    /// <summary>
    /// Problem 463
    /// </summary>
    public static int IslandPerimeter(int[][] grid)
    {
        int[][] dirs = new int[][]
        {
            new int[] { 0, 1 },
            new int[] { 0, -1 },
            new int[] { 1, 0 },
            new int[] { -1, 0 }
        };

        int perimeter = 0;
        int rows = grid.Length;
        int cols = grid[0].Length;

        // Check each cell in the grid if it's land. If it is, check if the surrounding cells are 
        // out of bounds or water - if it is we increment perimeter by 1.
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (grid[i][j] == 1)
                {
                    foreach (int[] dir in dirs)
                    {
                        int nrow = i + dir[0];
                        int ncol = j + dir[1];

                        if (nrow < 0 || nrow >= rows || ncol < 0 || ncol >= cols || grid[nrow][ncol] == 0)
                        {
                            perimeter++;
                        }
                    }
                }
            }
        }

        return perimeter;
    }
}
