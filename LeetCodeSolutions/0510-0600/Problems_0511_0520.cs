namespace LeetCodeSolutions._0510_0600;

public class Problems_0511_0520
{
    /// <summary>
    /// Problem 514
    /// </summary>
    /// <param name="ring"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static int FindRotateSteps(string ring, string key)
    {
        // Create a dictionary where each character in the ring is mapped to a list of ints of its indices.
        Dictionary<char, List<int>> charIndices = new();
        for (int i = 0; i < ring.Length; i++)
        {
            if (!charIndices.TryGetValue(ring[i], out List<int>? value))
            {
                value = new List<int>();
                charIndices[ring[i]] = value;
            }

            value.Add(i);
        }

        // Create 2D dp array to store the min number of steps needed to spell each character of the key
        // at each possible position of the ring. Initialize with int.MaxValue to indicate it's not calculated yet.
        int[,] dp = new int[key.Length, ring.Length];
        for (int i = 0; i < key.Length; i++)
        {
            for (int j = 0; j < ring.Length; j++)
            {
                dp[i, j] = int.MaxValue;
            }
        }

        // For the first character of the key, iterate through each occurance of that character in the ring.
        // Calculate the steps needed to align it with the first character of the key.
        // index = clockwise, ring.Length - index = counterclockwise. + 1 to press the button.
        foreach (int index in charIndices[key[0]])
        {
            dp[0, index] = Math.Min(index, ring.Length - index) + 1;
        }

        // For each subsequent character in the key, iterate through each occurrence of that character in the ring.
        // For each occurrence, calculate the minimum steps needed to spell the current character by considering all possible previous characters in the key. 
        for (int i = 1; i < key.Length; i++)
        {
            foreach (int index1 in charIndices[key[i]])
            {
                foreach (int index2 in charIndices[key[i - 1]])
                {
                    int distance = Math.Abs(index1 - index2);
                    int steps = Math.Min(distance, ring.Length - distance) + 1;
                    dp[i, index1] = Math.Min(dp[i, index1], dp[i - 1, index2] + steps);
                }
            }
        }

        int minSteps = int.MaxValue;
        foreach (int index in charIndices[key[^1]])
        {
            minSteps = Math.Min(minSteps, dp[key.Length - 1, index]);
        }

        return minSteps;
    }
}
