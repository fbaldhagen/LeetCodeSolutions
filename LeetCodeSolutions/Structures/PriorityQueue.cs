namespace LeetCodeSolutions.Structures;

public class PriorityQueue<T>
{
    private IList<T> heap;
    private readonly IComparer<T> comparer;

    public int Count => heap.Count;

    public PriorityQueue()
    {
        heap = new List<T>();
        comparer = Comparer<T>.Default;
    }

    public void Enqueue(T item)
    {
        heap.Add(item);
        int i = Count - 1;

        while (i > 0)
        {
            int parent = (i - 1) / 2;
            if (comparer.Compare(heap[parent], heap[i]) <= 0)
            {
                break;
            }
            heap = Helpers.Swap(heap, parent, i);
            i = parent;
        }
    }

    public T Dequeue()
    {
        if (Count == 0)
        {
            throw new InvalidOperationException("Queue is empty.");
        }

        T item = heap[0];
        int lastIndex = Count - 1;
        heap[0] = heap[lastIndex];

        heap.RemoveAt(lastIndex);
        lastIndex--;
        int i = 0;

        while (true)
        {
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left > lastIndex)
            {
                break;
            }

            int minChild = left;

            if (right <= lastIndex && comparer.Compare(heap[right], heap[left]) < 0)
            {
                minChild = right;
            }

            if (comparer.Compare(heap[i], heap[minChild]) <= 0)
            {
                break;
            }

            heap = Helpers.Swap(heap, i, minChild);
            i = minChild;
        }
        return item;
    }

    public T Peek()
    {
        if (Count == 0)
        {
            throw new InvalidOperationException("Queue is empty.");
        }

        return heap[0];
    }
}
