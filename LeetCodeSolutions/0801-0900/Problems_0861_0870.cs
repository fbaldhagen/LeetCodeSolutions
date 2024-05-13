namespace LeetCodeSolutions._0801_0900;

public class Problems_0861_0870
{
    /// <summary>
    /// Problem 861
    /// </summary>
    /// <param name="grid"></param>
    /// <returns></returns>
    public static int MatrixScore(int[][] grid)
    {
        int m = grid.Length;
        int n = grid[0].Length;
        int score = m;

        for (int c = 1; c < n; c++)
        {
            int count = 0;
            for (int r = 0; r < m; r++)
            {
                var row = grid[r];
                count += 1 ^ row[0] ^ row[c];
            }
            score = (score << 1) + Math.Max(count, m - count);
        }
        return score;
    }
}
