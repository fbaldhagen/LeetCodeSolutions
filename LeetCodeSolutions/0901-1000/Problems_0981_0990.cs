using LeetCodeSolutions.Structures;
namespace LeetCodeSolutions._0901_1000;

public class Problems_0981_0990
{
    /// <summary>
    /// Problem 988
    /// </summary>
    /// <returns></returns>
    public static string SmallestFromLeaf(TreeNode root)
    {
        return DFS(root, ""); // Start DFS traversal from the root with an empty string

        static string DFS(TreeNode? node, string path)
        {
            if (node == null)
            {
                return string.Empty;
            }

            // Construct the current path by appending the character represented by the current node's value
            string currentPath = Convert.ToChar('a' + node.val) + path;

            // Check if the current node is a leaf node
            if (node.left == null && node.right == null)
                return currentPath; // Return the current path if it's a leaf node

            // Recursively traverse the left and right subtrees and return the lexicographically smallest path
            string leftPath = DFS(node.left, currentPath);
            string rightPath = DFS(node.right, currentPath);

            // Return the lexicographically smallest path between left and right subtrees
            if (leftPath == "" || rightPath == "")
            {
                return leftPath == "" ? rightPath : leftPath;
            }

            return leftPath.CompareTo(rightPath) < 0 ? leftPath : rightPath;
        }
    }
}