namespace LeetCodeSolutions._2301_2400;

public class Problems_2371_2380
{
    /// <summary>
    /// Problem 2373
    /// </summary>
    /// <param name="grid"></param>
    /// <returns></returns>
    public static int[][] LargestLocal(int[][] grid)
    {
        int n = grid.Length;
        int[][] maxLocal = new int[n - 2][];

        for (int i = 0; i < n - 2; i++)
        {
            maxLocal[i] = new int[n - 2];
        }

        for (int i = 0; i < n - 2; i++)
        {
            for (int j = 0; j < n - 2; j++)
            {
                int max = int.MinValue;
                for (int k = i; k < i + 3; k++)
                {
                    for (int l = j; l < j + 3; l++)
                    {
                        max = Math.Max(max, grid[k][l]);
                    }
                }
                maxLocal[i][j] = max;
            }
        }

        return maxLocal;
    }
}
