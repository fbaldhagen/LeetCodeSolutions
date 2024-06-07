using LeetCodeSolutions.Structures;

namespace LeetCodeSolutions._0201_0300;

public class Problems_0291_0300
{
    /// <summary>
    /// Problem 292
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static bool CanWinNim(int n)
    {
        // If n is not a multiple of 4, you can win the game.
        // If n is a multiple of 4, you will lose if both players play optimally.
        return n % 4 != 0;
    }

    /// <summary>
    /// Problem 299
    /// </summary>
    /// <param name="secret"></param>
    /// <param name="guess"></param>
    /// <returns></returns>
    public static string GetHint(string secret, string guess)
    {
        int bulls = 0;
        int cows = 0;

        Dictionary<char, int> dict = [];

        foreach (char c in secret)
        {
            if (dict.TryGetValue(c, out int value))
            {
                dict[c] = ++value;
            }
            else
            {
                dict.Add(c, 1);
            }
        }

        // Find all bulls
        for (int i = 0; i < guess.Length; i++)
        {
            if (guess[i] == secret[i])
            {
                bulls++;

                int value = dict[guess[i]];
                if (value == 1)
                {
                    dict.Remove(guess[i]);
                }
                else
                {
                    dict[guess[i]] = --value;
                }
            }
        }


        // Find cows
        for (int i = 0; i < guess.Length; i++)
        {
            if (guess[i] != secret[i] && dict.TryGetValue(guess[i], out int value))
            {
                cows++;

                if (value == 1)
                {
                    dict.Remove(guess[i]);
                }
                else
                {
                    dict[guess[i]] = --value;
                }
            }
        }

        return bulls + "A" + cows + "B";
    }
}


/// <summary>
/// Problem 295
/// </summary>
public class MedianFinder
{
    private readonly PriorityQueue<int, int> left = new();
    private readonly PriorityQueue<int, int> right = new();
    bool odd = false;

    public void AddNum(int n)
    {
        odd = !odd;
        int m = right.EnqueueDequeue(n, -n);
        left.Enqueue(m, m);

        if (left.Count - 1 > right.Count)
        {
            m = left.Dequeue();
            right.Enqueue(m, -m);
        }
    }

    public double FindMedian()
    {
        return odd ? left.Peek() : (left.Peek() + right.Peek()) / 2.0;
    }
}

/// <summary>
/// Problem 297
/// </summary>
public class Codec
{

    // Encodes a tree to a single string.
    public string Serialize(TreeNode root)
    {
        if (root == null) return "#";

        List<string> result = [];
        SerializeHelper(root, result);
        return string.Join(",", result);
    }

    private static void SerializeHelper(TreeNode node, List<string> result)
    {
        if (node == null)
        {
            result.Add("#");
            return;
        }

        result.Add(node.val.ToString());
        SerializeHelper(node.left, result);
        SerializeHelper(node.right, result);
    }

    // Decodes your encoded data to tree.
    public TreeNode Deserialize(string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            return null;
        }

        Queue<string> nodes = new(data.Split(','));
        return DeserializeHelper(nodes);
    }

    private TreeNode DeserializeHelper(Queue<string> nodes)
    {
        if (nodes.Count == 0)
        {
            return null;
        }

        string value = nodes.Dequeue();
        if (value == "#")
        {
            return null;
        }

        TreeNode node = new(int.Parse(value))
        {
            left = DeserializeHelper(nodes),
            right = DeserializeHelper(nodes)
        };

        return node;
    }
}