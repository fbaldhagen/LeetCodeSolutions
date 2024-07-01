namespace LeetCodeSolutions._0301_0400;

public class Problems_0351_0360
{
    /// <summary>
    /// Problem 352
    /// </summary>
    public class SummaryRanges
    {
        // SortedSet to store the numbers in the stream in sorted order without duplicates.
        private readonly SortedSet<int> values;

        // Constructor initializes the SortedSet.
        public SummaryRanges()
        {
            values = [];
        }

        // Adds a number to the SortedSet.
        public void AddNum(int value)
        {
            values.Add(value);
        }

        // Returns a list of disjoint intervals as a 2D array.
        public int[][] GetIntervals()
        {
            // If there are no values, return an empty array.
            if (values.Count == 0)
                return [];

            List<int[]> intervals = [];
            int left = -1, right = -1;

            // Iterate through the sorted values to form intervals.
            foreach (int value in values)
            {
                // If left is not set, start a new interval.
                if (left < 0)
                {
                    left = right = value;
                }
                // If the current value is consecutive, extend the current interval.
                else if (value == right + 1)
                {
                    right = value;
                }
                // If the current value is not consecutive, finalize the current interval and start a new one.
                else
                {
                    intervals.Add([left, right]);
                    left = right = value;
                }
            }

            // Add the last interval.
            intervals.Add([left, right]);

            // Convert the list of intervals to a 2D array and return.
            return [.. intervals];
        }
    }
}
