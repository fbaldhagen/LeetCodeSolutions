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
}
