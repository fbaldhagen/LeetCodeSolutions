namespace LeetCodeSolutions._0901_1000;

public class Problems_0991_1000
{
    /// <summary>
    /// Problem 995
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static int MinKBitFlips(int[] nums, int k)
    {
        int count = 0; // Initialize the flip count
        int flip = 0; // This variable will help us keep track of the current flip state

        // Early return if k is greater than the length of the array
        if (k > nums.Length)
        {
            return -1;
        }

        // Iterate through the array
        for (int i = 0; i < nums.Length; i++)
        {
            // If we've passed at least k elements, update the flip state
            // This step "removes" the effect of the flip that started k positions before
            if (i >= k)
            {
                flip ^= 1 - nums[i - k];
            }

            // If the current element, considering the flips, is 0, we need to flip the next k elements
            if (nums[i] == flip)
            {
                // Check if flipping k elements from the current position would go out of bounds
                if (i + k > nums.Length)
                {
                    return -1; // If it does, return -1 as it's not possible to flip to achieve the desired result
                }

                // Perform the flip by updating the current element and flip state
                nums[i] = 0; // Mark the start of a flip
                flip ^= 1; // Toggle the flip state
                count++; // Increment the flip count
            }
            else
            {
                // If the current element is already 1, just mark it as not needing a flip
                nums[i] = 1;
            }
        }

        return count; // Return the total number of flips performed
    }

}
