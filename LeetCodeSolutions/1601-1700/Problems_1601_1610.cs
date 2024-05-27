namespace LeetCodeSolutions._1601_1700;

public class Problems_1601_1610
{
    /// <summary>
    /// Problem 1608
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int SpecialArray(int[] nums)
    {
        // Sort the array in non-descending order
        Array.Sort(nums);

        int left = 0;
        int right = nums.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            // Calculate x (numbers to the left of current, including current)
            int x = nums.Length - mid;

            if (x <= nums[mid] && (mid == 0 || nums[mid - 1] < x))
            {
                return x;
            }
            else if (nums[mid] < x)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return -1;
    }
}
