using LeetCodeSolutions.Structures;
namespace LeetCodeSolutions._0201_0300;

public class Problems_0211_0220
{
    // Problem 211 in the separate class WordDictionary

    /// <summary>
    /// Problem 212
    /// </summary>
    /// <param name="board"></param>
    /// <param name="words"></param>
    /// <returns></returns>
    public static IList<string> FindWords(char[][] board, string[] words)
    {
        List<string> result = new();
        TrieNode root = BuildTrie(words);

        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[0].Length; j++)
            {
                DFS(board, i, j, root, result);
            }
        }

        return result;

        static void DFS(char[][] board, int i, int j, TrieNode node, List<string> result)
        {
            char c = board[i][j];
            if (c == '#' || node.children[c - 'a'] == null)
            {
                return;
            }

            node = node.children[c - 'a'];
            if (node.word != null)
            {
                result.Add(node.word);
                node.word = null; // Avoid duplicates
            }

            board[i][j] = '#'; // Mark visited

            if (i > 0) DFS(board, i - 1, j, node, result);
            if (j > 0) DFS(board, i, j - 1, node, result);
            if (i < board.Length - 1) DFS(board, i + 1, j, node, result);
            if (j < board[0].Length - 1) DFS(board, i, j + 1, node, result);

            board[i][j] = c; // Restore board
        }

        static TrieNode BuildTrie(string[] words)
        {
            TrieNode root = new();
            foreach (string word in words)
            {
                TrieNode node = root;
                foreach (char c in word)
                {
                    int index = c - 'a';
                    if (node.children[index] == null)
                    {
                        node.children[index] = new TrieNode();
                    }
                    node = node.children[index];
                }
                node.word = word;
            }
            return root;
        }
    }

    /// <summary>
    /// Problem 213
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int Rob(int[] nums)
    {
        int n = nums.Length;

        switch (n)
        {
            case 0:
                return 0;
            case 1:
                return nums[0];
            default:
                break;
        }

        return Math.Max(RobHelper(nums, 0, n - 2), RobHelper(nums, 1, n - 1));


        static int RobHelper(int[] nums, int start, int end)
        {
            int prevMax = 0;
            int currMax = 0;

            for (int i = start; i <= end; i++)
            {
                int temp = currMax;
                currMax = Math.Max(currMax, prevMax + nums[i]);
                prevMax = temp;
            }

            return currMax;
        }
    }

    /// <summary>
    /// Problem 214
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string ShortestPalindrome(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }

        int n = s.Length;
        string longestPrefix = string.Empty;

        for (int i = n; i > 0; i--)
        {
            if (IsPalindrome(s[..i]))
            {
                longestPrefix = s[..i];
                break; // Found the longest palindrome prefix, exit the loop
            }
        }

        return Helpers.ReverseString(s[longestPrefix.Length..]) + s;

        static bool IsPalindrome(string str)
        {
            int left = 0;
            int right = str.Length - 1;

            while (left < right)
            {
                if (str[left] != str[right])
                {
                    return false;
                }

                left++;
                right--;
            }

            return true;
        }
    }

    /// <summary>
    /// Problem 215
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static int FindKthLargest(int[] nums, int k)
    {
        // Create a min-heap
        PriorityQueue<int> minHeap = new();

        foreach (int num in nums)
        {
            if (minHeap.Count < k)
            {
                // Add num to the min-heap if it's not full yet
                minHeap.Enqueue(num);
            }
            else if (num > minHeap.Peek())
            {
                // Replace the smallest element if num is larger
                minHeap.Dequeue();
                minHeap.Enqueue(num);
            }
        }

        // The root of the min-heap is the kth largest element
        return minHeap.Peek();
    }

    /// <summary>
    /// Problem 216
    /// </summary>
    /// <param name="k"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public static IList<IList<int>> CombinationSum3(int k, int n)
    {
        IList<IList<int>> result = new List<IList<int>>();
        if (k <= 0 || n <= 0 || k > 9 || n > 45 || n < k * (k + 1) / 2)
        {
            return result; // No valid combinations possible
        }

        IList<int> combination = new List<int>();
        Backtrack(result, combination, k, n, 1);
        return result;

        static void Backtrack(IList<IList<int>> result, IList<int> combo, int k, int remain, int start)
        {
            if (remain < 0 || combo.Count > k)
            {
                return;
            }

            if (remain == 0 && combo.Count == k)
            {
                result.Add(new List<int>(combo));
            }
            else
            {
                for (int i = start; i <= 9; i++)
                {
                    combo.Add(i);
                    Backtrack(result, combo, k, remain - i, i + 1);
                    combo.Remove(i);
                }
            }
        }
    }

    /// <summary>
    /// Problem 217
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static bool ContainsDuplicate(int[] nums)
    {
        HashSet<int> set = new();

        foreach (int num in nums)
        {
            if (!set.Add(num))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Problem 218
    /// </summary>
    /// <param name="buildings"></param>
    /// <returns></returns>
    public static IList<IList<int>> GetSkyline(int[][] buildings)
    {
        return MergeSort(buildings, 0, buildings.Length - 1);

        IList<IList<int>> MergeSort(int[][] buildings, int left, int right)
        {
            if (left == right)
            {
                IList<IList<int>> result = new List<IList<int>>
            {
                new List<int> { buildings[left][0], buildings[left][2] },
                new List<int> { buildings[left][1], 0 }
            };
                return result;
            }

            int mid = left + (right - left) / 2;
            IList<IList<int>> leftSkyline = MergeSort(buildings, left, mid);
            IList<IList<int>> rightSkyline = MergeSort(buildings, mid + 1, right);

            return Merge(leftSkyline, rightSkyline);
        }

        IList<IList<int>> Merge(IList<IList<int>> left, IList<IList<int>> right)
        {
            IList<IList<int>> result = [];
            int h1 = 0, h2 = 0;
            int i = 0, j = 0;

            while (i < left.Count && j < right.Count)
            {
                int x;
                if (left[i][0] < right[j][0])
                {
                    x = left[i][0];
                    h1 = left[i][1];
                    i++;
                }
                else if (left[i][0] > right[j][0])
                {
                    x = right[j][0];
                    h2 = right[j][1];
                    j++;
                }
                else
                {
                    x = left[i][0];
                    h1 = left[i][1];
                    h2 = right[j][1];
                    i++;
                    j++;
                }
                int maxHeight = Math.Max(h1, h2);
                if (result.Count == 0 || maxHeight != result[result.Count - 1][1])
                {
                    result.Add(new List<int> { x, maxHeight });
                }
            }

            while (i < left.Count)
            {
                result.Add(left[i]);
                i++;
            }

            while (j < right.Count)
            {
                result.Add(right[j]);
                j++;
            }

            return result;
        }
    }

    /// <summary>
    /// Problem 219
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static bool ContainsNearbyDuplicate(int[] nums, int k)
    {
        Dictionary<int, int> seen = new();
        for (int i = 0; i < nums.Length; i++)
        {
            if (seen.TryGetValue(nums[i], out int value))
            {
                int lastIndex = value;
                if (Math.Abs(i - lastIndex) <= k)
                {
                    return true;
                }
            }

            seen[nums[i]] = i;
        }

        return false;
    }

    /// <summary>
    /// Problem 220
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="indexDiff"></param>
    /// <param name="valueDiff"></param>
    /// <returns></returns>
    public static bool ContainsNearbyAlmostDuplicate(int[] nums, int indexDiff, int valueDiff)
    {
        // Create a sorted set to store elements within the sliding window
        SortedSet<long> set = new();

        // Iterate through the array
        for (int i = 0; i < nums.Length; i++)
        {
            // If the window size exceeds indexDiff, remove the oldest element from the set
            if (i > indexDiff)
            {
                set.Remove(nums[i - indexDiff - 1]);
            }

            // Define the range within which potential duplicates may exist
            long left = (long)nums[i] - valueDiff;
            long right = (long)nums[i] + valueDiff;

            // Get the subset of elements in the set that fall within the defined range
            var view = set.GetViewBetween(left, right);

            // If the subset is not empty, there exists a pair of elements that satisfy the conditions
            if (view.Count > 0)
            {
                return true;
            }

            // Add the current element to the set
            set.Add(nums[i]);
        }

        // If no pair of elements satisfying the conditions is found, return false
        return false;
    }
}

/// <summary>
/// Problem 211
/// </summary>
public class WordDictionary
{
    private readonly List<string> words;

    public WordDictionary()
    {
        words = new List<string>();
    }

    public void AddWord(string word)
    {
        words.Add(word);
    }

    public bool Search(string word)
    {
        foreach (string w in words)
        {
            if (IsMatch(w, word))
            {
                return true;
            }
        }
        return false;
    }

    private static bool IsMatch(string word, string pattern)
    {
        if (word.Length != pattern.Length)
        {
            return false;
        }

        for (int i = 0; i < word.Length; i++)
        {
            if (pattern[i] != '.' && pattern[i] != word[i])
            {
                return false;
            }
        }
        return true;
    }
}