using LeetCodeSolutions.Structures;
using System.Text;

namespace LeetCodeSolutions._0001_0100;

public class Problems_0011_0020
{
    /// <summary>
    /// Problem 11
    /// </summary>
    /// <param name="height"></param>
    /// <returns></returns>
    public static int MaxArea(int[] height)
    {
        int maxArea = 0;
        int left = 0;
        int right = height.Length - 1;

        while (left < right)
        {
            int width = right - left;

            maxArea = Math.Max(maxArea, Math.Min(height[left], height[right]) * width);

            if (height[left] <= height[right])
            {
                left++;
            }
            else
            {
                right--;
            }
        }
        return maxArea;
    }

    /// <summary>
    /// Problem 12
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public static string IntToRoman(int num)
    {
        List<(int, string)> symbols = new()
        {
            (1000, "M"),
            (900, "CM"),
            (500, "D"),
            (400, "CD"),
            (100, "C"),
            (90, "XC"),
            (50, "L"),
            (40, "XL"),
            (10, "X"),
            (9, "IX"),
            (5, "V"),
            (4, "IV"),
            (1, "I")
        };


        StringBuilder result = new();

        foreach (var (value, symbol) in symbols)
        {
            while (num >= value)
            {
                result.Append(symbol);
                num -= value;
            }
        }

        return result.ToString(); ;
    }

    /// <summary>
    /// Problem 13
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static int RomanToInt(string s)
    {
        Dictionary<char, int> symbols = new()
        {
            { 'I', 1 },
            { 'V', 5 },
            { 'X', 10 },
            { 'L', 50 },
            { 'C', 100 },
            { 'D', 500 },
            { 'M', 1000 }
        };

        int result = 0;
        int prevValue = 0;

        for (int i = s.Length - 1; i >= 0; i--)
        {
            int currValue = symbols[s[i]];
            if (currValue < prevValue)
            {
                result -= currValue;
            }
            else
            {
                result += currValue;
            }
            prevValue = currValue;
        }

        return result;
    }

    /// <summary>
    /// Problem 14
    /// </summary>
    /// <param name="strs"></param>
    /// <returns></returns>
    public static string LongestCommonPrefix(string[] strs)
    {
        if (strs is null || strs.Length == 0)
        {
            return string.Empty;
        }

        // Get the length of the shortest string, ie. the longest possible prefix
        int minLength = strs.Min(str => str.Length);

        StringBuilder result = new();

        // Check every character up to the length of the shortest string.
        for (int i = 0; i < minLength; i++)
        {
            // doesnt matter which string (but we know there is one at index 0)
            char currentChar = strs[0][i];

            if (strs.All(s => s[i] == currentChar))
            {
                result.Append(currentChar);
            }
            else
            {
                // Break on a mismatch
                break;
            }
        }

        return result.ToString();
    }

    /// <summary>
    /// Problem 15
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static IList<IList<int>> ThreeSum(int[] nums)
    {
        IList<IList<int>> result = [];

        // Sort the array in non-descreasing order
        Array.Sort(nums);

        for (int i = 0; i < nums.Length - 2; i++)
        {
            // Skip duplicates
            if (i > 0 && nums[i] == nums[i - 1])
            {
                continue;
            }

            // Skip unnecessary iterations if current element is positive
            if (nums[i] > 0) break;

            int j = i + 1;
            int k = nums.Length - 1;

            while (j < k)
            {
                int sum = nums[i] + nums[j] + nums[k];

                if (sum == 0)
                {
                    result.Add(new List<int> { nums[i], nums[j], nums[k] });

                    // Skip duplicates
                    while (j < k && nums[j] == nums[j + 1])
                    {
                        j++;
                    }
                    while (j < k && nums[k] == nums[k - 1])
                    {
                        k--;
                    }

                    j++;
                    k--;
                }
                else if (sum < 0)
                {
                    j++;
                }
                else
                {
                    k--;
                }
            }
        }
        return result;
    }

    /// <summary>
    /// Problem 16
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static int ThreeSumClosest(int[] nums, int target)
    {
        //Sort the array in non-decreasing order

        Array.Sort(nums);

        int closest = int.MaxValue;

        for (int i = 0; i < nums.Length - 2; i++)
        {
            // same as previous problem
            int j = i + 1;
            int k = nums.Length - 1;

            while (j < k)
            {
                int sum = nums[i] + nums[j] + nums[k];

                // Check for exact target sum
                if (sum == target)
                {
                    return sum;
                }

                if (Math.Abs(sum - target) < Math.Abs(closest - target))
                {
                    closest = sum;
                }

                if (sum < target)
                {
                    j++;
                }
                else
                {
                    k--;
                }
            }
        }

        return closest;
    }

    /// <summary>
    /// Problem 17
    /// </summary>
    /// <param name="digits"></param>
    /// <returns></returns>
    public static IList<string> LetterCombinations(string digits)
    {
        if (string.IsNullOrEmpty(digits))
        {
            return new List<string>();
        }

        Dictionary<char, string> digitLetterMap = new()
        {
            {'2', "abc"},
            {'3', "def"},
            {'4', "ghi"},
            {'5', "jkl"},
            {'6', "mno"},
            {'7', "pqrs"},
            {'8', "tuv"},
            {'9', "wxyz"},
        };

        List<string> result = new()
        {
            string.Empty
        };

        foreach (var c in digits)
        {
            List<string> tempResult = new();

            string lettersFromDigit = digitLetterMap[c];

            foreach (var existing in result)
            {
                foreach (var letter in lettersFromDigit)
                {
                    tempResult.Add(existing + letter);
                }
            }
            result = tempResult;
        }

        return result;
    }

    /// <summary>
    /// Problem 18
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static IList<IList<int>> FourSum(int[] nums, int target)
    {
        // initialize return list
        List<IList<int>> result = new();
        //sort array in non-descending
        Array.Sort(nums);

        for (int i = 0; i < nums.Length - 3; i++)
        {
            for (int j = i + 1; j < nums.Length - 2; j++)
            {
                int left = j + 1;
                int right = nums.Length - 1;

                while (left < right)
                {
                    long sum = (long)nums[i] + nums[j] + nums[left] + nums[right];

                    if (sum == target)
                    {
                        result.Add(new List<int>
                        {
                            nums[i], nums[j], nums[left], nums[right]
                        });

                        // skip duplicates
                        while (left < right && nums[left] == nums[left + 1])
                        {
                            left++;
                        }
                        while (left < right && nums[right] == nums[right - 1])
                        {
                            right--;
                        }
                        // move on
                        left++;
                        right--;
                    }
                    else if (sum < target)
                    {
                        left++;
                    }
                    else if (sum > target)
                    {
                        right--;
                    }
                }
                //skip duplicates 
                while (j < nums.Length - 2 && nums[j] == nums[j + 1])
                {
                    j++;
                }
            }
            // skip duplicates
            while (i < nums.Length - 3 && nums[i] == nums[i + 1])
            {
                i++;
            }
        }

        return result;
    }

    /// <summary>
    /// Problem 19
    /// </summary>
    /// <param name="head"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public static ListNode RemoveNthFromEnd(ListNode head, int n)
    {
        // dummy pointer to head
        ListNode dummy = new()
        {
            next = head
        };

        // two pointers, one fast and one slow
        ListNode fast = dummy;
        ListNode slow = dummy;

        // create gap of size n between slow and fast
        for (int i = 0; i < n; i++)
        {
            fast = fast.next;
        }

        while (fast.next is not null)
        {
            fast = fast.next;
            slow = slow.next;
        }

        slow.next = slow.next.next;

        return dummy.next;
    }

    /// <summary>
    /// Problem 20
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static bool IsValid(string s)
    {
        Stack<char> stack = new();

        // go through the string
        foreach (char c in s)
        {
            // starting parenthesis gets pushed onto the stack
            if (c == '(' || c == '[' || c == '{')
            {
                stack.Push(c);
            }
            // closing parenthesis gets popped
            else if (c == ')' || c == ']' || c == '}')
            {
                if (stack.Count == 0)
                {
                    return false;
                }
                char toPop = stack.Peek();
                if (c == ')' && toPop != '(' || c == ']' && toPop != '[' || c == '}' && toPop != '{')
                {
                    return false;
                }
                stack.Pop();
            }
        }

        return stack.Count == 0;
    }
}