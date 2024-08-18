namespace LeetCodeSolutions._0301_0400;

public class Problems_0371_0380
{
    /// <summary>
    /// Problem 371
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int GetSum(int a, int b)
    {
        // Continue until there is no carry left
        while (b != 0)
        {
            // Calculate the carry, which is where both bits are 1
            int carry = a & b;

            // XOR gives the sum without considering the carry
            a ^= b;

            // Shift the carry left by 1 to add it in the next higher bit position
            b = carry << 1;
        }

        // The sum is now stored in 'a'
        return a;
    }
}
