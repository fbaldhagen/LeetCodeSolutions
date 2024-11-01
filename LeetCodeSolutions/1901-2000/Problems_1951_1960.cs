using System.Text;

namespace LeetCodeSolutions._1901_2000;

public class Problems_1951_1960
{
    /// <summary>
    /// Problem 1957
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string MakeFancyString(string s)
    {
        StringBuilder sb = new();

        int cons = 1;
        char prev = s[0];
        sb.Append(prev);

        for (int c = 1; c < s.Length; c++)
        {
            if (s[c].Equals(prev))
            {
                cons++;
                if (cons != 3)
                {
                    sb.Append(s[c]);
                }
            }
            else
            {
                sb.Append(s[c]);
                cons = 1;
                prev = s[c];
            }
        }

        return sb.ToString();
    }
}
