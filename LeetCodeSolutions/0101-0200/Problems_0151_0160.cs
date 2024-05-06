using LeetCodeSolutions.Structures;

namespace LeetCodeSolutions._0101_0200;

public class Problems_0151_0160
{
    /// <summary>
    /// Problem 151
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string ReverseWords(string s)
    {
        // Split the string into an array by spaces after removing leading and trailing whitespaces.
        string[] words = s.Trim().Split(' ');

        // Reverse the array
        Array.Reverse(words);

        // Join the reversed words into a string separated by single spaces'
        return string.Join(" ", words);
    }

    /// <summary>
    /// Problem 152
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int MaxProduct(int[] nums)
    {
        // Return 0 if theres nothing to multiply
        if (nums is null || nums.Length == 0)
        {
            return 0;
        }

        int n = nums.Length;
        // Keep track of largest positive and negative values
        int maxProd = nums[0];
        int minProd = nums[0];

        int result = nums[0];

        // Iterate over the array
        for (int i = 1; i < n; i++)
        {
            // Keep the current max stored
            int tempMax = maxProd;
            // Update the max and min products
            maxProd = Math.Max(nums[i], Math.Max(maxProd * nums[i], minProd * nums[i]));
            minProd = Math.Min(nums[i], Math.Min(tempMax * nums[i], minProd * nums[i]));
            result = Math.Max(result, maxProd);
        }

        return result;
    }

    /// <summary>
    /// Problem 153
    /// </summary>
    /// <param name="ints"></param>
    /// <returns></returns>
    public static int FindMin(int[] nums)
    {
        int left = 0;
        int right = nums.Length - 1;

        while (left < right)
        {
            // Find the middle index
            int mid = left + (right - left) / 2;

            // If the value at mid is greater than the one at the rightmost, the rotation was
            // somewhere between left and right, and therefore the minimum value lies to the right
            if (nums[mid] > nums[right])
            {
                left = mid + 1;
            }
            // If the value at mid is less than or equal to the value at right, update the right pointer
            else
            {
                right = mid;
            }
        }

        // At this point, left will point to the minimum element
        return nums[left];
    }
    /// <summary>
    /// Problem 154
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int FindMinII(int[] nums)
    {
        int left = 0;
        int right = nums.Length - 1;

        while (left < right)
        {
            // Find the middle index
            int mid = left + (right - left) / 2;

            // If the value at mid is greater than the one at the rightmost, the rotation was
            // somewhere between left and right, and therefore the minimum value lies to the right
            if (nums[mid] > nums[right])
            {
                left = mid + 1;
            }
            // If the value at mid is less than or equal to the value at right, update the right pointer
            else if (nums[mid] < nums[right])
            {
                right = mid;
            }
            // nums[mid] == nums[right]
            else
            {
                // Move right pointer towards left
                right--;
            }
        }

        return nums[left];
    }

    // Problems 156-159 are premium, and therefore skipped

    /// <summary>
    /// Problem 160
    /// </summary>
    /// <param name="headA"></param>
    /// <param name="headB"></param>
    /// <returns></returns>
    public static ListNode GetIntersectionNode(ListNode headA, ListNode headB)
    {
        if (headA == null || headB == null)
        {
            return null;
        }

        ListNode ptrA = headA;
        ListNode ptrB = headB;

        // Traverse both lists until they meet or reach the end
        while (ptrA != ptrB)
        {
            // Redirect ptrA to the head of listB if it reaches the end
            ptrA = (ptrA == null) ? headB : ptrA.next;
            // Redirect ptrB to the head of listA if it reaches the end
            ptrB = (ptrB == null) ? headA : ptrB.next;
        }

        // Return the intersection node if it exists, otherwise return null
        return ptrA;
    }
}

/// <summary>
/// Solution to Problem 156
/// </summary>
public class Problem_155
{
    public class MinStack
    {
        private Stack<int> stack;
        private Stack<int> minStack;

        public MinStack()
        {
            stack = new Stack<int>();
            minStack = new Stack<int>();
        }

        public void Push(int val)
        {
            stack.Push(val);

            if (minStack.Count == 0 || val <= minStack.Peek())
            {
                minStack.Push(val);
            }
        }

        public void Pop()
        {
            if (stack.Count > 0)
            {
                int popped = stack.Pop();

                if (popped == minStack.Peek())
                {
                    minStack.Pop();
                }
            }
        }

        public int Top()
        {
            if (stack.Count > 0)
            {
                return stack.Peek();
            }
            throw new InvalidOperationException("Stack is empty");
        }

        public int GetMin()
        {
            if (minStack.Count > 0)
            {
                return minStack.Peek();
            }
            throw new InvalidOperationException("Stack is empty");
        }
    }
}