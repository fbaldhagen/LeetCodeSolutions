using LeetCodeSolutions.Structures;

namespace LeetCodeSolutions._0101_0200;

public class Problems_0131_0140
{
    /// <summary>
    /// Problem 131
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static IList<IList<string>> Partition(string s)
    {
        // Standard setup for a backtracking approach
        IList<IList<string>> result = new List<IList<string>>();
        IList<string> currentList = new List<string>();
        Backtrack(result, currentList, s, 0);
        return result;

        // Solve using a backtracking algo
        static void Backtrack(IList<IList<string>> result, IList<string> currentList, string s, int start)
        {
            if (start == s.Length)
            {
                result.Add(new List<string>(currentList));
                return;
            }

            for (int end = start; end < s.Length; end++)
            {
                if (IsPalindrome(s, start, end))
                {
                    currentList.Add(s.Substring(start, end - start + 1));
                    Backtrack(result, currentList, s, end + 1);
                    currentList.RemoveAt(currentList.Count - 1);
                }
            }
        }

        // Checks if the partition is a valid palindrome
        static bool IsPalindrome(string s, int low, int high)
        {
            while (low < high)
            {
                if (s[low] != s[high])
                {
                    return false;
                }
                low++;
                high--;
            }

            return true;
        }
    }

    /// <summary>
    /// Problem 132
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static int MinCut(string s)
    {
        int n = s.Length;
        bool[,] isPalindrome = new bool[n, n];
        int[] cuts = new int[n];

        // Dynamic programming approach:
        // We iterate through the string and for each position i, we check all substrings ending at i.
        // We update the isPalindrome matrix to mark whether the substring from j to i is a palindrome.
        // We also update the cuts array to store the minimum cuts needed for palindrome partitioning up to index i.
        for (int i = 0; i < n; i++)
        {
            // Worst case: need i cuts for i characters
            cuts[i] = i;
            for (int j = 0; j <= i; j++)
            {
                // If the characters at positions j and i are equal and the substring between j and i is a palindrome
                // (either the length is 1 or 2, or it's a palindrome already), we mark the substring from j to i as a palindrome.
                // Then, we update the cuts array at position i to be the minimum of its current value and (cuts[j-1] + 1)
                // where cuts[j-1] represents the minimum cuts needed up to the position before j.
                if (s[j] == s[i] && (i - j <= 1 || isPalindrome[j + 1, i - 1]))
                {
                    isPalindrome[j, i] = true;
                    cuts[i] = (j == 0) ? 0 : Math.Min(cuts[i], cuts[j - 1] + 1);
                }
            }
        }

        return cuts[n - 1];
    }

    /// <summary>
    /// Problem 133
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    /// <remarks>
    /// Note that this problem uses a different definition for Node than many other problems. <br></br>
    /// Instead of the class name used on LC, which is the same as the other type of node (Node), 
    /// I'm using "GraphNode" to differentiate between the two.
    /// </remarks>
    public static GraphNode CloneGraph(GraphNode node)
    {
        // Use a dictionary to keep track of visited nodes, and their corresponding clones
        Dictionary<GraphNode, GraphNode> visited = new();
        return Clone(node);

        // Recursive helper method to clone the graph
        GraphNode Clone(GraphNode node)
        {
            // Base case: If the node is null, return null
            if (node is null)
            {
                return null;
            }

            // If the node has already been visited, return its clone from the dictionary
            if (visited.TryGetValue(node, out GraphNode value))
            {
                return value;
            }

            // Create a clone of the current node
            GraphNode cloneNode = new()
            {
                val = node.val,
                neighbors = new List<GraphNode>()
            };

            // Mark the original node as visited and add it to the dictionary with its clone
            visited.Add(node, cloneNode);

            // Recursively clone all neighbors of the current node
            foreach (GraphNode neighbor in node.neighbors)
            {
                cloneNode.neighbors.Add(Clone(neighbor));
            }

            // Return the cloned node
            return cloneNode;
        }
    }

    /// <summary>
    /// Problem 134
    /// </summary>
    /// <param name="gas"></param>
    /// <param name="cost"></param>
    /// <returns></returns>
    public static int CanCompleteCircuit(int[] gas, int[] cost)
    {
        int n = gas.Length;  // == cost.Length

        int startIndex = 0;
        int totalGas = 0;
        int totalCost = 0;
        int currentGas = 0;

        for (int i = 0; i < n; i++)
        {
            totalGas += gas[i];
            totalCost += cost[i];

            currentGas += gas[i] - cost[i];

            // If currentGas < 0, we cannot reach the next station
            // Start from the next station
            if (currentGas < 0)
            {
                startIndex = i + 1;
                currentGas = 0;
            }
        }

        // If totalGas is less than totalCost, it's impossible to complete the circuit
        if (totalGas < totalCost)
        {
            return -1;
        }

        return startIndex;
    }

    /// <summary>
    /// Problem 135
    /// </summary>
    /// <param name="ratings"></param>
    /// <returns></returns>
    public static int Candy(int[] ratings)
    {
        int n = ratings.Length;
        int[] candies = new int[n];

        // Every kid gets atleast one candy
        for (int i = 0; i < n; i++)
        {
            candies[i] = 1;
        }

        // If a kid has a better rating than the previous kid,
        // it gets one candy more than the previous
        for (int i = 1; i < n; i++)
        {
            if (ratings[i] > ratings[i - 1])
            {
                candies[i] = candies[i - 1] + 1;
            }
        }

        // Check from the other direction as well
        for (int i = n - 2; i >= 0; i--)
        {
            if (ratings[i] > ratings[i + 1])
            {
                // Pick the largest value to not mess up the other direction
                candies[i] = Math.Max(candies[i], candies[i + 1] + 1);
            }
        }

        // Count the total number of candies
        int totalCandies = 0;
        foreach (int candy in candies)
        {
            totalCandies += candy;
        }

        return totalCandies;
    }

    /// <summary>
    /// Problem 136
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int SingleNumber(int[] nums)
    {
        int result = 0;

        // Using bitwise XOR operation, duplicate numbers will cancel eachother out. So looping
        // over the array, and bitwise XOR-ing every element, only the unique element will remain
        foreach (int num in nums)
        {
            result ^= num;
        }

        return result;
    }

    /// <summary>
    /// Problem 137
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int SingleNumberII(int[] nums)
    {
        // Another bitmanipulation approach
        // Think of these as bit arrays that keep track of the number of times a particular bit appears.
        int ones = 0;
        int twos = 0;

        // We iterate through each number in the array.
        foreach (int num in nums)
        {
            // 1. (ones ^ num) performs an XOR operation between the current
            // value of ones and the current number (num).
            // 2. ~twos negates the bits in twos, meaning we clear any bits that are set in twos.
            // The bitwise AND (&) operation ensures that we only keep the bits
            // that are set in ones but not in twos.
            ones = (ones ^ num) & ~twos;
            // Same as above, but for twos
            twos = (twos ^ num) & ~ones;
        }

        // After looping through all numbers, the value of ones will contain the single number
        // that appears once, as all other numbers have been cancelled out by appearing three times.
        return ones;
    }

    /// <summary>
    /// Problem 138
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public static NodeWithRandom CopyRandomList(NodeWithRandom head)
    {
        // Case where head is null..
        if (head == null)
        {
            return null;
        }

        // Step 1: Insert copied nodes next to original nodes
        NodeWithRandom curr = head;
        while (curr != null)
        {
            NodeWithRandom copy = new(curr.val)
            {
                next = curr.next
            };
            curr.next = copy;
            curr = copy.next;
        }

        // Step 2: Set random pointers for copied nodes
        curr = head;
        while (curr != null)
        {
            if (curr.random != null)
            {
                curr.next.random = curr.random.next;
            }
            curr = curr.next.next;
        }

        // Step 3: Separate original and copied linked lists
        curr = head;
        NodeWithRandom newHead = head.next;
        NodeWithRandom newCurr = newHead;
        while (curr != null)
        {
            curr.next = newCurr.next;
            curr = curr.next;
            if (curr != null)
            {
                newCurr.next = curr.next;
                newCurr = newCurr.next;
            }
        }

        return newHead;
    }

    /// <summary>
    /// Problem 139
    /// </summary>
    /// <param name="s"></param>
    /// <param name="wordDict"></param>
    /// <returns></returns>
    public static bool WordBreak(string s, IList<string> wordDict)
    {
        int n = s.Length;

        // Dynamic programming approach.
        // dp[i] represents if s[0:i] can be segmented into words in the dict
        bool[] dp = new bool[n + 1];
        // Base case, empty string can be segmented
        dp[0] = true;

        // Create a hashset from the provided word list
        HashSet<string> wordSet = new(wordDict);

        // Iterate from i to n, check if substring s[j:i] (0 < j < i - 1) exist in the set
        // and if dp[j] is true, - > dp[i] = true
        for (int i = 1; i <= n; i++)
        {
            for (int j = 0; j < i; j++)
            {
                if (dp[j] && wordSet.Contains(s[j..i]))
                {
                    dp[i] = true;
                    break;
                }
            }
        }

        return dp[n];
    }

    /// <summary>
    /// Problem 140
    /// </summary>
    /// <param name="s"></param>
    /// <param name="wordDict"></param>
    /// <returns></returns>
    public static IList<string> WordBreakII(string s, IList<string> wordDict)
    {
        // Create a HashSet for faster lookup
        HashSet<string> wordSet = new(wordDict);
        // List to store the result sentences
        List<string> result = new();
        // Call the recursive helper function to generate sentences
        GenerateSentences(s, wordSet, 0, "", result);
        // Return the result list
        return result;

        /// Recursive helper function to generate sentences
        static void GenerateSentences(string s, HashSet<string> wordSet, int start, string partial, List<string> result)
        {
            // Base case: if start reaches the end of the string, add the partial sentence to the result list
            if (start == s.Length)
            {
                // Trim to remove the trailing space
                result.Add(partial.Trim());
                return;
            }

            // Loop through each index starting from 'start'
            for (int i = start; i < s.Length; i++)
            {
                // Extract the substring from 'start' to 'i'
                string word = s.Substring(start, i - start + 1);
                // If the extracted substring exists in the wordSet, recursively call the function with updated parameters
                if (wordSet.Contains(word))
                {
                    // Append the valid word to the partial sentence and continue recursion
                    GenerateSentences(s, wordSet, i + 1, partial + word + " ", result);
                }
            }
        }
    }
}
