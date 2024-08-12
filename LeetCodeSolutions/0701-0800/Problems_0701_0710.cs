namespace LeetCodeSolutions._0701_0800;

public class Problems_0701_0710
{
    /// <summary>
    /// Problem 703
    /// </summary>
    public class KthLargest
    {
        private readonly int _k;
        private readonly PriorityQueue<int, int> _minHeap;

        public KthLargest(int k, int[] nums)
        {
            _k = k;
            _minHeap = new PriorityQueue<int, int>();

            foreach (int num in nums)
            {
                Add(num);
            }
        }

        public int Add(int val)
        {
            // Add the new value to the min heap
            _minHeap.Enqueue(val, val);

            // If the heap exceeds size k, remove the smallest element
            if (_minHeap.Count > _k)
            {
                _minHeap.Dequeue();
            }

            // The kth largest element will always be at the root of the heap
            return _minHeap.Peek();
        }
    }
}
