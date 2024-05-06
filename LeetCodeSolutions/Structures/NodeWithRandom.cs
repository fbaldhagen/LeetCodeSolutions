namespace LeetCodeSolutions.Structures;

/// <summary>
/// Definition for a NodeWithRandom. <br></br>
/// Has an integer value (val) as well as next and random pointers.
/// </summary>
public class NodeWithRandom
{
    public int val;
    public NodeWithRandom? next;
    public NodeWithRandom? random;

    public NodeWithRandom(int _val)
    {
        val = _val;
        next = null;
        random = null;
    }
}
