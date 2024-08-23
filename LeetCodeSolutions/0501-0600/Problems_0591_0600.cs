using System.Text.RegularExpressions;

namespace LeetCodeSolutions._0501_0600;

public class Problems_0591_0600
{
    /// <summary>
    /// Problem 592
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public static string FractionAddition(string expression)
    {
        // Split the expression into fractions using regular expression
        var matches = Regex.Matches(expression, @"[+-]?\d+/\d+");
        int numerator = 0;
        int denominator = 1;

        foreach (Match match in matches.Cast<Match>())
        {
            // Parse the current fraction
            var fraction = match.Value;
            var parts = fraction.Split('/');
            int num = int.Parse(parts[0]);
            int denom = int.Parse(parts[1]);

            // Calculate the numerator and denominator of the result
            numerator = numerator * denom + num * denominator;
            denominator *= denom;

            // Simplify the fraction by dividing by the greatest common divisor (GCD)
            int gcd = GCD(Math.Abs(numerator), denominator);
            numerator /= gcd;
            denominator /= gcd;
        }

        return numerator + "/" + denominator;

        static int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
    }
}
