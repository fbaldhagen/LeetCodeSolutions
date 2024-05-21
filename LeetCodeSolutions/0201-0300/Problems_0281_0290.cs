namespace LeetCodeSolutions._0201_0300;

public class Problems_0281_0290
{
    /// <summary>
    /// Problem 282
    /// </summary>
    /// <param name="num"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static IList<string> AddOperators(string num, int target)
    {
        IList<string> result = [];

        // Initiate the backtracking process
        Backtrack("", 0, 0, 0);

        return result;

        void Backtrack(string path, int pos, long eval, long multed)
        {
            // Terminal condition: if we've reached the end of the string
            if (pos == num.Length)
            {
                // If the evaluated expression equals the target, add to the result list
                if (target == eval)
                {
                    result.Add(path);
                }
                return;
            }

            // Iterate over the string from the current position
            for (int i = pos; i < num.Length; i++)
            {
                // Skip numbers with leading zeros
                if (i != pos && num[pos] == '0')
                {
                    break;
                }

                // Extract the current number from the string
                long curr = long.Parse(num.Substring(pos, i - pos + 1));

                // If at the start of the expression, initialize with the current number
                if (pos == 0)
                {
                    Backtrack(path + curr, i + 1, curr, curr);
                }
                else
                {
                    // Addition
                    Backtrack(path + "+" + curr, i + 1, eval + curr, curr);
                    // Subtraction
                    Backtrack(path + "-" + curr, i + 1, eval - curr, -curr);
                    // Multiplication
                    Backtrack(path + "*" + curr, i + 1, eval - multed + multed * curr, multed * curr);
                }
            }
        }
    }

    /// <summary>
    /// Problem 283
    /// </summary>
    /// <param name="nums"></param>
    public static void MoveZeroes(int[] nums)
    {
        int lastNonZeroFoundAt = 0;

        // Move all non-zero elements to the beginning of the array
        for (int cur = 0; cur < nums.Length; cur++)
        {
            if (nums[cur] != 0)
            {
                nums[lastNonZeroFoundAt] = nums[cur];
                lastNonZeroFoundAt++;
            }
        }

        // Fill remaining positions with zeros
        for (int i = lastNonZeroFoundAt; i < nums.Length; i++)
        {
            nums[i] = 0;
        }
    }
}
