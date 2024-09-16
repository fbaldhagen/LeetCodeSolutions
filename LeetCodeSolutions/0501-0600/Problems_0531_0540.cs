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

    /// <summary>
    /// Problem 539
    /// </summary>
    /// <param name="timePoints"></param>
    /// <returns></returns>
    public static int FindMinDifference(IList<string> timePoints)
    {
        // Array to mark minutes
        bool[] timeSeen = new bool[1440];

        // Convert each time to minutes and check for duplicates
        foreach (var time in timePoints)
        {
            var parts = time.Split(':');
            int minutes = int.Parse(parts[0]) * 60 + int.Parse(parts[1]);

            if (timeSeen[minutes])
            {
                return 0;  // Duplicate found, minimum difference is 0
            }

            timeSeen[minutes] = true;
        }

        // Traverse the boolean array to find the minimum time difference
        int firstTime = -1, prevTime = -1, minDifference = int.MaxValue;

        for (int i = 0; i < 1440; i++)
        {
            if (timeSeen[i])
            {
                if (firstTime == -1)
                {
                    firstTime = i;
                }

                if (prevTime != -1)
                {
                    minDifference = Math.Min(minDifference, i - prevTime);
                }

                prevTime = i;
            }
        }

        // Handle circular difference (difference between last and first time)
        minDifference = Math.Min(minDifference, (1440 - prevTime + firstTime));

        return minDifference;
    }
}
