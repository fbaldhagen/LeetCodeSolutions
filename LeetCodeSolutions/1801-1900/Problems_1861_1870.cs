namespace LeetCodeSolutions._1801_1900;

public class Problems_1861_1870
{
    /// <summary>
    /// Problem 1863
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int SubsetXORSum(int[] nums)
    {
        // Call recursive helper method starting with index 0 and initial XOR value 0
        return GetXOR(0, 0, nums);

        static int GetXOR(int i, int xor, int[] nums)
        {
            // Base case: if the current index is equal to the length of the array, return the accumulated XOR value
            if (i >= nums.Length)
            {
                return xor;
            }

            // Recursive case: calculate XOR sums for subsets including and excluding the current element
            // Calculate XOR sum for subsets that include the current element (nums[i])
            int leftXOR = GetXOR(i + 1, xor ^ nums[i], nums);

            // Calculate XOR sum for subsets that exclude the current element
            int rightXOR = GetXOR(i + 1, xor, nums);

            // Sum the results of both recursive calls to include all subsets
            return leftXOR + rightXOR;
        }
    }
}
