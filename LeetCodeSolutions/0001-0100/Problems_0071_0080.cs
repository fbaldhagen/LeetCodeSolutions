namespace LeetCodeSolutions._0001_0100;

public class Problems_0071_0080
{
    /// <summary>
    /// Problem 71
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string SimplifyPath(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            // If the input path is null or empty, return the root directory "/"
            return "/";
        }

        // Split the string on '/' characters to get individual directory names
        string[] directories = path.Split('/');

        // Create a stack to keep track of directories
        Stack<string> stack = new();

        foreach (string directory in directories)
        {
            // Skip empty directory names or current directory references "."
            if (string.IsNullOrEmpty(directory) || directory.Equals("."))
            {
                continue;
            }

            // If encountering a parent directory reference ".."
            if (directory.Equals(".."))
            {
                // If there are directories in the stack, pop the last directory (move up one level)
                if (stack.Count > 0)
                {
                    stack.Pop();
                }
            }
            else
            {
                // If it's a normal directory name, push it onto the stack
                stack.Push(directory);
            }
        }

        // Check if the stack becomes empty after processing all directories
        // If so, return the root directory "/"
        if (stack.Count == 0)
        {
            return "/";
        }

        // Convert the stack to an array and reverse it to get the simplified path
        string[] resultArray = stack.ToArray();
        Array.Reverse(resultArray);
        // Join the directory names with '/' and return the simplified path
        return "/" + string.Join("/", resultArray);
    }

    /// <summary>
    /// Problem 72
    /// </summary>
    /// <param name="word1"></param>
    /// <param name="word2"></param>
    /// <returns></returns>
    public static int MinDistance(string word1, string word2)
    {
        int m = word1.Length;
        int n = word2.Length;

        int[,] dp = new int[m + 1, n + 1];

        for (int i = 0; i <= m; i++)
        {
            dp[i, 0] = i; // number of operations to turn string1 to an empty string
        }

        for (int i = 1; i <= n; i++)
        {
            dp[0, i] = i; // number of operations to turn an empty string to string2
        }

        // Fill in the rest of the 2D array
        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                if (word1[i - 1] == word2[j - 1])
                {
                    // If the characters match up they dont require an operation / change
                    dp[i, j] = dp[i - 1, j - 1];
                }
                else
                {
                    // If they dont match, we use the best of the 3 operations: insertion, deletion or swap.
                    dp[i, j] = 1 + Math.Min(dp[i, j - 1], Math.Min(dp[i - 1, j], dp[i - 1, j - 1]));
                }
            }
        }

        return dp[m, n];
    }

    /// <summary>
    /// Problem 73
    /// </summary>
    public static void SetZeroes(int[][] matrix)
    {
        int m = matrix.Length;
        int n = matrix[0].Length;

        // Keep track if 1st row and/or col has zeros
        bool firstRowHasZero = false;
        bool firstColHasZero = false;

        // Check the first row
        for (int i = 0; i < n; i++)
        {
            if (matrix[0][i] == 0)
            {
                firstRowHasZero = true;
                break;
            }
        }

        // Check the first col
        for (int i = 0; i < m; i++)
        {
            if (matrix[i][0] == 0)
            {
                firstColHasZero = true;
                break;
            }
        }

        // Loop through the matrix, use the first row and col as markers
        for (int i = 1; i < m; i++)
        {
            for (int j = 1; j < n; j++)
            {
                if (matrix[i][j] == 0)
                {
                    // Mark the row and col
                    matrix[0][j] = 0;
                    matrix[i][0] = 0;
                }
            }
        }

        // Update the matrix after looking at the first row and col
        for (int i = 1; i < m; i++)
        {
            for (int j = 1; j < n; j++)
            {
                // If there's a zero in either the row or column, set current cell to 0
                if (matrix[0][j] == 0 || matrix[i][0] == 0)
                {
                    matrix[i][j] = 0;
                }
            }
        }

        // Update the first row and col based on the boolean flags
        if (firstRowHasZero)
        {
            for (int i = 0; i < n; i++)
            {
                matrix[0][i] = 0;
            }
        }

        if (firstColHasZero)
        {
            for (int i = 0; i < m; i++)
            {
                matrix[i][0] = 0;
            }
        }
    }

    /// <summary>
    /// Problem 74
    /// </summary>
    /// <param name="matrix"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static bool SearchMatrix(int[][] matrix, int target)
    {
        int m = matrix.Length;
        int n = matrix[0].Length;

        // Binary search?
        int left = 0;
        int right = m * n - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            // Get row with division by number of columns
            int row = mid / n;
            // Get col with remainder after that division
            int col = mid % n;

            int midValue = matrix[row][col];

            if (midValue == target)
            {
                return true;
            }
            else if (midValue < target)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return false;
    }

    /// <summary>
    /// Problem 75
    /// </summary>
    public static void SortColors(int[] nums)
    {
        int red = 0;
        int white = 0;
        int blue = 0;

        int n = nums.Length;

        // Count occurences
        for (int i = 0; i < n; i++)
        {
            if (nums[i] == 0)
            {
                red++;
            }
            else if (nums[i] == 1)
            {
                white++;
            }
            else
            {
                blue++;
            }
        }

        // Overwrite the array based on the counted values
        // First red, then white, and lastly blue
        for (int i = 0; i < n; i++)
        {
            while (red > 0)
            {
                nums[i] = 0;
                red--;
                i++;
            }
            while (white > 0)
            {
                nums[i] = 1;
                white--;
                i++;
            }
            while (blue > 0)
            {
                nums[i] = 2;
                blue--;
                i++;
            }
        }
    }

    /// <summary>
    /// Problem 75, alternative solution in one pass.
    /// </summary>
    /// <param name="nums"></param>
    public static void SortColors2(int[] nums)
    {
        // left and right pointers, left where we place reds, right blues.
        int left = 0;
        int right = nums.Length - 1;

        int i = 0;

        while (i <= right)
        {
            // If the current color is white, place it at the left pointer and increment the pointer by 1
            if (nums[i] == 0)
            {
                Helpers.Swap(nums, i, left);
                i++;
                left++;
            }
            else if (nums[i] == 1)
            {
                // If its white, we just leave it and move on to next index 
                i++;
            }
            else if (nums[i] == 2)
            {
                // If its blue, we place it at the right pointer, and decrement the pointer by 1
                // We dont increment i here, since we need to process the new value at index i.
                Helpers.Swap(nums, i, right);
                right--;
            }
        }
    }

    /// <summary>
    /// Problem 76
    /// </summary>
    /// <param name="s"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static string MinWindow(string s, string t)
    {
        // Initialize dictionaries to store frequency of characters in strings s and t
        Dictionary<char, int> freqT = new();
        Dictionary<char, int> freqS = new();

        // Count of characters in string t that are present in the current window
        int count = 0;

        // Pointers for the sliding window
        int left = 0;
        int right = 0;

        // Length of the minimum window found so far
        int minLength = int.MaxValue;

        // Resultant string representing the minimum window
        string result = "";

        // Initialize the frequency dictionary for string t
        foreach (char c in t)
        {
            if (freqT.TryGetValue(c, out int value))
            {
                freqT[c] = value + 1;
            }
            else
            {
                freqT[c] = 1;
            }
        }

        // Move the right pointer until it reaches the end of string s
        while (right < s.Length)
        {
            // Expand the window to the right
            if (freqS.TryGetValue(s[right], out int valueS))
                freqS[s[right]] = valueS + 1;
            else
                freqS[s[right]] = 1;

            // Check if the current character in s is also in t, and update the count accordingly
            if (freqT.TryGetValue(s[right], out int valueT) && freqS[s[right]] <= valueT)
            {
                count++;
            }

            // Contract the window from the left until the window is no longer valid
            while (count == t.Length && left <= right)
            {
                // Update the result if the current window is smaller than the previously recorded minimum window
                if (right - left + 1 < minLength)
                {
                    minLength = right - left + 1;
                    result = s.Substring(left, minLength);
                }

                // Move the left pointer and update the counts
                freqS[s[left]]--;

                // If the frequency of the character at the left pointer becomes less than required, update the count
                if (freqT.TryGetValue(s[left], out int value) && freqS[s[left]] < value)
                {
                    count--;
                }
                left++;
            }
            // Move the right pointer to expand the window
            right++;
        }

        return result;
    }

    /// <summary>
    /// Problem 77
    /// </summary>
    /// <param name="n"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static IList<IList<int>> Combine(int n, int k)
    {
        IList<IList<int>> result = new List<IList<int>>();
        IList<int> current = new List<int>();

        Backtrack(result, current, 1, n, k);
        return result;

        static void Backtrack(IList<IList<int>> result, IList<int> current, int start, int n, int k)
        {
            if (current.Count == k)
            {
                result.Add(new List<int>(current));
                return;
            }

            for (int i = start; i <= n; i++)
            {
                current.Add(i);
                Backtrack(result, current, i + 1, n, k);
                current.RemoveAt(current.Count - 1);
            }
        }
    }

    /// <summary>
    /// Problem 78
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static IList<IList<int>> Subsets(int[] nums)
    {
        IList<IList<int>> result = new List<IList<int>>();
        IList<int> current = new List<int>();

        Backtrack(result, current, nums, 0);

        return result;

        static void Backtrack(IList<IList<int>> result, IList<int> current, int[] nums, int start)
        {
            // Add current subset
            result.Add(new List<int>(current));

            // typical backtracking..
            for (int i = start; i < nums.Length; i++)
            {
                current.Add(nums[i]);
                Backtrack(result, current, nums, i + 1);
                current.RemoveAt(current.Count - 1);
            }
        }
    }

    /// <summary>
    /// Problem 79
    /// </summary>
    /// <param name="board"></param>
    /// <param name="word"></param>
    /// <returns></returns>
    public static bool Exist(char[][] board, string word)
    {
        // Dimensions of the board
        int m = board.Length;
        int n = board[0].Length;

        // Find the first letter of the word on the board and call Search with the coordinates
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (board[i][j] == word[0] && Search(board, word, i, j, 0))
                {
                    return true;
                }
            }
        }

        return false;

        static bool Search(char[][] board, string word, int i, int j, int index)
        {
            // If we get a sequence that is the same length as the word 
            // (the sequence is the word..) we return true
            if (index == word.Length)
            {
                return true;
            }

            // If the cell is out of the boards range or the letter doesnt match, we return false.
            if (i < 0 || i >= board.Length || j < 0 || j >= board[0].Length || board[i][j] != word[index])
            {
                return false;
            }

            // Save the letter so we can mark it as used in the board.
            char temp = board[i][j];
            // We use '#' to mark cells as visited
            board[i][j] = '#';

            // Call the method for cells that are adjacent to the current one.
            bool found = Search(board, word, i + 1, j, index + 1) ||
                         Search(board, word, i - 1, j, index + 1) ||
                         Search(board, word, i, j + 1, index + 1) ||
                         Search(board, word, i, j - 1, index + 1);

            // Revert the character change
            board[i][j] = temp;

            return found;
        }
    }

    /// <summary>
    /// Problem 80
    /// </summary>
    /// <returns></returns>
    public static int RemoveDuplicates(int[] nums)
    {
        int n = nums.Length;

        // If the length is less than or equal to 2, there's no need to remove duplicates,
        // as the array will have at most 2 unique elements
        if (n <= 2)
        {
            return n;
        }

        // Counter to keep track of the occurrences of the current element
        int count = 1;

        // Index to keep track of the position where the last non-duplicate element was found
        int lastNonDuplicate = 0;

        for (int i = 1; i < n; i++)
        {
            if (nums[i] == nums[lastNonDuplicate])
            {
                count++;

                // If the count is less than or equal to 2, it means the element
                // can still be included in the result as it hasn't occurred more than twice
                if (count <= 2)
                {
                    lastNonDuplicate++;

                    // Update the lastNonDuplicate position with the current element,
                    // effectively overwriting duplicates with new elements
                    nums[lastNonDuplicate] = nums[i];
                }
            }
            else
            {
                // Reset the count of occurrences for the new element
                count = 1;

                lastNonDuplicate++;

                // Update the lastNonDuplicate position with the current element,
                // effectively replacing the next position with a non-duplicate element
                nums[lastNonDuplicate] = nums[i];
            }
        }
        // Since the lastNonDuplicate index represents the last valid index of the
        // non-duplicate part of the array, adding 1 gives us the count of unique elements
        return lastNonDuplicate + 1;
    }
}
