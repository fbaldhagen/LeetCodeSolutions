using LeetCodeSolutions.Structures;

namespace LeetCodeSolutions._2301_2400;

public class Problems_2321_2330
{
    /// <summary>
    /// Problem 2326
    /// </summary>
    /// <param name="m"></param>
    /// <param name="n"></param>
    /// <param name="head"></param>
    /// <returns></returns>
    public static int[][] SpiralMatrix(int m, int n, ListNode head)
    {
        int[][] result = new int[m][];

        for (int i = 0; i < m; i++)
        {
            result[i] = new int[n];
        }


        int firstRow = 0;
        int firstCol = 0;
        int lastRow = m - 1;
        int lastCol = n - 1;

        while (firstCol <= lastCol && firstRow <= lastRow)
        {
            for (int i = firstCol; i <= lastCol; i++)
            {
                result[firstRow][i] = (head == null) ? -1 : head.val;

                if (head is not null)
                {
                    head = head.next;
                }
            }
            firstRow++;

            for (int i = firstRow; i <= lastRow; i++)
            {
                result[i][lastCol] = (head == null) ? -1 : head.val;

                if (head is not null)
                {
                    head = head.next;
                }
            }
            lastCol--;

            if (firstRow <= lastRow)
            {
                for (int i = lastCol; i >= firstCol; i--)
                {
                    result[lastRow][i] = (head == null) ? -1 : head.val;

                    if (head is not null)
                    {
                        head = head.next;
                    }
                }
                lastRow--;
            }


            if (firstCol <= lastCol)
            {
                for (int i = lastRow; i >= firstRow; i--)
                {
                    result[i][firstCol] = (head == null) ? -1 : head.val;

                    if (head is not null)
                    {
                        head = head.next;
                    }
                }
                firstCol++;
            }
        }

        return result;
    }
}
