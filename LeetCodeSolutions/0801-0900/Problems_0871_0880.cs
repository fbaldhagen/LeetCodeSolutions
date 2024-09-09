namespace LeetCodeSolutions._0801_0900;

public class Problems_0871_0880
{
    /// <summary>
    /// Problem 874
    /// </summary>
    /// <param name="commands"></param>
    /// <param name="obstacles"></param>
    /// <returns></returns>
    public static int RobotSim(int[] commands, int[][] obstacles)
    {
        // Initialize robot's starting position and direction
        (int x, int y) position = (0, 0);
        int currDir = 0; // 0 = North, 1 = East, 2 = South, 3 = West
        int maxDist = 0;

        // Define movement directions: north, east, south, west
        int[][] directions =
        [
                [0, 1],   // North
                [1, 0],   // East
                [0, -1],  // South
                [-1, 0]   // West
        ];

        // Convert obstacles to a set of tuples for quick lookup
        var obst = new HashSet<(int, int)>();
        foreach (var obstacle in obstacles)
        {
            obst.Add((obstacle[0], obstacle[1]));
        }

        // Process each command
        foreach (int command in commands)
        {
            if (command == -2) // Turn left
            {
                currDir = (currDir + 3) % 4; // Turn left: equivalent to -1 mod 4
            }
            else if (command == -1) // Turn right
            {
                currDir = (currDir + 1) % 4; // Turn right: equivalent to +1 mod 4
            }
            else // Move forward `command` steps
            {
                // Try moving step by step up to `command` units
                for (int step = 0; step < command; step++)
                {
                    // Calculate the next position
                    int nextX = position.x + directions[currDir][0];
                    int nextY = position.y + directions[currDir][1];

                    // Check if the next position is an obstacle
                    if (!obst.Contains((nextX, nextY)))
                    {
                        // Move to the next position
                        position = (nextX, nextY);

                        // Update the maximum distance squared from the origin
                        int currentDist = position.x * position.x + position.y * position.y;
                        maxDist = Math.Max(maxDist, currentDist);
                    }
                    else
                    {
                        // Stop moving if the next move hits an obstacle
                        break;
                    }
                }
            }
        }

        return maxDist;
    }
}