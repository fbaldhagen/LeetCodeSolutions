using LeetCodeSolutions.Structures;

namespace LeetCodeSolutions._0101_0200;

public class Problems_0171_0180
{
    /// <summary>
    /// Problem 171
    /// </summary>
    /// <param name="columnTitle"></param>
    /// <returns></returns>
    public static int TitleToNumber(string columnTitle)
    {
        int result = 0;

        // Iterate through the characters in reverse order
        for (int i = columnTitle.Length - 1; i >= 0; i--)
        {
            result += (columnTitle[i] - 'A' + 1) * (int)Math.Pow(26, columnTitle.Length - 1 - i);
        }

        return result;
    }

    /// <summary>
    /// Problem 172
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int TrailingZeroes(int n)
    {
        int count = 0;

        // Keep dividing n by 5 and accumulating the count
        while (n >= 5)
        {
            count += n / 5;
            n /= 5;
        }

        return count;
    }

    /// Problem 173 in a separate file <see cref="BSTIterator"/>

    /// <summary>
    /// Problem 174
    /// </summary>
    public static int CalculateMinimumHP(int[][] dungeon)
    {
        int m = dungeon.Length;
        int n = dungeon[0].Length;

        // Create a 2D array dp to store the minimum health required to reach the princess from each cell
        int[,] dp = new int[m, n];

        // Start from the princess cell
        dp[m - 1, n - 1] = Math.Max(1, 1 - dungeon[m - 1][n - 1]);

        // Fill the last row
        for (int j = n - 2; j >= 0; j--)
        {
            dp[m - 1, j] = Math.Max(1, dp[m - 1, j + 1] - dungeon[m - 1][j]);
        }

        // Fill the last column
        for (int i = m - 2; i >= 0; i--)
        {
            dp[i, n - 1] = Math.Max(1, dp[i + 1, n - 1] - dungeon[i][n - 1]);
        }

        // Fill the remaining cells from bottom-right to top-left
        for (int i = m - 2; i >= 0; i--)
        {
            for (int j = n - 2; j >= 0; j--)
            {
                int right = Math.Max(1, dp[i, j + 1] - dungeon[i][j]);
                int down = Math.Max(1, dp[i + 1, j] - dungeon[i][j]);
                dp[i, j] = Math.Min(right, down);
            }
        }

        return dp[0, 0];
    }

    // Problem 175
    //    SELECT
    //    p.firstName,
    //    p.lastName,
    //    a.city,
    //    a.state
    //FROM
    //    Person p
    //LEFT JOIN
    //    Address a ON p.personId = a.personId;

    // Problem 176
    //SELECT
    //(SELECT DISTINCT salary
    // FROM Employee
    // ORDER BY salary DESC
    // LIMIT 1 OFFSET 1) AS SecondHighestSalary;

    // Problem 177
    //CREATE FUNCTION getNthHighestSalary(N INT) RETURNS INT
    //BEGIN
    //SET N = N-1;
    //  RETURN(
    //  # Write your MySQL query statement below.
    //        SELECT DISTINCT salary FROM Employee
    //        ORDER BY salary DESC
    //        LIMIT 1 OFFSET N
    //  );
    //END

    //Problem 178
    // select score, (dense_rank() over(order by score desc)) as "rank" from scores

    /// <summary>
    /// Problem 179
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static string LargestNumber(int[] nums)
    {
        Array.Sort(nums, (a, b) => (b.ToString() + a.ToString()).CompareTo(a.ToString() + b.ToString()));

        if (nums[0] == 0)
        {
            return "0";
        }

        return string.Join("", nums);
    }

    // Problem 180
    // SELECT DISTINCT l1.num as ConsecutiveNums
    // FROM Logs l1
    // JOIN Logs l2 on l1.id = l2.id - 1
    // JOIN Logs l3 on l1.id = l3.id - 2
    // WHERE l1.num = l2.num AND l2.num = l3.num;
}

/// <summary>
/// Solution to Problem 173
/// </summary>
public class Problem_173
{
    /// <summary>
    /// Problem 173
    /// </summary>
    public class BSTIterator
    {
        private Stack<TreeNode> stack;
        public BSTIterator(TreeNode root)
        {
            stack = new Stack<TreeNode>();
            LeftMostInorder(root);
        }

        private void LeftMostInorder(TreeNode root)
        {
            while (root != null)
            {
                stack.Push(root);
                root = root.left;
            }
        }

        public bool HasNext()
        {
            return stack.Count > 0;
        }

        public int Next()
        {
            TreeNode topMostNode = stack.Pop();
            if (topMostNode.right != null)
                LeftMostInorder(topMostNode.right);
            return topMostNode.val;
        }
    }

}