using LeetCodeSolutions.Structures;
namespace LeetCodeSolutions._0001_0100;

public class Problems_0091_0100
{
    /// <summary>
    /// Problem 91
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static int NumDecodings(string s)
    {
        int n = s.Length;
        // Initialize dynamic programming array to store the number of ways to decode up to each position
        int[] dp = new int[n + 1];
        dp[0] = 1; // Base case: empty string has 1 way to decode

        // Iterate through the string
        for (int i = 1; i <= n; i++)
        {
            // Check if the current digit can be decoded as a single character
            if (s[i - 1] != '0')
            {
                dp[i] += dp[i - 1];
            }

            // Check if the current and previous digits can be decoded as a two-digit number
            if (i > 1 && s[i - 2] != '0' && int.Parse(s.Substring(i - 2, 2)) <= 26)
            {
                dp[i] += dp[i - 2];
            }
        }

        // Return the number of ways to decode the entire string
        return dp[n];
    }

    /// <summary>
    /// Problem 92
    /// </summary>
    /// <param name="head"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static ListNode ReverseBetween(ListNode head, int left, int right)
    {
        // No need to reverse if there are less than 2 nodes (or if left = right)
        if (head is null || head.next is null || left == right)
        {
            return head;
        }

        // Dummy node as a placeholder
        ListNode dummy = new()
        {
            next = head
        };

        // Two pointers curr and prev to point to "left-th" node and the one before it
        ListNode prev = dummy;
        ListNode curr = head;

        // Move the prev and curr pointers to their appropriate positions before starting the reversing process
        for (int i = 1; i < left; i++)
        {
            prev = curr; // move the prev pointer to the (left - 1)-th node
            curr = curr.next; // move the curr pointer to the left-th node

        }

        // Reverse nodes between left and right
        for (int i = left; i < right; i++)
        {
            // Store the next node to be reversed
            ListNode next = curr.next;
            // Move the next pointer of curr to the node after the next node
            curr.next = next.next;
            // Move the next pointer of the next node to point to the node before curr
            next.next = prev.next;
            // Move the next pointer of prev to the next node
            prev.next = next;
        }

        return dummy.next;
    }

    /// <summary>
    /// Problem 93
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static IList<string> RestoreIpAddresses(string s)
    {
        IList<string> result = new List<string>();
        Backtrack(s, 0, 0, new int[4], result);

        return result;

        static void Backtrack(string s, int index, int segments, int[] currentSegments, IList<string> validIPs)
        {
            if (index == s.Length && segments == 4)
            {
                validIPs.Add(string.Join(".", currentSegments));
                return;
            }

            if (segments == 4 || index == s.Length)
            {
                return;
            }

            int num = 0;

            for (int i = index; i < Math.Min(index + 3, s.Length); i++)
            {
                num = num * 10 + (s[i] - '0');

                if (num > 255)
                {
                    break;
                }

                currentSegments[segments] = num;
                Backtrack(s, i + 1, segments + 1, currentSegments, validIPs);

                if (num == 0)
                {
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Problem 94
    /// </summary>
    /// <returns></returns>
    public static IList<int> InorderTraversal(TreeNode root)
    {
        // Initialize an empty list to store the inorder traversal result
        IList<int> result = [];

        // Base case: If the root is null, return the empty result list
        if (root is null)
        {
            return result;
        }

        // Initialize a stack to perform iterative inorder traversal
        Stack<TreeNode> stack = new();

        // Start with the root node
        TreeNode curr = root;

        // Traverse the tree until both the current node and the stack are empty
        while (curr is not null || stack.Count > 0)
        {
            // Traverse to the leftmost node of the current subtree
            while (curr is not null)
            {
                stack.Push(curr); // Push the current node onto the stack
                curr = curr.left; // Move to the left child of the current node
            }

            // At this point, we've reached the leftmost node
            // Pop the top node from the stack (which is the leftmost node)
            curr = stack.Pop();

            // Add the value of the current node to the result list
            result.Add(curr.val);

            // Move to the right child of the current node
            curr = curr.right;
        }

        // Return the result list containing the inorder traversal
        return result;
    }

    /// <summary>
    /// Problem 95
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static IList<TreeNode> GenerateTrees(int n)
    {
        if (n == 0)
        {
            return new List<TreeNode>();
        }
        return GenerateTrees(1, n);

        static IList<TreeNode> GenerateTrees(int start, int end)
        {
            IList<TreeNode> result = [];

            if (start > end)
            {
                result.Add(null);
                return result;
            }

            // Iterate through each number i in the range [start, end]
            for (int i = start; i <= end; i++)
            {
                // Recursively generate left and right subtrees
                IList<TreeNode> leftSubtrees = GenerateTrees(start, i - 1);
                IList<TreeNode> rightSubtrees = GenerateTrees(i + 1, end);

                // Combine left and right subtrees with the current root i to form BSTs
                foreach (var leftTree in leftSubtrees)
                {
                    foreach (var rightTree in rightSubtrees)
                    {
                        // Create root node with value i
                        TreeNode root = new(i)
                        {
                            left = leftTree, // Assign left subtree
                            right = rightTree // Assign right subtree
                        };
                        result.Add(root); // Add the BST to the result list
                    }
                }
            }

            return result;
        }
    }

    /// <summary>
    /// Problem 96
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int NumTrees(int n)
    {
        if (n <= 1)
        {
            return 1; // Only 1 unique with 0 and 1 node.
        }

        int[] dp = new int[n + 1];
        // Initial values. One unique way with 0 and 1.
        dp[0] = 1;
        dp[1] = 1;

        // Calculate number of unique BSTs for each value of n
        for (int i = 2; i <= n; i++)
        {
            for (int j = 1; j <= i; j++)
            {
                // Calculate the number of unique BSTs formed by j as the root,
                // by multiplying the number of unique BSTs on the left subtree (dp[j - 1])
                // with the number of unique BSTs on the right subtree (dp[i - j]).
                dp[i] += dp[j - 1] * dp[i - j];
            }
        }

        return dp[n];
    }

    /// <summary>
    /// Problem 97
    /// </summary>
    /// <returns></returns>
    public static bool IsInterleave(string s1, string s2, string s3)
    {
        int m = s1.Length;
        int n = s2.Length;
        int len = s3.Length;

        // Check if the lengths of s1 and s2 sum up to the length of s3
        if (m + n != len)
        {
            return false;
        }

        bool[,] dp = new bool[m + 1, n + 1];

        // Initialize dp[0, 0] as true since empty strings can interleave to form an empty string
        dp[0, 0] = true;

        // Check for s1[0...i-1] and empty s2
        for (int i = 1; i <= m; i++)
        {
            dp[i, 0] = dp[i - 1, 0] && s1[i - 1] == s3[i - 1];
        }

        // Check for s2[0...j-1] and empty s1
        for (int j = 1; j <= n; j++)
        {
            dp[0, j] = dp[0, j - 1] && s2[j - 1] == s3[j - 1];
        }

        // Check for interleaving of s1 and s2
        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                dp[i, j] = (dp[i - 1, j] && s1[i - 1] == s3[i + j - 1]) ||
                           (dp[i, j - 1] && s2[j - 1] == s3[i + j - 1]);
            }
        }

        return dp[m, n];
    }

    /// <summary>
    /// Problem 98
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public static bool IsValidBST(TreeNode root)
    {
        // Call the helper method with initial values for minVal and maxVal
        return IsValidBST(root);

        // Helper method to validate whether a subtree is a valid binary search tree
        static bool IsValidBST(TreeNode node, int? minVal = null, int? maxVal = null)
        {
            // Base case: If the node is null, it's a valid BST
            if (node is null)
            {
                return true;
            }

            // Check if the current node's value violates the BST property
            if ((minVal is not null && node.val <= minVal) || (maxVal is not null && node.val >= maxVal))
            {
                return false;
            }

            // Recursively validate the left and right subtrees
            // For the left subtree, update the maximum allowed value as the value of the current node
            // For the right subtree, update the minimum allowed value as the value of the current node
            return IsValidBST(node.left, minVal, node.val) && IsValidBST(node.right, node.val, maxVal);
        }
    }

    /// <summary>
    /// Problem 99
    /// </summary>
    public static void RecoverTree(TreeNode root)
    {
        // Variables to store the misplaced nodes and the previous node during traversal
        TreeNode firstMisplacedNode = null;
        TreeNode secondMisplacedNode = null;
        TreeNode prevNode = null;

        // Perform inorder traversal to find misplaced nodes
        Traverse(root);

        // Swap the values of the misplaced nodes
        (secondMisplacedNode.val, firstMisplacedNode.val) = (firstMisplacedNode.val, secondMisplacedNode.val);

        // Helper function to perform inorder traversal and find misplaced nodes
        void Traverse(TreeNode node)
        {
            if (node == null)
            {
                return;
            }

            // Inorder traversal
            Traverse(node.left);

            // Check if the current node's value is less than the previous node's value
            if (prevNode != null && node.val < prevNode.val)
            {
                // If this is the first misplaced node, record it
                if (firstMisplacedNode == null)
                {
                    firstMisplacedNode = prevNode;
                }
                // If this is the second misplaced node, record it
                secondMisplacedNode = node;
            }

            // Update the previous node
            prevNode = node;

            // Continue inorder traversal
            Traverse(node.right);
        }
    }

    /// <summary>
    /// Problem 100
    /// </summary>
    /// <param name="p"></param>
    /// <param name="q"></param>
    /// <returns></returns>
    public static bool IsSameTree(TreeNode p, TreeNode q)
    {
        // If both nodes are the same reference or both are null, they are considered identical
        if (p == q)
        {
            return true;
        }

        // If either node is null or their values are different, they are not identical
        if (p is null || q is null || p.val != q.val)
        {
            return false;
        }

        // Recursively check the left and right subtrees
        return IsSameTree(p.left, q.left) && IsSameTree(p.right, q.right);
    }
}