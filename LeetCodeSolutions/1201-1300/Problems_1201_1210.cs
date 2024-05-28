namespace LeetCodeSolutions._1201_1300;

public class Problems_1201_1210
{
    /// <summary>
    /// Problem 1208
    /// </summary>
    /// <param name="s"></param>
    /// <param name="t"></param>
    /// <param name="maxCost"></param>
    /// <returns></returns>
    public static int EqualSubstring(string s, string t, int maxCost)
    {
        int n = s.Length;
        int left = 0;
        int right = 0;
        int currentCost = 0;
        int maxLength = 0;

        while (right < n)
        {
            currentCost += Math.Abs(s[right] - t[right]);

            while (currentCost > maxCost)
            {
                currentCost -= Math.Abs(s[left] - t[left]);
                left++;
            }

            maxLength = Math.Max(maxLength, right - left + 1);
            right++;
        }

        return maxLength;
    }
}
