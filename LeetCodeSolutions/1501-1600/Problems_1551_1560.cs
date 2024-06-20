namespace LeetCodeSolutions._1501_1600;

public class Problems_1551_1560
{
    /// <summary>
    /// Problem 1552
    /// </summary>
    /// <param name="position"></param>
    /// <param name="m"></param>
    /// <returns></returns>
    public static int MaxDistance(int[] position, int m)
    {
        Array.Sort(position);
        int left = 1;
        int right = position[^1] - position[0];

        while (left < right)
        {
            int mid = (right - left + 1) / 2 + left;
            if (CanPlaceBalls(position, m, mid))
            {
                left = mid; // Try for a larger minimum distance
            }
            else
            {
                right = mid - 1; // Try for a smaller minimum distance
            }
        }
        return left;

        static bool CanPlaceBalls(int[] position, int m, int minDist)
        {
            int count = 1;
            int lastPosition = position[0];

            for (int i = 1; i < position.Length; i++)
            {
                if (position[i] - lastPosition >= minDist)
                {
                    count++;
                    lastPosition = position[i];
                    if (count == m)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
