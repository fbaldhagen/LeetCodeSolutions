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
}
