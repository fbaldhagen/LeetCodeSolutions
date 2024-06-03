using LeetCodeSolutions.Structures;

namespace LeetCodeSolutions._2401_2500;

public class Problems_2481_2490
{
    /// <summary>
    /// Problem 2486
    /// </summary>
    /// <param name="s"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static int AppendCharacters(string s, string t)
    {
        int sIndex = 0;
        int tIndex = 0;

        // Traverse through both strings
        while (sIndex < s.Length && tIndex < t.Length)
        {
            if (s[sIndex] == t[tIndex])
            {
                tIndex++;
            }
            sIndex++;
        }

        // Characters remaining in t after the loop are the ones that need to be appended
        return t.Length - tIndex;
    }

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