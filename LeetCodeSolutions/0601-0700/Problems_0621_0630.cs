using LeetCodeSolutions.Structures;
namespace LeetCodeSolutions._0601_0700;

public class Problems_0621_0630
{
    /// <summary>
    /// Problem 623
    /// </summary>
    /// <param name="root"></param>
    /// <param name="val"></param>
    /// <param name="depth"></param>
    /// <returns></returns>
    public static TreeNode AddOneRow(TreeNode root, int val, int depth)
    {
        // From the problem statement:
        // If depth == 1 that means there is no depth depth - 1 at all, then create a tree node with value val
        // as the new root of the whole original tree, and the original tree is the new root's left subtree.
        if (depth == 1)
        {
            return new TreeNode()
            {
                val = val,
                left = root,
            };
        }

        DFS(root, 1);
        return root;

        void DFS(TreeNode? node, int level)
        {
            // Couldn't reach the level from current branch
            if (node is null)
            {
                return;
            }

            // Check if we're on the right level (i.e. a level up),
            // if we're not - call the method with child nodes and next level
            if (level == depth - 1)
            {
                // Add row of nodes with value val, keep the subtrees as children
                node.left = new TreeNode()
                {
                    val = val,
                    left = node.left
                };

                node.right = new TreeNode()
                {
                    val = val,
                    right = node.right
                };
            }
            else
            {
                DFS(node.left, level + 1);
                DFS(node.right, level + 1);
            }
        }
    }

    /// <summary>
    /// Problem 624
    /// </summary>
    /// <param name="arrays"></param>
    /// <returns></returns>
    public static int MaxDistance(IList<IList<int>> arrays)
    {
        // Initialize variables to store the minimum and maximum values encountered
        int minVal = arrays[0][0];
        int maxVal = arrays[0][arrays[0].Count - 1];

        // Initialize the maximum distance to zero
        int maxDistance = 0;

        // Iterate through each array starting from the second one
        for (int i = 1; i < arrays.Count; i++)
        {
            int firstElement = arrays[i][0];
            int lastElement = arrays[i][arrays[i].Count - 1];

            // Calculate the possible maximum distance using the current array
            maxDistance = Math.Max(maxDistance, Math.Abs(lastElement - minVal));
            maxDistance = Math.Max(maxDistance, Math.Abs(maxVal - firstElement));

            // Update the min and max values based on the current array
            minVal = Math.Min(minVal, firstElement);
            maxVal = Math.Max(maxVal, lastElement);
        }

        return maxDistance;
    }
}