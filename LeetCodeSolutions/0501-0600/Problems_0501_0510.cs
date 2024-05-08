using System;

namespace LeetCodeSolutions._0501_0600;

public class Problems_0501_0510
{
    /// <summary>
    /// Problem 506
    /// </summary>
    /// <param name="score"></param>
    /// <returns></returns>
    public static string[] FindRelativeRanks(int[] score)
    {
        // Initialize an array containing the indices of the athletes (1 to length of score)
        int[] place = Enumerable.Range(1, score.Length).ToArray();

        // Define a method to get the rank based on the athlete's placement
        static string GetRank(int i) => i switch
        {
            1 => "Gold Medal",
            2 => "Silver Medal",
            3 => "Bronze Medal",
            var x => x.ToString(),
        };

        // Initialize an array of ranks by converting the place array using the GetRank method
        string[] ranks = Array.ConvertAll(place, GetRank);

        // Sort the score array along with the place array
        Array.Sort(score, place);

        // Reverse the place array to get the correct placement order
        Array.Reverse(place);

        // Sort the ranks array based on the sorted place array
        Array.Sort(place, ranks);

        // Return the ranks array containing the ranks of the athletes based on their scores
        return ranks;
    }

}