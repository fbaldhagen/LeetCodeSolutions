using System.Text;

namespace LeetCodeSolutions._0001_0100;

public class Problems_0051_0060
{
    /// <summary>
    /// Problem 51
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static IList<IList<string>> SolveNQueens(int n)
    {
        IList<IList<string>> result = [];

        // set up initial board
        char[][] board = new char[n][];

        for (int i = 0; i < n; i++)
        {
            board[i] = new char[n];
            for (int j = 0; j < n; j++)
            {
                board[i][j] = '.';
            }
        }

        SolveNQueensRecursive(result, board, 0, n);
        return result;

        void SolveNQueensRecursive(IList<IList<string>> result, char[][] board, int row, int n)
        {
            if (row == n)
            {
                result.Add(GenerateBoard(board));
                return;
            }

            for (int col = 0; col < n; col++)
            {
                if (IsValid(board, row, col, n))
                {
                    board[row][col] = 'Q';
                    SolveNQueensRecursive(result, board, row + 1, n);
                    board[row][col] = '.';
                }
            }
        }

        bool IsValid(char[][] board, int row, int col, int n)
        {
            // Check if there is a queen in the same column
            for (int i = 0; i < row; i++)
            {
                if (board[i][col] == 'Q')
                {
                    return false;
                }
            }

            // Check if there is a queen in the left diagonal
            for (int i = row - 1, j = col - 1; i >= 0 && j >= 0; i--, j--)
            {
                if (board[i][j] == 'Q')
                {
                    return false;
                }
            }

            // Check if there is a queen in the right diagonal
            for (int i = row - 1, j = col + 1; i >= 0 && j < n; i--, j++)
            {
                if (board[i][j] == 'Q')
                {
                    return false;
                }
            }

            return true;
        }

        IList<string> GenerateBoard(char[][] board)
        {
            IList<string> result = [];
            foreach (var row in board)
            {
                result.Add(new string(row));
            }
            return result;
        }
    }

    /// <summary>
    /// Problem 52
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int TotalNQueens(int n)
    {
        // Literally the same as Problem 51, but we return result.Count instead. Not optimal since we keep unneccessary stuff in memory.
        return SolveNQueens(n).Count;
    }

    /// <summary>
    /// Problem 53
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int MaxSubArray(int[] nums)
    {
        int n = nums.Length;

        int current = nums[0];
        int max = current;

        // start from i = 1 since we start with current = nums[0]
        for (int i = 1; i < n; i++)
        {
            // Current maximum is either (Previous current max + current value) or just current value
            current = Math.Max(current, current + nums[i]);
            max = Math.Max(max, current);
        }

        return max;
    }

    /// <summary>
    /// Problem 54
    /// </summary>
    /// <param name="matrix"></param>
    /// <returns></returns>
    public static IList<int> SpiralOrder(int[][] matrix)
    {
        // Initialize return list
        IList<int> spiral = [];

        // Check if its a valid matrix, return spiral if it's not.
        if (matrix is null || matrix.Length == 0 || matrix[0].Length == 0)
        {
            return spiral;
        }

        // dimensions of the matrix and initial positions
        int height = matrix.Length; // number of rows
        int width = matrix[0].Length; // number of cols

        int top = 0;
        int bottom = height - 1;

        int left = 0;
        int right = width - 1;

        while (top <= bottom && left <= right)
        {
            // from top left to top right
            for (int i = left; i <= right; i++)
            {
                spiral.Add(matrix[top][i]);
            }
            top++;

            // from top right to bottom right
            for (int i = top; i <= bottom; i++)
            {
                spiral.Add(matrix[i][right]);
            }
            right--;


            if (top <= bottom)
            {
                // bottom right to bottom left
                for (int i = right; i >= left; i--)
                {
                    spiral.Add(matrix[bottom][i]);
                }
                bottom--;
            }

            if (left <= right)
            {
                // bottom left to top left
                for (int i = bottom; i >= top; i--)
                {
                    spiral.Add(matrix[i][left]);
                }
                left++;
            }
        }

        return spiral;
    }

    /// <summary>
    /// Problem 55
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static bool CanJump(int[] nums)
    {
        int n = nums.Length;

        int maxLength = 0;

        for (int i = 0; i < n; i++)
        {
            if (i > maxLength)
            {
                return false;
            }

            maxLength = Math.Max(maxLength, i + nums[i]);
        }

        return true;
    }

    /// <summary>
    /// Problem 56
    /// </summary>
    /// <param name="intervals"></param>
    /// <returns></returns>
    public static int[][] Merge(int[][] intervals)
    {
        // Sort intervals based on start times
        Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));

        List<int[]> mergedIntervals = new();
        int[] currentInterval = intervals[0];

        for (int i = 1; i < intervals.Length; i++)
        {
            if (currentInterval[1] >= intervals[i][0])
            {
                // Merge overlapping intervals
                currentInterval[1] = Math.Max(currentInterval[1], intervals[i][1]);
            }
            else
            {
                // Add non-overlapping interval to the result
                mergedIntervals.Add(currentInterval);
                currentInterval = intervals[i];
            }
        }

        // Add the last interval to the result
        mergedIntervals.Add(currentInterval);

        return mergedIntervals.ToArray();
    }

    /// <summary>
    /// Problem 57
    /// </summary>
    /// <param name="intervals"></param>
    /// <param name="newInterval"></param>
    /// <returns></returns>
    public static int[][] Insert(int[][] intervals, int[] newInterval)
    {
        List<int[]> mergedIntervals = new();

        foreach (var interval in intervals)
        {
            if (interval[0] > newInterval[1])
            {
                mergedIntervals.Add(newInterval);
                newInterval = interval;
            }
            else if (interval[1] < newInterval[0])
            {
                mergedIntervals.Add(interval);
            }
            else
            {
                newInterval[0] = Math.Min(interval[0], newInterval[0]);
                newInterval[1] = Math.Max(interval[1], newInterval[1]);
            }
        }
        mergedIntervals.Add(newInterval);

        return mergedIntervals.ToArray();
    }

    /// <summary>
    /// Problem 58
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static int LengthOfLastWord(string s)
    {
        int length = 0;
        bool wordFound = false;
        for (int i = s.Length - 1; i >= 0; i--)
        {
            if (!s[i].Equals(' '))
            {
                wordFound = true;
                length++;
            }
            else if (wordFound)
            {
                break;
            }
        }
        return length;

        // Or solve with existing methods
        // 1. Get rid of leading and trailing blank space with .Trim()
        // 2. Split the string on blank spaces into a string[] array with .Split(' ')
        // 3. Get the last item in the array with .Last().
        // 4. Get the length with .Length
        // return s.Trim().Split(' ').Last().Length;
    }

    /// <summary>
    /// Problem 59
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int[][] GenerateMatrix(int n)
    {
        // Set up return matrix with n rows and n columns
        int[][] matrix = new int[n][];

        for (int i = 0; i < n; i++)
        {
            matrix[i] = new int[n];
        }


        int num = 1;

        int rowStart = 0;
        int rowEnd = n - 1;
        int colStart = 0;
        int colEnd = n - 1;

        while (rowStart <= rowEnd && colStart <= colEnd)
        {
            // From top left to top right
            for (int i = colStart; i <= colEnd; i++)
            {
                matrix[rowStart][i] = num;
                num++;
            }
            rowStart++;

            // top right to bottom right
            for (int i = rowStart; i <= rowEnd; i++)
            {
                matrix[i][colEnd] = num;
                num++;
            }
            colEnd--;

            if (rowStart <= rowEnd)
            {
                // bottom right to bottom left
                for (int i = colEnd; i >= colStart; i--)
                {
                    matrix[rowEnd][i] = num;
                    num++;
                }
                rowEnd--;
            }

            if (colStart <= colEnd)
            {
                // bottom left to top left
                for (int i = rowEnd; i >= rowStart; i--)
                {
                    matrix[i][colStart] = num;
                    num++;
                }
                colStart++;
            }
        }

        return matrix;
    }

    /// <summary>
    /// Problem 60
    /// </summary>
    /// <param name="n"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static string GetPermutation(int n, int k)
    {
        List<int> nums = new();

        for (int i = 1; i <= n; i++)
        {
            nums.Add(i);
        }

        int[] factorial = new int[n];
        factorial[0] = 1;

        for (int i = 1; i < n; i++)
        {
            factorial[i] = factorial[i - 1] * i;
        }

        k--; // Zero-based

        StringBuilder sb = new();

        for (int i = n - 1; i >= 0; i--)
        {
            int index = k / factorial[i];
            k %= factorial[i];

            sb.Append(nums[index]);
            nums.RemoveAt(index);
        }

        return sb.ToString();
    }
}
