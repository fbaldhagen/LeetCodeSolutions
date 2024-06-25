using LeetCodeSolutions.Structures;

namespace LeetCodeSolutions._0501_0600;

public class Problems_0531_0540
{
    /// <summary>
    /// Problem 538
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public static TreeNode ConvertBST(TreeNode root)
    {
        // Track the sum of the tree. Start with larger values.
        int sum = 0;

        // Call helper method to add to the sum as well as modify values.
        ConvertBSTHelper(root);

        // Return the modified tree
        return root;

        // Helper method. Start with the largest values and add to the sum.
        // Update the node value to the current value + sum. 
        TreeNode? ConvertBSTHelper(TreeNode? root)
        {
            if (root is not null)
            {
                ConvertBSTHelper(root.right);
                sum += root.val;
                root.val = sum;
                ConvertBSTHelper(root.left);
            }

            return root;
        }
    }
}
