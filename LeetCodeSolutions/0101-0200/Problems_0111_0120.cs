using LeetCodeSolutions.Structures;

namespace LeetCodeSolutions._0101_0200;

public class Problems_0111_0120
{
    /// <summary>
    /// Problem 111
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public static int MinDepth(TreeNode root)
    {
        // If the root (or another node) is empty we return 0.
        if (root is null)
        {
            return 0;
        }

        // If the left subtree is empty, only consider the right subtree
        if (root.left is null)
        {
            // Calculate the depth
            return MinDepth(root.right) + 1;
        }

        // If the right subtree is empty, only consider the left subtree
        if (root.right is null)
        {
            return MinDepth(root.left) + 1;
        }

        // If both subtrees are non-empty, return the minimum depth of the two subtrees
        // The "+ 1" represents the current level (root node) being included in the calculation
        return Math.Min(MinDepth(root.left), MinDepth(root.right)) + 1;
    }

    /// <summary>
    /// Problem 112
    /// </summary>
    /// <param name="root"></param>
    /// <param name="targetSum"></param>
    /// <returns></returns>
    public static bool HasPathSum(TreeNode root, int targetSum)
    {
        // If the root is null return false
        if (root is null)
        {
            return false;
        }

        // Check if we're at a leaf and if the accumulated values would add up to targetSum
        if (root.right is null && root.left is null && root.val == targetSum)
        {
            return true;
        }

        // Call the method for the left and right subtrees with the target sum adjusted. 
        return HasPathSum(root.left, targetSum - root.val) || HasPathSum(root.right, targetSum - root.val);
    }

    /// <summary>
    /// Problem 113
    /// </summary>
    /// <param name="root"></param>
    /// <param name="targetSum"></param>
    /// <returns></returns>
    public static IList<IList<int>> PathSum(TreeNode root, int targetSum)
    {
        // Set up lists for result and current path
        IList<IList<int>> result = new List<IList<int>>();
        List<int> currentPath = new();
        // Use backtracking to try paths and discarding ones that dont work.
        Backtrack(root, targetSum, currentPath, result);
        return result;

        static void Backtrack(TreeNode node, int targetSum, List<int> currentPath, IList<IList<int>> result)
        {
            // If we end up on "null" it's not a part of solution
            if (node is null)
            {
                return;
            }

            // Add the value of the current node.
            currentPath.Add(node.val);

            // Check if we're at a leaf and end up at targetSum. If we do we add the path to the "result" list.
            if (node.left is null && node.right is null && node.val == targetSum)
            {
                result.Add(new List<int>(currentPath));
            }

            // Call the method for the left and right subtrees with the target sum adjusted
            Backtrack(node.left, targetSum - node.val, currentPath, result);
            Backtrack(node.right, targetSum - node.val, currentPath, result);

            // Remove the node at the end
            currentPath.RemoveAt(currentPath.Count - 1);
        }
    }

    /// <summary>
    /// Problem 114
    /// </summary>
    public static void Flatten(TreeNode root)
    {
        // If the root is null or if it's a leaf node no flattening is needed
        if (root is null || (root.left is null && root.right is null))
        {
            return;
        }

        // Flatten the left subtree
        if (root.left is not null)
        {
            Flatten(root.left);

            // Store the right subtree
            TreeNode tempRight = root.right;
            root.right = root.left;
            root.left = null;

            // Find the rightmost node of the flattened left subtree
            TreeNode rightmost = root.right;

            while (rightmost.right is not null)
            {
                rightmost = rightmost.right;
            }

            // Attach the stored right subtree to the rightmost node of the flattened left subtree
            rightmost.right = tempRight;
        }

        Flatten(root.right);
    }

    /// <summary>
    /// Problem 115
    /// </summary>
    /// <returns></returns>
    public static int NumDistinct(string s, string t)
    {
        int m = s.Length;
        int n = t.Length;

        // Create an mxn 2D dp array to store the number of distinct subsequences
        int[,] dp = new int[m + 1, n + 1];

        // Initialize the first column with 1s (empty string matches any string once)
        for (int i = 0; i <= m; i++)
            dp[i, 0] = 1;

        // Fill the dp array based on the recurrence relation
        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                if (s[i - 1] == t[j - 1])
                {
                    // If the characters match, two choices: include the character or exclude it
                    dp[i, j] = dp[i - 1, j - 1] + dp[i - 1, j];
                }
                else
                {
                    // If the characters don't match, exclude the character
                    dp[i, j] = dp[i - 1, j];
                }
            }
        }

        // The result is stored in the bottom-right cell of the dp array
        return dp[m, n];
    }

    /// <summary>
    /// Problem 116
    /// </summary>
    /// <param name="root"></param>
    public static Node Connect(Node root)
    {
        // Base case where root is null
        if (root is null)
        {
            return root;
        }

        // levelRoot refers to the parent node of each level
        Node levelRoot = root;

        while (levelRoot.left is not null)
        {
            Node curr = levelRoot;

            // Process nodes at the current level
            while (curr is not null)
            {
                // Set the next pointer of the left child the right child
                curr.left.next = curr.right;

                // If there is a next node, set the current right child to point to the "next" nodes left child node
                if (curr.next is not null)
                {
                    curr.right.next = curr.next.left;
                }

                // Move to the next node in the current level
                curr = curr.next;
            }

            // Move to the next level
            levelRoot = levelRoot.left;
        }

        // Return the root node
        return root;
    }

    /// <summary>
    /// Problem 117
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    /// <remarks>Pretty much the same as 116, but not guaranteed to be a perfectly balanced tree.</remarks>
    public static Node ConnectII(Node root)
    {
        // Check if the root is null
        if (root == null)
            return root;

        // Initialize a queue for level order traversal
        Queue<Node> queue = new();
        queue.Enqueue(root);

        // Level order traversal
        while (queue.Count > 0)
        {
            // Get the number of nodes at the current level
            int levelSize = queue.Count;
            // Initialize a pointer to the previous node
            Node prev = null;

            // Process nodes at the current level
            for (int i = 0; i < levelSize; i++)
            {
                // Dequeue a node from the queue
                Node current = queue.Dequeue();

                // If there's a previous node, set its next pointer to the current node
                if (prev != null)
                {
                    prev.next = current;
                }

                // Update the previous node pointer to the current node
                prev = current;

                // Enqueue the left child if exists
                if (current.left != null)
                    queue.Enqueue(current.left);
                // Enqueue the right child if exists
                if (current.right != null)
                    queue.Enqueue(current.right);
            }
        }

        // Return the modified root node
        return root;
    }

    /// <summary>
    /// Problem 118
    /// </summary>
    /// <param name="numRows"></param>
    /// <returns></returns>
    public static IList<IList<int>> Generate(int numRows)
    {
        // Initialize an array of arrays to store the Pascal's triangle
        int[][] triangle = new int[numRows][];

        // Populate the triangle with arrays representing each row
        for (int i = 0; i < numRows; i++)
        {
            triangle[i] = new int[i + 1];
            triangle[i][0] = 1; // First element of each row is 1
            triangle[i][i] = 1; // Last element of each row is 1

            // Calculate the elements in between
            for (int j = 1; j < i; j++)
            {
                triangle[i][j] = triangle[i - 1][j - 1] + triangle[i - 1][j];
            }
        }

        // Convert the array of arrays to a list of lists
        IList<IList<int>> resultList = [];
        foreach (int[] rowArray in triangle)
        {
            resultList.Add(rowArray.ToList());
        }

        return resultList;
    }

    /// <summary>
    /// Problem 119
    /// </summary>
    /// <param name="rowIndex"></param>
    /// <returns></returns>
    public static IList<int> GetRow(int rowIndex)
    {
        // Initialize an array to store the current row
        int[] row = new int[rowIndex + 1];

        // Set the first element of the row to 1
        row[0] = 1;

        // Calculate the elements in the row
        for (int i = 1; i <= rowIndex; i++)
        {
            // Start from the end to avoid overwriting values prematurely
            for (int j = i; j >= 1; j--)
            {
                row[j] += row[j - 1];
            }
        }

        // Return the calculated row
        return row;
    }

    /// <summary>
    /// Problem 120
    /// </summary>
    /// <param name="triangle"></param>
    /// <returns></returns>
    public static int MinimumTotal(IList<IList<int>> triangle)
    {
        int n = triangle.Count;
        // Solve using 2D dp array
        int[][] dp = new int[n][];

        // Initialize bottom row of dp array
        dp[n - 1] = new int[n];
        for (int i = 0; i < n; i++)
        {
            dp[n - 1][i] = triangle[n - 1][i];
        }

        // Start with the 2nd row from the bottom, work our way up
        for (int i = n - 2; i >= 0; i--)
        {
            dp[i] = new int[i + 1];
            // Left to right in the column. Sum to reach this is current sum + sum in cell same column a row down,
            // or current sum + sum in cell a row down and a column to the right. Minimize this.
            for (int j = 0; j <= i; j++)
            {
                dp[i][j] = triangle[i][j] + Math.Min(dp[i + 1][j], dp[i + 1][j + 1]);
            }
        }

        // Return top value
        return dp[0][0];
    }
}
