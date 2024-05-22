using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace LeetCodeSolutions._0201_0300;

public class Problems_0271_0280
{
    /// <summary>
    /// Problem 273
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public static string NumberToWords(int num)
    {
        // Handle the special case where the number is 0
        if (num == 0)
        {
            return "Zero";
        }

        // Define the words for numbers from 0 to 19, and the tens multiples from 20 to 90
        string[] words = {
            "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven",
            "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen",
            "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"
        };

        // Call the recursive helper function to build the word representation
        return Process(words, num);

        // Helper function to convert a number to words using the provided words array
        static string Process(string[] words, int num)
        {
            string ans = "";

            // Handle billions
            if (num >= 1000000000)
            {
                ans += Process(words, num / 1000000000) + " Billion " + Process(words, num % 1000000000);
            }
            // Handle millions
            else if (num >= 1000000)
            {
                ans += Process(words, num / 1000000) + " Million " + Process(words, num % 1000000);
            }
            // Handle thousands
            else if (num >= 1000)
            {
                ans += Process(words, num / 1000) + " Thousand " + Process(words, num % 1000);
            }
            // Handle hundreds
            else if (num >= 100)
            {
                ans += Process(words, num / 100) + " Hundred " + Process(words, num % 100);
            }
            // Handle numbers from 20 to 99
            else if (num >= 20)
            {
                ans += words[(num - 20) / 10 + 20] + " " + Process(words, num % 10);
            }
            // Handle numbers from 0 to 19
            else
            {
                ans += words[num];
            }

            // Trim any trailing whitespace and return the result
            return ans.Trim();
        }
    }

    /// <summary>
    /// Problem 274
    /// </summary>
    /// <param name="citations"></param>
    /// <returns></returns>
    public static int HIndex(int[] citations)
    {
        // sort the array in ascending order.
        Array.Sort(citations);
        int n = citations.Length;

        // for each element at index i, calculate the potential h-index as n - i 
        // (the number of papers that have citations greater than or equal to the current value).
        for (int i = 0; i < n; i++)
        {
            int h = n - i;
            if (citations[i] >= h)
            {
                return h;
            }
        }

        return 0;
    }

    /// <summary>
    /// Problem 275
    /// </summary>
    /// <param name="citations"></param>
    /// <returns></returns>
    public static int HIndexII(int[] citations)
    {
        int n = citations.Length;

        int left = 0;
        int right = n - 1;
        int currHIndex = 0;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            // calculate the potential h-index as n - current index (mid)
            if (citations[mid] >= n - mid)
            {
                currHIndex = n - mid;
                right = mid - 1;
            }
            else
            {
                left = mid + 1;
            }
        }

        return currHIndex;
    }

    /// <summary>
    /// Problem 279
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int NumSquares(int n)
    {
        int[] dp = new int[n + 1];
        Array.Fill(dp, int.MaxValue);

        // Base case: 0 requires 0 perfect squares
        dp[0] = 0;

        for (int i = 1; i <= n; i++)
        {
            // Initialize j for perfect squares calculation
            int j = 1;
            // Iterate through perfect squares less than or equal to i
            while (j * j <= i)
            {
                // Update dp[i] with the minimum of its current value and the value obtained by subtracting a perfect square and adding 1
                dp[i] = Math.Min(dp[i], dp[i - j * j] + 1);
                j++;
            }
        }

        return dp[n];
    }
}