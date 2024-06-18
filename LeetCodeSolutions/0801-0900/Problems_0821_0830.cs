namespace LeetCodeSolutions._0801_0900;

public class Problems_0821_0830
{
    /// <summary>
    /// Problem 826
    /// </summary>
    /// <param name="difficulty"></param>
    /// <param name="profit"></param>
    /// <param name="worker"></param>
    /// <returns></returns>
    public static int MaxProfitAssignment(int[] difficulty, int[] profit, int[] worker)
    {
        // Sort difficulty and profit together based on difficulty values.
        Array.Sort(difficulty, profit);
        Array.Sort(worker);

        int i = 0; // Worker index
        int j = 0; // Job index

        int workerProfit = 0;
        int totalProfit = 0;

        // Iterate over all workers
        while (i < worker.Length)
        {
            // Check if all jobs have been considered. If they have, move to the next worker,
            // who we know is able to perform the same jobs as the previous one thanks to the sorting of worker.
            if (j >= difficulty.Length)
            {
                totalProfit += workerProfit;
                i++;
                continue;
            }

            // If a worker is able to perform a more difficult job, check if it yields a larger profit
            if (worker[i] >= difficulty[j])
            {
                workerProfit = Math.Max(workerProfit, profit[j]);
                j++;
            }
            // If the worker cant do the job, update the total profit and move on to the next worker.
            else
            {
                totalProfit += workerProfit;
                i++;
            }
        }

        return totalProfit;
    }
}
