using LeetCodeSolutions.Structures;

namespace LeetCodeSolutions._0101_0200;

public class Problems_0101_0110
{
    /// <summary>
    /// Problem 101
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public static bool IsSymmetric(TreeNode root)
    {
        // If the tree is empty its symmetric
        if (root is null)
        {
            return true;
        }

        // Use a queue to keep track of nodes
        Queue<TreeNode> queue = new();
        queue.Enqueue(root.left);
        queue.Enqueue(root.right);

        while (queue.Count > 0)
        {
            // Dequeue two nodes at a time
            TreeNode left = queue.Dequeue();
            TreeNode right = queue.Dequeue();

            // Move on if both nodes are null
            if (left is null && right is null)
            {
                continue;
            }
            // If the value isnt the same or one is null but not the other it's not symmetric
            if (left is null || right is null || left.val != right.val)
            {
                return false;
            }

            // The order in which we enqueue ensures that the nodes we dequeue will be 
            // the correct nodes to compare
            queue.Enqueue(left.left);
            queue.Enqueue(right.right);
            queue.Enqueue(left.right);
            queue.Enqueue(right.left);
        }

        return true;
    }

    /// <summary>
    /// Problem 102
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public static IList<IList<int>> LevelOrder(TreeNode root)
    {
        IList<IList<int>> result = [];

        if (root is null)
        {
            return result;
        }

        // Set up a queue and start with the root
        Queue<TreeNode> queue = new();
        queue.Enqueue(root);

        // Run as long as there are nodes in the queue
        while (queue.Count > 0)
        {
            // Get the number of nodes currently in the queue (all on the same level)
            int levelSize = queue.Count;

            // Create new List to hold values on this level
            IList<int> levelNodes = [];

            for (int i = 0; i < levelSize; i++)
            {
                // Dequeue the nodes from left to right and add the value to the list.
                TreeNode node = queue.Dequeue();
                levelNodes.Add(node.val);

                // When a node has been dequeued and its value added, add the children
                // (if they are not null) to the queue, left to right.
                if (node.left is not null)
                {
                    queue.Enqueue(node.left);
                }
                if (node.right is not null)
                {
                    queue.Enqueue(node.right);
                }
            }
            // After going through all the nodes in a level, add that list to the result list.
            result.Add(levelNodes);
        }

        return result;
    }

    /// <summary>
    /// Problem 103
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public static IList<IList<int>> ZigzagLevelOrder(TreeNode root)
    {
        IList<IList<int>> result = [];

        if (root is null)
        {
            return result;
        }

        // Set up a queue and start with the root
        Queue<TreeNode> queue = new();
        queue.Enqueue(root);

        // Keep track if we should go left-to-right or right-to-left
        bool leftToRight = true;

        // Run as long as there are nodes in the queue
        while (queue.Count > 0)
        {
            // Get the number of nodes currently in the queue (all on the same level)
            int levelSize = queue.Count;

            // Create new List to hold values on this level
            IList<int> levelNodes = [];

            for (int i = 0; i < levelSize; i++)
            {
                // Dequeue the nodes from left to right and add the value to the list.
                TreeNode node = queue.Dequeue();

                // If left to right, add as usual (Append). If right to left, insert it at index 0.
                if (leftToRight)
                {
                    levelNodes.Add(node.val);
                }
                else
                {
                    levelNodes.Insert(0, node.val);
                }

                // When a node has been dequeued and its value added, add the children
                // (if they are not null) to the queue, left to right.
                if (node.left is not null)
                {
                    queue.Enqueue(node.left);
                }
                if (node.right is not null)
                {
                    queue.Enqueue(node.right);
                }
            }
            // After going through all the nodes in a level, add that list to the result list.
            result.Add(levelNodes);
            // Toggle leftToRight to ensure a zigzag behavior
            leftToRight = !leftToRight;
        }

        return result;
    }

    /// <summary>
    /// Problem 104
    /// </summary>
    /// <returns></returns>
    public static int MaxDepth(TreeNode root)
    {
        // If a node is - return 0
        if (root is null)
        {
            return 0;
        }

        // Calculate the maximum left and right depths recursively
        int leftDepth = MaxDepth(root.left);
        int rightDepth = MaxDepth(root.right);
        // Return the maximum of the left and the right depths + 1 to get the maximum depth.
        return Math.Max(leftDepth, rightDepth) + 1;
    }

    /// <summary>
    /// Problem 105
    /// </summary>
    /// <param name="preorder">Array representing the preorder traversal of the binary tree</param>
    /// <param name="inorder">Array representing the inorder traversal of the binary tree</param>
    /// <returns>The root node of the constructed binary tree</returns>
    public static TreeNode BuildTree(int[] preorder, int[] inorder)
    {
        // Call the helper function to build the binary tree recursively
        return Helper(0, preorder.Length - 1, 0, inorder.Length - 1, preorder, inorder);

        // Helper function to build the binary tree recursively
        static TreeNode Helper(int preStart, int preEnd, int inStart, int inEnd, int[] preorder, int[] inorder)
        {
            // Base case: If either of the traversal arrays is empty, return null
            if (preStart > preEnd || inStart > inEnd)
            {
                return null;
            }

            // The value at the current root node is the first element in the preorder traversal
            int rootValue = preorder[preStart];
            // Create a new TreeNode with the root value
            TreeNode root = new(rootValue);

            // Find the index of the root value in the inorder traversal
            int rootIndexInInorder = Array.IndexOf(inorder, rootValue);

            // Calculate the size of the left subtree
            int leftSubtreeSize = rootIndexInInorder - inStart;

            // Recursively build the left subtree
            root.left = Helper(preStart + 1, preStart + leftSubtreeSize, inStart, rootIndexInInorder - 1, preorder, inorder);
            // Recursively build the right subtree
            root.right = Helper(preStart + leftSubtreeSize + 1, preEnd, rootIndexInInorder + 1, inEnd, preorder, inorder);

            // Return the root node of the current subtree
            return root;
        }
    }

    /// <summary>
    /// Problem 106
    /// </summary>
    /// <param name="inorder"></param>
    /// <param name="postorder"></param>
    /// <returns></returns>
    public static TreeNode BuildTree2(int[] inorder, int[] postorder)
    {
        // Call the helper function to build the binary tree recursively
        return Helper(0, inorder.Length - 1, 0, postorder.Length - 1, inorder, postorder);

        static TreeNode Helper(int inStart, int inEnd, int postStart, int postEnd, int[] inorder, int[] postorder)
        {
            // Base case: If either of the traversal arrays is empty, return null
            if (inStart > inEnd || postStart > postEnd)
            {
                return null;
            }

            // The value at the current root node is the last element in the postorder traversal
            int rootValue = postorder[postEnd];
            // Create a new TreeNode with the root value
            TreeNode root = new(rootValue);

            // Find the index of the root value in the inorder traversal
            int rootIndexInInorder = Array.IndexOf(inorder, rootValue);

            // Calculate the size of the left subtree
            int leftSubtreeSize = rootIndexInInorder - inStart;

            // Recursively build the left subtree
            root.left = Helper(inStart, rootIndexInInorder - 1, postStart, postStart + leftSubtreeSize - 1, inorder, postorder);
            // Recursively build the right subtree
            root.right = Helper(rootIndexInInorder + 1, inEnd, postStart + leftSubtreeSize, postEnd - 1, inorder, postorder);

            // Return the root node of the current subtree
            return root;
        }
    }

    /// <summary>
    /// Problem 107
    /// </summary>
    /// <returns></returns>
    public static IList<IList<int>> LevelOrderBottom(TreeNode root)
    {
        IList<IList<int>> result = [];

        if (root is null)
        {
            return result;
        }

        // Set up a queue and enqueue the root node
        Queue<TreeNode> queue = new();
        queue.Enqueue(root);

        // Run as long as we have nodes in the queue. The idea is to go through each tree level and add the node values from left to right to the result,
        // as well as add their child nodes to the queue. We make sure to insert at index 0 so that the leaves will end up at the beginning and the root at the end
        while (queue.Count > 0)
        {
            // Get the current number of nodes in the queue. This is the number of nodes in the current level.
            int levelSize = queue.Count;
            IList<int> levelNodes = [];

            // Run as many times as there are nodes in the level.
            // We dequeue and process the ones in the level whilst adding their children, ie. the next level, to the queue.
            for (int i = 0; i < levelSize; i++)
            {
                TreeNode node = queue.Dequeue();
                levelNodes.Add(node.val);

                if (node.left is not null)
                {
                    queue.Enqueue(node.left);
                }
                if (node.right is not null)
                {
                    queue.Enqueue(node.right);
                }
            }
            // Make sure to insert at index 0 to have a bottom-up order. 
            result.Insert(0, levelNodes);
        }

        return result;
    }

    /// <summary>
    /// Problem 108
    /// </summary>
    /// <returns></returns>
    public static TreeNode SortedArrayToBST(int[] nums)
    {
        return SortedArrayHelper(nums, 0, nums.Length - 1);

        // Helper function to make use of recursion
        static TreeNode SortedArrayHelper(int[] nums, int left, int right)
        {
            // Terminal condition, we're done processing the array
            if (left > right)
            {
                return null;
            }

            // Calculate the middle index and add its value as the root
            int mid = left + (right - left) / 2;
            TreeNode root = new(nums[mid])
            {
                // Calculate the left and right subtrees recursively
                left = SortedArrayHelper(nums, left, mid - 1),
                right = SortedArrayHelper(nums, mid + 1, right)
            };

            return root;
        }
    }

    /// <summary>
    /// Problem 109
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public static TreeNode SortedListToBST(ListNode head)
    {
        // Two edge cases where head is either null or just one node.
        if (head is null)
        {
            return null;
        }
        if (head.next is null)
        {
            return new TreeNode(head.val);
        }

        // Set up "helper" nodes
        ListNode previous = null;
        ListNode slow = head;
        ListNode fast = head;

        // Find the middle node using fast and slow pointers
        while (fast is not null && fast.next is not null)
        {
            previous = slow;
            slow = slow.next;
            fast = fast.next.next;
        }
        // Create a new root with the middle node value
        TreeNode root = new(slow.val);

        // "Cut off" the linked list, effectively creating 2 separate lists
        previous.next = null;
        // Recursively create left and right subtrees using the 2 separated lists, (head - previous) and (slow.next - tail).
        root.left = SortedListToBST(head);
        root.right = SortedListToBST(slow.next);

        return root;
    }

    /// <summary>
    /// Problem 110
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public static bool IsBalanced(TreeNode root)
    {
        if (root is null)
        {
            return true;
        }

        int leftHeight = Height(root.left);
        int rightHeight = Height(root.right);

        if (Math.Abs(leftHeight - rightHeight) > 1)
        {
            return false;
        }

        return IsBalanced(root.left) && IsBalanced(root.right);


        static int Height(TreeNode node)
        {
            if (node is null)
            {
                return 0;
            }

            int leftHeight = Height(node.left);
            int rightHeight = Height(node.right);

            return Math.Max(leftHeight, rightHeight) + 1;
        }
    }
}
