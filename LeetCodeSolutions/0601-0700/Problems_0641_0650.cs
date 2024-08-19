namespace LeetCodeSolutions._0601_0700;

public class Problems_0641_0650
{
    /// <summary>
    /// Problem 648
    /// </summary>
    /// <param name="dictionary"></param>
    /// <param name="sentence"></param>
    /// <returns></returns>
    public static string ReplaceWords(IList<string> dictionary, string sentence)
    {
        HashSet<string> set = new(dictionary);

        string[] words = sentence.Split(' ');

        for (int i = 0; i < words.Length; i++)
        {
            int wordLength = words[i].Length;

            for (int j = 1; j < wordLength; j++)
            {
                if (set.Contains(words[i][..j]))
                {
                    words[i] = words[i][..j];
                    break;
                }
            }
        }

        return string.Join(" ", words);
    }

    /// <summary>
    /// Problem 650
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int MinSteps(int n)
    {
        int steps = 0;
        int divisor = 2;

        // Find the smallest divisors of n
        while (n > 1)
        {
            // If n is divisible by divisor, we can use the Copy All and Paste operations
            if (n % divisor == 0)
            {
                // Add the divisor to the steps (copy all and then paste)
                steps += divisor;
                // Reduce n by dividing it by the divisor
                n /= divisor;
            }
            else
            {
                // If not divisible, increase the divisor
                divisor++;
            }
        }

        return steps;
    }
}
