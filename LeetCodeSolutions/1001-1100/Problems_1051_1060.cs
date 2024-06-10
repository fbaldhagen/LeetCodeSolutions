namespace LeetCodeSolutions._1001_1100;

public class Problems_1051_1060
{
    /// <summary>
    /// Problem 1051
    /// </summary>
    /// <param name="heights"></param>
    /// <returns></returns>
    public static int HeightChecker(int[] heights)
    {
        int result = 0;

        // Copy heights and sort the new array. 
        int[] expected = new int[heights.Length];
        Array.Copy(heights, expected, heights.Length);
        Array.Sort(expected);
        
        // Check for mismatches, increment result if mismatched.
        for (int i = 0; i < expected.Length; i++)
        {
            if (heights[i] != expected[i])
            {
                result++;
            }
        }

        return result;
    }
}
