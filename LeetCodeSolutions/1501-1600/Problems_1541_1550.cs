namespace LeetCodeSolutions._1501_1600;

public class Problems_1541_1550
{
    /// <summary>
    /// Problem 1550
    /// </summary>
    /// <param name="arr"></param>
    /// <returns></returns>
    public static bool ThreeConsecutiveOdds(int[] arr)
    {
        // Keep track of consecutive odd elements
        int counter = 0;

        // Iterate over each element
        foreach (int num in arr)
        {
            // If element is odd - increment and check if conditions are filled. Else reset counter.
            if (num % 2 != 0)
            {
                counter++;

                if (counter == 3)
                {
                    return true;
                }
            }
            else
            {
                counter = 0;
            }
        }
        
        // No three consecutive odd elements in the array.
        return false;
    }
}
