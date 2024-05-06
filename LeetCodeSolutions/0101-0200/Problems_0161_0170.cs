using System.Text;
namespace LeetCodeSolutions._0101_0200;

public class Problems_0161_0170
{
    // Problems 161, 163 and 170 are premium and therefore skipped

    /// <summary>
    /// Problem 162
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int FindPeakElement(int[] nums)
    {
        int n = nums.Length;

        int left = 0;
        int right = n - 1;

        while (left < right)
        {
            int mid = left + (right - left) / 2;

            if (nums[mid] > nums[mid + 1])
            {
                right = mid;
            }
            else
            {
                left = mid + 1;
            }
        }

        return left;
    }

    /// <summary>
    /// Problem 164
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int MaximumGap(int[] nums)
    {
        if (nums == null || nums.Length < 2)
        {
            return 0;
        }

        // Find the minimum and maximum elements in the array
        int minNum = nums[0];
        int maxNum = nums[0];
        foreach (int num in nums)
        {
            minNum = Math.Min(minNum, num);
            maxNum = Math.Max(maxNum, num);
        }

        // Calculate the bucket size and number of buckets
        int bucketSize = Math.Max(1, (maxNum - minNum) / (nums.Length - 1));
        int numBuckets = (maxNum - minNum) / bucketSize + 1;

        // Initialize buckets
        int[] minBucket = new int[numBuckets];
        int[] maxBucket = new int[numBuckets];
        for (int i = 0; i < numBuckets; i++)
        {
            minBucket[i] = int.MaxValue;
            maxBucket[i] = int.MinValue;
        }

        // Assign numbers into buckets
        foreach (int num in nums)
        {
            int bucketIndex = (num - minNum) / bucketSize;
            minBucket[bucketIndex] = Math.Min(minBucket[bucketIndex], num);
            maxBucket[bucketIndex] = Math.Max(maxBucket[bucketIndex], num);
        }

        // Calculate the maximum difference between buckets
        int maxGap = 0;
        int prevMax = minNum;
        for (int i = 0; i < numBuckets; i++)
        {
            if (minBucket[i] != int.MaxValue && maxBucket[i] != int.MinValue)
            {
                maxGap = Math.Max(maxGap, minBucket[i] - prevMax);
                prevMax = maxBucket[i];
            }
        }

        return maxGap;
    }

    /// <summary>
    /// Problem 165
    /// </summary>
    /// <param name="version1"></param>
    /// <param name="version2"></param>
    /// <returns></returns>
    public static int CompareVersion(string version1, string version2)
    {
        string[] v1 = version1.Split('.');
        string[] v2 = version2.Split('.');

        int maxLength = Math.Max(v1.Length, v2.Length);

        for (int i = 0; i < maxLength; i++)
        {
            int num1 = i < v1.Length ? int.Parse(v1[i]) : 0;
            int num2 = i < v2.Length ? int.Parse(v2[i]) : 0;

            if (num1 < num2)
            {
                return -1;
            }
            else if (num1 > num2)
            {
                return 1;
            }
        }

        return 0;
    }

    /// <summary>
    /// Problem 166
    /// </summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    /// <returns></returns>
    public static string FractionToDecimal(int numerator, int denominator)
    {
        if (numerator == 0)
            return "0";

        StringBuilder result = new();

        // Append sign
        if ((numerator < 0) ^ (denominator < 0))
            result.Append('-');

        // Convert to positive
        long num = Math.Abs((long)numerator);
        long den = Math.Abs((long)denominator);

        // Append integer part
        result.Append(num / den);
        long remainder = num % den;

        if (remainder == 0)
            return result.ToString();

        // Append fractional part
        result.Append('.');

        // Use a dictionary to store remainders and their positions
        Dictionary<long, int> remainderDict = [];
        while (remainder != 0)
        {
            if (remainderDict.TryGetValue(remainder, out int value))
            {
                // Found a repeating remainder
                result.Insert(value, "(");
                result.Append(')');
                break;
            }
            remainderDict[remainder] = result.Length;
            num = remainder * 10;
            result.Append(num / den);
            remainder = num % den;
        }

        return result.ToString();
    }

    /// <summary>
    /// Problem 167
    /// </summary>
    /// <param name="numbers"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static int[] TwoSum(int[] numbers, int target)
    {
        // Two pointer approach
        int left = 0;
        int right = numbers.Length - 1;

        while (left < right)
        {
            // Check if the current sum is more or less than the target sum
            // If it's less than the target sum, we need to increase the sum by swapping the small number for a larger one. 
            // If it's larger than the target sum, we swap the right to a smaller one. Since the array is sorted in non-decreasing order 
            // this is done easily by incrementing/decrementing the two pointers.
            int sum = numbers[left] + numbers[right];

            if (sum == target)
            {
                return new int[] { left + 1, right + 1 };
            }
            else if (sum < target)
            {
                left++;
            }
            else
            {
                right--;
            }
        }

        // The problem cases are guaranteed to have a unique solution, so we won't end up here.
        return null;
    }

    /// <summary>
    /// Problem 168
    /// </summary>
    /// <param name="columnNumber"></param>
    /// <returns></returns>
    public static string ConvertToTitle(int columnNumber)
    {
        StringBuilder title = new();

        while (columnNumber > 0)
        {
            columnNumber--;
            title.Insert(0, (char)('A' + columnNumber % 26));
            columnNumber /= 26;
        }

        return title.ToString();
    }

    /// <summary>
    /// Problem 169
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int MajorityElement(int[] nums)
    {
        int candidate = 0;
        int count = 0;

        foreach (int num in nums)
        {
            if (count == 0)
            {
                candidate = num;
                count++;
            }
            else if (candidate == num)
            {
                count++;
            }
            else
            {
                count--;
            }
        }

        return candidate;
    }

    // 170 is premium
}
