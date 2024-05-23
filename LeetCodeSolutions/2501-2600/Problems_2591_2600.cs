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
        Array.Sort(nums);
        return CountBeautifulSubsets(nums, k, [], 0);

        static int CountBeautifulSubsets(int[] nums, int k, List<int> currentSubset, int index)
        {
            if (index == nums.Length)
            {
                return currentSubset.Count > 0 ? 1 : 0;
            }

            int countWithoutCurrent = CountBeautifulSubsets(nums, k, currentSubset, index + 1);

            bool canIncludeCurrent = true;
            foreach (int num in currentSubset)
            {
                if (Math.Abs(num - nums[index]) == k)
                {
                    canIncludeCurrent = false;
                    break;
                }
            }

            int countWithCurrent = 0;
            if (canIncludeCurrent)
            {
                currentSubset.Add(nums[index]);
                countWithCurrent = CountBeautifulSubsets(nums, k, currentSubset, index + 1);
                currentSubset.RemoveAt(currentSubset.Count - 1);
            }

            return countWithoutCurrent + countWithCurrent;
        }
    }
}
