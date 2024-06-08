namespace LeetCodeSolutions._0501_0600;

public class Problems_0521_0530
{
    /// <summary>
    /// Problem 523
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static bool CheckSubarraySum(int[] nums, int k)
    {
        // Map the running sum remainder to its index
        Dictionary<int, int> remainderMap = [];
        remainderMap[0] = -1;

        int runningSum = 0;

        for (int i = 0; i < nums.Length; i++)
        {
            runningSum += nums[i];
            int remainder = runningSum % k;

            // Check if any remainder matches this, and if it's is possible to create a valid subarray with it
            if (remainderMap.TryGetValue(remainder, out int value))
            {
                if (i - value > 1)
                {
                    return true;
                }
            }
            else
            {
                remainderMap[remainder] = i;
            }
        }

        return false;
    }
}
