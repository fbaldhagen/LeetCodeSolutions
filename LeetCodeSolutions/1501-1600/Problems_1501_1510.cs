namespace LeetCodeSolutions._1501_1600;

public class Problems_1501_1510
{
    /// <summary>
    /// Problem 1509
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int MinDifference(int[] nums)
    {
        if (nums.Length <= 4)
        {
            return 0;
        }

        Array.Sort(nums);
        int n = nums.Length;

        // Consider the smallest difference after changing at most 3 elements
        int minDiff = int.MaxValue;

        // Compare all 4 scenarios
        minDiff = Math.Min(minDiff, nums[n - 1] - nums[3]);
        minDiff = Math.Min(minDiff, nums[n - 2] - nums[2]);
        minDiff = Math.Min(minDiff, nums[n - 3] - nums[1]);
        minDiff = Math.Min(minDiff, nums[n - 4] - nums[0]);

        return minDiff;
    }
}
