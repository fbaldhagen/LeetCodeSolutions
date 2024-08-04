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

    /// <summary>
    /// Problem 365
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static bool CanMeasureWater(int x, int y, int target)
    {
        // This problem can be reduced to a problem of finding whether
        // the target amount can be obtained using combinations of x and y
        // based on the Bézout's identity (ax + by = gcd(x, y))

        // Method to find the greatest common divisor
        static int Gcd(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        // If target is greater than the sum of x and y, it is impossible to measure
        if (target > x + y)
        {
            return false;
        }

        // Check if target is a multiple of the gcd of x and y
        return target % Gcd(x, y) == 0;
    }

    /// <summary>
    /// Problem 367
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public static bool IsPerfectSquare(int num)
    {
        // Binary search approach to determine if num is a perfect square
        long left = 1;
        long right = num;

        while (left <= right)
        {
            long mid = left + (right - left) / 2;
            long square = mid * mid;

            if (square == num)
            {
                return true;
            }
            else if (square < num)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return false;
    }

    /// <summary>
    /// Problem 368
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static IList<int> LargestDivisibleSubset(int[] nums)
    {
        // Sort the array to ensure that for any i < j, nums[j] % nums[i] can be easily checked
        Array.Sort(nums);

        // dp[i] will store the size of the largest divisible subset that ends with nums[i]
        int[] dp = new int[nums.Length];

        // previous[i] will store the index of the previous element in the largest divisible subset ending at nums[i]
        int[] previous = new int[nums.Length];

        // Initialize variables to track the index of the largest subset's end
        int maxIndex = 0;

        for (int i = 0; i < nums.Length; i++)
        {
            dp[i] = 1; // Each number is divisible by itself, so minimum subset length is 1
            previous[i] = -1; // Initialize with -1 meaning no previous element

            for (int j = 0; j < i; j++)
            {
                // Check if nums[i] is divisible by nums[j] and update dp and previous accordingly
                if (nums[i] % nums[j] == 0 && dp[j] + 1 > dp[i])
                {
                    dp[i] = dp[j] + 1;
                    previous[i] = j;
                }
            }

            // Update maxIndex if we find a larger subset
            if (dp[i] > dp[maxIndex])
            {
                maxIndex = i;
            }
        }

        // Backtrack to find the largest divisible subset
        List<int> result = [];
        int k = maxIndex;
        while (k >= 0)
        {
            result.Add(nums[k]);
            k = previous[k];
        }

        // Reverse the result to get the correct order
        result.Reverse();
        return result;
    }
}