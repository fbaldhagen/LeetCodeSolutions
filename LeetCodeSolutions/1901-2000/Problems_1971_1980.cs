namespace LeetCodeSolutions._1901_2000;

public class Problems_1971_1980
{
    /// <summary>
    /// Problem 1975
    /// </summary>
    /// <param name="matrix"></param>
    /// <returns></returns>
    public static long MaxMatrixSum(int[][] matrix)
    {
        int n = matrix.Length;
        long sum = 0;
        int minAbs = int.MaxValue;
        int negativeCount = 0;

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                sum += Math.Abs(matrix[i][j]);
                if (matrix[i][j] < 0)
                {
                    negativeCount++;
                }
                minAbs = Math.Min(minAbs, Math.Abs(matrix[i][j]));
            }
        }

        if (negativeCount % 2 != 0)
        {
            sum -= 2 * minAbs;
        }

        return sum;
    }
}