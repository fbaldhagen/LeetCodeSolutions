namespace LeetCodeSolutions._1901_2000;

public class Problems_1931_1940
{
    /// <summary>
    /// Problem 1937
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public static long MaxPoints(int[][] points)
    {
        int m = points.Length;
        int n = points[0].Length;

        // Array to store the maximum points up to the previous row
        long[] dp = new long[n];

        // Initialize dp array with the first row's values
        for (int j = 0; j < n; j++)
        {
            dp[j] = points[0][j];
        }

        // Iterate through each row starting from the second one
        for (int i = 1; i < m; i++)
        {
            // Array to store the max points achievable for the current row
            long[] current = new long[n];

            // Compute left max values for the current row
            long[] left = new long[n];
            left[0] = dp[0];
            for (int j = 1; j < n; j++)
            {
                left[j] = Math.Max(left[j - 1], dp[j] + j);
            }

            // Compute right max values for the current row
            long[] right = new long[n];
            right[n - 1] = dp[n - 1] - (n - 1);
            for (int j = n - 2; j >= 0; j--)
            {
                right[j] = Math.Max(right[j + 1], dp[j] - j);
            }

            // Calculate the max points for the current row
            for (int j = 0; j < n; j++)
            {
                current[j] = points[i][j] + Math.Max(left[j] - j, right[j] + j);
            }

            // Update dp array with current row's values
            dp = current;
        }

        // Return the maximum value from the last row
        long maxPoints = dp.Max();
        return maxPoints;
    }
}
