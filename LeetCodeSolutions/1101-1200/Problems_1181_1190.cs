using System.Text;

namespace LeetCodeSolutions._1101_1200;

public class Problems_1181_1190
{
    /// <summary>
    /// Problem 1190
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string ReverseParentheses(string s)
    {
        // Stack to keep track of open parentheses indices
        Stack<int> stack = new();
        // StringBuilder to build the resulting string
        StringBuilder sb = new(s);

        for (int currPos = 0; currPos < sb.Length; currPos++)
        {
            // Push the index of the opening parenthesis onto the stack
            if (sb[currPos] == '(')
            {
                stack.Push(currPos);
            }
            // When encountering a closing parenthesis, pop from the stack and reverse the substring
            else if (sb[currPos] == ')')
            {
                int startPos = stack.Pop();
                ReverseString(sb, startPos + 1, currPos - 1);
                // After reversing, remove both the opening and closing parentheses
                sb.Remove(currPos, 1);
                sb.Remove(startPos, 1);
                // Adjust the current position after removal
                currPos -= 2;
            }
        }

        return sb.ToString();

        static void ReverseString(StringBuilder sb, int start, int end)
        {
            while (start < end)
            {
                (sb[end], sb[start]) = (sb[start], sb[end]);
                start++;
                end--;
            }
        }
    }
}
