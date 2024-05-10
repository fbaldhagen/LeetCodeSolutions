namespace LeetCodeSolutions._0701_0800;
public class Problems_0781_0790
{
    /// <summary>
    /// Problem 786
    /// </summary>
    /// <param name="arr"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static int[] KthSmallestPrimeFraction(int[] arr, int k)
    {
        // Length of the array
        int n = arr.Length;
        // Initialize lower and upper bounds for binary search
        double low = 0;
        double high = 1;

        // Binary search loop
        while (low < high)
        {
            // Calculate the middle index
            double mid = low + (high - low) / 2;

            // Call the helper method to find fractions less than mid
            int[] res = GetFractionsLessThanMid(arr, k, n, mid);

            // Check if the k-th fraction is found
            if (res[0] == k)
            {
                // Return the k-th smallest prime fraction
                return [arr[res[1]], arr[res[2]]];
            }
            // If the number of fractions less than mid is greater than k, adjust the upper bound
            else if (res[0] > k)
            {
                high = mid;
            }
            // If the number of fractions less than mid is less than k, adjust the lower bound
            else
            {
                low = mid;
            }
        }

        // If the k-th smallest prime fraction is not found, return an empty array
        return [];

        // Helper method to find fractions less than mid
        static int[] GetFractionsLessThanMid(int[] arr, int k, int n, double mid)
        {
            // Initialize variables to track the maximum fraction less than mid and its indices
            double maxLessThanMid = 0.0;
            int x = 0, y = 0, total = 0, j = 1;

            // Loop through the array
            for (int i = 0; i < n - 1; i++)
            {
                // Increment j while the fraction is greater than mid
                while (j < n && arr[i] > arr[j] * mid)
                {
                    j++;
                }
                // If j reaches the end of the array, exit the loop
                if (j == n) break;

                // Count the number of fractions less than mid
                total += (n - j);

                // Calculate the current fraction
                double fraction = (double)arr[i] / arr[j];

                // Update variables if the current fraction is greater than the maximum fraction less than mid
                if (fraction > maxLessThanMid)
                {
                    maxLessThanMid = fraction;
                    x = i;
                    y = j;
                }
            }
            // Return the total count of fractions less than mid and the indices of the maximum fraction less than mid
            return [total, x, y];
        }
    }
}