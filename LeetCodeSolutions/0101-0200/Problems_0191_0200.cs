using LeetCodeSolutions.Structures;

namespace LeetCodeSolutions._0101_0200;

public class Problems_0191_0200
{
    /// <summary>
    /// Problem 191
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int HammingWeight(int n)
    {
        // Initialize a counter for set bits
        int count = 0;

        // Iterate through each bit of the integer
        while (n != 0)
        {
            // Add the least significant bit to the count 
            count += n & 1;

            // Right shift the integer to check the next bit
            n >>= 1;
        }

        // Return the total count of set bits
        return count;
    }

    // Problem 192, Bash
    //#!/bin/bash

    //# Declare an associative array
    //    declare -A wordCounts

    //# Read the file line by line
    //while IFS= read -r line; do
    //    # For each line, read word by word
    //    for word in $line; do
    //        # Increment the word's count
    //        ((wordCounts["$word"]++))
    //    done
    //done<words.txt

    //# Sort the word counts in descending order by frequency
    //for word in "${!wordCounts[@]}"; do
    //    echo "$word ${wordCounts[$word]}"
    //done | sort -k2 -nr

    // Problem 193, Bash
    //grep -E '^\([0-9]{3}\) [0-9]{3}-[0-9]{4}$|^[0-9]{3}-[0-9]{3}-[0-9]{4}$' file.txt

    // Problem 194, Bash
    //awk '
    //{
    //    for (i = 1; i <= NF; i++) {
    //        if (NR == 1) {
    //            row[i] = $i;
    //        } else
    //{
    //    row[i] = row[i] " " $i;
    //}
    //    }
    //}
    //END {
    //    for (i = 1; row[i] != ""; i++) {
    //        print row[i];
    //    }
    //}' file.txt

    // Problem 195, Bash
    //sed -n '10p' file.txt

    // Problem 196, MySQL
    //DELETE p1 FROM Person p1
    //JOIN Person p2 ON p1.email = p2.email AND p1.id > p2.id;

    // Problem 197, MySQL
    //SELECT w.id FROM Weather w as Id
    //JOIN Weather w_prev ON w.recordDate = DATE_ADD(w_prev.recordDate, INTERVAL 1 DAY)
    //WHERE w.temperature > w_prev.temperature;

    /// <summary>
    /// Problem 198
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int Rob(int[] nums)
    {
        if (nums is null || nums.Length == 0)
        {
            return 0;
        }

        if (nums.Length == 1)
        {
            return nums[0];
        }

        int[] dp = new int[nums.Length];

        dp[0] = nums[0];
        dp[1] = Math.Max(nums[0], nums[1]);

        for (int i = 2; i < nums.Length; i++)
        {
            dp[i] = Math.Max(dp[i - 2] + nums[i], dp[i - 1]);
        }

        return dp[nums.Length - 1];
    }

    /// <summary>
    /// Problem 199
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public static IList<int> RightSideView(TreeNode root)
    {
        IList<int> rightView = [];

        if (root is null)
        {
            return rightView;
        }

        // Use a queue to traverse the tree
        Queue<TreeNode> queue = new();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            // Get the number of nodes in the current level
            int levelSize = queue.Count;

            // Reset the rightmost on the current level
            TreeNode rightMost = null;

            // Go through the entire level
            for (int i = 0; i < levelSize; i++)
            {
                TreeNode curr = queue.Dequeue();

                // Update the rightmost node (last to get dequeued on each level is the visible one)
                rightMost = curr;

                // Add child nodes to the queue
                if (curr.left is not null)
                {
                    queue.Enqueue(curr.left);
                }
                if (curr.right is not null)
                {
                    queue.Enqueue(curr.right);
                }
            }

            // Add the last dequeued node on the level to the rightView list.
            rightView.Add(rightMost.val);
        }

        return rightView;
    }

    /// <summary>
    /// Problem 200
    /// </summary>
    /// <returns></returns>
    public static int NumIslands(char[][] grid)
    {
        if (grid is null || grid.Length == 0)
        {
            return 0;
        }

        int m = grid.Length;
        int n = grid[0].Length;

        int count = 0;

        for (int row = 0; row < m; row++)
        {
            for (int col = 0; col < n; col++)
            {
                if (grid[row][col] == '1')
                {
                    DFS(grid, row, col);
                    count++;
                }
            }
        }

        return count;

        static void DFS(char[][] grid, int row, int col)
        {
            if (row < 0 ||
                row >= grid.Length ||
                col < 0 ||
                col >= grid[0].Length ||
                grid[row][col] == '0')
            {
                return;
            }

            grid[row][col] = '0';
            DFS(grid, row + 1, col);
            DFS(grid, row - 1, col);
            DFS(grid, row, col + 1);
            DFS(grid, row, col - 1);
        }
    }
}
