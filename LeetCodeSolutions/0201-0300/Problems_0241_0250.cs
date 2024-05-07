namespace LeetCodeSolutions._0201_0300;

public class Problems_0241_0250
{
    /// <summary>
    /// Problem 241
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public static IList<int> DiffWaysToCompute(string expression)
    {
        IList<int> result = [];

        for (int i = 0; i < expression.Length; i++)
        {
            char c = expression[i];

            if (c == '+' || c == '-' || c == '*')
            {
                string leftExp = expression[..i];
                string rightExp = expression[(i + 1)..];

                IList<int> leftResults = DiffWaysToCompute(leftExp);
                IList<int> rightResults = DiffWaysToCompute(rightExp);

                foreach (int left in leftResults)
                {
                    foreach (int right in rightResults)
                    {
                        if (c == '+')
                        {
                            result.Add(left + right);
                        }
                        else if (c == '-')
                        {
                            result.Add(left - right);
                        }
                        else if (c == '*')
                        {
                            result.Add(left * right);
                        }
                    }
                }
            }
        }


        if (result.Count == 0)
        {
            result.Add(int.Parse(expression));
        }

        return result;
    }

    /// <summary>
    /// Problem 242
    /// </summary>
    /// <param name="s"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static bool IsAnagram(string s, string t)
    {
        Dictionary<char, int> count = new();

        if (s.Length != t.Length)
        {
            return false;
        }

        foreach (char c in s)
        {
            if (count.TryGetValue(c, out int value))
            {
                count[c] = ++value;
            }
            else
            {
                count.Add(c, 1);
            }
        }

        foreach (char c in t)
        {
            if (count.TryGetValue(c, out int value) && value > 0)
            {
                count[c] = --value;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    // 242 - 250 are premium and skipped
}