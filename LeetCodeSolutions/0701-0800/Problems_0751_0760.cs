namespace LeetCodeSolutions._0701_0800;

public class Problems_0751_0760
{
    /// <summary>
    /// Problem 752
    /// </summary>
    /// <param name="deadends"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static int OpenLock(string[] deadends, string target)
    {
        HashSet<string> deadSet = new(deadends);
        HashSet<string> visited = new();
        Queue<string> queue = new();

        queue.Enqueue("0000");
        visited.Add("0000");

        int level = 0;

        while (queue.Count > 0)
        {
            int size = queue.Count;
            for (int i = 0; i < size; i++)
            {
                string current = queue.Dequeue();
                if (deadSet.Contains(current))
                {
                    continue;
                }

                if (current == target)
                {
                    return level;
                }

                // Generate next possible combinations by rotating each wheel
                for (int j = 0; j < 4; j++)
                {
                    for (int k = -1; k <= 1; k += 2)
                    {
                        char[] nextWheel = current.ToCharArray();
                        nextWheel[j] = (char)(((nextWheel[j] - '0') + k + 10) % 10 + '0');
                        string next = new(nextWheel);

                        if (visited.Contains(next))
                        {
                            continue;
                        }
                        visited.Add(next);
                        queue.Enqueue(next);
                    }
                }
            }
            level++;
        }

        return -1; // Target not reachable
    }
}
