using LeetCodeSolutions.Structures;

namespace LeetCodeSolutions._0301_0400;

public class Problems_0321_0330
{
    /// <summary>
    /// Problem 321
    /// </summary>
    /// <param name="nums1"></param>
    /// <param name="nums2"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static int[] MaxNumber(int[] nums1, int[] nums2, int k)
    {
        int m = nums1.Length;
        int n = nums2.Length;
        int[] result = new int[k];

        // Iterate over all possible lengths of subarrays taken from nums1
        // i represents the number of elements taken from nums1, hence k-i from nums2
        for (int i = Math.Max(0, k - n); i <= Math.Min(k, m); i++)
        {
            // Generate the maximum subarrays from nums1 and nums2 of lengths i and k-i respectively
            int[] candidate = Merge(MaxSubarray(nums1, i), MaxSubarray(nums2, k - i), k);

            // Update the result if the candidate is greater than the current result
            if (Greater(candidate, 0, result, 0))
            {
                result = candidate;
            }
        }

        return result;

        // Helper function to generate the maximum subarray of a given length from a single array
        static int[] MaxSubarray(int[] nums, int k)
        {
            int n = nums.Length;
            int[] result = new int[k];
            int j = 0;
            for (int i = 0; i < n; i++)
            {
                // Ensure the result array remains the largest possible by removing smaller elements
                while (n - i + j > k && j > 0 && result[j - 1] < nums[i])
                {
                    // Remove the last element from the result
                    j--;
                }
                // Add the current element to the result if there's still space
                if (j < k)
                {
                    result[j++] = nums[i];
                }
            }
            return result;
        }

        // Helper function to merge two arrays into the largest possible sequence
        static int[] Merge(int[] nums1, int[] nums2, int k)
        {
            int[] result = new int[k];
            int i = 0;
            int j = 0;
            int r = 0;

            // Merge elements from nums1 and nums2 into the result array
            while (r < k)
            {
                // Choose the larger element from nums1 or nums2 while maintaining order
                if (Greater(nums1, i, nums2, j))
                {
                    result[r++] = nums1[i++];
                }
                else
                {
                    result[r++] = nums2[j++];
                }
            }
            return result;
        }

        // Helper function to compare two subarrays to determine which is larger
        static bool Greater(int[] nums1, int i, int[] nums2, int j)
        {
            // Compare elements starting from indices i and j in nums1 and nums2 respectively
            while (i < nums1.Length && j < nums2.Length && nums1[i] == nums2[j])
            {
                i++;
                j++;
            }
            // Return true if nums1's remaining elements are larger, false otherwise
            return j == nums2.Length || (i < nums1.Length && nums1[i] > nums2[j]);
        }
    }

    /// <summary>
    /// Problem 322
    /// </summary>
    /// <param name="coins"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static int CoinChange(int[] coins, int amount)
    {
        // Initialize a dp array to store the minimum number of coins for each amount
        int[] dp = new int[amount + 1];

        // Initialize dp array with a value larger than any possible number of coins (amount + 1)
        Array.Fill(dp, amount + 1);

        // Base case: 0 coins are needed to make amount 0
        dp[0] = 0;

        // Iterate through each amount from 1 to amount
        for (int i = 1; i <= amount; i++)
        {
            // Iterate through each coin denomination
            foreach (int coin in coins)
            {
                // If the current coin can be used to form the current amount
                if (coin <= i)
                {
                    // Update dp[i] by taking the minimum of its current value and 1 + dp[i - coin]
                    dp[i] = Math.Min(dp[i], 1 + dp[i - coin]);
                }
            }
        }

        // If dp[amount] is still amount + 1, it means no valid combination was found
        return dp[amount] > amount ? -1 : dp[amount];
    }

    /// <summary>
    /// Problem 324
    /// </summary>
    /// <param name="nums"></param>
    public static void WiggleSort(int[] nums)
    {
        Array.Sort(nums);
        int[] result = new int[nums.Length];
        int mid;

        if (nums.Length % 2 == 0)
        {
            mid = nums.Length / 2;
        }
        else
        {
            mid = nums.Length / 2 + 1;
        }

        int evenIndex = mid - 1;
        int oddIndex = nums.Length - 1;

        for (int i = 0; i < nums.Length; i++)
        {
            if (i % 2 == 0)
            {
                result[i] = nums[evenIndex];
                evenIndex--;
            }
            else
            {
                result[i] = nums[oddIndex];
                oddIndex--;
            }
        }

        Array.Copy(result, nums, nums.Length);
    }

    /// <summary>
    /// Problem 326
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static bool IsPowerOfThree(int n)
    {
        // 3^19 (1162261467) is the largest power of 3 that fits in an int.
        // Any power of three will be a divisor of 1162261467
        return n > 0 && (1162261467 % n == 0);
    }

    /// <summary>
    /// Problem 327
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="lower"></param>
    /// <param name="upper"></param>
    /// <returns></returns>
    public static int CountRangeSum(int[] nums, int lower, int upper)
    {
        // Array to compute prefix sums
        long[] prefixSums = new long[nums.Length + 1];
        for (int i = 0; i < nums.Length; i++)
        {
            // Each element in prefixSums represents the sum of elements from the start up to the current index
            prefixSums[i + 1] = prefixSums[i] + nums[i];
        }

        return CountWhileSorting(prefixSums, 0, prefixSums.Length, lower, upper);

        // Count range sums while sorting
        static int CountWhileSorting(long[] sums, int start, int end, int lower, int upper)
        {
            // Base case: if the range has 1 or fewer elements, no valid range sums can be found
            if (end - start <= 1)
            {
                return 0;
            }

            int mid = (start + end) / 2;
            // Count the number of valid range sums in each half and sum them up
            int count = CountWhileSorting(sums, start, mid, lower, upper) + CountWhileSorting(sums, mid, end, lower, upper);

            // Initialize pointers for the right half
            int j = mid;
            int k = mid;
            int l = mid;

            // Temporary array to store merged and sorted prefix sums
            long[] cache = new long[end - start];
            int r = 0;

            // Iterate over each prefix sum in the left half
            for (int i = start; i < mid; i++)
            {
                // Move the pointer k to find the first prefix sum in the right half
                // where the difference is at least `lower`
                while (k < end && sums[k] - sums[i] < lower)
                {
                    k++;
                }

                // Move the pointer j to find the first prefix sum in the right half
                // where the difference is greater than `upper`
                while (j < end && sums[j] - sums[i] <= upper)
                {
                    j++;
                }

                // Merge the two halves while keeping them sorted
                while (l < end && sums[l] < sums[i])
                {
                    cache[r++] = sums[l++];
                }

                // Add the current prefix sum from the left half to the cache
                cache[r++] = sums[i];
                // The number of valid range sums for the current prefix sum in the left half
                count += j - k;
            }

            // Copy the sorted elements back into the original sums array
            Array.Copy(cache, 0, sums, start, l - start);
            return count;
        }
    }

    /// <summary>
    /// Problem 328
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public static ListNode OddEvenList(ListNode head)
    {
        if (head is null || head.next is null)
        {
            return head;
        }

        ListNode odd = head;
        ListNode even = head.next;
        ListNode evenHead = even;

        while (even is not null && even.next is not null)
        {
            odd.next = even.next;
            odd = odd.next;
            even.next = odd.next;
            even = even.next;
        }

        odd.next = evenHead;

        return head;
    }

    /// <summary>
    /// Problem 330
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int MinPatches(int[] nums, int n)
    {
        // Track smallest number that cant be formed
        long miss = 1;
        // Index to iterate through nums
        int i = 0;
        // Count number of patches
        int patches = 0;

        // Cover all numbers up to n
        while (miss <= n)
        {
            // i in bounds and nums[i] is less than or equal to miss.
            
            if (i < nums.Length && nums[i] <= miss)
            {
                // We can use nums[i] to form new sums, update miss to miss + nums[i]
                miss += nums[i];
                // Increment i by 1
                i++;
            }
            // If nums[i] is greater than miss or i is out of bounds, it means miss is the smallest
            // number that cannot be formed, so we patch by adding miss to the array
            else
            {
                // This doubles miss (since we can now form all sums up to 2 * miss - 1), and increment the patches count.
                miss += miss;
                patches++;
            }
        }

        return patches;
    }
}