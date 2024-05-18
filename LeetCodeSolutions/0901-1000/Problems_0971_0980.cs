using LeetCodeSolutions.Structures;

namespace LeetCodeSolutions._0901_1000;

public class Problems_0971_0980
{
    /// <summary>
    /// Problem 979
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public static int DistributeCoins(TreeNode root)
    {
        int moves = 0;
        DFS(root);
        return moves;

        // Helper method to perform DFS and calculate the balance of coins
        int DFS(TreeNode node)
        {
            // Base case: if the node is null, return 0 balance
            if (node == null)
            {
                return 0;
            }

            // Recursively calculate the balance of the left subtree
            int leftBalance = DFS(node.left);

            // Recursively calculate the balance of the right subtree
            int rightBalance = DFS(node.right);

            // Calculate the balance of the current node
            // node.val - 1 because each node needs exactly 1 coin
            int nodeBalance = node.val - 1;

            // The total balance is the sum of the current node's balance and its subtrees' balances
            int totalBalance = nodeBalance + leftBalance + rightBalance;

            // The number of moves needed to balance this node is the absolute value of the total balance
            // Add this to the global moves counter
            moves += Math.Abs(totalBalance);

            // Return the total balance to the parent node
            return totalBalance;
        }
    }

}