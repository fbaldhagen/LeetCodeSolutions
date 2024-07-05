using LeetCodeSolutions.Structures;

namespace LeetCodeSolutions._2001_2100;

public class Problems_2051_2060
{
    /// <summary>
    /// Problem 2058
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public static int[] NodesBetweenCriticalPoints(ListNode head)
    {
        // Needs to be at least 4 nodes to get a distance between two critical points
        if (head == null || head.next == null || head.next.next == null)
        {
            return [-1, -1];
        }

        // Keep a list of indices of critical points.
        List<int> criticalPoints = [];

        // Pointers to the node and the surrounding nodes.
        ListNode prev = head;
        ListNode curr = head.next;
        ListNode next = curr.next;

        // Start at index 1 since 0 cannot be a critical point.
        int index = 1;
        while (next is not null)
        {
            // Conditions for a critical point
            if ((curr.val > prev.val && curr.val > next.val) ||
            (curr.val < prev.val && curr.val < next.val))
            {
                criticalPoints.Add(index);
            }

            // "Move on" in the list
            prev = curr;
            curr = next;

            // Must be a 'next' node for there to be any more critical points.
            if (next.next is null)
            {
                break;
            }
            next = next.next;
            index++;
        }

        // Must be at least 2 critical points to determine a distance between them.
        if (criticalPoints.Count < 2)
        {
            return [-1, -1];
        }

        int minDistance = int.MaxValue;
        for (int i = 1; i < criticalPoints.Count; i++)
        {
            minDistance = Math.Min(minDistance, criticalPoints[i] - criticalPoints[i - 1]);
            // Minumum distance is 1, no need to keep calculating distances.
            if (minDistance == 1)
            {
                break;
            }
        }

        // Max distance is simpler, just the last index - first index
        int maxDistance = criticalPoints[^1] - criticalPoints[0];

        return [minDistance, maxDistance];
    }
}
