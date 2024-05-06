using LeetCodeSolutions.Structures;

namespace LeetCodeSolutions._2401_2500;

public class Problems_2481_2490
{
    /// <summary>
    /// Problem 2487
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public static ListNode RemoveNodes(ListNode head)
    {
        head = Reverse(head);
        int max = head.val;
        ListNode cur = head;

        while (cur.next != null)
        {
            if (max > cur.next.val)
            {
                cur.next = cur.next.next;
            }
            else
            {
                max = cur.next.val;
                cur = cur.next;
            }
        }

        return Reverse(head);

        static ListNode Reverse(ListNode cur)
        {
            ListNode prv = null;
            ListNode next = cur;

            while (next != null)
            {
                cur = next;
                next = cur.next;
                cur.next = prv;
                prv = cur;
            }
            return cur;
        }
    }
}