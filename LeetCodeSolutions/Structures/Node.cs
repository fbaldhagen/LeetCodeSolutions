namespace LeetCodeSolutions.Structures;

/// <summary>
/// Definition for a Node, used in several problems. <br></br>
/// Has an integer value (val) as well as left, right and next pointers to other Nodes.
/// </summary>
public class Node
{
    public int val;
    public Node? left;
    public Node? right;
    public Node? next;

    public Node() { }

    public Node(int _val)
    {
        val = _val;
    }

    public Node(int _val, Node _left, Node _right, Node _next)
    {
        val = _val;
        left = _left;
        right = _right;
        next = _next;
    }
}