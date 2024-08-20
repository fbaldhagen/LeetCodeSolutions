namespace LeetCodeSolutions._1101_1200;

public class Problems_1131_1140
{
    /// <summary>
    /// Problem 1140
    /// </summary>
    /// <param name="piles"></param>
    /// <returns></returns>
    public static int StoneGameII(int[] piles)
    {
        int n = piles.Length;
        
        // Create a 2D array to store the maximum stones Alice can get from pile i with M = m
        int[,] dp = new int[n + 1, n + 1];

        // Create an array to store the suffix sums
        int[] suffixSum = new int[n + 1];
        
        // Compute the suffix sums
        for (int i = n - 1; i >= 0; i--)
        {
            suffixSum[i] = suffixSum[i + 1] + piles[i];
        }

        // Bottom-up DP approach
        for (int i = n - 1; i >= 0; i--)
        {
            for (int m = 1; m <= n; m++)
            {
                // We initialize the dp value as 0
                dp[i, m] = 0;
                // Try every possible number of piles that can be taken
                for (int x = 1; x <= 2 * m && i + x <= n; x++)
                {
                    dp[i, m] = Math.Max(dp[i, m], suffixSum[i] - dp[i + x, Math.Max(m, x)]);
                }
            }
        }

        // Return the max stones Alice can collect starting from the 0th pile with M = 1
        return dp[0, 1];
    }
}
