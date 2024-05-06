namespace LeetCodeSolutions.Structures;

/// <summary>
/// Definition for a ListNode in a singly linked list, used in several problems. <br></br>
/// Has an integer value (val) and a pointer to the next Node in the linked list (or null if there isn't any)
/// </summary>
public class ListNode
{
    public int val;
    public ListNode? next;

    public ListNode(int val = 0, ListNode? next = null)
    {
        this.val = val;
        this.next = next;
    }
}
