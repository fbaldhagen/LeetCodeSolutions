using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeSolutions._0701_0800
{
    public class Problems_0711_0720
    {
        /// <summary>
        /// Problem 719
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int SmallestDistancePair(int[] nums, int k)
        {
            Array.Sort(nums);
            int n = nums.Length;
            int low = 0;
            int high = nums[n - 1] - nums[0];

            int CountPairsWithDistanceLessThanOrEqual(int mid)
            {
                int count = 0;
                int left = 0;

                for (int right = 0; right < n; right++)
                {
                    while (nums[right] - nums[left] > mid)
                    {
                        left++;
                    }

                    count += right - left;
                }

                return count;
            }

            while (low < high)
            {
                int mid = (low + high) / 2;

                if (CountPairsWithDistanceLessThanOrEqual(mid) < k)
                {
                    low = mid + 1;
                }
                else
                {
                    high = mid;
                }
            }

            return low;
        }
    }
}