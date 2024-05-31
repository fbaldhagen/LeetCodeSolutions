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