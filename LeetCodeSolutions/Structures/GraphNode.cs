namespace LeetCodeSolutions.Structures;

/// <summary>
/// Definition for a GraphNode, a Node with a value (val) and a List of connected GraphNodes (neighbors)
/// </summary>
/// <remarks> Note that this differs from the "normal" Node used in many other problems. </remarks>
public class GraphNode
{
    public int val;
    public IList<GraphNode> neighbors;

    public GraphNode()
    {
        val = 0;
        neighbors = new List<GraphNode>();
    }

    public GraphNode(int _val)
    {
        val = _val;
        neighbors = new List<GraphNode>();
    }

    public GraphNode(int _val, List<GraphNode> _neighbors)
    {
        val = _val;
        neighbors = _neighbors;
    }
}