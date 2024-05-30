namespace LeetCodeSolutions._1401_1500;

public class Problems_1441_1450
{
    /// <summary>
    /// Problem 1442
    /// </summary>
    /// <param name="arr"></param>
    /// <returns></returns>
    public static int CountTriplets(int[] arr)
    {
        int count = 0;

        // Iterate over the starting point of the triplet (i)
        for (int i = 0; i < arr.Length - 1; i++)
        {
            // Initialize xor with the first element in the current subarray
            int xor = arr[i];

            // Iterate over the ending point of the triplet (k)
            for (int k = i + 1; k < arr.Length; k++)
            {
                // Compute the cumulative xor from arr[i] to arr[k]
                xor ^= arr[k];

                // If xor is 0, we found a valid triplet
                if (xor == 0)
                {
                    // All j (i < j <= k) form valid triplets with i and k
                    count += k - i;
                }
            }
        }

        return count;
    }
}
