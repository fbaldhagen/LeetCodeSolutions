namespace LeetCodeSolutions._0901_1000;

public class Problems_0941_0950
{
    /// <summary>
    /// Problem 945
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int MinIncrementForUnique(int[] nums)
    {
        // Sort in non-descending order
        Array.Sort(nums);

        // Start counting
        int minIncrement = 0;
        for (int i = 1; i < nums.Length; i++)
        {
            // Compare with previous element
            if (nums[i] <= nums[i - 1])
            {
                // Calculate the increment. Previous value - current value + 1
                int increment = nums[i - 1] + 1 - nums[i];
                nums[i] = nums[i - 1] + 1;
                minIncrement += increment;
            }
        }

        return minIncrement;
    }
}
