using System;

namespace LeetCodeSolutions._0501_0600;

public class Problems_0501_0510
{
    /// <summary>
    /// Problem 502
    /// </summary>
    /// <param name="k"></param>
    /// <param name="w"></param>
    /// <param name="profits"></param>
    /// <param name="capital"></param>
    /// <returns></returns>
    public static int FindMaximizedCapital(int k, int w, int[] profits, int[] capital)
    {
        int n = profits.Length;

        // Min-heap to store projects by their capital requirement
        var minHeap = new PriorityQueue<(int capital, int profit), int>();

        // Max-heap to store available projects by their profits
        var maxHeap = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b.CompareTo(a)));

        // Insert all projects into the min-heap
        for (int i = 0; i < n; i++)
        {
            minHeap.Enqueue((capital[i], profits[i]), capital[i]);
        }

        int currentCapital = w;

        for (int i = 0; i < k; i++)
        {
            // Move projects that can be unlocked with the current capital to the max-heap
            while (minHeap.Count > 0 && minHeap.Peek().capital <= currentCapital)
            {
                var project = minHeap.Dequeue();
                maxHeap.Enqueue(project.profit, project.profit);
            }

            // If no projects can be started, break the loop
            if (maxHeap.Count == 0)
            {
                break;
            }

            // Select the project with the maximum profit
            currentCapital += maxHeap.Dequeue();
        }

        return currentCapital;
    }

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