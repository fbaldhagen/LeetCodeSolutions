namespace LeetCodeSolutions._1001_1100;

public class Problems_1081_1090
{
    /// <summary>
    /// Problem 1081
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string SmallestSubsequence(string s)
    {
        Stack<char> stack = [];
        HashSet<char> seen = [];
        Dictionary<char, int> lastOcc = [];

        for (int i = 0; i < s.Length; i++)
        {
            lastOcc[s[i]] = i;
        }

        for (int i = 0; i < s.Length; i++)
        {
            char c = s[i];

            if (!seen.Contains(c))
            {
                while (stack.Count > 0 && c < stack.Peek() && i < lastOcc[stack.Peek()])
                {
                    seen.Remove(stack.Pop());
                }
                seen.Add(c);
                stack.Push(c);
            }
        }

        char[] result = [.. stack];
        Array.Reverse(result);
        return new string(result);
    }
}
