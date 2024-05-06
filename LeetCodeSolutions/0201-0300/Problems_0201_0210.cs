using LeetCodeSolutions.Structures;
namespace LeetCodeSolutions._0201_0300;

public class Problems_0201_0210
{
    /// <summary>
    /// Problem 201
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static int RangeBitwiseAnd(int left, int right)
    {
        // Initialize a variable to keep track of the number of shifts needed
        int shift = 0;

        // Iterate until left and right become equal
        while (left < right)
        {
            // Right-shift both left and right by 1 bit
            // This effectively divides them by 2, discarding any remainder
            left >>= 1;
            right >>= 1;

            // Increment the shift count
            shift++;
        }

        // Once left and right are equal, left will contain the common prefix
        // Left-shift left by the number of shifts to restore the common prefix
        return left << shift;
    }

    /// <summary>
    /// Problem 202
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static bool IsHappy(int n)
    {
        // HashSet to keep track of sums seen during the process
        HashSet<int> seen = new();

        // Repeat the process until we reach 1 (happy number) or enter a cycle (not happy)
        while (n != 1)
        {
            int squareSum = 0;

            // Calculate the sum of squares of digits
            while (n > 0)
            {
                int digit = n % 10; // Extract the last digit
                squareSum += digit * digit; // Add the square of the digit to the sum
                n /= 10; // Move to the next digit
            }

            // Check if we've seen this sum before
            if (seen.Contains(squareSum))
            {
                return false; // If we've seen the sum before, we're in a cycle
            }

            seen.Add(squareSum); // Add the sum to the set of seen sums
            n = squareSum; // Set the number to the calculated sum for the next iteration
        }

        return true; // If we reach 1, the number is happy
    }

    /// <summary>
    /// Problem 203
    /// </summary>
    /// <param name="head"></param>
    /// <param name="val"></param>
    /// <returns></returns>
    public static ListNode? RemoveElements(ListNode? head, int val)
    {
        // Skip all leading nodes where node.val == val
        while (head is not null && head.val == val)
        {
            head = head.next;
        }

        // Use two pointers to traverse the list
        ListNode prev = new();
        ListNode? curr = head;

        while (curr is not null)
        {
            // Remove the nodes where node.val == val
            if (curr.val == val)
            {
                prev.next = curr.next;
            }
            else
            {
                prev = curr;
            }
            // Go to next node
            curr = curr.next;
        }

        return head;
    }

    /// <summary>
    /// Problem 204
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int CountPrimes(int n)
    {
        // 2 is the first prime, and we want the number of primes less than n
        if (n <= 2)
        {
            return 0;
        }

        // Assume all odd numbers are prime initially, exclude even numbers.
        int count = n / 2;

        // Initialize a boolean array to track whether each number is prime
        bool[] isPrime = new bool[n];
        Array.Fill(isPrime, true);
        isPrime[0] = false; // 0 is not prime
        isPrime[1] = false; // 1 is not prime

        // Iterate through odd numbers starting from 3 up to the square root of n
        for (int i = 3; i * i < n; i += 2)
        {
            if (isPrime[i])
            {
                // Mark multiples of i as not prime, start marking from i*i, in steps of 2*i to skip evens.
                for (int j = i * i; j < n; j += 2 * i)
                {
                    if (isPrime[j])
                    {
                        // Reduce count for each non-prime found
                        count--;
                        // Mark as not prime
                        isPrime[j] = false;
                    }
                }
            }
        }

        // Return the count of primes less than n
        return count;
    }

    /// <summary>
    /// Problem 205
    /// </summary>
    /// <param name="s"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static bool IsIsomorphic(string s, string t)
    {
        // If the strings are are not the same length, they can't be isomorphic
        if (s.Length != t.Length)
        {
            return false;
        }

        // Keep two dictionaries with mappings from s->t and t->s
        Dictionary<char, char> sToT = new();
        Dictionary<char, char> tToS = new();

        // Loop over the letters in both strings
        for (int i = 0; i < s.Length; i++)
        {
            // Get the characters on index i from both strings
            char sChar = s[i];
            char tChar = t[i];

            // Assume theres no existing mappings
            bool existingMappings = false;

            // Check if there is an existing mapping from sChar already
            if (sToT.TryGetValue(sChar, out char tValue))
            {
                existingMappings = true;
                // If the mapping exists, make sure it's matching the correct letter
                if (tValue != tChar)
                {
                    // Doesn't match -> not isomorphic
                    return false;
                }
            }
            // Check if there's an existing mapping from tChar already
            if (tToS.TryGetValue(tChar, out char sValue))
            {
                existingMappings = true;
                // If the mapping exists, make sure it's matching the correct letter
                if (sValue != sChar)
                {
                    // Doesn't match -> not isomorphic
                    return false;
                }
            }

            // No existing mappings, so we create them
            if (!existingMappings)
            {
                sToT[sChar] = tChar;
                tToS[tChar] = sChar;
            }
        }

        return true;
    }

    /// <summary>
    /// Problem 206
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public static ListNode? ReverseList(ListNode? head)
    {
        // Can't reverse an empty list, or a list with one node
        if (head is null || head.next is null)
        {
            return head;
        }

        // Two pointers
        ListNode? prev = null;
        ListNode? curr = head;

        // Traverse the entire list
        while (curr != null)
        {
            // "Remember" the next node 
            ListNode? next = curr.next;
            // Reverse the next pointer
            curr.next = prev;

            // Move on, by updating the current node to prev, and the next node in line to curr
            prev = curr;
            curr = next;
        }

        // Return the tail node
        return prev;
    }

    /// <summary>
    /// Problem 207
    /// </summary>
    /// <param name="numCourses"></param>
    /// <param name="prerequisites"></param>
    /// <returns></returns>
    public static bool CanFinish(int numCourses, int[][] prerequisites)
    {
        List<int>[] graph = new List<int>[numCourses];

        for (int i = 0; i < numCourses; i++)
        {
            graph[i] = new List<int>();
        }

        int[] indegrees = new int[numCourses];

        foreach (int[] prerequisite in prerequisites)
        {
            int course = prerequisite[0];
            int prerequisiteCourse = prerequisite[1];

            graph[prerequisiteCourse].Add(course);
            indegrees[course]++;
        }

        Queue<int> queue = new();

        for (int i = 0; i < numCourses; i++)
        {
            if (indegrees[i] == 0)
            {
                queue.Enqueue(i);
            }
        }

        int count = 0;
        while (queue.Count > 0)
        {
            int course = queue.Dequeue();
            count++;

            foreach (int nextCourse in graph[course])
            {
                indegrees[nextCourse]--;
                if (indegrees[nextCourse] == 0)
                {
                    queue.Enqueue(nextCourse);
                }
            }
        }

        // If count matches the total number of courses, it means all courses can be finished
        return count == numCourses;
    }

    /// Problem 208 in separate file <see cref="Trie"/>

    /// <summary>
    /// Problem 209
    /// </summary>
    /// <param name="target"></param>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int MinSubArrayLen(int target, int[] nums)
    {
        // Initial variable values for sliding window approach
        int minLength = int.MaxValue;
        int runningSum = 0;

        // Start 'sliding' the window
        for (int right = 0, left = 0; right < nums.Length; right++)
        {
            runningSum += nums[right];

            // If we satisfy the condition (windows sum >= target) we update the minLength and
            // move the left edge of the window one to the right
            while (runningSum >= target)
            {
                minLength = Math.Min(minLength, right - left + 1);
                runningSum -= nums[left];
                left++;
            }
        }

        // If minLength wasnt changed (still int.MaxValue) we return 0, else the minimum length
        return minLength != int.MaxValue ? minLength : 0;
    }

    /// <summary>
    /// Problem 210
    /// </summary>
    /// <param name="numCourses"></param>
    /// <param name="prerequisites"></param>
    /// <returns></returns>
    public static int[] FindOrder(int numCourses, int[][] prerequisites)
    {
        // Initialize an array to store the number of prerequisites for each course
        int[] degree = new int[numCourses];

        // Create a lookup to map each prerequisite's parent course to its children courses
        var parentToChildren = prerequisites.ToLookup(p => p[1], c =>
        {
            degree[c[0]]++;
            return c[0];
        });

        // Initialize a list to store the order in which courses can be taken
        List<int> bfs = new(numCourses);

        // Populate the BFS list with courses that have no prerequisites
        for (int i = 0; i < numCourses; ++i)
        {
            if (degree[i] == 0) bfs.Add(i);
        }

        // Perform BFS traversal
        for (int i = 0; i < bfs.Count; ++i)
        {
            // For each course in the BFS list
            foreach (int j in parentToChildren[bfs[i]])
            {
                // Decrement the degree of each child course
                if (--degree[j] == 0)
                {
                    // If all prerequisites of a child are satisfied, add it to the BFS list
                    bfs.Add(j);
                }
            }
        }

        // Check if all courses can be taken
        return bfs.Count == numCourses ? bfs.ToArray() : Array.Empty<int>();
    }
}

/// <summary>
/// A trie (pronounced as "try") or prefix tree is a tree data structure used to efficiently store and retrieve keys in a dataset of strings.
/// There are various applications of this data structure, such as autocomplete and spellchecker. <br><br></br></br>
/// Problem 208
/// </summary>
public class Trie
{
    private readonly Dictionary<char, Trie> children;
    private bool isEndOfWord;

    /// <summary>
    /// Initializes a new instance of the Trie class.
    /// </summary>
    public Trie()
    {
        children = new Dictionary<char, Trie>();
        isEndOfWord = false;
    }

    /// <summary>
    /// Inserts a word into the trie.
    /// </summary>
    /// <param name="word">The word to be inserted.</param>
    public void Insert(string word)
    {
        Trie current = this;
        foreach (char c in word)
        {
            if (!current.children.TryGetValue(c, out Trie? value))
            {
                value = new Trie();
                current.children[c] = value;
            }
            current = value;
        }
        current.isEndOfWord = true;
    }

    /// <summary>
    /// Searches for a word in the trie.
    /// </summary>
    /// <param name="word">The word to search for.</param>
    /// <returns>True if the word is found in the trie; otherwise, false.</returns>
    public bool Search(string word)
    {
        Trie node = SearchPrefix(word);
        return node != null && node.isEndOfWord;
    }

    /// <summary>
    /// Determines whether there is a previously inserted string word that has the given prefix.
    /// </summary>
    /// <param name="prefix">The prefix to search for.</param>
    /// <returns>True if there is a word with the given prefix; otherwise, false.</returns>
    public bool StartsWith(string prefix)
    {
        return SearchPrefix(prefix) != null;
    }

    private Trie SearchPrefix(string prefix)
    {
        Trie current = this;
        foreach (char c in prefix)
        {
            if (current.children.TryGetValue(c, out Trie? value))
            {
                current = value;
            }
            else
            {
                return null;
            }
        }
        return current;
    }
}