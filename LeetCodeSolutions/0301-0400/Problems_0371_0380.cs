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

    /// <summary>
    /// Problem 372
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int SuperPow(int a, int[] b)
    {
        const int MOD = 1337;
        return SuperPowHelper(a % MOD, b, b.Length - 1);

        static int SuperPowHelper(int a, int[] b, int index)
        {
            if (index == -1)
            {
                return 1;
            }

            // Calculate (a^b[index]) % MOD
            int part1 = PowMod(a, b[index]);

            // Calculate (previous result^10) % MOD
            int part2 = PowMod(SuperPowHelper(a, b, index - 1), 10);

            // Multiply both parts and return the result modulo MOD
            return (part1 * part2) % MOD;

            static int PowMod(int x, int n)
            {
                int result = 1;
                x %= MOD;
                while (n > 0)
                {
                    if (n % 2 == 1)
                    {
                        result = (result * x) % MOD;
                    }
                    x = (x * x) % MOD;
                    n /= 2;
                }
                return result;
            }
        }
    }
}