using LeetCodeSolutions.Structures;

namespace LeetCodeSolutions._0101_0200;

public class Problems_0121_0130
{
    /// <summary>
    /// Problem 121
    /// </summary>
    /// <param name="prices"></param>
    /// <returns></returns>
    public static int MaxProfit(int[] prices)
    {
        if (prices == null || prices.Length < 2)
            return 0;

        int n = prices.Length;
        // Use a 1D dp array. dp[i] is the maximum profit on day i
        int[] dp = new int[n];

        // Initialize the dp array. Can't make a profit on day 1.
        dp[0] = 0;
        int minPrice = prices[0];

        // Iterate through the prices array starting from the second day
        for (int i = 1; i < n; i++)
        {
            // Update the minimum price seen so far
            minPrice = Math.Min(minPrice, prices[i]);

            // Calculate the maximum profit for the current day
            dp[i] = Math.Max(dp[i - 1], prices[i] - minPrice);
        }

        // The maximum profit will be the last element of the dp array
        return dp[n - 1];
    }

    /// <summary>
    /// Problem 122
    /// </summary>
    /// <param name="prices"></param>
    /// <returns></returns>
    public static int MaxProfitII(int[] prices)
    {
        if (prices == null || prices.Length < 2)
        {
            return 0;
        }

        int n = prices.Length;
        // Dp array with two states: 0 for not holding, 1 for holding
        int[,] dp = new int[n, 2];

        // Base case: on the first day, if we buy, we will have negative profit
        dp[0, 0] = 0;
        dp[0, 1] = -prices[0];

        // Iterate through the prices array
        for (int i = 1; i < n; i++)
        {
            // If we don't hold a stock on day i, we can either continue not holding a stock or buy one
            dp[i, 0] = Math.Max(dp[i - 1, 0], dp[i - 1, 1] + prices[i]);
            // If we hold a stock on day i, we can either continue holding or sell it
            dp[i, 1] = Math.Max(dp[i - 1, 1], dp[i - 1, 0] - prices[i]);
        }

        // The maximum profit will be the maximum value of the last day in the not holding state
        return dp[n - 1, 0];
    }

    /// <summary>
    /// Problem 123
    /// </summary>
    /// <param name="prices"></param>
    /// <returns></returns>
    public static int MaxProfitIII(int[] prices)
    {
        if (prices is null || prices.Length < 2)
        {
            return 0;
        }

        int n = prices.Length;

        // dp[i, j, k] is profit on day i, with j transactions and state k (0 = not holding, 1 = holding)
        int[,,] dp = new int[n, 3, 2];

        // Base case: First day. If we're holding (k = 1), we have a negative profit
        // If we're not holding, we've either not bought, or we've bought and sold, j times.
        for (int j = 0; j < 3; j++)
        {
            dp[0, j, 0] = 0;
            dp[0, j, 1] = -prices[0];
        }

        // Iterate through the prices array
        for (int i = 1; i < n; i++)
        {
            for (int j = 1; j < 3; j++)
            {
                // If we don't hold a stock on day i and have completed j transactions, we can either continue not holding or buy one
                dp[i, j, 0] = Math.Max(dp[i - 1, j, 0], dp[i - 1, j, 1] + prices[i]);
                // If we hold a stock on day i and have completed j transactions, we can either continue holding or sell it
                dp[i, j, 1] = Math.Max(dp[i - 1, j, 1], dp[i - 1, j - 1, 0] - prices[i]);
            }
        }

        // The maximum profit will be the maximum value of the last day with at most maxTransactions transactions and not holding state
        int maxProfit = 0;
        for (int j = 0; j < 3; j++)
        {
            maxProfit = Math.Max(maxProfit, dp[n - 1, j, 0]);
        }

        return maxProfit;
    }

    /// <summary>
    /// Problem 124
    /// </summary>
    /// <param name="root"></param>
    public static int MaxPathSum(TreeNode root)
    {
        int maxSum = int.MinValue;
        CalculateMaxPathSum(root);
        return maxSum;

        // Recursive function to calculate the maximum path sum that includes the current node
        int CalculateMaxPathSum(TreeNode? node)
        {
            if (node is null)
            {
                return 0;
            }

            // Calculate the maximum path sum in the left subtree
            int leftSum = Math.Max(0, CalculateMaxPathSum(node.left));

            // Calculate the maximum path sum in the right subtree
            int rightSum = Math.Max(0, CalculateMaxPathSum(node.right));

            // Update the maximum path sum found so far
            maxSum = Math.Max(maxSum, leftSum + rightSum + node.val);

            // Return the maximum sum of the path that includes the current node
            return Math.Max(leftSum, rightSum) + node.val;
        }
    }

    /// <summary>
    /// Problem 125
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static bool IsPalindrome(string s)
    {
        // Use left and right pointers to traverse the string
        int left = 0;
        int right = s.Length - 1;

        while (left < right)
        {
            // Skip all non-alphanumerical characters, adjust pointers accordingly
            while (left < right && !char.IsLetterOrDigit(s[left]))
            {
                left++;
            }

            while (left < right && !char.IsLetterOrDigit(s[right]))
            {
                right--;
            }

            // Check if the lowercase representation of the current characters match. 
            // If they don't, it's not a palindrome
            if (char.ToLower(s[left]) != char.ToLower(s[right]))
            {
                return false;
            }

            // Move pointers inwards
            left++;
            right--;
        }

        // If we've traversed the entire string without catching a mismatch, return true
        return true;
    }

    /// <summary>
    /// Problem 126
    /// </summary>
    /// <param name="beginWord"></param>
    /// <param name="endWord"></param>
    /// <param name="wordList"></param>
    /// <returns></returns>
    public static IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList)
    {
        IList<IList<string>> ans = [];
        // Reverse graph starting from endWord
        Dictionary<string, HashSet<string>> reverse = [];
        // Remove duplicate words
        HashSet<string> wordSet = new(wordList);
        // Remove the first word to avoid cycle path
        wordSet.Remove(beginWord);
        // Store current layer nodes
        Queue<string> queue = new();
        // First layer has only beginWord
        queue.Enqueue(beginWord);
        // Store nextLayer nodes
        HashSet<string> nextLevel = [];
        // Find endWord flag
        bool findEnd = false;

        while (queue.Count > 0)
        {
            // Traverse current layer nodes
            string word = queue.Dequeue();
            foreach (string next in wordSet.ToList())
            {
                if (IsLadder(word, next))
                {
                    // Construct the reverse graph from endWord
                    if (!reverse.TryGetValue(next, out HashSet<string>? value))
                    {
                        value = [];
                        reverse[next] = value;
                    }

                    value.Add(word);

                    if (endWord == next)
                    {
                        findEnd = true;
                    }
                    nextLevel.Add(next); // Store next layer nodes
                }
            }

            // When current layer is all visited
            if (queue.Count == 0)
            // If find the endWord, then break the while loop
            {
                if (findEnd)
                {
                    break;
                }

                foreach (string nextWord in nextLevel)
                {
                    // Add next layer nodes to queue
                    queue.Enqueue(nextWord);
                    // Remove all next layer nodes in wordSet
                    wordSet.Remove(nextWord);
                }
                nextLevel.Clear();
            }
        }

        // If can't reach endWord from startWord, then return ans.
        if (!findEnd)
        {
            return ans;
        }

        HashSet<string> path =
        [
            endWord
        ];

        // Traverse reverse graph from endWord to beginWord
        FindPath(endWord, beginWord, reverse, ans, path);

        return ans;

        // Finds all the transformation paths recursively from endWord to beginWord.
        void FindPath(string endWord, string beginWord, Dictionary<string, HashSet<string>> graph, IList<IList<string>> ans, HashSet<string> path)
        {
            if (!graph.TryGetValue(endWord, out HashSet<string>? value))
            {
                return;
            }

            foreach (string word in value)
            {
                path.Add(word);
                if (beginWord == word)
                {
                    List<string> shortestPath = new(path);
                    // Reverse words in shortest path
                    shortestPath.Reverse();
                    // Add the shortest path to ans.
                    ans.Add(shortestPath);
                }
                else
                {
                    FindPath(word, beginWord, graph, ans, path);
                }
                path.Remove(word);
            }
        }

        // Checks if two words are ladder words (differ by only one character).
        bool IsLadder(string s, string t)
        {
            if (s.Length != t.Length)
            {
                return false;
            }
            int diffCount = 0;
            int n = s.Length;
            for (int i = 0; i < n; i++)
            {
                if (s[i] != t[i])
                {
                    diffCount++;
                }
                if (diffCount > 1)
                {
                    return false;
                }
            }
            return diffCount == 1;
        }
    }

    /// <summary>
    /// Problem 127
    /// </summary>
    /// <param name="beginWord"></param>
    /// <param name="endWord"></param>
    /// <param name="wordList"></param>
    /// <returns></returns>
    public static int LadderLength(string beginWord, string endWord, IList<string> wordList)
    {
        // Convert wordList to HashSet for efficient lookup
        HashSet<string> wordSet = new(wordList);

        // If endWord is not in wordList, return 0
        if (!wordSet.Contains(endWord))
        {
            return 0;
        }

        // Initialize a queue for BFS
        Queue<string> queue = new();
        queue.Enqueue(beginWord);

        // Initialize a HashSet to keep track of visited words
        HashSet<string> visited =
        [
            beginWord
        ];

        // Initialize a variable to keep track of transformation steps
        int steps = 1;

        // Begin BFS
        while (queue.Count > 0)
        {
            int size = queue.Count;

            // Iterate through all words at the current level
            for (int i = 0; i < size; i++)
            {
                var currentWord = queue.Dequeue();

                // Generate all possible transformations of the current word
                for (int j = 0; j < currentWord.Length; j++)
                {
                    var charArray = currentWord.ToCharArray();

                    // Try replacing each character with all lowercase alphabets
                    for (char c = 'a'; c <= 'z'; c++)
                    {
                        charArray[j] = c;
                        var transformedWord = new string(charArray);

                        // If transformedWord is endWord, return steps + 1
                        if (transformedWord == endWord)
                        {
                            return steps + 1;
                        }

                        // If transformedWord is in wordSet and not visited, enqueue it and mark as visited
                        if (wordSet.Contains(transformedWord) && !visited.Contains(transformedWord))
                        {
                            queue.Enqueue(transformedWord);
                            visited.Add(transformedWord);
                        }
                    }
                }
            }

            // Increment steps after processing all words at the current level
            steps++;
        }

        // If no transformation sequence exists, return 0
        return 0;
    }

    /// <summary>
    /// Problem 128
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int LongestConsecutive(int[] nums)
    {
        // For lengths of 0 and 1, the length is the same as the number of consecutive numbers
        if (nums.Length < 2)
        {
            return nums.Length;
        }

        // Initialize a set of all the numbers in nums
        HashSet<int> set = new(nums);

        // Keep track of the current longest consecutive sequence
        var longestConsecutive = 0;

        // Go through the elements in the set
        foreach (int number in set)
        {
            // Find possible start of the sequence
            if (!set.Contains(number - 1))
            {
                // Keep track of the length of the current sequence
                int length = 0;

                // As long as the set contains the next consequtive number in the sequence we increment the length
                // and update the longest consecutive if the length of the current sequence is longer
                while (set.Contains(number + length))
                {
                    length++;
                    longestConsecutive = Math.Max(longestConsecutive, length);
                }
            }
        }

        return longestConsecutive;
    }

    /// <summary>
    /// Problem 129
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public static int SumNumbers(TreeNode root)
    {
        // Handle case where root is null
        if (root == null)
        {
            return 0;
        }

        int totalSum = 0;

        // Keep a stack of tuples (node, currentSum) to keep track of running sums at a certain node
        Stack<(TreeNode node, int currentSum)> stack = new();
        // Start with the root node and sum = 0
        stack.Push((root, 0));

        while (stack.Count > 0)
        {
            // Get the next tuple in the stack
            var (node, currentSum) = stack.Pop();
            // Add the node value to the running sum
            currentSum = currentSum * 10 + node.val;

            // If it's a leaf node, add the running sum to the total sum.
            if (node.left == null && node.right == null)
            {
                totalSum += currentSum;
            }
            // If it's not a leaf node, add tuples of the right and left node (if they exist) and the running sum to the stack.
            else
            {
                if (node.left != null)
                {
                    stack.Push((node.left, currentSum));
                }
                if (node.right != null)
                {
                    stack.Push((node.right, currentSum));
                }
            }
        }

        // Return the total sum
        return totalSum;
    }

    /// <summary>
    /// Problem 130
    /// </summary>
    /// <param name="board"></param>
    public static void Solve(char[][] board)
    {
        if (board == null || board.Length == 0)
        {
            return;
        }

        int m = board.Length;
        int n = board[0].Length;

        // Mark 'O's on the border and their connected regions
        for (int i = 0; i < m; i++)
        {
            // Left border
            DFS(i, 0);
            // Right border
            DFS(i, n - 1);
        }

        for (int j = 0; j < n; j++)
        {
            // Top border
            DFS(0, j);
            // Bottom border
            DFS(m - 1, j);
        }

        // Flip 'O's that are not surrounded by 'X'
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (board[i][j] == 'O')
                {
                    board[i][j] = 'X';
                }
                // Restore marked 'T's back to 'O's
                else if (board[i][j] == 'T')
                {
                    board[i][j] = 'O';
                }
            }
        }

        // Depth first search to mark all connected 'O's
        void DFS(int x, int y)
        {
            // Break if current cell is out of cell, at a border, or not a 'O'
            if (x < 0 || x >= m || y < 0 || y >= n || board[x][y] == 'X' || board[x][y] == 'T')
            {
                return;
            }

            // Mark 'O' as temporary 'T'
            board[x][y] = 'T';

            // Define 4-directional neighbors
            int[][] directions = [[1, 0], [-1, 0], [0, 1], [0, -1]];

            // Explore neighbors
            foreach (int[] dir in directions)
            {
                DFS(x + dir[0], y + dir[1]);
            }
        }
    }
}
