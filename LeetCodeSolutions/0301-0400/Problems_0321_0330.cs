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
}