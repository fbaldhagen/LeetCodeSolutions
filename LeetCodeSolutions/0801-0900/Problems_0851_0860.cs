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

    /// <summary>
    /// Problem 860
    /// </summary>
    /// <param name="bills"></param>
    /// <returns></returns>
    public static bool LemonadeChange(int[] bills)
    {
        // Variables to keep track of the number of $5 and $10 bills.
        int five = 0;
        int ten = 0;

        foreach (int bill in bills)
        {
            // If the customer pays with a $5 bill, simply increase the count of $5 bills.
            if (bill == 5)
            {
                five++;
            }
            // If the customer pays with a $10 bill, we need to give back $5 as change.
            else if (bill == 10)
            {
                if (five > 0)
                {
                    five--;
                    ten++;
                }
                else
                {
                    return false;
                }
            }
            // If the customer pays with a $20 bill, we prefer to give one $10 and one $5 as change.
            else
            {
                if (ten > 0 && five > 0)
                {
                    ten--;
                    five--;
                }
                // If we don't have a $10 bill, we give three $5 bills instead.
                else if (five >= 3)
                {
                    five -= 3;
                }
                else
                {
                    return false;
                }
            }
        }

        // If we've successfully given change to every customer, return true.
        return true;
    }
}