namespace LeetCodeSolutions._0301_0400;

public class Problems_0371_0380
{
    /// <summary>
    /// Problem 371
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int GetSum(int a, int b)
    {
        // Continue until there is no carry left
        while (b != 0)
        {
            // Calculate the carry, which is where both bits are 1
            int carry = a & b;

            // XOR gives the sum without considering the carry
            a ^= b;

            // Shift the carry left by 1 to add it in the next higher bit position
            b = carry << 1;
        }

        // The sum is now stored in 'a'
        return a;
    }

    /// <summary>
    /// Problem 372
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int SuperPow(int a, int[] b)
    {
        const int MOD = 1337;
        return SuperPowHelper(a % MOD, b, b.Length - 1);

        static int SuperPowHelper(int a, int[] b, int index)
        {
            if (index == -1)
            {
                return 1;
            }

            // Calculate (a^b[index]) % MOD
            int part1 = PowMod(a, b[index]);

            // Calculate (previous result^10) % MOD
            int part2 = PowMod(SuperPowHelper(a, b, index - 1), 10);

            // Multiply both parts and return the result modulo MOD
            return (part1 * part2) % MOD;

            static int PowMod(int x, int n)
            {
                int result = 1;
                x %= MOD;
                while (n > 0)
                {
                    if (n % 2 == 1)
                    {
                        result = (result * x) % MOD;
                    }
                    x = (x * x) % MOD;
                    n /= 2;
                }
                return result;
            }
        }
    }

    /// <summary>
    /// Problem 373
    /// </summary>
    /// <param name="nums1"></param>
    /// <param name="nums2"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static IList<IList<int>> KSmallestPairs(int[] nums1, int[] nums2, int k)
    {
        List<IList<int>> result = [];
        if (nums1.Length == 0 || nums2.Length == 0 || k == 0)
        {
            return result;
        }

        // Min-heap to store pairs (sum, index1, index2) with sum being the priority
        PriorityQueue<(int sum, int i, int j), int> minHeap = new();

        // Initialize the heap with pairs (nums1[0] + nums2[j], 0, j) for j from 0 to nums2.Length - 1
        for (int j = 0; j < nums2.Length && j < k; j++)
        {
            minHeap.Enqueue((nums1[0] + nums2[j], 0, j), nums1[0] + nums2[j]);
        }

        while (k > 0 && minHeap.Count > 0)
        {
            // Dequeue the smallest pair from the heap
            (_, int i, int j) = minHeap.Dequeue();
            result.Add([nums1[i], nums2[j]]);
            k--;

            // If there are more elements in nums1, enqueue the next pair (nums1[i + 1], nums2[j])
            if (i + 1 < nums1.Length)
            {
                minHeap.Enqueue((nums1[i + 1] + nums2[j], i + 1, j), nums1[i + 1] + nums2[j]);
            }
        }

        return result;
    }

    /// <summary>
    /// Problem 374
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int GuessNumber(int n)
    {
        // This is not a part of the solution, just a way to get guesses and check them.
        Random r = new();
        int number = r.Next(1, n + 1);


        int left = 1;
        int right = n;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            int ans = guess(mid);

            if (ans == -1)
            {
                right = mid - 1;
            }
            else if (ans == 1)
            {
                left = mid + 1;
            }
            else
            {
                return mid;
            }
        }

        return -1;

        // Placeholder
        int guess(int val)
        {
            if (val < number)
            {
                return -1;
            }
            else if (val > number)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }


    /// <summary>
    /// Problem 376
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int WiggleMaxLength(int[] nums)
    {
        int n = nums.Length;
        // Trivial wiggle sequences
        if (n == 1 || (n == 2 && nums[0] != nums[1]))
        {
            return n;
        }

        // Track lengths of sequences that end in an up or down wiggle
        int up = 1;
        int down = 1;

        for (int i = 1; i < n; i++)
        {
            if (nums[i] > nums[i - 1])
            {
                up = down + 1;
            }
            else if (nums[i] < nums[i - 1])
            {
                down = up + 1;
            }
        }

        return (up >= down) ? up : down;
    }
}