using LeetCodeSolutions.Structures;

namespace LeetCodeSolutions._0901_1000;

public class Problems_0971_0980
{
    /// <summary>
    /// Problem 974
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static int SubarraysDivByK(int[] nums, int k)
    {
        int result = 0;
        Dictionary<int, int> remainderMap = [];
        remainderMap[0] = 1; // Initialize with 0 remainder with frequency 1

        int runningSum = 0;

        for (int i = 0; i < nums.Length; i++)
        {
            runningSum += nums[i];
            int remainder = runningSum % k;

            // Handle negative remainders
            if (remainder < 0)
            {
                remainder += k;
            }

            if (remainderMap.TryGetValue(remainder, out int count))
            {
                result += count;
                remainderMap[remainder] = count + 1;
            }
            else
            {
                remainderMap[remainder] = 1;
            }
        }

        return result;
    }
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
        int DFS(TreeNode? node)
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