namespace LeetCodeSolutions._2001_2100;

public class Problems_2031_2040
{
    /// <summary>
    /// Problem 2037
    /// </summary>
    /// <param name="seats"></param>
    /// <param name="students"></param>
    /// <returns></returns>
    public static int MinMovesToSeat(int[] seats, int[] students)
    {
        Array.Sort(seats);
        Array.Sort(students);

        int diff = 0;
        for (int i = 0; i < seats.Length; i++)
        {
            diff += Math.Abs(students[i] - seats[i]);
        }

        return diff;
    }
}
