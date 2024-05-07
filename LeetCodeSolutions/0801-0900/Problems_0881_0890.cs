namespace LeetCodeSolutions._0801_0900;

public class Problems_0881_0890
{
    /// <summary>
    /// Problem 881
    /// </summary>
    /// <param name="people"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    public static int NumRescueBoats(int[] people, int limit)
    {
        // Sort the array of people in non-descending order
        Array.Sort(people);

        // Initialize variables and pointers.
        // left: lightest person that hasn't gotten in a boat
        // right: heaviest person that hasn't gotten in a boat
        int boats = 0;
        int left = 0;
        int right = people.Length - 1;

        // Keep going as long as left <= right.
        // Idea is to get the lightest and heaviest people in the same boat.
        // If that weighs too much, let the heavy person get a solo boat.
        while (left <= right)
        {
            // Can two people get in the same boat?
            if (people[left] + people[right] <= limit)
            {
                left++;
            }

            // Always a boat for the heavy person, could be that a light person is in it.
            right--;
            boats++;
        }

        return boats;
    }
}
