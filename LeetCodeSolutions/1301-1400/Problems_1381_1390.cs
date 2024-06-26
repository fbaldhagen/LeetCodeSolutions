using LeetCodeSolutions.Structures;

namespace LeetCodeSolutions._1301_1400;

public class Problems_1381_1390
{
    /// <summary>
    /// Problem 1382
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public static TreeNode? BalanceBST(TreeNode root)
    {
        List<int> nodes = [];
        InOrderTraversal(root, nodes);
        return BuildBalancedBST(nodes, 0, nodes.Count - 1);

        // In order traversal to get a List<int> of node values in increasing order
        void InOrderTraversal(TreeNode? node, List<int> nodes)
        {
            if (node == null)
            {
                return;
            }

            InOrderTraversal(node.left, nodes);
            nodes.Add(node.val);
            InOrderTraversal(node.right, nodes);
        }

        // Use the list of node values to build the balanced BST.
        TreeNode? BuildBalancedBST(List<int> nodes, int left, int right)
        {
            // Terminal condition
            if (left > right)
            {
                return null;
            }

            // Find middle value based on boundaries
            int mid = (left + right) / 2;
            // Create new TreeNode with that value, and call the function again for left and right sub-trees
            TreeNode root = new(nodes[mid])
            {
                left = BuildBalancedBST(nodes, left, mid - 1),
                right = BuildBalancedBST(nodes, mid + 1, right)
            };

            return root;
        }
    }
}
