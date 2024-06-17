namespace LeetCodeSolutions._0601_0700;

public class Problems_0631_0640
{
    /// <summary>
    /// Problem 633
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public static bool JudgeSquareSum(int c)
    {
        long left = 0;
        long right = (long)Math.Sqrt(c);

        while (left <= right)
        {
            long sum = left * left + right * right;

            if (sum == c)
            {
                return true;
            }
            else if (sum < c)
            {
                left++;
            }
            else
            {
                right--;
            }
        }

        return false;
    }
}
