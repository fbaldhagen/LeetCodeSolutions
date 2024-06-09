using LeetCodeSolutions.Structures;

namespace LeetCodeSolutions._0301_0400;

public class Problems_0311_0320
{
    /// <summary>
    /// Problem 312
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int MaxCoins(int[] nums)
    {
        int n = nums.Length;

        // Extend nums with 1's at the beginning and end
        int[] extendedNums = new int[n + 2];
        extendedNums[0] = 1;
        extendedNums[^1] = 1;
        for (int i = 1; i <= n; i++)
        {
            extendedNums[i] = nums[i - 1];
        }

        // 2D dp array. dp[m, n] stores the maximum coins by bursting balloons in the range [m, n].
        int[,] dp = new int[extendedNums.Length, extendedNums.Length];

        for (int length = 1; length <= n; length++)
        {
            for (int left = 1; left <= n - length + 1; left++)
            {
                int right = left + length - 1;
                for (int k = left; k <= right; k++)
                {
                    dp[left, right] = Math.Max(
                        dp[left, right],
                        dp[left, k - 1] + extendedNums[left - 1] * extendedNums[k] * extendedNums[right + 1] + dp[k + 1, right]
                    );
                }
            }
        }

        return dp[1, n];
    }

    /// <summary>
    /// Problem 313
    /// </summary>
    /// <param name="n"></param>
    /// <param name="primes"></param>
    /// <returns></returns>
    public static int NthSuperUglyNumber(int n, int[] primes)
    {
        PriorityQueue<int, int> pq = new();
        int prev = 1;

        for (int i = 1; i < n; i++)
        {
            for (int p = 0;  p < primes.Length; p++)
            {
                long factor = (long)primes[p] * (long)prev;

                if (factor <= Int32.MaxValue)
                {
                    pq.Enqueue((int)factor, (int) factor);
                }
            }

            int next = pq.Dequeue();
            while (next == prev)
            {
                next = pq.Dequeue();
            }
            prev = next;
        }

        return prev;
    }

    /// <summary>
    /// Problem 315
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static IList<int> CountSmaller(int[] nums)
    {
        int n = nums.Length;
        int[] result = new int[n];
        int[] sortedNums = (int[])nums.Clone();
        Array.Sort(sortedNums);

        Dictionary<int, int> ranks = [];
        for (int i = 0; i < n; i++)
        {
            ranks[sortedNums[i]] = i + 1;
        }

        int[] bit = new int[n + 1];

        for (int i = n - 1; i >= 0; i--)
        {
            int rank = ranks[nums[i]];
            result[i] = Query(rank - 1);
            Update(rank);
        }

        return result;


        void Update(int index)
        {
            while (index < bit.Length)
            {
                bit[index]++;
                index += index & -index;
            }
        }

        int Query(int index)
        {
            int sum = 0;
            while (index > 0)
            {
                sum += bit[index];
                index -= index & -index;
            }
            return sum;
        }
    }
}
