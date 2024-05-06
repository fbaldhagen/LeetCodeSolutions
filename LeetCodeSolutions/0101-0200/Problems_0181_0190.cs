namespace LeetCodeSolutions._0101_0200;

public class Problems_0181_0190
{
    // Problem 181
    // SELECT e.name AS Employee
    // FROM Employee e
    // JOIN Employee m ON e.managerId = m.id
    // WHERE e.salary > m.salary;

    // Problem 182
    // SELECT email
    // FROM Person
    // GROUP BY email
    // HAVING COUNT(*) > 1;

    // Problem 183
    // SELECT c.name AS Customers
    // FROM Customers c
    // LEFT JOIN Orders o ON c.id = o.customerId
    // WHERE o.id IS NULL;

    // Problem 184
    // SELECT d.name AS Department, e.name AS Employee, e.salary AS Salary
    // FROM Employee e
    // JOIN Department d ON e.departmentId = d.id
    // JOIN (
    //     SELECT departmentId, MAX(salary) AS max_salary
    //     FROM Employee
    //     GROUP BY departmentId
    // ) AS max_salaries
    // ON e.departmentId = max_salaries.departmentId
    // AND e.salary = max_salaries.max_salary;

    // Problem 185
    // WITH RankedSalaries AS(
    //     SELECT
    //         e.id,
    //         e.name AS Employee,
    //         e.salary,
    //         d.name AS Department,
    //         DENSE_RANK() OVER (PARTITION BY e.departmentId ORDER BY e.salary DESC) AS salary_rank
    //     FROM
    //         Employee e
    //     JOIN
    //         Department d ON e.departmentId = d.id
    // )
    // SELECT
    //     Department,
    //     Employee,
    //     salary
    // FROM
    //     RankedSalaries
    // WHERE
    //     salary_rank <= 3;

    // Problem 186 is premium and not included.

    /// <summary>
    /// Problem 187
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static IList<string> FindRepeatedDnaSequences(string s)
    {
        // HashSet to store unique substrings encountered
        HashSet<string> seen = new();
        // HashSet to store repeated substrings
        HashSet<string> repeated = new();

        // Iterate through the DNA sequence to find substrings
        for (int i = 0; i <= s.Length - 10; i++)
        {
            // Extract a substring of length 10 starting from index i
            string substring = s.Substring(i, 10);
            // If the substring is already in 'seen', it's a repeated sequence
            if (!seen.Add(substring))
            {
                // Add the repeated sequence to 'repeated' set
                repeated.Add(substring);
            }
        }

        // Convert the 'repeated' set to a list and return it
        return new List<string>(repeated);
    }

    /// <summary>
    /// Problem 188
    /// </summary>
    /// <param name="k"></param>
    /// <param name="prices"></param>
    /// <returns></returns>
    public static int MaxProfit(int k, int[] prices)
    {
        int n = prices.Length;

        // Zero or 1 transaction would yield no profit. (1 transaction = only sell)
        if (n <= 1)
        {
            return 0;
        }

        int[,] dp = new int[k + 1, n];

        for (int i = 1; i <= k; i++)
        {
            int maxDiff = -prices[0];

            for (int j = 1; j < n; j++)
            {
                dp[i, j] = Math.Max(dp[i, j - 1], prices[j] + maxDiff);
                maxDiff = Math.Max(maxDiff, dp[i - 1, j] - prices[j]);
            }
        }

        return dp[k, n - 1];
    }

    /// <summary>
    /// Problem 189
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    public static void Rotate(int[] nums, int k)
    {
        // Calculate number of effective steps
        k %= nums.Length;

        if (nums == null || nums.Length == 0 || k == 0)
        {
            return;
        }

        Reverse(nums, 0, nums.Length - 1);
        Reverse(nums, 0, k - 1);
        Reverse(nums, k, nums.Length - 1);

        static void Reverse(int[] nums, int start, int end)
        {
            while (start < end)
            {
                (nums[end], nums[start]) = (nums[start], nums[end]);
                start++;
                end--;
            }
        }
    }

    /// <summary>
    /// Problem 190
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static uint ReverseBits(uint n)
    {
        // Initialize the result variable to store the reversed bits
        uint result = 0;
        // Iterate through each bit of the input integer (32 bits for a 32-bit unsigned integer)
        for (int i = 0; i < 32; i++)
        {
            // Left shift the result by one position to make space for the next bit
            result <<= 1;

            // Set the least significant bit of the result to the current bit of the input integer
            result |= n & 1;

            // Right shift the input integer by one position to move to the next bit
            n >>= 1;
        }

        // Return the reversed bits
        return result;
    }
}
