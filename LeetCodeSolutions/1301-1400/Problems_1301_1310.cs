namespace LeetCodeSolutions._1301_1400;

public class Problems_1301_1310
{
    /// <summary>
    /// Problem 1310
    /// </summary>
    /// <param name="arr"></param>
    /// <param name="queries"></param>
    /// <returns></returns>
    public static int[] XorQueries(int[] arr, int[][] queries)
    {
        int n = arr.Length;

        // Compute prefix XORs, XOR from arr[0] to arr[i]
        int[] prefixSums = new int[n];

        // First element
        prefixSums[0] = arr[0];

        // Iterate over the array, compute prefix XORs
        for (int i = 1; i < n; i++)
        {
            prefixSums[i] = prefixSums[i - 1] ^ arr[i];
        }

        int[] answer = new int[queries.Length];

        for (int query = 0; query < queries.Length; query++)
        {
            int left = queries[query][0];
            int right = queries[query][1];

            if (left == 0)
            {
                answer[query] = prefixSums[right];
            }
            else
            {
                answer[query] = prefixSums[right] ^ prefixSums[left - 1];
            }
        }

        return answer;
    }
}
