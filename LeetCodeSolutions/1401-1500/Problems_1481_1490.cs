namespace LeetCodeSolutions._1401_1500;

public class Problems_1481_1490
{
    /// <summary>
    /// Problem 1482
    /// </summary>
    /// <param name="bloomDay"></param>
    /// <param name="m"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static int MinDays(int[] bloomDay, int m, int k)
    {
        // Possible range of days are [left, right]
        int left = bloomDay.Min();
        int right = bloomDay.Max();

        // Check the feasible range for solutions using binary search
        while (left < right)
        {
            // Calculate middle day and check if we can make flowers.
            int mid = left + (right - left) / 2;
            // If we can, set the day to the upper bound
            if (CanMakeBouquetsAtDay(mid))
            {
                right = mid;
            }
            // If we couldnt, set it to the lower bound
            else
            {
                left = mid + 1;
            }
        }

        // If we can make bouquets at the last remaining option, return it, else -1 (since no valid options)
        return CanMakeBouquetsAtDay(left) ? left : -1;

        bool CanMakeBouquetsAtDay(int day)
        {
            var bouquets = 0;
            var adjacent = 0;

            foreach (int bloom in bloomDay)
            {
                // Count adjacent flowers available for a bouquet
                adjacent = bloom <= day ? adjacent + 1 : 0;
                // If we have enough adjacent flowers for a bouquet, make one, else keep it
                bouquets = adjacent == k ? bouquets + 1 : bouquets;
                // If we could make a bouquet, reset adjacent to 0, else keep it
                adjacent = adjacent == k ? 0 : adjacent;
            }

            return bouquets >= m;
        }
    }
}
