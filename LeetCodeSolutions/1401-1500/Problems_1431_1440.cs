namespace LeetCodeSolutions._1401_1500;

public class Problems_1431_1440
{
    public static int LongestSubarray(int[] nums, int limit)
    {
        int left = 0;  // Left pointer of the sliding window
        int maxLength = 0;  // Variable to store the maximum length of the valid subarray
        LinkedList<int> maxDeque = new();  // Deque to store indices of elements in decreasing order
        LinkedList<int> minDeque = new();  // Deque to store indices of elements in increasing order

        // Iterate through the array with the right pointer
        for (int right = 0; right < nums.Length; right++)
        {
            // Maintain the max deque to keep the maximum value in the current window
            while (maxDeque.Count > 0 && nums[maxDeque.Last.Value] <= nums[right])
            {
                // Remove elements that are less than or equal to the current element
                maxDeque.RemoveLast();  
            }
            // Add the current element's index to the max deque
            maxDeque.AddLast(right);  

            // Maintain the min deque to keep the minimum value in the current window
            while (minDeque.Count > 0 && nums[minDeque.Last.Value] >= nums[right])
            {
                // Remove elements that are greater than or equal to the current element
                minDeque.RemoveLast();  
            }
            // Add the current element's index to the min deque
            minDeque.AddLast(right); 

            // Ensure the current window is valid by checking the difference between max and min elements
            while (nums[maxDeque.First.Value] - nums[minDeque.First.Value] > limit)
            {
                // Move the left pointer to shrink the window
                left++;  

                // Remove elements that are out of the window's range
                if (maxDeque.First.Value < left)
                {
                    // Remove the max element that is out of the current window
                    maxDeque.RemoveFirst();  
                }
                if (minDeque.First.Value < left)
                {
                    // Remove the min element that is out of the current window
                    minDeque.RemoveFirst();  
                }
            }

            // Calculate the maximum length of the valid subarray
            maxLength = Math.Max(maxLength, right - left + 1);
        }

        // Return the maximum length of the valid subarray
        return maxLength;  
    }
}
