namespace LeetCodeSolutions._0401_0500;

public class Problems_0401_0410
{
    /// <summary>
    /// Problem 409
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static int LongestPalindrome(string s)
    {
        Dictionary<char, int> map = [];

        foreach (char c in s)
        {
            if (map.TryGetValue(c, out int value))
            {
                map[c] = ++value;
            }
            else
            {
                map.Add(c, 1);
            }
        }

        int length = 0;
        bool oddFound = false;

        foreach (var value in map)
        {
            if (value.Value % 2 == 0)
            {
                length += value.Value;
            }
            else
            {
                length += value.Value - 1;
                oddFound = true;
            }
        }

        if (oddFound)
        {
            length++;
        }

        return length;
    }
}
