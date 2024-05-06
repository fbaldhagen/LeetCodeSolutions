namespace LeetCodeSolutions;

/// <summary>
/// Class with helper methods used in many problems.
/// </summary>
public static class Helpers
{
    /// <summary>
    /// Swaps two elements in the list or array.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list or array.</typeparam>
    /// <param name="collection">The list or array in which to swap elements.</param>
    /// <param name="i">The index of the first element to swap.</param>
    /// <param name="j">The index of the second element to swap.</param>
    /// <returns>The collection with elements at positions <paramref name="i"/> and <paramref name="j"/> swapped.</returns>
    public static IList<T> Swap<T>(IList<T> collection, int i, int j)
    {
        (collection[j], collection[i]) = (collection[i], collection[j]);
        return collection;
    }

    /// <summary>
    /// Reverses the order of elements in the specified range of the array.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    /// <param name="array">The array in which to reverse elements.</param>
    /// <param name="left">The index of the first element of the range to reverse.</param>
    /// <param name="right">The index of the last element of the range to reverse.</param>
    /// <returns>The array with elements in the specified range reversed.</returns>
    public static T[] Reverse<T>(T[] nums, int left, int right)
    {
        while (left < right)
        {
            (nums[right], nums[left]) = (nums[left], nums[right]);
            left++;
            right--;
        }
        return nums;
    }

    /// <summary>
    /// Reverses a string
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ReverseString(string str)
    {
        char[] charArray = str.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
}
