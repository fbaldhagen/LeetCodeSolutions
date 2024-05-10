namespace LeetCodeSolutions._2401_2500;

public class Problems_2421_2430
{
    /// <summary>
    /// Problem 2423
    /// </summary>
    /// <param name="word"></param>
    /// <returns></returns>
    public static bool EqualFrequency(string word)
    {
        // Set up frequency array of letters from a to z
        int[] freq = new int['z' + 1];

        // Fill the frequency array
        foreach (char letter in word)
        {
            freq[letter] += 1;
        }

        // Check if each character is in the word, and if removing one would 
        // satisfy the conditions of the problem
        for (char letter = 'a'; letter <= 'z'; letter++)
        {
            if (freq[letter] != 0 && Check(letter))
            {
                return true;
            }
        }

        return false;

        // Helper method to check if removing a letter would make it so
        // all letters in the word now have the same frequency.
        bool Check(char letter)
        {
            // Remove one occurence of the letter
            freq[letter] -= 1;
            // Get the letter that has the max frequency.
            int max = freq.Max();

            // Every character must either have the max frequency,
            // or not be in the string at all
            if (freq.All(f => f == max || f == 0))
            {
                return true;
            }

            // Restore the frequency array.
            freq[letter] += 1;
            return false;
        }
    }
}
