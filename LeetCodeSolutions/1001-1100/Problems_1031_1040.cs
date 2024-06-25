using LeetCodeSolutions.Structures;

namespace LeetCodeSolutions._1001_1100;

public class Problems_1031_1040
{
    /// <summary>
    /// Problem 1038
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public static TreeNode BstToGst(TreeNode root)
    {
        // Track the sum of the Tree
        int sum = 0;

        // Use a helper method to traverse the tree, add to the sum and modify the node values.
        BstToGstHelper(root);

        // Return the modified tree
        return root;

        // Helper method to traverse and modify values.
        // Start with larger values, add to sum, update node value to current value + sum.
        TreeNode? BstToGstHelper(TreeNode? root)
        {
            if (root is not null)
            {
                BstToGstHelper(root.right);
                sum += root.val;
                root.val = sum;
                BstToGstHelper(root.left);
            }

            return root;
        }
    }
}
