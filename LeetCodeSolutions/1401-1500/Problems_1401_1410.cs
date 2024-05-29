namespace LeetCodeSolutions._1401_1500;

public class Problems_1401_1410
{
    /// <summary>
    /// Problem 1404
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static int NumSteps(string s)
    {
        int steps = 0;

        while (s != "1")
        {
            // Even number
            if (s.EndsWith("0"))
            {
                // Remove the last character (ie. divide by 2)
                s = s[..^1];
            }
            else
            {
                // If uneven, keep track of carry
                bool carry = true;
                // Character array to manipulate string
                char[] charArray = s.ToCharArray();

                // Iterate backwards
                for (int i = charArray.Length - 1; i >= 0; i--)
                {
                    // If the character is a 1, add 1 to it. Keep carry as true.
                    if (charArray[i] == '1')
                    {
                        charArray[i] = '0';
                    }
                    else
                    {
                        // If the character is a 0, add the carry.
                        charArray[i] = '1';
                        carry = false;
                        break;
                    }
                }

                // If we have a carry after iterating over the entire array, add a 1 to the beginning of it.
                // If not, "update" (replace) s with the modified character array.
                if (carry)
                {
                    s = '1' + new string(charArray);
                }
                else
                {
                    s = new string(charArray);
                }
            }
            steps++;
        }

        return steps;
    }
}
