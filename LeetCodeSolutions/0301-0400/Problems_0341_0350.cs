using System.Collections.Immutable;

namespace LeetCodeSolutions._0301_0400;

public class Problems_0341_0350
{
    /// <summary>
    /// Problem 341
    /// </summary>
    public class Problem341
    {
        public class NestedIterator
        {
            private readonly Stack<NestedInteger> stack;

            public NestedIterator(IList<NestedInteger> nestedList)
            {
                stack = new Stack<NestedInteger>();
                // Initialize the stack with the elements in reverse order
                for (int i = nestedList.Count - 1; i >= 0; i--)
                {
                    stack.Push(nestedList[i]);
                }
            }

            public bool HasNext()
            {
                while (stack.Count > 0)
                {
                    NestedInteger current = stack.Peek();
                    if (current.IsInteger())
                    {
                        return true;
                    }
                    // Pop the list and push its elements in reverse order
                    stack.Pop();
                    IList<NestedInteger> nestedList = current.GetList();
                    for (int i = nestedList.Count - 1; i >= 0; i--)
                    {
                        stack.Push(nestedList[i]);
                    }
                }
                return false;
            }

            public int Next()
            {
                if (HasNext())
                {
                    return stack.Pop().GetInteger();
                }
                throw new InvalidOperationException("No more elements");
            }
        }

        // The interface and class below are not needed for the LC solution, only added to comply with naming conventions in .NET.
        // Interface should be INestedInteger, but we use NestedInteger in the problem. Class above in enough.
        public interface INestedInteger
        {
            /// <summary>
            /// Return true if this NestedInteger holds a single integer, rather than a nested list.
            /// </summary>
            /// <returns></returns>
            bool IsInteger();

            /// <summary>
            /// Return the single integer that this NestedInteger holds, if it holds a single integer
            /// Return null if this NestedInteger holds a nested list
            /// </summary>
            /// <returns></returns>
            int GetInteger();

            /// <summary>
            /// Return the nested list that this NestedInteger holds, if it holds a nested list <br/>
            /// Return null if this NestedInteger holds a single integer
            /// </summary>
            /// <returns></returns>
            IList<NestedInteger> GetList();
        }

        // Not needed for solution
        public class NestedInteger : INestedInteger
        {
            public int GetInteger()
            {
                throw new NotImplementedException();
            }

            public IList<NestedInteger> GetList()
            {
                throw new NotImplementedException();
            }

            public bool IsInteger()
            {
                throw new NotImplementedException();
            }
        }
    }


    /// <summary>
    /// Problem 342
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static bool IsPowerOfFour(int n)
    {
        // Make sure n is positive and a power of 2. If not, return false.
        if (n <= 0 || ((n & (n - 1)) != 0))
        {
            return false;
        }

        // Check if the single set bit is at an even position
        // Power of four numbers have their single '1' bit in even positions:
        // 1 (2^0), 4 (2^2), 16 (2^4), 64 (2^6), etc.
        // So, we can use n-1 to see if the number of trailing zeros is even
        return (n - 1) % 3 == 0;
    }

    /// <summary>
    /// Problem 343
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int IntegerBreak(int n)
    {
        // Base cases: for n = 2 and n = 3, the maximum product is n-1
        if (n <= 3)
        {
            return n - 1;
        }

        int product = 1;

        // While n is greater than 4, multiply product by 3 and decrease n by 3
        while (n > 4)
        {
            product *= 3;
            n -= 3;
        }

        // Multiply the remaining n to the product
        product *= n;

        return product;
    }

    /// <summary>
    /// Problem 344
    /// </summary>
    /// <param name="s"></param>
    public static void ReverseString(char[] s)
    {
        int i = 0;
        
        // Swap the ith char from the start with the ith char from the end, until at the middle.
        while (i < s.Length / 2)
        {
            (s[i], s[s.Length - 1 - i]) = (s[s.Length - 1 - i], s[i]);
            i++;
        }
    }

    /// <summary>
    /// Problem 345
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string ReverseVowels(string s)
    {
        if (s.Length == 1 || s.Length == 0)
        {
            return s;
        }

        char[] sArr = s.ToCharArray();
        int left = 0;
        int right = sArr.Length - 1;

        HashSet<char> vowels = ['a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U'];

        while (left < right)
        {
            while (left < right && !vowels.Contains(sArr[left]))
            {
                left++;
            }

            while (left < right && !vowels.Contains(sArr[right]))
            {
                right--;
            }

            if (vowels.Contains(sArr[left]) && vowels.Contains(sArr[right]))
            {
                (sArr[left], sArr[right]) = (sArr[right], sArr[left]);
                left++;
                right--;
            }
        }

        return new string(sArr);
    }

    /// <summary>
    /// Problem 347
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static int[] TopKFrequent(int[] nums, int k)
    {
        // Dictionary to store the frequency of each element
        Dictionary<int, int> frequencyMap = [];

        foreach (int num in nums)
        {
            if (frequencyMap.TryGetValue(num, out int value))
            {
                frequencyMap[num] = ++value;
            }
            else
            {
                frequencyMap[num] = 1;
            }
        }

        // Using a min-heap to keep track of the top k elements, SortedDictionary here
        SortedDictionary<int, List<int>> frequencyHeap = [];

        foreach (KeyValuePair<int, int> pair in frequencyMap)
        {
            int frequency = pair.Value;
            int num = pair.Key;

            if (!frequencyHeap.TryGetValue(frequency, out List<int>? value))
            {
                value = [];
                frequencyHeap[frequency] = value;
            }

            value.Add(num);
        }

        List<int> result = [];

        foreach (int frequency in frequencyHeap.Keys.Reverse())
        {
            foreach (int num in frequencyHeap[frequency])
            {
                result.Add(num);
                if (result.Count == k)
                {
                    return [.. result];
                }
            }
        }

        return [.. result];
    }

    /// <summary>
    /// Problem 349
    /// </summary>
    /// <param name="nums1"></param>
    /// <param name="nums2"></param>
    /// <returns></returns>
    public static int[] Intersection(int[] nums1, int[] nums2)
    {
        HashSet<int> set1 = new(nums1);

        HashSet<int> intersection = [];

        for (int i = 0; i < nums2.Length; i++)
        {
            if (set1.Contains(nums2[i]))
            {
                intersection.Add(nums2[i]);
            }
        }

        return [.. intersection];
    }
}