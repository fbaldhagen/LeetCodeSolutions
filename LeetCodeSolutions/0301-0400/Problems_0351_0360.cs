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

    /// <summary>
    /// Problem 354
    /// </summary>
    /// <param name="envelopes"></param>
    /// <returns></returns>
    public static int MaxEnvelopes(int[][] envelopes)
    {
        // Sort envelopes by width in ascending order.
        // If two envelopes have the same width, sort by height in descending order.
        Array.Sort(envelopes, (a, b) =>
        {
            if (a[0] == b[0])
            {
                // If widths are the same, sort by height in descending order.
                return b[1].CompareTo(a[1]);
            }
            else
            {
                // Otherwise, sort by width in ascending order.
                return a[0].CompareTo(b[0]);
            }
        });

        // List to store the heights of the longest increasing subsequence (LIS).
        List<int> maxLength = new(envelopes.Length);

        // Iterate over each envelope after sorting.
        foreach (int[] envelope in envelopes)
        {
            // Insert the height of the current envelope into the LIS.
            Insert(envelope[1]);
        }

        // The length of maxLength list represents the maximum number of envelopes that can be Russian-dolled.
        return maxLength.Count;

        // Helper method to insert the height into the LIS.
        void Insert(int height)
        {
            int left = 0;
            int right = maxLength.Count - 1;

            // Binary search to find the correct position to insert the height.
            while (left <= right)
            {
                int mid = (left + right) / 2;

                if (maxLength[mid] > height && (mid - 1 < 0 || height > maxLength[mid - 1]))
                {
                    // Replace the height at the correct position to maintain the smallest possible values.
                    maxLength[mid] = height;
                    break;
                }
                else if (maxLength[mid] < height)
                {
                    // If the current height is greater, move the left pointer to mid + 1.
                    left = mid + 1;
                }
                else
                {
                    // If the current height is smaller, move the right pointer to mid - 1.
                    right = mid - 1;
                }
            }

            // If the height is larger than all elements in maxLength, append it to the end.
            if (left == maxLength.Count)
            {
                maxLength.Add(height);
            }
        }
    }

    /// <summary>
    /// Problem 355
    /// </summary>
    public class Twitter
    {
        public List<Tweet> tweets;
        public Dictionary<int, List<int>> followers;

        public Twitter()
        {
            tweets = [];
            followers = [];
        }

        public void PostTweet(int userId, int tweetId)
        {
            tweets.Add(new Tweet(userId, tweetId));
        }

        public List<int> GetNewsFeed(int userId)
        {
            List<int> tw = [];
            int conta = 0;
            for (int i = tweets.Count - 1; (conta < 10) && i >= 0; i--)
            {
                int us = tweets[i].user;
                if (us == userId)
                {
                    tw.Add(tweets[i].tweet);
                    conta++;
                }
                else
                {
                    if (followers.TryGetValue(userId, out List<int>? value) && value.Contains(us))
                    {
                        tw.Add(tweets[i].tweet);
                        conta++;
                    }
                }
            }
            return tw;
        }

        public void Follow(int followerId, int followeeId)
        {
            if (followers.TryGetValue(followerId, out List<int>? value))
            {
                if (!value.Contains(followeeId))
                    value.Add(followeeId);
            }
            else
            {
                followers.Add(followerId, []);
                followers[followerId].Add(followeeId);
            }
        }

        public void Unfollow(int followerId, int followeeId)
        {
            if (followers.TryGetValue(followerId, out List<int>? value))
            {
                value.Remove(followeeId);
            }
        }

        public class Tweet(int user, int tweet)
        {
            public int user = user;
            public int tweet = tweet;
        }
    }
}