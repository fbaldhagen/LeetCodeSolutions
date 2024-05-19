namespace LeetCodeSolutions._3001_3100;

public class Problems_3061_3070
{
    /// <summary>
    /// Problem 3068
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    /// <param name="edges"></param>
    /// <returns></returns>
    public static long MaximumValueSum(int[] nums, int k, int[][] edges)
    {
        long sum = 0; // To keep track of the total sum of values after applying operations
        long minExtra = 1000000; // To track the minimum difference for balancing the operation count
        int count = 0; // To count the number of times the XOR operation increases a node's value

        // Iterate through each value in the nums array
        foreach (int val in nums)
        {
            // Check if applying XOR with k increases the value
            if ((val ^ k) > val)
            {
                // If XORing the value increases it, add the XORed value to the sum
                sum += val ^ k;
                // Update the minimum extra value needed to balance the operations
                minExtra = Math.Min(minExtra, (val ^ k) - val);
                // Increment the count of XOR operations that increased values
                count++;
            }
            else
            {
                // If XORing the value does not increase it, add the original value to the sum
                sum += val;
                // Update the minimum extra value needed to balance the operations
                minExtra = Math.Min(minExtra, val - (val ^ k));
            }
        }

        // If the count of operations that increased values is even, return the sum as is
        if (count % 2 == 0)
        {
            return sum;
        }
        else
        {
            // If the count is odd, subtract the minimum extra value to balance the operation count
            return sum - minExtra;
        }
    }
}
