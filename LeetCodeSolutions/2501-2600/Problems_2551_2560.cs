namespace LeetCodeSolutions._2501_2600;

public class Problems_2551_2560
{
    /// <summary>
    /// Problem 2558
    /// </summary>
    /// <param name="gifts"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static long PickGifts(int[] gifts, int k)
    {
        PriorityQueue<int, int> pq = new();
        foreach (int gift in gifts)
        {
            pq.Enqueue(gift, -gift);
        }

        for (int i = 0; i < k; i++)
        {
            int maxGift = pq.Dequeue();
            int leftBehind = (int)Math.Sqrt(maxGift);
            pq.Enqueue(leftBehind, -leftBehind);
        }

        long sumLeft = 0;
        while (pq.Count > 0)
        {
            sumLeft += pq.Dequeue();
        }

        return sumLeft;
    }
}
