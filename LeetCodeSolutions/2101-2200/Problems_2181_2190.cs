using LeetCodeSolutions.Structures;

namespace LeetCodeSolutions._2101_2200;

public class Problems_2181_2190
{
    /// <summary>
    /// Problem 2181
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public static ListNode? MergeNodes(ListNode head)
    {
        // This is useless since we're guaranteed atleast 3 nodes. Easy way to get rid of warnings, though.
        if (head.next is null)
        {
            return null;
        }

        // Dummy ListNode to keep track of the head of the resulting list
        ListNode dummy = new(0);

        // Node to traverse the new list
        ListNode curr = dummy;

        // Skip initial zero
        head = head.next;

        int sum = 0;

        // Traverse the entire linked list
        while (head != null)
        {
            // When encountering a zero, create a new node with accumulated sum, move on to the next node and reset sum.
            if (head.val == 0)
            {
                curr.next = new ListNode(sum);
                curr = curr.next;
                sum = 0;
            }
            else
            {
                // Accumulate values between zeros
                sum += head.val;
            }

            // Move to the next node
            if (head.next is null)
            {
                break;
            }
            head = head.next;
        }

        // Return the head of the new list
        return dummy.next;
    }
}
