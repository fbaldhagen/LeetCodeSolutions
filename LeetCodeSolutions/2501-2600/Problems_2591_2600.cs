namespace LeetCodeSolutions._2501_2600;

public class Problems_2591_2600
{
    /// <summary>
    /// Problem 2597
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static int BeautifulSubsets(int[] nums, int k)
    {
        // Sort the array to make it easier to handle the absolute difference condition
        Array.Sort(nums);

        // Call the recursive function to count beautiful subsets starting from the first element
        return CountBeautifulSubsets(nums, k, [], 0);

        // Define a recursive function to count beautiful subsets
        static int CountBeautifulSubsets(int[] nums, int k, List<int> currentSubset, int index)
        {
            // Base case: if we've considered all elements in nums
            if (index == nums.Length)
            {
                // If currentSubset is non-empty, return 1 (count it as a valid subset)
                // Otherwise, return 0
                return currentSubset.Count > 0 ? 1 : 0;
            }

            // Recursive call: count subsets excluding the current element
            int countWithoutCurrent = CountBeautifulSubsets(nums, k, currentSubset, index + 1);

            // Determine if we can include the current element without violating the condition
            bool canIncludeCurrent = true;
            foreach (int num in currentSubset)
            {
                // If the absolute difference between any element in currentSubset and nums[index] is k, we can't include it
                if (Math.Abs(num - nums[index]) == k)
                {
                    canIncludeCurrent = false;
                    break;
                }
            }

            int countWithCurrent = 0;
            if (canIncludeCurrent)
            {
                // Include the current element and count subsets including it
                currentSubset.Add(nums[index]);
                countWithCurrent = CountBeautifulSubsets(nums, k, currentSubset, index + 1);
                // Backtrack by removing the current element from the subset
                currentSubset.RemoveAt(currentSubset.Count - 1);
            }

            // Return the total count of subsets, both including and excluding the current element
            return countWithoutCurrent + countWithCurrent;
        }
    }
