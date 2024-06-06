using System.Collections.Generic;

namespace LeetCodeSolutions._0801_0900;

public class Problems_0841_0850
{
    /// <summary>
    /// Problem 846
    /// </summary>
    /// <param name="hand"></param>
    /// <param name="groupSize"></param>
    /// <returns></returns>
    public static bool IsNStraightHand(int[] hand, int groupSize)
    {
        if (hand.Length % groupSize != 0)
        {
            return false;
        }

        Dictionary<int, int> map = [];

        foreach (int item in hand)
        {
            map[item] = map.GetValueOrDefault(item, 0) + 1;
        }

        Array.Sort(hand);

        foreach (int num in hand)
        {
            if (map[num] > 0)
            {
                for (int i = num + 1; i < num + groupSize; i++)
                {
                    if (!map.TryGetValue(i, out int value))
                    {
                        return false;
                    }

                    if (value == 0)
                    {
                        return false;
                    }
                    map[i] = --value;
                }

                map[num]--;
            }
        }

        return true;
    }
}
