using LeetCodeSolutions.Structures;
using System.Text;
namespace LeetCodeSolutions._0001_0100;

public class Problems_0061_0070
{
    /// <summary>
    /// Problem 61
    /// </summary>
    /// <returns></returns>
    public static ListNode RotateRight(ListNode head, int k)
    {
        if (head is null || head.next is null || k == 0)
        {
            return head;
        }

        ListNode tail = head;

        int length = 1;
        while (tail.next is not null)
        {
            tail = tail.next;
            length++;
        }

        // Effective step
        k %= length;
        // no change (rotation looped)
        if (k == 0)
        {
            return head;
        }

        ListNode newTail = head;
        for (int i = 0; i < length - 1 - k; i++)
        {
            newTail = newTail.next;
        }

        ListNode newHead = newTail.next;
        newTail.next = null;
        tail.next = head;

        return newHead;
    }

    /// <summary>
    /// Problem 62
    /// </summary>
    /// <param name="m"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int UniquePaths(int m, int n)
    {
        // initialize 2d array
        int[,] dp = new int[m, n];

        // Base cases
        // Only one way to stay in the top row (consecutive right steps)

        for (int i = 0; i < m; i++)
        {
            dp[i, 0] = 1;
        }
        // and only one way to stay in the left col (consecutive down steps)
        for (int i = 0; i < n; i++)
        {
            dp[0, i] = 1;
        }

        // the number of unique ways to reach dp[i, j] is the number of ways to reach the 
        // cell above (dp[i - 1, j]) plus the number of ways to reach the cell to the left (dp[i, j - 1])
        for (int i = 1; i < m; i++)
        {
            for (int j = 1; j < n; j++)
            {
                dp[i, j] = dp[i - 1, j] + dp[i, j - 1];
            }
        }

        return dp[m - 1, n - 1];
    }

    /// <summary>
    /// Problem 63
    /// </summary>
    /// <param name="obstacleGrid"></param>
    /// <returns></returns>
    public static int UniquePathsWithObstacles(int[][] obstacleGrid)
    {
        // dimensions of the grid, and the corresponding dp array.
        int m = obstacleGrid.Length;
        int n = obstacleGrid[0].Length;

        int[,] dp = new int[m, n];

        // if start position is an obstacle there is no way to reach bottom right.
        if (obstacleGrid[0][0] == 1)
        {
            return 0;
        }

        // Start position
        dp[0, 0] = 1;

        // Base cases along the edges.
        // Left column
        for (int i = 1; i < m; i++)
        {
            if (obstacleGrid[i][0] == 1)
            {
                dp[i, 0] = 0;
            }
            else
            {
                dp[i, 0] = dp[i - 1, 0];
            }
        }
        // Top row
        for (int i = 1; i < n; i++)
        {
            if (obstacleGrid[0][i] == 1)
            {
                dp[0, i] = 0;
            }
            else
            {
                dp[0, i] = dp[0, i - 1];
            }
        }

        // Fill the dp array.
        // There are dp[i - 1, j] + dp[i, j - 1] ways reach obstacleGrid[i][j]
        for (int i = 1; i < m; i++)
        {
            for (int j = 1; j < n; j++)
            {
                if (obstacleGrid[i][j] == 1)
                {
                    dp[i, j] = 0;
                }
                else
                {
                    dp[i, j] = dp[i - 1, j] + dp[i, j - 1];
                }
            }
        }

        // Return the bottom right value
        return dp[m - 1, n - 1];
    }

    /// <summary>
    /// Problem 64
    /// </summary>
    /// <param name="grid"></param>
    /// <returns></returns>
    public static int MinPathSum(int[][] grid)
    {
        // Dimension of the 2D array / dp array
        int m = grid.Length;
        int n = grid[0].Length;

        // initialize the dp array
        int[,] dp = new int[m, n];

        // Start value in top left
        dp[0, 0] = grid[0][0];

        // Sum in dp[i, 0] is value in the leftmost cell in the previous row + value in current cell (also leftmost)
        for (int i = 1; i < m; i++)
        {
            dp[i, 0] = dp[i - 1, 0] + grid[i][0];
        }

        // Sum in dp[0, i] is value in the top cell in the previous col + value in current cell (also top)
        for (int i = 1; i < n; i++)
        {
            dp[0, i] = dp[0, i - 1] + grid[0][i];
        }

        for (int i = 1; i < m; i++)
        {
            for (int j = 1; j < n; j++)
            {
                dp[i, j] = Math.Min(dp[i - 1, j], dp[i, j - 1]) + grid[i][j];
            }
        }

        return dp[m - 1, n - 1];
    }

    /// <summary>
    /// Problem 65
    /// </summary>
    /// <returns></returns>
    public static bool IsNumber(string s)
    {
        bool seenDecimalPoint = false;
        bool seenE = false;
        bool seenNumber = false;
        bool seenNumberAfterE = false;

        // Get rid of leading and trailing whitespace
        s = s.Trim();

        if (s.Length == 0)
        {
            return false;
        }

        for (int i = 0; i < s.Length; i++)
        {
            char c = s[i];

            if (char.IsDigit(c))
            {
                seenNumber = true;
                seenNumberAfterE = true;
            }
            else if (c == '.')
            {
                if (seenDecimalPoint || seenE)
                {
                    return false;
                }

                seenDecimalPoint = true;
            }
            else if (c == 'e' || c == 'E')
            {
                if (seenE || !seenNumber)
                {
                    return false;
                }
                seenE = true;
                seenNumberAfterE = false;
            }
            else if (c == '-' || c == '+')
            {
                if (i > 0 && s[i - 1] != 'e' && s[i - 1] != 'E')
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        return seenNumber && seenNumberAfterE;
    }

    /// <summary>
    /// Problem 66
    /// </summary>
    /// <param name="digits"></param>
    /// <returns></returns>
    public static int[] PlusOne(int[] digits)
    {
        int n = digits.Length;

        for (int i = n - 1; i >= 0; i--)
        {
            // if the current digit isnt 9, add 1 to it, and return the array
            if (digits[i] < 9)
            {
                digits[i] = digits[i] + 1;
                return digits;
            }

            // if it is 9, set it to 0 and move on to the next digit
            digits[i] = 0;
        }


        // if we traverse the entire array then the resulting array will be 
        // of size n + 1, with a leading 1 and the rest zeros
        int[] result = new int[n + 1];
        result[0] = 1;

        return result;
    }

    /// <summary>
    /// Problem 67
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static string AddBinary(string a, string b)
    {
        // Initialize carry to 0
        int carry = 0;

        // Initialize indices for strings a and b
        int i = a.Length - 1;
        int j = b.Length - 1;

        // StringBuilder to store the result
        StringBuilder result = new();

        // Iterate through the strings a and b and perform binary addition
        // Start from the least significant digit (rightmost) and move towards the most significant digit (leftmost)
        while (i >= 0 || j >= 0 || carry > 0)
        {
            // Initialize sum with the current value of carry
            int sum = carry;

            // Add the corresponding digits from string a and update the sum
            if (i >= 0)
            {
                sum += a[i] - '0'; // Convert char to int
                i--; // Move to the next digit in string a
            }

            // Add the corresponding digits from string b and update the sum
            if (j >= 0)
            {
                sum += b[j] - '0'; // Convert char to int
                j--; // Move to the next digit in string b
            }

            // Insert the least significant digit of the sum at the beginning of the result
            result.Insert(0, sum % 2);

            // Update the carry for the next iteration
            carry = sum / 2;
        }

        // Convert the result to string and return
        return result.ToString();
    }

    /// <summary>
    /// Problem 68
    /// </summary>
    /// <param name="words"></param>
    /// <param name="maxWidth"></param>
    /// <returns></returns>
    public static IList<string> FullJustify(string[] words, int maxWidth)
    {
        // Initialize a list to store the resulting justified lines
        List<string> result = new();

        // Initialize index to 0 to keep track of the current word
        int i = 0;

        // Loop through the words array until all words are processed
        while (i < words.Length)
        {
            // Initialize j to i + 1, which represents the index of the next word in the line
            int j = i + 1;

            // Initialize lineLength to the length of the first word of the line
            int lineLength = words[i].Length;

            // Find the index of the last word that can fit in the current line without exceeding maxWidth
            while (j < words.Length && lineLength + 1 + words[j].Length <= maxWidth)
            {
                lineLength += 1 + words[j].Length;
                j++;
            }

            // Calculate the number of words in the current line
            int numWords = j - i;

            // Calculate the number of spaces to add to the line
            int numSpaces = maxWidth - lineLength + numWords - 1;

            // Create a StringBuilder to construct the current justified line
            StringBuilder sb = new();

            // If it's the last line or there's only one word in the line, left-justify the line
            if (numWords == 1 || j == words.Length)
            {
                sb.Append(words[i]);
                for (int k = i + 1; k < j; k++)
                {
                    sb.Append(' '); // Append a space between words
                    sb.Append(words[k]); // Append the next word
                }
                sb.Append(new string(' ', maxWidth - sb.Length)); // Add extra spaces to the right of the line until it reaches maxWidth
            }
            else
            {
                // Calculate the number of spaces to distribute evenly between words
                int spacePerWord = numSpaces / (numWords - 1);

                // Calculate the number of extra spaces due to uneven distribution
                int extraSpaces = numSpaces % (numWords - 1);

                for (int k = i; k < j; k++)
                {
                    sb.Append(words[k]); // Append the next word

                    if (k < j - 1)
                    {
                        sb.Append(new string(' ', spacePerWord)); // Append spaces between words
                        if (extraSpaces > 0)
                        {
                            sb.Append(' '); // Append an extra space if available
                            extraSpaces--;
                        }
                    }
                }
            }

            // Add the constructed line to the result list
            result.Add(sb.ToString());

            // Update index to the index of the next word that needs to be processed
            i = j;
        }

        // Return the list of fully justified lines
        return result;
    }

    /// <summary>
    /// Problem 69
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static int MySqrt(int x)
    {
        if (x < 2)
        {
            return x;
        }

        int left = 1;
        int right = x / 2;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            long square = (long)mid * mid;

            if (square == x)
            {
                return mid;
            }
            else if (square < x)
            {
                left = mid + 1;
            }
            else if (square > x)
            {
                right = mid - 1;
            }
        }

        return right;
    }

    /// <summary>
    /// Problem 70
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int ClimbStains(int n)
    {
        // one way to reach 1 step (1), two ways to reach 2 (1-1, 2)
        if (n == 1) return 1;
        if (n == 2) return 2;

        // dp[i] is ways to get to step i. Therefore array of size n + 1
        int[] dp = new int[n + 1];

        // Same logic as above..
        dp[1] = 1;
        dp[2] = 2;

        // Fill dp array. Can reach each cell from one or two steps behind
        for (int i = 3; i <= n; i++)
        {
            dp[i] = dp[i - 1] + dp[i - 2];
        }

        // Return last step.
        return dp[n];
    }
}
