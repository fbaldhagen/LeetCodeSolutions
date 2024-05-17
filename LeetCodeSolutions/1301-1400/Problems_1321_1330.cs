using LeetCodeSolutions.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeSolutions._1301_1400;

public class Problems_1321_1330
{
    /// <summary>
    /// Problem 1325
    /// </summary>
    /// <param name="root"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static TreeNode RemoveLeafNodes(TreeNode root, int target)
    {
        // Helper function to recursively remove leaf nodes with the target value
        static TreeNode RemoveLeafNodesHelper(TreeNode node, int target)
        {
            if (node == null)
            {
                return null;
            }

            // Recursively process the left and right subtrees
            node.left = RemoveLeafNodesHelper(node.left, target);
            node.right = RemoveLeafNodesHelper(node.right, target);

            // If the current node becomes a leaf and has the target value, remove it
            if (node.left == null && node.right == null && node.val == target)
            {
                return null;
            }

            return node;
        }

        // Start the recursive process from the root node
        return RemoveLeafNodesHelper(root, target);
    }
}
