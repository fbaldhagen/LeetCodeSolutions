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

    /// <summary>
    /// Problem 258
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public static int AddDigits(int num)
    {
        // Solved with number theory https://en.wikipedia.org/wiki/Digital_root
        if (num == 0)
        {
            return 0;
        }
        else
        {
            return 1 + (num - 1) % 9;
        }
    }

    // Skipped 259 (Premium)

    /// <summary>
    /// Problem 260
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int[] SingleNumber(int[] nums)
    {
        // Step 1: XOR all numbers to get XOR of the two single numbers
        // (XOR operation cancels out the bits that occur an even number of times,
        // leaving only the bits that occur an odd number of times.)
        int xor = 0;
        foreach (int num in nums)
        {
            xor ^= num;
        }

        // Step 2: Find a bit where the two single numbers differ
        int diffBit = 1;
        while ((xor & diffBit) == 0)
        {
            diffBit <<= 1;
        }

        // Step 3: Partition numbers into two groups based on the different bit
        int num1 = 0, num2 = 0;
        foreach (int num in nums)
        {
            if ((num & diffBit) != 0)
            {
                num1 ^= num;
            }
            else
            {
                num2 ^= num;
            }
        }

        return [num1, num2];
    }
}
