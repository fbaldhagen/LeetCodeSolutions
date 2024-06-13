using LeetCodeSolutions.Structures;
using System.Text;

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

    /// <summary>
    /// Problem 316
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string RemoveDuplicateLetters(string s)
    {
        Stack<char> stack = [];
        HashSet<char> seen = [];
        Dictionary<char, int> lastOcc = [];

        for (int i = 0; i < s.Length; i++)
        {
            lastOcc[s[i]] = i;
        }

        for (int i = 0; i < s.Length; i++)
        {
            char c = s[i];

            if (!seen.Contains(c))
            {
                while (stack.Count > 0 && c < stack.Peek() && i < lastOcc[stack.Peek()])
                {
                    seen.Remove(stack.Pop());
                }
                seen.Add(c);
                stack.Push(c);
            }
        }

        char[] result = [.. stack];
        Array.Reverse(result);
        return new string(result);
    }

    /// <summary>
    /// Problem 318
    /// </summary>
    /// <param name="words"></param>
    /// <returns></returns>
    public static int MaxProduct(string[] words)
    {
        int n = words.Length;
        int[] masks = new int[n];
        int[] lengths = new int[n];

        // create a bitmask that represents the characters present in the word
        for (int i = 0; i < n; i++)
        {
            int mask = 0;

            foreach (char c in words[i])
            {
                mask |= 1 << (c - 'a');
            }

            masks[i] = mask;
            lengths[i] = words[i].Length;
        }

        int maxProd = 0;

        // check if their bitmasks have any common bits set using bitwise AND (&).
        // If the result is 0, it means the words do not share any common letters.
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                if ((masks[i] & masks[j]) == 0)
                {
                    maxProd = Math.Max(maxProd, lengths[i] * lengths[j]);
                }
            }
        }

        return maxProd;
    }

    /// <summary>
    /// Problem 319
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int BulbSwitch(int n)
    {
        /// Returns the number of bulbs that remain on after n rounds.
        /// A bulb ends up on if it is toggled an odd number of times, 
        /// which occurs if the bulb is at a position that is a perfect square.
        /// Therefore, the number of bulbs that remain on is the number of perfect
        /// squares less than or equal to n, which is given by the integer part of sqrt(n)
        return (int)Math.Sqrt(n);
    }
}