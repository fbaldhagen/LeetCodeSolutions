namespace LeetCodeSolutions._0301_0400;

public class Problems_0301_0310
{
    /// <summary>
    /// Problem 300
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int LengthOfLIS(int[] nums)
    {
        int len = 0;
        int[] tails = new int[nums.Length];

        foreach (int num in nums)
        {
            int left = 0;
            int right = len;

            while (left < right)
            {
                int mid = left + (right - left) / 2;
                if (tails[mid] < num)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid;
                }
            }

            tails[left] = num;
            if (left == len)
            {
                len++;
            }
        }

        return len;
    }

    /// <summary>
    /// Problem 301
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static IList<string> RemoveInvalidParentheses(string s)
    {
        IList<string> result = [];

        if (string.IsNullOrEmpty(s))
        {
            return result;
        }

        Queue<string> queue = [];
        HashSet<string> visited = [];

        queue.Enqueue(s);
        visited.Add(s);

        bool found = false;

        while (queue.Count > 0)
        {
            int size = queue.Count;

            for (int i = 0; i < size; i++)
            {
                string curr = queue.Dequeue();

                if (IsValid(curr))
                {
                    result.Add(curr);
                    found = true;
                }

                if (found)
                {
                    continue;
                }

                for (int j = 0; j < curr.Length; j++)
                {
                    string next = string.Concat(curr.AsSpan(0, j), curr.AsSpan(j + 1));

                    if (!visited.Contains(next))
                    {
                        queue.Enqueue(next);
                        visited.Add(next);
                    }
                }
            }

            if (found)
            {
                break;
            }
        }

        return result.Count == 0 ? [""] : result;

        static bool IsValid(string s)
        {
            int count = 0;
            foreach (char c in s)
            {
                if (c == '(')
                {
                    count++;
                }
                if (c == ')')
                {
                    count--;
                    if (count < 0)
                    {
                        return false;
                    }
                }
            }

            return count == 0;
        }
    }

    /// <summary>
    /// Problem 303
    /// </summary>
    public class NumArray
    {
        private readonly int[] sumArray;
        public NumArray(int[] nums)
        {
            sumArray = new int[nums.Length];

            int currSum = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                currSum += nums[i];
                sumArray[i] = currSum;
            }
        }

        public int SumRange(int left, int right)
        {
            return left == 0 ? sumArray[right] : sumArray[right] - sumArray[left - 1];
        }
    }

    /// <summary>
    /// Problem 304
    /// </summary>
    public class NumMatrix
    {
        private readonly int[][] prefixSum;

        public NumMatrix(int[][] matrix)
        {
            int rows = matrix.Length;
            int cols = matrix[0].Length;

            prefixSum = new int[rows + 1][];

            for (int i = 0; i <= rows; i++)
            {
                prefixSum[i] = new int[cols + 1];
            }

            for (int r = 1; r <= rows; r++)
            {
                for (int c = 1; c <= cols; c++)
                {
                    prefixSum[r][c] = matrix[r - 1][c - 1]
                                    + prefixSum[r - 1][c]
                                    + prefixSum[r][c - 1]
                                    - prefixSum[r - 1][c - 1];
                }
            }
        }

        public int SumRegion(int row1, int col1, int row2, int col2)
        {
            return prefixSum[row2 + 1][col2 + 1]
                 - prefixSum[row1][col2 + 1]
                 - prefixSum[row2 + 1][col1]
                 + prefixSum[row1][col1];
        }
    }

    /// <summary>
    /// Problem 306
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public static bool IsAdditiveNumber(string num)
    {
        int n = num.Length;

        // Helper function to perform DFS to find additive sequence
        bool DFS(int start, long prev1, long prev2, int count)
        {
            if (start == n)
            {
                return count >= 3;
            }

            long current = 0;
            for (int i = start; i < n; i++)
            {
                // Avoid numbers with leading zeros
                if (i > start && num[start] == '0')
                {
                    break;
                }

                current = current * 10 + (num[i] - '0');

                // If count is less than 2, we need at least two numbers to start checking
                if (count < 2 || current == prev1 + prev2)
                {
                    if (DFS(i + 1, prev2, current, count + 1))
                    {
                        return true;
                    }
                }
                else if (current > prev1 + prev2)
                {
                    break;
                }
            }

            return false;
        }

        // Start the DFS from the first character
        return DFS(0, 0, 0, 0);
    }

    /// <summary>
    /// Problem 307
    /// </summary>
    public class NumArrayII
    {
        private readonly int[] segmentTree;
        private readonly int n;

        /// <summary>
        /// Initializes a new instance of the <see cref="NumArrayII"/> class.
        /// </summary>
        /// <param name="nums">The integer array to be managed.</param>
        public NumArrayII(int[] nums)
        {
            n = nums.Length;
            segmentTree = new int[2 * n];
            BuildSegmentTree(nums);
        }

        /// <summary>
        /// Builds the segment tree from the provided integer array.
        /// </summary>
        /// <param name="nums">The integer array used to build the segment tree.</param>
        private void BuildSegmentTree(int[] nums)
        {
            for (int i = n, j = 0; i < 2 * n; i++, j++)
            {
                segmentTree[i] = nums[j];
            }
            for (int i = n - 1; i > 0; --i)
            {
                segmentTree[i] = segmentTree[i * 2] + segmentTree[i * 2 + 1];
            }
        }

        /// <summary>
        /// Updates the value at the specified index of the array.
        /// </summary>
        /// <param name="index">The index of the element to update.</param>
        /// <param name="val">The new value to set at the specified index.</param>
        public void Update(int index, int val)
        {
            index += n;
            segmentTree[index] = val;
            while (index > 0)
            {
                int left = index;
                int right = index;
                if (index % 2 == 0)
                {
                    right = index + 1;
                }
                else
                {
                    left = index - 1;
                }
                segmentTree[index / 2] = segmentTree[left] + segmentTree[right];
                index /= 2;
            }
        }

        /// <summary>
        /// Calculates the sum of elements in the array between the specified range [left, right].
        /// </summary>
        /// <param name="left">The starting index of the range (inclusive).</param>
        /// <param name="right">The ending index of the range (inclusive).</param>
        /// <returns>The sum of elements in the specified range.</returns>
        public int SumRange(int left, int right)
        {
            left += n;
            right += n;
            int sum = 0;
            while (left <= right)
            {
                if (left % 2 == 1)
                {
                    sum += segmentTree[left];
                    left++;
                }
                if (right % 2 == 0)
                {
                    sum += segmentTree[right];
                    right--;
                }
                left /= 2;
                right /= 2;
            }
            return sum;
        }
    }

    /// <summary>
    /// Problem 309
    /// </summary>
    /// <param name="prices"></param>
    /// <returns></returns>
    public static int MaxProfit(int[] prices)
    {
        int sold = int.MinValue;
        int hold = int.MinValue;
        int profit = 0;

        foreach (int price in prices)
        {
            int temp = sold;

            sold = hold + price;
            hold = Math.Max(hold, profit - price);

            profit = Math.Max(profit, temp);
        }

        return Math.Max(sold, profit);
    }

    /// <summary>
    /// Problem 310
    /// </summary>
    /// <param name="n"></param>
    /// <param name="edges"></param>
    /// <returns></returns>
    public static IList<int> FindMinHeightTrees(int n, int[][] edges)
    {
        // Special case when there's only one node
        if (n == 1)
        {
            return [0];
        }

        // Construct adjacency list
        List<IList<int>> adjList = new(n);

        for (int i = 0; i < n; i++)
        {
            adjList.Add([]);
        }

        foreach (int[] edge in edges)
        {
            int u = edge[0];
            int v = edge[1];
            adjList[u].Add(v);
            adjList[v].Add(u);
        }

        // Initialize leaves queue
        Queue<int> leaves = new();
        for (int i = 0; i < n; i++)
        {
            // If node has only one neighbor, it's a leaf
            if (adjList[i].Count == 1)
            {
                leaves.Enqueue(i);
            }
        }

        // Remove leaves level by level until 1 or 2 nodes left
        while (n > 2)
        {
            int leavesCount = leaves.Count;
            n -= leavesCount;

            for (int i = 0; i < leavesCount; i++)
            {
                int leaf = leaves.Dequeue();

                foreach (int neighbor in adjList[leaf])
                {
                    // Remove leaf from its neighbor's adjacency list
                    adjList[neighbor].Remove(leaf);

                    // If neighbor becomes a leaf
                    if (adjList[neighbor].Count == 1)
                    {
                        leaves.Enqueue(neighbor);
                    }
                }
            }
        }

        // Remaining nodes in 'leaves' are the roots of MHTs
        var result = new List<int>(leaves);
        return result;
    }
}