namespace LeetCodeSolutions._0301_0400;

public class Problems_0311_0320
{
    /// <summary>
    /// Problem 312
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int MaxCoins(int[] nums)
    {
        int n = nums.Length;

        // Extend nums with 1's at the beginning and end
        int[] extendedNums = new int[n + 2];
        extendedNums[0] = 1;
        extendedNums[^1] = 1;
        for (int i = 1; i <= n; i++)
        {
            extendedNums[i] = nums[i - 1];
        }

        // 2D dp array. dp[m, n] stores the maximum coins by bursting balloons in the range [m, n].
        int[,] dp = new int[extendedNums.Length, extendedNums.Length];

        for (int length = 1; length <= n; length++)
        {
            for (int left = 1; left <= n - length + 1; left++)
            {
                int right = left + length - 1;
                for (int k = left; k <= right; k++)
                {
                    dp[left, right] = Math.Max(
                        dp[left, right],
                        dp[left, k - 1] + extendedNums[left - 1] * extendedNums[k] * extendedNums[right + 1] + dp[k + 1, right]
                    );
                }
            }
        }

        return dp[1, n];
    }
}
