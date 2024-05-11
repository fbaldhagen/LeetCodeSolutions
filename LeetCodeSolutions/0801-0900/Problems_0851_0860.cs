namespace LeetCodeSolutions._0801_0900;

public class Problems_0851_0860
{
    /// <summary>
    /// Problem 857
    /// </summary>
    /// <param name="quality"></param>
    /// <param name="wage"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static double MincostToHireWorkers(int[] quality, int[] wage, int k)
    {
        // Create an array to store the wage-to-quality ratio and quality of each worker
        double[][] workers = new double[quality.Length][];

        // Populate the workers array with the wage-to-quality ratio and quality of each worker
        for (int i = 0; i < quality.Length; ++i)
        {
            workers[i] = [(double)wage[i] / quality[i], quality[i]];
        }

        // Sort the workers array based on the wage-to-quality ratio
        Array.Sort(workers, (a, b) => a[0].CompareTo(b[0]));

        // Initialize variables for the result and sum of quality
        double result = double.MaxValue;
        double sum = 0;

        // Use a priority queue to keep track of the top k workers by quality
        PriorityQueue<double, double> pq = new();

        // Iterate through each worker in the sorted workers array
        foreach (double[] worker in workers)
        {
            // Add the quality of the current worker to the sum
            sum += worker[1];

            // Enqueue the negative quality of the current worker into the priority queue
            pq.Enqueue(-worker[1], -worker[1]);

            // If the size of the priority queue exceeds k, dequeue the worker with the highest quality
            if (pq.Count > k)
            {
                sum += pq.Dequeue();
            }

            // If the size of the priority queue is equal to k
            if (pq.Count == k)
            {
                // Calculate the total cost of hiring the top k workers with the current worker as the highest paid
                result = Math.Min(result, sum * worker[0]);
            }
        }

        // Return the minimum total cost of hiring k workers satisfying the conditions
        return result;
    }
}