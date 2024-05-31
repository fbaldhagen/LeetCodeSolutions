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
