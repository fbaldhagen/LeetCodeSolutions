namespace LeetCodeSolutions._0201_0300;

public class Problems_0261_0270
{
    // 261 is premium

    /// <summary>
    /// Problem 262
    /// </summary>
    public static void TripsAndUsers()
    {
        // SELECT
        //     request_at AS Day,
        //     ROUND(
        //         SUM(CASE WHEN status IN ('cancelled_by_driver', 'cancelled_by_client') THEN 1 ELSE 0 END) /
        //         NULLIF(
        //             COUNT(*),
        //             0
        //         ),
        //         2
        //     ) AS `Cancellation Rate`
        // FROM 
        //     Trips t
        // JOIN 
        //     Users uc ON t.client_id = uc.users_id AND uc.banned = 'No'
        // JOIN
        //     Users ud ON t.driver_id = ud.users_id AND ud.banned = 'No'
        // WHERE
        //     request_at BETWEEN '2013-10-01' AND '2013-10-03'
        // GROUP BY 
        //     request_at
        // ORDER BY 
        //     request_at;
    }

    /// <summary>
    /// Problem 263
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static bool IsUgly(int n)
    {
        // Ugly numbers are positive
        if (n <= 0)
        {
            return false;
        }

        // Repeatedly divide the input number n by 2, 3, and 5 until it cannot be divided anymore
        while (n % 2 == 0)
        {
            n /= 2;
        }

        while (n % 3 == 0)
        {
            n /= 3;
        }

        while (n % 5 == 0)
        {
            n /= 5;
        }

        // Check if the result is 1, which would make it an ugly number.
        return n == 1;
    }

    /// <summary>
    /// Problem 264
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int NthUglyNumber(int n)
    {
        // Initialize an array to store the ugly numbers
        int[] ugly = new int[n];

        // The first ugly number is 1
        ugly[0] = 1;

        // Pointers to track the next ugly number to be multiplied by 2, 3, and 5 respectively
        int p2 = 0, p3 = 0, p5 = 0;

        // Iterate from the second ugly number to the nth
        for (int i = 1; i < n; i++)
        {
            // Calculate the next ugly number by finding the minimum among the products of
            // the current ugly numbers pointed by p2, p3, and p5 with 2, 3, and 5 respectively
            ugly[i] = Math.Min(ugly[p2] * 2, Math.Min(ugly[p3] * 3, ugly[p5] * 5));

            // If the next ugly number is obtained by multiplying the current ugly number pointed by p2 with 2,
            // move the p2 pointer to the next ugly number
            if (ugly[i] == ugly[p2] * 2)
            {
                p2++;
            }

            // Similarly, if the next ugly number is obtained by multiplying the current ugly number pointed by p3 with 3,
            // move the p3 pointer to the next ugly number
            if (ugly[i] == ugly[p3] * 3)
            {
                p3++;
            }

            // If the next ugly number is obtained by multiplying the current ugly number pointed by p5 with 5,
            // move the p5 pointer to the next ugly number
            if (ugly[i] == ugly[p5] * 5)
            {
                p5++;
            }
        }

        // Return the nth ugly number
        return ugly[n - 1];
    }

    // 265 - 267 premium

    /// <summary>
    /// Problem 268
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int MissingNumber(int[] nums)
    {
        // The sum of an array with values [0, n] where n is a non-negative integer
        // can be calculated using the formula for the sum of an arithmetic series:
        // Sum = n * (n + 1) / 2. We have n numbers, ie. one is missing.
        
        int n = nums.Length;
        int sumOfComplete = n * (n + 1) / 2;

        int mySum = 0;
        
        for (int i = 0; i < n; i++)
        {
            mySum += nums[i];
        }

        return sumOfComplete - mySum;
    }
}
