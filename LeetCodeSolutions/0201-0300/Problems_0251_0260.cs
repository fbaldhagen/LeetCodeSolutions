using LeetCodeSolutions.Structures;

namespace LeetCodeSolutions._0201_0300;

public class Problems_0251_0260
{
    // 251 - 256 skipped (Premium)

    /// <summary>
    /// Problem 257
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public static IList<string> BinaryTreePaths(TreeNode root)
    {
        IList<string> paths = [];

        if (root != null)
        {
            DFS(root, "", paths);
        }

        return paths;

        // Recursive DFS helper method
        static void DFS(TreeNode node, string path, IList<string> paths)
        {
            // If both children are null, add the value and return.
            if (node.left is null && node.right is null)
            {
                paths.Add(path + node.val);
                return;
            }

            // If there is a left child, call the recursive function on it, with added path.
            if (node.left is not null)
            {
                DFS(node.left, path + node.val + "->", paths);
            }

            // Same for right child (if there is one)
            if (node.right is not null)
            {
                DFS(node.right, path + node.val + "->", paths);
            }
        }
    }
}
