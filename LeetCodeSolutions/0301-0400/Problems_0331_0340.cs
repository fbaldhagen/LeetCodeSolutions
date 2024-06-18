namespace LeetCodeSolutions._0301_0400;

public class Problems_0331_0340
{
    /// <summary>
    /// Problem 331
    /// </summary>
    /// <param name="preorder"></param>
    /// <returns></returns>
    public static bool IsValidSerialization(string preorder)
    {
        // Split the string on ','
        string[] nodes = preorder.Split(',');

        // Each node (including null-nodes) takes up a slot. A non-null node creates 2 new.
        // Can never go below 0 slots.
        // Start off with one initial slot for the root node.
        int slots = 1;

        foreach (string node in nodes)
        {
            slots--;

            if (slots < 0)
            {
                return false;
            }

            if (!node.Equals("#"))
            {
                slots += 2;
            }
        }

        return slots == 0;
    }
}
