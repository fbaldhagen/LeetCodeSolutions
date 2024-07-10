namespace LeetCodeSolutions._1501_1600;

public class Problems_1591_1600
{
  /// <summary>
  /// Problem 1598
  /// </summary>
  public int MinOperations(string[] logs)
  {
    int depth = 0;

    foreach (var log in logs)
    {
        if (log == "../")
        {
            if (depth > 0)
            {
                depth--;
            }
        }
        else if (log != "./")
        {
            depth++;
        }
    }

    return depth;
  }
}
