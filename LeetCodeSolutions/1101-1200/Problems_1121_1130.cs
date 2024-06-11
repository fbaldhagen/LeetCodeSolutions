namespace LeetCodeSolutions._1101_1200;

public class Problems_1121_1130
{
    /// <summary>
    /// Problem 1122
    /// </summary>
    /// <param name="arr1"></param>
    /// <param name="arr2"></param>
    /// <returns></returns>
    public static int[] RelativeSortArray(int[] arr1, int[] arr2)
    {
        // Keep track of frequencies in arr1
        Dictionary<int, int> freq = [];
        foreach (int i in arr1)
        {
            if (!freq.TryGetValue(i, out int value))
            {
                value = 0;
            }
            freq[i] = ++value;
        }

        int[] result = new int[arr1.Length];
        int curr = 0;

        // Iterate over arr2. Add elements from arr1 if they exist in both arrays.
        foreach (int num in arr2)
        {
            if (freq.TryGetValue(num, out int value))
            {
                for (int j = 0; j < value; j++)
                {
                    result[curr++] = num;
                }
                // Remove the element from the dictionary after adding them to the result array.
                freq.Remove(num);
            }
        }

        // Grab all the integers from arr1 that didnt occur in arr2.
        int[] remainingNums = [.. freq.Keys];
        // Sort in ascending/non-descending order
        Array.Sort(remainingNums);

        // Iterate over the remaining numbers add add them to the end of the result array.
        foreach (int num in remainingNums)
        {
            int count = freq[num];

            for (int i = 0; i < count; i++)
            {
                result[curr++] = num;
            }
        }

        return result;
    }
}
