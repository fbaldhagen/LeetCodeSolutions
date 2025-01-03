namespace LeetCodeSolutions._2201_2300;

public class Problems_2261_2270
{
    /// <summary>
    /// Problem 2270
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int WaysToSplitArray(int[] nums)
    {
        int n = nums.Length;
        int validSplits = 0;

        long totalSum = 0;
        foreach (int num in nums)
        {
            totalSum += num;
        }

        long leftSum = 0;
        for (int i = 0; i < n - 1; i++)
        {
            leftSum += nums[i];
            long rightSum = totalSum - leftSum;

            if (leftSum >= rightSum)
            {
                validSplits++;
            }
        }

        return validSplits;
    }
}