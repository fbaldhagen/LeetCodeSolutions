namespace LeetCodeSolutions._3001_3100;

public class Problems_3071_3080
{
    /// <summary>
    /// Problem 3075
    /// </summary>
    /// <param name="happiness"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static long MaximumHappinessSum(int[] happiness, int k)
    {
        // Sort the array in descending/non-ascending order
        Array.Sort(happiness, (a, b) => b - a);
        long sum = 0;

        // Since the values all decrement at the same rate, we just pick the k
        // largest values in happiness, decrementing one for each value we've already picked.
        // If the value would be less than 1, we dont add anything. 
        for (int i = 0; i < k; i++)
        {
            sum += Math.Max(0, happiness[i] - i);
        }

        return sum;
    }
}
