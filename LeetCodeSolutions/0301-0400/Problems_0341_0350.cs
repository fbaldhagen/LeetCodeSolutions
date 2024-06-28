namespace LeetCodeSolutions._0301_0400;

public class Problems_0341_0350
{
    /// <summary>
    /// Problem 342
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static bool IsPowerOfFour(int n)
    {
        // Make sure n is positive and a power of 2. If not, return false.
        if (n <= 0 || ((n & (n - 1)) != 0))
        {
            return false;
        }

        // Check if the single set bit is at an even position
        // Power of four numbers have their single '1' bit in even positions:
        // 1 (2^0), 4 (2^2), 16 (2^4), 64 (2^6), etc.
        // So, we can use n-1 to see if the number of trailing zeros is even
        return (n - 1) % 3 == 0;
    }

    /// <summary>
    /// Problem 344
    /// </summary>
    /// <param name="s"></param>
    public static void ReverseString(char[] s)
    {
        int i = 0;
        
        // Swap the ith char from the start with the ith char from the end, until at the middle.
        while (i < s.Length / 2)
        {
            (s[i], s[s.Length - 1 - i]) = (s[s.Length - 1 - i], s[i]);
            i++;
        }
    }

    /// <summary>
    /// Problem 349
    /// </summary>
    /// <param name="nums1"></param>
    /// <param name="nums2"></param>
    /// <returns></returns>
    public static int[] Intersection(int[] nums1, int[] nums2)
    {
        HashSet<int> set1 = new(nums1);

        HashSet<int> intersection = [];

        for (int i = 0; i < nums2.Length; i++)
        {
            if (set1.Contains(nums2[i]))
            {
                intersection.Add(nums2[i]);
            }
        }

        return [.. intersection];
    }
}