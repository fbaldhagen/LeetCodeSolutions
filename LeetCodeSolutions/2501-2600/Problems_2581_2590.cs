namespace LeetCodeSolutions._2501_2600;

public class Problems_2581_2590
{
    /// <summary>
    /// Problem 2584
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int FindValidSplit(int[] nums)
    {
        // Dictionary to store the last found index of each prime factor
        Dictionary<int, int> factorLastFound = [];

        // Function to get prime factors of a number at index idx
        HashSet<int> GetPrimeFactors(int idx)
        {
            int x = nums[idx];
            HashSet<int> res = [];
            for (int i = 2; i * i <= x; i++)
            {
                while (x % i == 0)
                {
                    x /= i;
                    res.Add(i);
                    factorLastFound[i] = idx;
                }
            }
            if (x > 1)
            {
                res.Add(x);
                factorLastFound[x] = idx;
            }
            return res;
        }

        // Calculate prime factors for each number in nums
        HashSet<int>[] factors = new HashSet<int>[nums.Length];
        for (int i = 0; i < nums.Length; i++)
        {
            factors[i] = GetPrimeFactors(i);
        }

        int res = -1, lastFactorFound = 0;
        // Iterate through each index of nums
        for (int i = 0; i < nums.Length - 1; i++)
        {
            HashSet<int> set1 = factors[i];
            foreach (var el in set1)
            {
                // If the last found index of any prime factor is the last index of nums, return the current result
                if (factorLastFound[el] == nums.Length - 1)
                {
                    return res;
                }
                lastFactorFound = Math.Max(factorLastFound[el], lastFactorFound);
            }
            // If the last found index is the current index, return the current result
            if (lastFactorFound == i)
            {
                return i;
            }
        }
        return res;
    }
}
