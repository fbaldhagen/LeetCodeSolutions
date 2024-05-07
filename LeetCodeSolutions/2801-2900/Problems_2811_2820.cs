using LeetCodeSolutions.Structures;

namespace LeetCodeSolutions._2801_2900;

public class Problems_2811_2820
{
    /// <summary>
    /// Problem 2816
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public static ListNode DoubleIt(ListNode head)
    {
        // Call the recursive helper method to perform the doubling operation and get the final carry
        int carry = DoubleItHelper(head);

        // If there's a carry after doubling, create a new head node with the carry value.
        if (carry > 0)
        {
            ListNode newHead = new()
            {
                val = carry,
                next = head
            };

            head = newHead;
        }

        // Return the head of the doubled list.
        return head;

        // Recursive helper method to traverse the linked list in reversed order and double the value
        static int DoubleItHelper(ListNode? node)
        {
            if (node is null)
            {
                return 0;
            }

            // Recursively call the helper for the next node. Keep the carry.
            int carry = DoubleItHelper(node.next);

            // Double the value of the node, return the carry.
            int sum = node.val * 2 + carry;
            node.val = sum % 10;
            return sum / 10;
        }
    }
}
