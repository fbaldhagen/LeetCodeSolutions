using LeetCodeSolutions.Structures;
namespace LeetCodeSolutions._0001_0100;

public class Problems_0021_0030
{
    /// <summary>
    /// Problem 21
    /// </summary>
    /// <param name="list1"></param>
    /// <param name="list2"></param>
    /// <returns></returns>
    public static ListNode MergeTwoLists(ListNode list1, ListNode list2)
    {
        // recursive solution

        //if (list1 == null)
        //{
        //    return list2;
        //}
        //if (list2 == null)
        //{
        //    return list1;
        //}

        //if (list1.val <= list2.val)
        //{
        //    list1.next = MergeTwoLists(list1, list2);
        //    return list1;
        //}
        //else
        //{
        //    list2.next = MergeTwoLists(list1, list2);
        //    return list2;
        //}
        // end recursive solution


        // iterative solution with dummy node
        ListNode dummy = new();
        ListNode current = dummy;

        while (list1 is not null && list2 is not null)
        {
            if (list1.val <= list2.val)
            {
                current.next = list1;
                list1 = list1.next;
            }
            else
            {
                current.next = list2;
                list2 = list2.next;
            }

            current = current.next;
        }

        if (list1 is not null)
        {
            current.next = list1;
        }
        else if (list2 is not null)
        {
            current.next = list2;
        }

        return dummy.next;
        // end iterative solution

        // iterative solution without dummy node
        //ListNode mergedList = null;
        //ListNode current = null;

        //while (list1 is not null && list2 is not null)
        //{
        //    if (list1.val <= list2.val)
        //    {
        //        if (mergedList is null)
        //        {
        //            mergedList = list1;
        //            current = mergedList;
        //        }
        //        else
        //        {
        //            current.next = list1;
        //            current = current.next;
        //        }
        //        list1 = list1.next;
        //    }
        //    else
        //    {
        //        if (mergedList is null)
        //        {
        //            mergedList = list2;
        //            current = mergedList;
        //        }
        //        else
        //        {
        //            current.next = list2;
        //            current = current.next;
        //        }
        //        list2 = list2.next;
        //    }
        //}

        //if (list1 is not null)
        //{
        //    if (mergedList is null)
        //    {
        //        mergedList = list1;
        //    }
        //    else
        //    {
        //        current.next = list1;
        //    }
        //}
        //else if (list2 is not null)
        //{
        //    if (mergedList is null)
        //    {
        //        mergedList = list2;
        //    }
        //    else
        //    {
        //        current.next = list2;
        //    }
        //}

        //return mergedList;
    }

    /// <summary>
    /// Problem 22
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static IList<string> GenerateParenthesis(int n)
    {
        IList<string> result = new List<string>();
        GenerateParenthesisHelper(result, "", 0, 0, n);
        return result;

        static void GenerateParenthesisHelper(IList<string> result, string current, int open, int close, int n)
        {
            if (current.Length == 2 * n)
            {
                result.Add(current);
                return;
            }

            if (open < n)
            {
                GenerateParenthesisHelper(result, current + "(", open + 1, close, n);
            }

            if (close < open)
            {
                GenerateParenthesisHelper(result, current + ")", open, close + 1, n);
            }
        }
    }

    /// <summary>
    /// Problem 23
    /// </summary>
    /// <param name="lists"></param>
    /// <returns></returns>
    public static ListNode MergeKLists(ListNode[] lists)
    {
        if (lists is null || lists.Length == 0)
        {
            return null;
        }

        return MergeLists(lists, 0, lists.Length - 1);

        ListNode MergeLists(ListNode[] lists, int start, int end)
        {
            if (start == end)
            {
                return lists[start];
            }

            int mid = start + (end - start) / 2;
            ListNode left = MergeLists(lists, start, mid);
            ListNode right = MergeLists(lists, mid + 1, end);

            return Merge(left, right);
        }

        ListNode Merge(ListNode l1, ListNode l2)
        {
            if (l1 is null)
            {
                return l2;
            }
            if (l2 is null)
            {
                return l1;
            }

            if (l1.val < l2.val)
            {
                l1.next = Merge(l1.next, l2);
                return l1;
            }
            else
            {
                l2.next = Merge(l1, l2.next);
                return l2;
            }
        }
    }

    /// <summary>
    /// Problem 24
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public static ListNode SwapPairs(ListNode head)
    {
        if (head is null || head.next is null)
        {
            return head;
        }

        ListNode dummy = new()
        {
            next = head
        };

        ListNode current = dummy;

        while (current.next is not null && current.next.next is not null)
        {
            ListNode first = current.next;
            ListNode second = current.next.next;

            first.next = second.next;
            second.next = first;
            current.next = second;
            current = first;
        }

        return dummy.next;
    }

    /// <summary>
    /// Problem 25
    /// </summary>
    /// <param name="head"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static ListNode ReverseKGroup(ListNode head, int k)
    {
        ListNode dummy = new()
        {
            next = head
        };

        ListNode beforeReverse = dummy;
        ListNode endOfCurrentReverse = dummy;

        while (beforeReverse.next is not null)
        {
            // Move pointer to end of sublist to be reversed.
            for (int i = 0; i < k && endOfCurrentReverse is not null; i++)
            {
                endOfCurrentReverse = endOfCurrentReverse.next;
            }

            // We've reversed every possible sublist, break out of loop.
            if (endOfCurrentReverse is null)
            {
                break;
            }

            ListNode prev = null;
            ListNode current = beforeReverse.next;

            // Reverse sublist
            for (int i = 0; i < k; i++)
            {
                ListNode nextUp = current.next;
                current.next = prev;
                prev = current;
                current = nextUp;
            }

            ListNode nextBeforeReverse = beforeReverse.next;
            beforeReverse.next.next = current;
            beforeReverse.next = prev;
            beforeReverse = endOfCurrentReverse = nextBeforeReverse;
        }

        return dummy.next;
    }

    /// <summary>
    /// Problem 26
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int RemoveDuplicates(int[] nums)
    {
        int k = 1;
        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] != nums[i - 1])
            {
                nums[k] = nums[i];
                k++;
            }
        }

        return k;
    }

    /// <summary>
    /// Problem 27
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="val"></param>
    /// <returns></returns>
    public static int RemoveElement(int[] nums, int val)
    {
        int j = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] != val)
            {
                nums[j] = nums[i];
                j++;
            }
        }

        return j;
    }

    /// <summary>
    /// Problem 28
    /// </summary>
    /// <param name="haystack"></param>
    /// <param name="needle"></param>
    /// <returns></returns>
    public static int StrStr(string haystack, string needle)
    {
        if (needle.Length == 0)
        {
            return 0;
        }

        for (int i = 0; i <= haystack.Length - needle.Length; i++)
        {
            if (haystack.Substring(i, needle.Length) == needle)
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// Problem 29
    /// </summary>
    /// <param name="dividend"></param>
    /// <param name="divisor"></param>
    /// <returns></returns>
    public static int Divide(int dividend, int divisor)
    {
        // Handle special case to avoid overflow
        if (dividend == int.MinValue && divisor == -1)
        {
            return int.MaxValue;
        }

        // Initialize variables
        int result = 0;
        long absDividend = Math.Abs((long)dividend);
        long absDivisor = Math.Abs((long)divisor);

        // Perform division algorithm
        while (absDividend >= absDivisor)
        {
            // Initialize shift for bitwise operations
            int shiftCount = 0;

            // Use bitwise shift to find the highest power of 2
            while (absDividend >= (absDivisor << shiftCount))
            {
                shiftCount++;
            }

            // Adjust the shift and update absDividend and result
            shiftCount--;
            absDividend -= absDivisor << shiftCount;
            result += 1 << shiftCount;
        }

        // Adjust the sign of the result based on the signs of the original dividend and divisor
        if (dividend < 0 && divisor > 0 || dividend > 0 && divisor < 0)
        {
            result = -result;
        }

        // Return the final result
        return result;
    }

    /// <summary>
    /// Problem 30
    /// </summary>
    /// <param name="s"></param>
    /// <param name="words"></param>
    /// <returns></returns>
    public static IList<int> FindSubstring(string s, string[] words)
    {
        // Length of concatenated substring is the number of words * the lenght of the words
        int wordLength = words[0].Length;
        int lengthOfConcatenated = words.Length * wordLength;

        List<int> indices = new();

        // No valid indices if the string is shorter than the length of valid substring.
        if (s.Length < lengthOfConcatenated)
        {
            return indices;
        }

        // Set up dictionary
        Dictionary<string, int> countWords = new();
        foreach (string word in words)
        {
            if (countWords.TryGetValue(word, out int value))
            {
                countWords[word] = ++value;
            }
            else
            {
                countWords[word] = 1;
            }
        }


        for (int i = 0; i <= s.Length - lengthOfConcatenated; i++)
        {
            string window = s.Substring(i, lengthOfConcatenated);
            Dictionary<string, int> windowCount = new();

            for (int j = 0; j < window.Length; j += wordLength)
            {
                string currentWord = window.Substring(j, wordLength);

                if (windowCount.TryGetValue(currentWord, out int value))
                {
                    windowCount[currentWord] = ++value;
                }
                else
                {
                    windowCount[currentWord] = 1;
                }
            }

            if (windowCount.Count == countWords.Count && windowCount.All(pair => countWords.TryGetValue(pair.Key, out int count) && count == pair.Value))
            {
                indices.Add(i);
            }
        }

        return indices;
    }

    /// <summary>
    /// Alternative solution to Problem 30.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="words"></param>
    /// <returns></returns>
    public static IList<int> FindSubstringAlternative(string s, string[] words)
    {
        List<int> indices = new();

        if (string.IsNullOrEmpty(s) || words.Length == 0 || words is null)
        {
            return indices;
        }

        int wordLength = words[0].Length;
        int wordCount = words.Length;
        int concatenatedLength = wordLength * wordCount;

        Dictionary<string, int> wordFrequency = new();

        foreach (string word in words)
        {
            if (wordFrequency.TryGetValue(word, out int value))
            {
                wordFrequency[word] = ++value;
            }
            else
            {
                wordFrequency.Add(word, 1);
            }
        }

        for (int i = 0; i <= s.Length - concatenatedLength; i++)
        {
            Dictionary<string, int> substringFrequency = new();

            int j;
            for (j = 0; j < wordCount; j++)
            {
                string currentWord = s.Substring(i + j * wordLength, wordLength);

                if (!wordFrequency.ContainsKey(currentWord))
                {
                    break;
                }

                if (substringFrequency.TryGetValue(currentWord, out int value))
                {
                    substringFrequency[currentWord] = ++value;
                }
                else
                {
                    substringFrequency.Add(currentWord, 1);
                }

                if (substringFrequency[currentWord] > wordFrequency[currentWord])
                {
                    break;
                }
            }

            if (j == wordCount)
            {
                indices.Add(i);
            }
        }

        return indices;
    }
}
