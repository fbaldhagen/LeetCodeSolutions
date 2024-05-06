using LeetCodeSolutions.Structures;

namespace LeetCodeSolutions._0201_0300;

public class Problems_0231_0240
{
    /// <summary>
    /// Problem 231
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static bool IsPowerOfTwo(int n)
    {
        // Check if n and (n - 1) have no common set bits, indicating that n is a power of two
        // This operation effectively removes the rightmost set bit from n
        // If n is a power of two, subtracting 1 will result in flipping all the bits to the right of the rightmost set bit,
        // and performing a bitwise AND operation will yield 0
        return n > 0 && (n & (n - 1)) == 0;
    }

    /// <summary>
    /// Problem 233
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int CountDigitOne(int n)
    {
        int count = 0;

        for (long i = 1; i <= n; i *= 10)
        {
            long divider = i * 10;

            // For each position, we consider three cases:
            // 1. (n / divider) * i: Count of 1s contributed by multiples of the current digit place
            // 2. Math.Min(Math.Max(n % divider - i + 1, 0), i): Count of 1s contributed by the remainder
            // 3. Math.Max(n % divider - i + 1, 0) handles the case where the remainder is less than i
            // and we shouldn't count additional 1s.
            count += (int)((n / divider) * i + Math.Min(Math.Max(n % divider - i + 1, 0), i));
        }

        return count;
    }

    /// <summary>
    /// Problem 234
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public static bool IsPalindrome(ListNode head)
    {
        ListNode slow = head;
        ListNode fast = head;

        // Find middle node w. slow and fast pointer
        while (fast is not null && fast.next is not null)
        {
            slow = slow.next;
            fast = fast.next.next;
        }

        ListNode curr = slow;
        ListNode prev = null;

        // Reverse the 2nd half
        while (curr is not null)
        {
            ListNode next = curr.next;
            curr.next = prev;
            prev = curr;
            curr = next;
        }

        ListNode p1 = prev;
        ListNode p2 = head;

        // Traverse both halves, confirm palindrome.
        while (p1 is not null)
        {
            if (p1.val != p2.val)
            {
                return false;
            }

            p1 = p1.next;
            p2 = p2.next;
        }

        return true;
    }

    /// <summary>
    /// Problem 235
    /// </summary>
    /// <param name="root"></param>
    /// <param name="p"></param>
    /// <param name="q"></param>
    /// <returns></returns>
    public static TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
    {
        TreeNode curr = root;

        while (curr != null)
        {
            if (p.val > curr.val && q.val > curr.val)
            {
                // Root is in the right subtree
                curr = curr.right;
            }
            else if (p.val < curr.val && q.val < curr.val)
            {
                // Root is in the left subtree
                curr = curr.left;
            }
            else
            {
                // q and p are in different subtrees, or one is the root -> current node is the LCA
                return curr;
            }
        }

        return curr;
    }

    /// <summary>
    /// Problem 236
    /// </summary>
    /// <param name="root"></param>
    /// <param name="p"></param>
    /// <param name="q"></param>
    /// <returns></returns>
    public static TreeNode LowestCommonAncestorII(TreeNode root, TreeNode p, TreeNode q)
    {
        // If the current node is null or matches one of the given nodes, return the current node
        if (root == null || root == p || root == q)
        {
            return root;
        }

        // Recursively search for the LCA in the left and right subtrees
        TreeNode leftLCA = LowestCommonAncestorII(root.left, p, q);
        TreeNode rightLCA = LowestCommonAncestorII(root.right, p, q);

        // If both leftLCA and rightLCA are not null, it means the current node is the LCA
        if (leftLCA != null && rightLCA != null)
            return root;

        // Otherwise, return the non-null result from either leftLCA or rightLCA
        return leftLCA ?? rightLCA;
    }

    /// <summary>
    /// Problem 237
    /// </summary>
    /// <param name="node"></param>
    public static void DeleteNode(ListNode node)
    {
        node.val = node.next.val;
        node.next = node.next.next;
    }

    /// <summary>
    /// Problem 238
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int[] ProductExceptSelf(int[] nums)
    {
        int n = nums.Length;

        // Initialize arrays to store prefix and suffix products
        int[] prefix = new int[n];
        int[] suffix = new int[n];

        // Compute prefix products
        prefix[0] = 1;
        for (int i = 1; i < n; i++)
        {
            prefix[i] = prefix[i - 1] * nums[i - 1];
        }

        // Compute suffix products
        suffix[n - 1] = 1;
        for (int i = n - 2; i >= 0; i--)
        {
            suffix[i] = suffix[i + 1] * nums[i + 1];
        }

        // Combine prefix and suffix products to get the final result
        int[] result = new int[n];
        for (int i = 0; i < n; i++)
        {
            result[i] = prefix[i] * suffix[i];
        }

        return result;
    }

    /// <summary>
    /// Problem 239
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static int[] MaxSlidingWindow(int[] nums, int k)
    {
        int n = nums.Length;

        // Resulting array is of size n - k + 1
        int[] result = new int[n - k + 1];
        LinkedList<int> deque = new();

        for (int i = 0; i < n; i++)
        {
            // Remove indices outside the current window from the deque
            if (deque.Count > 0 && deque.First.Value < i - k + 1)
            {
                deque.RemoveFirst();
            }

            // Remove indices of elements smaller than the current element
            while (deque.Count > 0 && nums[i] >= nums[deque.Last.Value])
            {
                deque.RemoveLast();
            }

            // Add current index to the deque
            deque.AddLast(i);

            // If the window is fully formed, add maximum to the result
            if (i >= k - 1)
            {
                result[i - k + 1] = nums[deque.First.Value];
            }
        }

        return result;
    }

    /// <summary>
    /// Problem 240
    /// </summary>
    /// <param name="matrix"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static bool SearchMatrix(int[][] matrix, int target)
    {
        int m = matrix.Length;
        int n = matrix[0].Length;

        // Start from the bottom-left corner
        int row = m - 1;
        int col = 0;

        while (row >= 0 && col < n)
        {
            if (matrix[row][col] == target)
            {
                return true;
            }
            else if (matrix[row][col] < target)
            {
                // Move right if current value is less than target
                col++;
            }
            else
            {
                // Move up if current value is greater than target
                row--;
            }
        }

        return false;
    }
}

/// <summary>
/// Implementation of a Queue using two Stacks.
/// Problem 232
/// </summary>
public class MyQueue
{
    // Stacks for incoming and outgoing elements
    private Stack<int> inStack;
    private Stack<int> outStack;

    public MyQueue()
    {
        inStack = new Stack<int>();
        outStack = new Stack<int>();
    }

    public void Push(int x)
    {
        // Pushes an element to the incoming stack
        inStack.Push(x);
    }

    public int Pop()
    {
        // If outStack is empty, transfer elements from inStack
        if (outStack.Count == 0)
        {
            TransferElements();
        }

        // Pop element from the outgoing stack
        return outStack.Pop();
    }

    public int Peek()
    {
        // If outStack is empty, transfer elements from inStack
        if (outStack.Count == 0)
        {
            TransferElements();
        }

        // Peek at the front element of the outgoing stack
        return outStack.Peek();
    }

    public bool Empty()
    {
        // Check if both stacks are empty
        return inStack.Count == 0 && outStack.Count == 0;
    }

    private void TransferElements()
    {
        while (!(inStack.Count == 0))
        {
            outStack.Push(inStack.Pop());
        }
    }
}