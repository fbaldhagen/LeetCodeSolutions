using LeetCodeSolutions.Structures;

namespace LeetCodeSolutions._0001_0100
{
    public class Problems_0001_0010
    {
        /// <summary>
        /// Problem 1
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int[] TwoSum(int[] nums, int target)
        {
            Dictionary<int, int> seen = new();

            int i = 0;
            foreach (int num in nums)
            {
                int diff = target - num;

                if (seen.TryGetValue(diff, out int value))
                {
                    return new int[] { value, i };
                }
                else
                {
                    seen[num] = i;
                }
                i++;
            }
            return new int[] { 0 };
        }

        /// <summary>
        /// Problem 2
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            ListNode dummyHead = new();
            ListNode current = dummyHead;
            int carry = 0;

            while (l1 != null || l2 != null || carry != 0)
            {
                int sum = carry;

                if (l1 != null)
                {
                    sum += l1.val;
                    l1 = l1.next;
                }

                if (l2 != null)
                {
                    sum += l2.val;
                    l2 = l2.next;

                }

                carry = sum / 10;
                current.next = new ListNode(sum % 10);
                current = current.next;
            }


            return dummyHead.next;

        }

        /// <summary>
        /// Problem 3
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int LengthOfLongestSubstring(string s)
        {
            if (s == null) return 0;
            if (s.Length == 1) return 1;

            HashSet<char> mySet = new();

            int l = 0;
            int r = 0;

            int maxLength = 0;
            while (l < s.Length && r < s.Length)
            {
                if (!mySet.Contains(s[r]))
                {
                    mySet.Add(s[r]);
                    r++;
                }
                else
                {
                    mySet.Remove(s[l]);
                    l++;
                }
                maxLength = Math.Max(maxLength, r - l);
            }
            return maxLength;
        }

        /// <summary>
        /// Problem 4
        /// </summary>
        /// <param name="nums1"></param>
        /// <param name="nums2"></param>
        /// <returns></returns>
        public static double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            // Ensure nums1 is the smaller array
            if (nums1.Length > nums2.Length)
            {
                // Swap arrays if nums1 is larger
                (nums2, nums1) = (nums1, nums2);
            }

            // Lengths of arrays nums1 and nums2
            int m = nums1.Length;
            int n = nums2.Length;

            // Initialize binary search pointers for nums1
            int low = 0;
            int high = m;

            // Binary search on the smaller array nums1
            while (low <= high)
            {
                // Calculate partition indices for nums1 and nums2
                int partitionX = (low + high) / 2;
                int partitionY = (m + n + 1) / 2 - partitionX;

                // Determine elements at partition boundaries for both arrays
                int maxX = (partitionX == 0) ? int.MinValue : nums1[partitionX - 1];
                int minX = (partitionX == m) ? int.MaxValue : nums1[partitionX];

                int maxY = (partitionY == 0) ? int.MinValue : nums2[partitionY - 1];
                int minY = (partitionY == n) ? int.MaxValue : nums2[partitionY];

                // Check if we found the correct partition
                if (maxX <= minY && maxY <= minX)
                {
                    // Calculate median based on even or odd total elements
                    if ((m + n) % 2 == 0)
                    {
                        return (Math.Max(maxX, maxY) + Math.Min(minX, minY)) / 2.0;
                    }
                    else
                    {
                        return Math.Max(maxX, maxY);
                    }
                }
                else if (maxX > minY)
                {
                    // Adjust the partition for nums1 to the left
                    high = partitionX - 1;
                }
                else
                {
                    // Adjust the partition for nums1 to the right
                    low = partitionX + 1;
                }
            }

            // Return 0 if arrays are not sorted or do not overlap
            return 0;
        }

        /// <summary>
        /// Problem 5
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string LongestPalindrome(string s)
        {
            if (s.Length < 2)
            {
                return s;
            }

            int start = 0;
            int end = 0;

            for (int i = 0; i < s.Length; i++)
            {
                int len1 = 0, len2 = 0;

                // Expand for odd-length palindrome
                for (int j = 0; i - j >= 0 && i + j < s.Length && s[i - j] == s[i + j]; j++)
                {
                    len1 = 2 * j + 1;
                }

                // Expand for even-length palindrome
                for (int j = 0; i - j >= 0 && i + 1 + j < s.Length && s[i - j] == s[i + 1 + j]; j++)
                {
                    len2 = 2 * j + 2;
                }

                int len = Math.Max(len1, len2);

                if (len > end - start)
                {
                    start = i - (len - 1) / 2;
                    end = i + len / 2;
                }
            }

            return s.Substring(start, end - start + 1);
        }

        /// <summary>
        /// Problem 6
        /// </summary>
        /// <param name="s"></param>
        /// <param name="numRows"></param>
        /// <returns></returns>
        public static string Convert(string s, int numRows)
        {
            if (numRows == 1)
            {
                return s;
            }

            string ans = "";

            int n = s.Length;
            int charsInSection = 2 * numRows - 2;

            for (int currRow = 0; currRow < numRows; currRow++)
            {
                int index = currRow;

                while (index < n)
                {
                    ans += s[index];

                    if (currRow != 0 && currRow != numRows - 1)
                    {
                        int charsBetween = charsInSection - 2 * currRow;
                        int nextIndex = index + charsBetween;

                        if (nextIndex < n)
                        {
                            ans += s[nextIndex];
                        }
                    }
                    index += charsInSection;
                }
            }
            return ans;
        }

        /// <summary>
        /// Problem 7
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int Reverse(int x)
        {
            int reverse = 0;

            while (x != 0)
            {
                if (reverse > int.MaxValue / 10 || (reverse == int.MaxValue / 10 && x % 10 > 7))
                {
                    return 0;
                }
                if (reverse < int.MinValue / 10 || (reverse == int.MaxValue / 10 && x % 10 < -8))
                {
                    return 0;
                }

                reverse = reverse * 10 + x % 10;
                x /= 10;
            }

            return reverse;
        }

        /// <summary>
        /// Problem 8
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int MyAtoi(string s)
        {
            int i = 0, sign = 1, result = 0;

            // Ignore leading whitespace
            while (i < s.Length && s[i] == ' ')
            {
                i++;
            }

            // Check sign
            if (i < s.Length && (s[i] == '+' || s[i] == '-'))
            {
                sign = (s[i++] == '+') ? 1 : -1;
            }

            // Convert digits
            while (i < s.Length && s[i] >= '0' && s[i] <= '9')
            {
                int digit = s[i++] - '0';
                // Check for overflow
                if (result > int.MaxValue / 10 || (result == int.MaxValue / 10 && digit > 7))
                {
                    return (sign == 1) ? int.MaxValue : int.MinValue;
                }

                result = result * 10 + digit;
            }

            return result * sign;
        }

        /// <summary>
        /// Problem 9
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static bool IsPalindrome(int x)
        {

            // Handle negative numbers and multiples of 10
            if (x < 0 || (x != 0 && x % 10 == 0))
            {
                return false;
            }

            int reversed = 0;
            while (x > reversed)
            {
                reversed = reversed * 10 + x % 10;
                x /= 10;
            }

            // Handle odd-length palindromes by ignoring the middle digit
            return (x == reversed || x == reversed / 10);
        }

        /// <summary>
        /// Problem 10
        /// </summary>
        /// <param name="s"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool IsMatch(string s, string p)
        {
            // Initialize the DP table
            int m = s.Length, n = p.Length;
            bool[,] dp = new bool[m + 1, n + 1];
            dp[0, 0] = true;

            // Handle patterns that start with '*'
            for (int j = 1; j <= n; j++)
            {
                if (p[j - 1] == '*')
                {
                    dp[0, j] = dp[0, j - 2];
                }
            }

            // Fill the DP table
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (p[j - 1] == '.' || p[j - 1] == s[i - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1];
                    }
                    else if (p[j - 1] == '*')
                    {
                        dp[i, j] = dp[i, j - 2]
                        || (dp[i - 1, j]
                        && (s[i - 1] == p[j - 2]
                        || p[j - 2] == '.'));
                    }
                    else
                    {
                        dp[i, j] = false;
                    }
                }
            }

            return dp[m, n];
        }
    }
}
