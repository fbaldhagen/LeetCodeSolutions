using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCodeSolutions._2001_2100;

public class Problems_2021_2030
{
    /// <summary>
    /// Problem 2022
    /// </summary>
    /// <param name="original"></param>
    /// <param name="m"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int[][] Construct2DArray(int[] original, int m, int n)
    {
        // Step 1: Check if the total number of elements matches the desired dimensions
        if (original.Length != m * n)
        {
            return [];
        }

        // Step 2: Initialize the 2D array
        int[][] result = new int[m][];
        for (int i = 0; i < m; i++)
        {
            result[i] = new int[n];
        }

        // Step 3: Fill the 2D array with elements from the 1D array
        int index = 0;
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                result[i][j] = original[index++];
            }
        }

        return result;
    }
}
