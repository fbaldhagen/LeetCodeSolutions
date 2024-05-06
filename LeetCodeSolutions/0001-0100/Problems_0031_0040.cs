using System.Text;
namespace LeetCodeSolutions._0001_0100;

public class Problems_0031_0040
{
    /// <summary>
    /// Problem 31
    /// </summary>
    /// <param name="nums"></param>
    public static void NextPermutation(int[] nums)
    {
        int n = nums.Length;
        int i = n - 2;

        // find the first index i (from the right) so that nums[i] < nums[i + 1]
        while (i >= 0 && nums[i] >= nums[i + 1])
        {
            i--;
        }

        if (i >= 0)
        {
            int j = n - 1;
            // find the rightmost index to the right of i, so that nums[j] > nums[i]
            while (j > i && nums[j] <= nums[i])
            {
                j--;
            }

            // swap nums[j] with nums[i]
            Helpers.Swap(nums, i, j);
        }

        // reverse the subarray to the right of nums[i]
        Helpers.Reverse(nums, i + 1, n - 1);
    }

    /// <summary>
    /// Problem 32
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static int LongestValidParentheses(string s)
    {
        // use a stack to keep track of indices.
        Stack<int> stack = new();

        int longest = 0;

        // Go through the string
        for (int i = 0; i < s.Length; i++)
        {
            // the index of an opening parenthesis is pushed onto the stack
            if (s[i] == '(')
            {
                stack.Push(i);
            }
            else
            {
                // a closing parenthesis pops the index of the last open parenthesis from the stack
                stack.Pop();
                // if the stack is empty, push the current index onto the stack
                if (stack.Count == 0)
                {
                    longest = Math.Max(longest, i + 1);
                }
                else
                {
                    // update the longest valid substring.
                    longest = Math.Max(longest, i - stack.Peek());
                }
            }
        }

        return longest;
    }

    /// <summary>
    /// Problem 33
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static int Search(int[] nums, int target)
    {
        // Binary search approach

        int left = 0;
        int right = nums.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (nums[mid] == target)
            {
                return mid;
            }

            // check if left half is sorted
            if (nums[mid] >= nums[left])
            {
                // is target in the left, sorted half?
                if (target >= nums[left] && target < nums[mid])
                {
                    right = mid - 1;
                }
                // it's in the right half
                else
                {
                    left = mid + 1;
                }
            }
            // right is sorted
            else
            {
                // is target in the right, sorted half?
                if (target > nums[mid] && target <= nums[right])
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
        }

        return -1;
    }

    /// <summary>
    /// Problem 34
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static int[] SearchRange(int[] nums, int target)
    {
        // Two pointers
        int left = 0;
        int right = nums.Length - 1;

        int[] result = new int[] { -1, -1 };

        // Edge case..
        if (nums is null || nums.Length == 0)
        {
            return result;
        }

        // Binary search
        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (target == nums[mid])
            {
                int leftBound = mid;
                int rightBound = mid;

                // Check for same value in adjacent indices (if there are any)
                while (leftBound > 0 && nums[leftBound - 1] == target)
                {
                    leftBound--;
                }

                while (rightBound < nums.Length - 1 && nums[rightBound + 1] == target)
                {
                    rightBound++;
                }

                // Update the array with correct values
                result[0] = leftBound;
                result[1] = rightBound;

                return result;
            }

            if (nums[mid] < target)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return result;
    }

    /// <summary>
    /// Problem 35
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static int SearchInsert(int[] nums, int target)
    {
        int left = 0;
        int right = nums.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (nums[mid] == target)
            {
                return mid;
            }

            if (nums[mid] < target)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return left;
    }

    /// <summary>
    /// Problem 36
    /// </summary>
    /// <param name="board"></param>
    /// <returns></returns>
    public static bool IsValidSudoku(char[][] board)
    {
        // Use hashsets to keep track of what numbers are present in rows, cols and boxes
        HashSet<char>[] rows = new HashSet<char>[9];
        HashSet<char>[] cols = new HashSet<char>[9];
        HashSet<char>[] boxes = new HashSet<char>[9];

        for (int i = 0; i < 9; i++)
        {
            rows[i] = new();
            cols[i] = new();
            boxes[i] = new();
        }

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                char number = board[i][j];

                int boxIndex = (i / 3) * 3 + j / 3;

                if (!number.Equals('.'))
                {
                    if (!rows[i].Add(number) || !cols[j].Add(number) || !boxes[boxIndex].Add(number))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    /// <summary>
    /// Problem 37
    /// </summary>
    /// <param name="board"></param>
    public static void SolveSudoku(char[][] board)
    {
        Solve(board);

        // Solve using a recursive backtracking algorithm
        static bool Solve(char[][] board)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (board[row][col] == '.')
                    {
                        for (char c = '1'; c <= '9'; c++)
                        {
                            if (IsValidPlacement(board, row, col, c))
                            {
                                board[row][col] = c;

                                if (Solve(board))
                                {
                                    return true;
                                }
                                else
                                {
                                    board[row][col] = '.';
                                }
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        static bool PresentInCol(char[][] board, int col, char num)
        {
            for (int row = 0; row < 9; row++)
            {
                if (board[row][col] == num)
                {
                    return true;
                }
            }
            return false;
        }

        static bool PresentInRow(char[][] board, int row, char num)
        {
            for (int col = 0; col < 9; col++)
            {
                if (board[row][col] == num)
                {
                    return true;
                }
            }
            return false;
        }

        static bool PresentInBox(char[][] board, int row, int col, char num)
        {
            int BoxStartCol = col - col % 3;
            int BoxStartRow = row - row % 3;

            for (int rowLoop = BoxStartRow; rowLoop < BoxStartRow + 3; rowLoop++)
            {
                for (int colLoop = BoxStartCol; colLoop < BoxStartCol + 3; colLoop++)
                {
                    if (board[rowLoop][colLoop] == num)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        static bool IsValidPlacement(char[][] board, int row, int col, char num)
        {
            return !PresentInRow(board, row, num)
                && !PresentInCol(board, col, num)
                && !PresentInBox(board, row, col, num);
        }
    }

    /// <summary>
    /// Problem 38
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static string CountAndSay(int n)
    {
        // Base case
        string s = "1";

        for (int i = 2; i <= n; i++)
        {
            StringBuilder sb = new();

            int count = 1;

            char d = s[0];

            for (int j = 1; j < s.Length; j++)
            {
                if (s[j] == d)
                {
                    count++;
                }
                else
                {
                    sb.Append(count);
                    sb.Append(d);
                    count = 1;
                    d = s[j];
                }
            }
            sb.Append(count);
            sb.Append(d);
            s = sb.ToString();
        }

        return s;
    }

    /// <summary>
    /// Problem 39
    /// </summary>
    /// <param name="candidates"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static IList<IList<int>> CombinationSum(int[] candidates, int target)
    {
        List<IList<int>> result = new();
        List<int> currentCombo = new();

        Backtrack(candidates, target, 0, currentCombo, result);

        return result;

        static void Backtrack(int[] candidates, int target, int start, IList<int> currentCombo, IList<IList<int>> result)
        {
            if (target == 0)
            {
                result.Add(new List<int>(currentCombo));
                return;
            }

            for (int i = start; i < candidates.Length; i++)
            {
                if (candidates[i] <= target)
                {
                    currentCombo.Add(candidates[i]);
                    Backtrack(candidates, target - candidates[i], i, currentCombo, result);
                    currentCombo.RemoveAt(currentCombo.Count - 1);
                }
            }
        }
    }


    /// <summary>
    /// Problem 40
    /// </summary>
    /// <param name="candidates"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static IList<IList<int>> CombinationSum2(int[] candidates, int target)
    {
        List<IList<int>> result = new();
        List<int> currentCombo = new();

        Array.Sort(candidates);
        Backtrack(candidates, target, 0, currentCombo, result);

        return result;

        static void Backtrack(int[] candidates, int target, int start, IList<int> currentCombo, IList<IList<int>> result)
        {
            if (target == 0)
            {
                result.Add(new List<int>(currentCombo));
                return;
            }

            for (int i = start; i < candidates.Length; i++)
            {
                if (i > start && candidates[i] == candidates[i - 1])
                {
                    continue;
                }

                if (candidates[i] <= target)
                {
                    currentCombo.Add(candidates[i]);
                    Backtrack(candidates, target - candidates[i], i + 1, currentCombo, result);
                    currentCombo.RemoveAt(currentCombo.Count - 1);
                }
            }
        }
    }
}
