using LeetCodeSolutions.Structures;

namespace LeetCodeSolutions._0201_0300;

public class Problems_0221_0230
{
    /// <summary>
    /// Problem 221
    /// </summary>
    /// <param name="matrix"></param>
    /// <returns></returns>
    public static int MaximalSquare(char[][] matrix)
    {
        // Check if the matrix is empty or null, in which case return 0
        if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0)
        {
            return 0;
        }

        // Get the number of rows and columns in the matrix
        int m = matrix.Length;
        int n = matrix[0].Length;

        // Initialize variables to keep track of the maximum square side length and a 2D array to store dynamic programming results
        int maxSide = 0;
        int[,] dp = new int[m, n];

        // Loop through each cell in the matrix
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                // If the current cell contains '1'
                if (matrix[i][j] == '1')
                {
                    // If it's the first row or first column, set dp[i, j] to 1
                    if (i == 0 || j == 0)
                    {
                        dp[i, j] = 1;
                    }
                    // Otherwise, calculate the minimum of the values to the left, above, and diagonally left-up, and add 1
                    else
                    {
                        dp[i, j] = Math.Min(dp[i - 1, j], Math.Min(dp[i, j - 1], dp[i - 1, j - 1])) + 1;
                    }
                    // Update maxSide with the maximum of its current value and the value in dp[i, j]
                    maxSide = Math.Max(maxSide, dp[i, j]);
                }
            }
        }

        // Return the area of the largest square by squaring the maxSide
        return maxSide * maxSide;
    }

    /// <summary>
    /// Problem 222
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public static int CountNodes(TreeNode root)
    {
        if (root is null)
        {
            return 0;
        }

        // Calculate the heights of the left and right subtrees
        int leftHeight = GetHeight(root.left);
        int rightHeight = GetHeight(root.right);

        // Left subtree is a full binary tree
        if (leftHeight == rightHeight)
        {
            // Calculate the number of nodes in the full left subtree and add it to the number of nodes in the right subtree
            return (1 << leftHeight) + CountNodes(root.right);
        }
        // Right subtree is a full binary tree
        else
        {
            // Calculate the number of nodes in the full right subtree and add it to the number of nodes in the left subtree
            return (1 << rightHeight) + CountNodes(root.left);
        }

        // Function to calculate the height of a binary tree rooted at the given node
        static int GetHeight(TreeNode? node)
        {
            int height = 0;

            // Traverse down the left child until reaching a leaf node
            while (node is not null)
            {
                height++;
                node = node.left;
            }

            return height;
        }
    }

    /// <summary>
    /// Problem 223
    /// </summary>
    /// <param name="ax1"></param>
    /// <param name="ay1"></param>
    /// <param name="ax2"></param>
    /// <param name="ay2"></param>
    /// <param name="bx1"></param>
    /// <param name="by1"></param>
    /// <param name="bx2"></param>
    /// <param name="by2"></param>
    /// <returns></returns>
    public static int ComputeArea(int ax1, int ay1, int ax2, int ay2, int bx1, int by1, int bx2, int by2)
    {
        int areaA = (ax2 - ax1) * (ay2 - ay1);
        int areaB = (bx2 - bx1) * (by2 - by1);

        int overlapWidth = Math.Max(0, Math.Min(ax2, bx2) - Math.Max(ax1, bx1));
        int overlapHeight = Math.Max(0, Math.Min(ay2, by2) - Math.Max(ay1, by1));
        int overlapArea = overlapWidth * overlapHeight;

        return areaA + areaB - overlapArea;
    }

    /// <summary>
    /// Problem 224
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static int Calculate(string s)
    {
        // Use a stack to track parentheses
        Stack<int> stack = new();

        // Initialize variables
        int operand = 0;
        int result = 0;
        int sign = 1;

        // Iterate over each character in s
        foreach (char c in s)
        {
            switch (c)
            {
                // If the current character is '+', add the current operand to the result
                // with the appropriate sign, reset the operand, and set the sign to positive.
                case '+':
                    result += sign * operand;
                    operand = 0;
                    sign = 1;
                    break;

                // If the current character is '-', add the current operand to the result
                // with the appropriate sign, reset the operand, and set the sign to negative.
                case '-':
                    result += sign * operand;
                    operand = 0;
                    sign = -1;
                    break;

                // If the current character is '(', push the current result and sign onto the stack,
                // reset the result to 0, and set the sign to positive.
                case '(':
                    stack.Push(result);
                    stack.Push(sign);
                    result = 0;
                    sign = 1;
                    break;

                // If the current character is ')', add the current operand to the result
                // with the appropriate sign, reset the operand, and calculate the sub-expression result
                // by multiplying it with the sign from the stack and adding the previous result from the stack.
                case ')':
                    result += sign * operand;
                    operand = 0;
                    result *= stack.Pop();
                    result += stack.Pop();
                    break;

                // If the current character is a digit, update the current operand accordingly.
                default:
                    if (Char.IsDigit(c))
                    {
                        operand = operand * 10 + (c - '0');
                    }
                    break;
            }
        }

        result += sign * operand;

        return result;
    }

    // Problem 225 is in the separate file MyStack.cs

    /// <summary>
    /// Problem 226
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public static TreeNode InvertTree(TreeNode root)
    {
        if (root is null)
        {
            return root;
        }

        InvertChildren(root);
        return root;

        static void InvertChildren(TreeNode? node)
        {
            if (node is null)
            {
                return;
            }

            // Swap left and right child nodes
            (node.left, node.right) = (node.right, node.left);

            InvertChildren(node.right);
            InvertChildren(node.left);
        }
    }

    /// <summary>
    /// Problem 227
    /// </summary>
    /// <returns></returns>
    public static int CalculateII(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return 0;
        }

        // Stack to store intermediate results
        Stack<int> stack = new();
        // Initialize variables
        int operand = 0; // Current operand
        int result = 0; // Total result
        char sign = '+'; // Current operator, initialized as addition for the first operand

        // Iterate over each character in the string
        for (int i = 0; i < s.Length; i++)
        {
            char c = s[i];

            // If the character is a digit, build the current operand
            if (char.IsDigit(c))
            {
                operand = operand * 10 + (c - '0');
            }

            // If the character is an operator or it's the last character in the string
            if ((!char.IsDigit(c) && c != ' ') || i == s.Length - 1)
            {
                // Based on the previous operator, perform the corresponding operation
                if (sign == '+')
                {
                    stack.Push(operand); // For addition, push the operand onto the stack
                }
                else if (sign == '-')
                {
                    stack.Push(-operand); // For subtraction, push the negated operand onto the stack
                }
                else if (sign == '*')
                {
                    stack.Push(stack.Pop() * operand); // For multiplication, pop the last operand, perform multiplication, then push the result onto the stack
                }
                else if (sign == '/')
                {
                    stack.Push(stack.Pop() / operand); // For division, pop the last operand, perform division, then push the result onto the stack
                }

                // Update the operator to the current character
                sign = c;
                // Reset the operand for the next number
                operand = 0;
            }
        }

        // Calculate the final result by summing up all values in the stack
        while (stack.Count > 0)
        {
            result += stack.Pop();
        }

        // Return the final result
        return result;
    }

    /// <summary>
    /// Problem 228
    /// </summary>
    /// <param name="nums">A sorted unique integer array.</param>
    /// <returns></returns>
    public static IList<string> SummaryRanges(int[] nums)
    {
        IList<string> result = [];

        // Edge cases where nums either is empty or just has one element.
        if (nums.Length == 0)
        {
            return result;
        }

        if (nums.Length == 1)
        {
            result.Add(nums[0].ToString());
            return result;
        }

        // Keep track of the start and end of each sub-range.
        int start = nums[0];
        int end = nums[0];

        // Go through each integer
        for (int i = 1; i < nums.Length; i++)
        {
            // If the current number isn't the last + 1, we should create a new range.
            if (nums[i] != end + 1)
            {
                // Add the range to the result.
                result.Add(BuildString(start, end));
                start = nums[i];
                // If the last element isn't a part of a range.
                if (i == nums.Length - 1)
                {
                    result.Add(start.ToString());
                    return result;
                }
            }
            // If the last element is a part of the current range.
            else if (i == nums.Length - 1)
            {
                result.Add(BuildString(start, nums[i]));
                return result;
            }

            end = nums[i];
        }

        return result;

        // Helper method to build the strings
        static string BuildString(int start, int end)
        {
            if (start == end)
            {
                return start.ToString();
            }

            return start.ToString() + "->" + end.ToString();
        }
    }

    /// <summary>
    /// Problem 229
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static IList<int> MajorityElement(int[] nums)
    {
        IList<int> result = [];

        if (nums == null || nums.Length == 0)
        {
            return result;
        }

        int candidate1 = 0;
        int candidate2 = 0;
        int count1 = 0;
        int count2 = 0;

        // Find candidates that appears more than ⌊ n/3 ⌋ times (--> max 2 numbers)
        foreach (int num in nums)
        {
            if (num == candidate1)
            {
                count1++;
            }
            else if (num == candidate2)
            {
                count2++;
            }
            else if (count1 == 0)
            {
                candidate1 = num;
                count1 = 1;
            }
            else if (count2 == 0)
            {
                candidate2 = num;
                count2 = 1;
            }
            else
            {
                count1--;
                count2--;
            }
        }

        // Verify candidates
        count1 = 0;
        count2 = 0;
        foreach (int num in nums)
        {
            if (num == candidate1)
            {
                count1++;
            }
            else if (num == candidate2)
            {
                count2++;
            }
        }

        // Check if candidates appear more than ⌊ n/3 ⌋ times
        int n = nums.Length;
        if (count1 > n / 3)
        {

            result.Add(candidate1);
        }

        if (count2 > n / 3 && candidate1 != candidate2)
        {

            result.Add(candidate2);
        }

        return result;
    }

    /// <summary>
    /// Problem 230
    /// </summary>
    /// <param name="root"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static int KthSmallest(TreeNode root, int k)
    {
        // Keep track of nth smallest, and the result.
        int count = 0;
        int result = 0;

        // Use a recursive method to traverse the tree using inorder traversal.
        FindSmallest(root, k);
        return result;

        void FindSmallest(TreeNode node, int k)
        {
            if (node is null)
            {
                return;
            }

            FindSmallest(node.left, k);

            count++;
            if (count == k)
            {
                result = node.val;
                return;
            }

            FindSmallest(node.right, k);
        }
    }
}

/// <summary>
/// Implementation of a Stack using Queues.
/// Problem 225
/// </summary>
public class MyStack
{
    private Queue<int> primaryQueue;
    private Queue<int> secondaryQueue;

    public MyStack()
    {
        primaryQueue = new Queue<int>();
        secondaryQueue = new Queue<int>();
    }

    /// <summary>
    /// Pushes element x to the top of the stack.
    /// </summary>
    /// <param name="x"></param>
    public void Push(int x)
    {
        primaryQueue.Enqueue(x);

        while (secondaryQueue.Count > 0)
        {
            primaryQueue.Enqueue(secondaryQueue.Dequeue());
        }

        // Swap the primary and secondary queues
        (secondaryQueue, primaryQueue) = (primaryQueue, secondaryQueue);
    }

    /// <summary>
    /// Removes the element on the top of the stack and returns it.
    /// </summary>
    /// <returns></returns>
    public int Pop()
    {
        return secondaryQueue.Dequeue();
    }

    /// <summary>
    /// Returns the element on the top of the stack.
    /// </summary>
    /// <returns></returns>
    public int Top()
    {
        return secondaryQueue.Peek();
    }

    /// <summary>
    /// Returns true if the stack is empty, false otherwise.
    /// </summary>
    /// <returns></returns>
    public bool Empty()
    {
        return secondaryQueue.Count == 0;
    }
}