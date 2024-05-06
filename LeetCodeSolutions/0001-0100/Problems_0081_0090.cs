using LeetCodeSolutions.Structures;
namespace LeetCodeSolutions._0001_0100;

public class Problems_0081_0090
{
    /// <summary>
    /// Problem 81
    /// </summary>
    /// <returns></returns>
    public static bool Search(int[] nums, int target)
    {
        // modified binary search
        int left = 0;
        int right = nums.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (nums[mid] == target)
            {
                return true;
            }

            // Check if numbers at the edges and mid are the same. If they are - move pointers "inward".
            if (nums[left] == nums[mid] && nums[mid] == nums[right])
            {
                left++;
                right--;
            }
            else if (nums[left] <= nums[mid])
            {
                // check for rotation to determine which half the target would be in
                if (nums[left] <= target && target < nums[mid])
                {
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }
            else
            {
                if (nums[mid] < target && target < nums[right])
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Problem 82
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public static ListNode DeleteDuplicates(ListNode head)
    {
        // Create dummy node that points to head
        ListNode dummy = new(0, head);

        // Node to keep track of the latest unique node
        ListNode prev = dummy;

        while (head is not null)
        {
            // If there are duplicates, skip them and set prev's (last node with unique  value)
            // next pointer to the first new node with a unique value
            if (head.next is not null && head.val == head.next.val)
            {
                while (head.next is not null && head.val == head.next.val)
                {
                    head = head.next;
                }
                prev.next = head.next;
            }
            // If the next node is unique (or doesnt exist, ie. is null) we update prev
            else
            {
                prev = prev.next;
            }

            // Update head to the next node
            head = head.next;
        }

        // return the head of the linked list
        return dummy.next;
    }

    /// <summary>
    /// Problem 83
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public static ListNode DeleteDuplicates2(ListNode head)
    {
        // Check if there less than 2 nodes in the list. 0 or 1 -> return head since no dupes
        if (head is null || head.next is null)
        {
            return head;
        }
        // Node to traverse the linked list
        ListNode curr = head;

        // Traverse the entire linked list
        while (curr.next is not null)
        {
            // If there are duplicate values, move 2 steps and skip that node
            if (curr.val == curr.next.val)
            {
                curr.next = curr.next.next;
            }
            // No dupes, we traverse as usual
            else
            {
                curr = curr.next;
            }
        }
        // Return the head node
        return head;
    }

    /// <summary>
    /// Problem 84
    /// </summary>
    /// <returns></returns>
    public static int LargestRectangleArea(int[] heights)
    {
        // Set up a stack to keep track of heights
        Stack<int> stack = new();

        int maxArea = 0;
        // Keep track of index of heights
        int i = 0;

        while (i < heights.Length)
        {
            // If the stack is empty or the current height is greater than or equal to the height at the top of the stack
            if (stack.Count == 0 || heights[stack.Peek()] <= heights[i])
            {
                stack.Push(i++); // Add the index to the stack and move to the next index
            }
            else
            {
                int top = stack.Pop(); // Get the index of the previous height from the stack
                                       // Calculate the width of the rectangle
                int width = stack.Count == 0 ? i : i - stack.Peek() - 1;
                // Calculate the area of the rectangle and update the maximum area if necessary
                maxArea = Math.Max(maxArea, heights[top] * width);
            }
        }

        // After processing all heights, there might be some heights left in the stack
        // Calculate the area for the remaining heights
        while (stack.Count > 0)
        {
            int top = stack.Pop(); // Get the index of the height from the stack
                                   // Calculate the width of the rectangle
            int width = stack.Count == 0 ? i : i - stack.Peek() - 1;
            // Calculate the area of the rectangle and update the maximum area if necessary
            maxArea = Math.Max(maxArea, heights[top] * width);
        }

        return maxArea; // Return the maximum area found
    }

    /// <summary>
    /// Problem 85
    /// </summary>
    /// <returns></returns>
    public static int MaximalRectangle(char[][] matrix)
    {
        // Edge cases: if the matrix is null or empty, return 0 as there can't be any rectangle
        if (matrix is null || matrix.Length == 0 || matrix[0].Length == 0)
        {
            return 0;
        }

        // Variables to keep track of rows, cols, and the maximum area found
        int rows = matrix.Length;
        int cols = matrix[0].Length;
        int maxArea = 0;
        // Array to store heights of the current column
        int[] heights = new int[cols];

        // Iterate through each row
        for (int i = 0; i < rows; i++)
        {
            // Update the heights array based on the current row
            for (int j = 0; j < cols; j++)
            {
                if (matrix[i][j] == '1')
                {
                    heights[j]++;
                }
                else
                {
                    // If the current cell is '0', reset the height to 0
                    heights[j] = 0;
                }
            }
            // Calculate the largest rectangle area using the heights array
            maxArea = Math.Max(maxArea, LargestRectangleArea(heights));
        }

        return maxArea;

        // Function to calculate the largest rectangle area given heights of columns
        static int LargestRectangleArea(int[] heights)
        {
            // Stack to store indices of heights
            Stack<int> stack = new();
            int maxArea = 0;

            // Loop through each height and calculate the area
            for (int i = 0; i <= heights.Length; i++)
            {
                // Get the current height or set it to 0 if it's beyond the array bounds
                int currentHeight = i == heights.Length ? 0 : heights[i];

                // Process the heights in the stack
                while (stack.Count > 0 && currentHeight < heights[stack.Peek()])
                {
                    // Pop the top element from the stack
                    int height = heights[stack.Pop()];
                    // Calculate the width of the rectangle
                    int width = stack.Count == 0 ? i : i - stack.Peek() - 1;
                    // Calculate the area and update maxArea if necessary
                    maxArea = Math.Max(maxArea, height * width);
                }
                // Push the current index onto the stack
                stack.Push(i);
            }

            return maxArea;
        }
    }

    /// <summary>
    /// Problem 86
    /// </summary>
    /// <param name="head"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public static ListNode Partition(ListNode head, int x)
    {
        // Initialize two dummy nodes to hold elements less than x and greater than or equal to x respectively
        ListNode dummyLess = new(0);
        ListNode dummyGreaterEqual = new(0);

        // Initialize pointers to traverse the original linked list and the two partitions
        ListNode lessPointer = dummyLess;
        ListNode greaterEqualPointer = dummyGreaterEqual;


        // Traverse the original linked list
        while (head != null)
        {
            if (head.val < x)
            {
                // If the current node's value is less than x, add it to the "less than x" partition
                lessPointer.next = head;
                lessPointer = lessPointer.next;
            }
            else
            {
                // If the current node's value is greater than or equal to x, add it to the "greater than or equal to x" partition
                greaterEqualPointer.next = head;
                greaterEqualPointer = greaterEqualPointer.next;
            }
            // Move to the next node in the original linked list
            head = head.next;
        }

        // Connect the two partitions
        lessPointer.next = dummyGreaterEqual.next;
        // Set the end of the "greater than or equal to x" partition to null to avoid cycles
        greaterEqualPointer.next = null;

        // Return the head of the "less than x" partition
        return dummyLess.next;
    }

    /// <summary>
    /// Problem 87
    /// </summary>
    /// <param name="s1"></param>
    /// <param name="s2"></param>
    /// <returns></returns>
    public static bool IsScramble(string s1, string s2)
    {
        int len = s1.Length;

        // dp[i, j, k] will represent whether s2[j...] is a scrambled string of s1[i...] with length k
        bool[,,] dp = new bool[len, len, len + 1];

        // Base case: single character
        for (int i = 0; i < len; i++)
        {
            for (int j = 0; j < len; j++)
            {
                // A single character substring is a scrambled string only if the characters match
                dp[i, j, 1] = s1[i] == s2[j];
            }
        }

        // Build up from length 2 to len
        for (int k = 2; k <= len; k++)
        {
            // i represents the starting index in s1
            for (int i = 0; i <= len - k; i++)
            {
                // j represents the starting index in s2
                for (int j = 0; j <= len - k; j++)
                {
                    // l represents the length of the split point
                    for (int l = 1; l < k; l++)
                    {
                        // Check if s2[j...] is a scrambled string of s1[i...] with lengths l and k-l
                        // Two possibilities: 1) No swap between left and right parts; 2) Swap between left and right parts
                        if ((dp[i, j, l] && dp[i + l, j + l, k - l]) ||
                            (dp[i, j + k - l, l] && dp[i + l, j, k - l]))
                        {
                            // If either case is true, set dp[i, j, k] to true and break the loop
                            dp[i, j, k] = true;
                            break;
                        }
                    }
                }
            }
        }

        // Return whether s2 is a scrambled string of s1 starting from index 0 with length len
        return dp[0, 0, len];
    }

    /// <summary>
    /// Problem 88
    /// </summary>
    /// <param name="nums1"></param>
    /// <param name="m"></param>
    /// <param name="nums2"></param>
    /// <param name="n"></param>
    public static void Merge(int[] nums1, int m, int[] nums2, int n)
    {
        // Pointers to the last non-zero numbers of nums1 and nums2, respectively.
        int pointer1 = m - 1;
        int pointer2 = n - 1;

        // Index to track the current position where we are placing the merged elements.
        int currentPos = m + n - 1;

        // Loop until we have processed all elements from nums2.
        while (pointer2 >= 0)
        {
            // If there are still elements remaining in nums1, and the current element in nums1
            // is greater than the current element in nums2, copy the element from nums1 to
            // the current position in nums1 and move the pointer1 and currentPos accordingly.
            if (pointer1 >= 0 && nums1[pointer1] > nums2[pointer2])
            {
                nums1[currentPos] = nums1[pointer1];
                pointer1--;
            }
            // Otherwise, copy the current element from nums2 to the current position in nums1
            // and move the pointer2 and currentPos accordingly.
            else
            {
                nums1[currentPos] = nums2[pointer2];
                pointer2--;
            }
            // Move to the previous position in nums1 to place the next merged element.
            currentPos--;
        }
    }

    /// <summary>
    /// Problem 89
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static IList<int> GrayCode(int n)
    {
        // Initialize an empty list to store the gray code sequence.
        IList<int> result = [0];

        // Iterate through each bit position from 0 to n - 1.
        for (int i = 0; i < n; i++)
        {
            // Calculate the current size of the result list.
            int size = result.Count;

            // Calculate the mask to toggle the i-th bit using bitwise left shift.
            // for example i = 3 -> shift 00000001 to 00001000 (8)
            int mask = 1 << i;

            // Iterate through the existing elements in reverse order.
            // This ensures that we toggle only one bit at a time to maintain the gray code sequence.
            for (int j = size - 1; j >= 0; j--)
            {
                // Add new elements to the result list by performing bitwise OR with the mask.
                // This toggles the i-th bit in the binary representation of each existing element.
                result.Add(result[j] | mask);
            }
        }

        // Return the generated gray code sequence.
        return result;
    }

    /// <summary>
    /// Problem 90
    /// </summary>
    /// <returns></returns>
    public static IList<IList<int>> SubsetsWithDup(int[] nums)
    {
        // Sort to handle duplicates easily
        Array.Sort(nums);
        // Set up lists to hold result and current subset
        IList<IList<int>> subsets = new List<IList<int>>();
        IList<int> current = new List<int>();
        // Call backtracking algorithm with start index 0
        Backtrack(nums, subsets, current, 0);
        return subsets;

        static void Backtrack(int[] nums, IList<IList<int>> subsets, IList<int> currentSubset, int startIndex)
        {
            // Add the subset to the result
            subsets.Add(new List<int>(currentSubset));

            for (int i = startIndex; i < nums.Length; i++)
            {
                // Skip duplicates
                if (i > startIndex && nums[i] == nums[i - 1])
                {
                    continue;
                }
                // Add the number to the current subset and then basic backtracking
                currentSubset.Add(nums[i]);
                Backtrack(nums, subsets, currentSubset, i + 1);
                currentSubset.RemoveAt(currentSubset.Count - 1);
            }
        }
    }
}
