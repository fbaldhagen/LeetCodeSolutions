namespace LeetCodeSolutions.Structures;

/// <summary>
/// Definition for a binary tree node, used in several problems. <br></br>
/// Has an integer value (val) as well as left and right pointers to other nodes in the tree.
/// </summary>
public class TreeNode
{
    public int val;
    public TreeNode? left;
    public TreeNode? right;

    public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }
}