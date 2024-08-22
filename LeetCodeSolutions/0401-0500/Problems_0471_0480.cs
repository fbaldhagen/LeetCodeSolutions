namespace LeetCodeSolutions._0401_0500;

public class Problems_0471_0480
{
    /// <summary>
    /// Problem 476
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public static int FindComplement(int num)
    {
        // Calculate the number of bits in the binary representation of num
        int bitLength = (int)Math.Log2(num) + 1;

        // Create a mask with all bits set to 1 of the same length as num
        int mask = (1 << bitLength) - 1;

        // Calculate the complement by XORing num with the mask
        return num ^ mask;
    }
}
