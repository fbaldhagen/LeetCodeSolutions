namespace LeetCodeSolutions._0301_0400;

public class Problems_0361_0370
{
    /// <summary>
    /// Problem 363
    /// </summary>
    /// <param name="matrix"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static int MaxSumSubmatrix(int[][] matrix, int k)
    {
        int m = matrix.Length; // Number of rows in the matrix
        int n = matrix[0].Length; // Number of columns in the matrix
        int result = int.MinValue; // Initialize result to the smallest possible value

        // Iterate over all pairs of columns
        for (int left = 0; left < n; left++)
        {
            int[] rowSum = new int[m]; // Initialize rowSum array to store sums of elements for each row

            for (int right = left; right < n; right++)
            {
                // Update row sums between left and right columns
                for (int i = 0; i < m; i++)
                {
                    rowSum[i] += matrix[i][right];
                }

                // Find the maximum sum subarray no larger than k using the rowSum array
                result = Math.Max(result, MaxSumSubarray(rowSum, k));
                // If the result equals k, we found the largest possible subarray sum no larger than k, so return it
                if (result == k)
                {
                    return k;
                }
            }
        }

        return result; // Return the maximum subarray sum found that is no larger than k

        static int MaxSumSubarray(int[] nums, int k)
        {
            int maxSum = int.MinValue;
            for (int start = 0; start < nums.Length; start++)
            {
                int currSum = 0;
                for (int end = start; end < nums.Length; end++)
                {
                    currSum += nums[end];
                    if (currSum <= k)
                    {
                        maxSum = Math.Max(maxSum, currSum);
                    }
                }
            }
            return maxSum;
        }
    }
}
