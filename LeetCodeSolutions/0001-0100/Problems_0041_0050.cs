using System.Text;
namespace LeetCodeSolutions._0001_0100;

public class Problems_0041_0050
{
    /// <summary>
    /// Problem 41
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int FirstMissingPositive(int[] nums)
    {
        int n = nums.Length;

        for (int i = 0; i < n; i++)
        {
            while (nums[i] > 0 && nums[i] <= n && nums[nums[i] - 1] != nums[i])
            {
                Helpers.Swap(nums, i, nums[i] - 1);
            }
        }

        for (int i = 0; i < n; i++)
        {
            if (nums[i] != i + 1)
            {
                return i + 1;
            }
        }

        return n + 1;
    }

    /// <summary>
    /// Problem 42
    /// </summary>
    /// <param name="height"></param>
    /// <returns></returns>
    public static int Trap(int[] height)
    {
        int left = 0;
        int right = height.Length - 1;
        int left_max = 0;
        int right_max = 0;

        int result = 0;

        while (left <= right)
        {
            if (height[left] <= height[right])
            {
                if (height[left] > left_max)
                {
                    left_max = height[left];
                }
                else
                {
                    result += left_max - height[left];
                }
                left++;
            }
            else
            {
                if (height[right] > right_max)
                {
                    right_max = height[right];
                }
                else
                {
                    result += right_max - height[right];
                }
                right--;
            }
        }
        return result;

    }

    /// <summary>
    /// Problem 43
    /// </summary>
    /// <param name="num1"></param>
    /// <param name="num2"></param>
    /// <returns></returns>
    public static string Multiply(string num1, string num2)
    {
        int n1 = num1.Length;
        int n2 = num2.Length;

        int[] prod = new int[n1 + n2];

        for (int i = n1 - 1; i >= 0; i--)
        {
            for (int j = n2 - 1; j >= 0; j--)
            {
                int p1 = j + j;
                int p2 = p1 + 1;
                int mul = (num1[i] - '0') * (num2[j] - '0');
                int sum = mul + prod[p2];
                prod[p1] += sum / 10;
                prod[p2] = sum % 10;
            }
        }

        StringBuilder sb = new();
        foreach (int digit in prod)
        {
            if (sb.Length == 0 && digit == 0) continue;
            sb.Append(digit);
        }

        return sb.Length == 0 ? "0" : sb.ToString();
    }

    /// <summary>
    /// Problem 44
    /// </summary>
    /// <param name="s"></param>
    /// <param name="p"></param>
    /// <returns></returns>
    public static bool IsMatch(string s, string p)
    {
        int sL = s.Length;
        int pL = p.Length;

        // Create a 2D array to store the intermediate results
        bool[,] dp = new bool[sL + 1, pL + 1];

        // Empty pattern matches empty string
        dp[0, 0] = true;

        // Handle patterns with '*' at the beginning
        for (int j = 1; j <= pL; j++)
        {
            if (p[j - 1] == '*')
            {
                dp[0, j] = dp[0, j - 1];
            }
        }

        // Fill in the DP array
        for (int i = 1; i <= sL; i++)
        {
            for (int j = 1; j <= pL; j++)
            {
                if (p[j - 1] == '*')
                {
                    // '*' can match 0 or more characters
                    dp[i, j] = dp[i - 1, j] || dp[i, j - 1];
                }
                else if (p[j - 1] == '?' || s[i - 1] == p[j - 1])
                {
                    // '?' matches any single character, or characters match
                    dp[i, j] = dp[i - 1, j - 1];
                }
            }
        }

        // The result is stored in the bottom-right cell
        return dp[sL, pL];
    }

    /// <summary>
    /// Problem 45
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int Jump(int[] nums)
    {
        int n = nums.Length;
        int jumps = 0;
        int currentEnd = 0;
        int farthest = 0;

        for (int i = 0; i < n - 1; i++)
        {
            farthest = Math.Max(farthest, nums[i] + i);

            if (i == currentEnd)
            {
                jumps++;
                currentEnd = farthest;
            }
        }

        return jumps;
    }

    /// <summary>
    /// Problem 46
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static IList<IList<int>> Permute(int[] nums)
    {
        IList<IList<int>> result = [];
        Backtrack(0);
        return result;

        void Backtrack(int start)
        {
            if (start == nums.Length - 1)
            {
                result.Add(new List<int>(nums));
            }

            for (int i = start; i < nums.Length; i++)
            {
                Helpers.Swap(nums, start, i);

                Backtrack(start + 1);

                Helpers.Swap(nums, start, i);
            }
        }
    }

    /// <summary>
    /// Problem 47
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static IList<IList<int>> PermuteUnique(int[] nums)
    {
        IList<IList<int>> result = [];
        Array.Sort(nums);
        bool[] used = new bool[nums.Length];
        Backtrack(new List<int>());
        return result;

        void Backtrack(List<int> current)
        {
            if (current.Count == nums.Length)
            {
                result.Add(new List<int>(current));
                return;
            }

            for (int i = 0; i < nums.Length; i++)
            {
                if (used[i] || (i > 0 && nums[i] == nums[i - 1] && !used[i - 1]))
                {
                    continue;
                }

                used[i] = true;
                current.Add(nums[i]);

                Backtrack(current);
                used[i] = false;

                current.RemoveAt(current.Count - 1);
            }
        }
    }

    /// <summary>
    /// Problem 48
    /// </summary>
    /// <param name="matrix"></param>
    public static void Rotate(int[][] matrix)
    {
        int n = matrix.Length; // = matrix[i].Length, ie. an n x n matrix

        // Reflect the matrix along its diagonal (BL - TR)
        for (int i = 0; i < n; i++)
        {
            for (int j = i; j < n; j++)
            {
                (matrix[j][i], matrix[i][j]) = (matrix[i][j], matrix[j][i]);
            }
        }

        // After reflecting, reverse each row
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n / 2; j++)
            {
                (matrix[i][j], matrix[i][n - j - 1]) = (matrix[i][n - j - 1], matrix[i][j]);
            }
        }
    }

    /// <summary>
    /// Problem 49
    /// </summary>
    /// <param name="strs"></param>
    /// <returns></returns>
    public static IList<IList<string>> GroupAnagrams(string[] strs)
    {
        Dictionary<string, List<string>> dict = new();

        foreach (string str in strs)
        {
            char[] chars = str.ToCharArray();
            Array.Sort(chars);

            string key = new(chars);

            if (!dict.TryGetValue(key, out var anagramGroup))
            {
                anagramGroup = new List<string>();
                dict.Add(key, anagramGroup);
            }
            anagramGroup.Add(str);
        }

        return dict.Values.Cast<IList<string>>().ToList();
    }

    /// <summary>
    /// Problem 50
    /// </summary>
    /// <param name="x"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public static double MyPow(double x, int n)
    {
        if (n == 0) return 1;
        if (x == 0) return 0;

        // Handle the edge case of n being int.MinValue
        if (n == int.MinValue)
        {
            return 1 / (MyPow(x, int.MaxValue) * x);
        }

        // Handle negative powers
        if (n < 0)
        {
            return 1 / MyPow(x, -n);
        }

        // Divide and conquer for even powers
        if (n % 2 == 0)
        {
            double half = MyPow(x, n / 2);
            return half * half;
        }

        // Divide and conquer for odd powers
        return x * MyPow(x, n - 1);
    }
}
