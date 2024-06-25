namespace LeetCodeSolutions._0301_0400;
using LeetCodeSolutions.Structures.Trie;

public class Problems_0331_0340
{
    /// <summary>
    /// Problem 331
    /// </summary>
    /// <param name="preorder"></param>
    /// <returns></returns>
    public static bool IsValidSerialization(string preorder)
    {
        // Split the string on ','
        string[] nodes = preorder.Split(',');

        // Each node (including null-nodes) takes up a slot. A non-null node creates 2 new.
        // Can never go below 0 slots.
        // Start off with one initial slot for the root node.
        int slots = 1;

        foreach (string node in nodes)
        {
            slots--;

            if (slots < 0)
            {
                return false;
            }

            if (!node.Equals("#"))
            {
                slots += 2;
            }
        }

        return slots == 0;
    }

    /// <summary>
    /// Problem 332
    /// </summary>
    /// <param name="tickets"></param>
    /// <returns></returns>
    public static IList<string> FindItinerary(IList<IList<string>> tickets)
    {
        // Build the graph using a dictionary of lists
        var graph = new Dictionary<string, List<string>>();
        foreach (var ticket in tickets)
        {
            if (!graph.TryGetValue(ticket[0], out List<string>? value))
            {
                value = ([]);
                graph[ticket[0]] = value;
            }

            value.Add(ticket[1]);
        }

        // Sort the adjacency lists to ensure lexical order
        foreach (var list in graph.Values)
        {
            list.Sort();
        }

        List<string> result = [];
        Stack<string> stack = new();
        stack.Push("JFK"); // Start DFS from JFK

        while (stack.Count > 0)
        {
            while (graph.ContainsKey(stack.Peek()) && graph[stack.Peek()].Count > 0)
            {
                var next = graph[stack.Peek()].First(); // Get smallest lexical airport
                graph[stack.Peek()].Remove(next); // Remove the used ticket
                stack.Push(next); // Push the next airport onto the stack
            }
            result.Insert(0, stack.Pop()); // Insert into result list in reverse order
        }

        return result;
    }

    /// <summary>
    /// Problem 334
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static bool IncreasingTriplet(int[] nums)
    {
        int first = int.MaxValue;
        int second = int.MaxValue;

        foreach (int num in nums)
        {
            if (num <= first)
            {
                // Update first if num is smaller than first
                first = num; 
            }
            else if (num <= second)
            {
                // Update second if num is smaller than second but greater than first
                second = num; 
            }
            else
            {
                // If we find a number greater than both first and second, we found our triplet
                return true;
            }
        }

        // If no increasing triplet is found after traversing entire array.
        return false; 
    }

    /// <summary>
    /// Problem 335
    /// </summary>
    /// <param name="distance"></param>
    /// <returns></returns>
    public static bool IsSelfCrossing(int[] distance)
    {
        int n = distance.Length;

        for (int i = 3; i < n; i++)
        {
            // Fourth line crosses the first line and onward for similar configurations
            if (distance[i] >= distance[i - 2] && distance[i - 1] <= distance[i - 3])
            {
                return true;
            }

            // Fifth line meets first line and onward for similar configurations
            if (i >= 4 && distance[i - 1] == distance[i - 3] && distance[i] + distance[i - 4] >= distance[i - 2])
            {
                return true;
            }

            // Sixth line crosses first line and onward for similar configurations
            if (i >= 5 && distance[i - 2] >= distance[i - 4] && distance[i] >= distance[i - 2] - distance[i - 4] &&
                distance[i - 1] <= distance[i - 3] && distance[i - 1] >= distance[i - 3] - distance[i - 5])
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Problem 336.
    /// </summary>
    /// <param name="words"></param>
    /// <returns></returns>
    public static IList<IList<int>> PalindromePairs(string[] words)
    {
        // This problem uses the Trie structure defined in the folder Structures, and the namespace LeetCodeSolutions.Structures.Trie
        IList<IList<int>> result = [];
        Trie trie = new();

        // Insert all words into the trie
        for (int i = 0; i < words.Length; i++)
        {
            trie.Insert(words[i], i);
        }

        // Search for palindrome pairs
        for (int i = 0; i < words.Length; i++)
        {
            Search(words, i, trie, result);
        }

        return result;

        void Search(string[] words, int index, Trie trie, IList<IList<int>> result)
        {
            TrieNode current = trie.root;
            string word = words[index];

            for (int j = 0; j < word.Length; j++)
            {
                // If remaining suffix is palindrome and there is a word in Trie
                // that matches the prefix, we found a pair
                if (current.IsEndOfWord && current.WordIndex != index && IsPalindrome(word, j, word.Length - 1))
                {
                    result.Add([index, current.WordIndex]);
                }

                if (!current.Children.TryGetValue(word[j], out current))
                {
                    return;
                }
            }

            // Check the remaining words in the Trie for palindromic combinations
            foreach (int palindromeSuffixIndex in current.PalindromeSuffixes)
            {
                if (index == palindromeSuffixIndex) continue;
                result.Add([index, palindromeSuffixIndex]);
            }
        }

        bool IsPalindrome(string word, int left, int right)
        {
            while (left < right)
            {
                if (word[left++] != word[right--])
                {
                    return false;
                }
            }
            return true;
        }
    }
}