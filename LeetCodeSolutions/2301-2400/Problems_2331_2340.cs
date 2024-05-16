using LeetCodeSolutions.Structures;

namespace LeetCodeSolutions._2301_2400;

public class Problems_2331_2340
{
    /// <summary>
    /// Problem 2331
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static bool EvaluateTree(TreeNode root)
    {
        // Helper function to evaluate the tree
        static bool Evaluate(TreeNode node)
        {
            // If the node is a leaf, return its boolean value
            if (node.left == null && node.right == null)
            {
                return node.val == 1;
            }

            // Recursively evaluate the left and right subtrees
            bool leftValue = Evaluate(node.left);
            bool rightValue = Evaluate(node.right);

            // Apply the operation based on the node's value
            if (node.val == 2) // OR operation
            {
                return leftValue || rightValue;
            }
            else if (node.val == 3) // AND operation
            {
                return leftValue && rightValue;
            }

            throw new InvalidOperationException("Invalid node value");
        }

        // Start evaluation from the root node
        return Evaluate(root);
    }
}
