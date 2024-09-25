namespace LeetCodeSolutions._2401_2500;

public class Problems_2411_2420
{
    /// <summary>
    /// Problem 2416
    /// Placed in its own class with the appropriate internal classes.
    /// </summary>
    public class Problem2416
    {
        /// <summary>
        /// Main function to calculate the sum of prefix scores for each word
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public static int[] SumPrefixScores(string[] words)
        {
            // Create a Trie and insert all words into it
            Trie trie = new();

            foreach (string word in words)
            {
                trie.Insert(word);
            }

            // Array to store the result for each word
            int[] result = new int[words.Length];

            // For each word, calculate the sum of prefix scores using the Trie
            for (int i = 0; i < words.Length; i++)
            {
                result[i] = trie.GetPrefixScore(words[i]);
            }

            return result;
        }

        // Internal class representing a node in the Trie
        private class TrieNode
        {
            // Children array representing possible next characters (26 lowercase letters)
            public TrieNode[] Children { get; }

            // Count of how many words have passed through this node (prefix count)
            public int PrefixCount { get; set; }

            // Constructor initializes the children array and prefix count
            public TrieNode()
            {
                Children = new TrieNode[26]; // English alphabet, all lowercase
                PrefixCount = 0;
            }
        }

        // Internal class representing the Trie structure
        private class Trie
        {
            // Root node of the Trie
            private readonly TrieNode root;

            // Constructor initializes the root node
            public Trie()
            {
                root = new TrieNode();
            }

            // Insert a word into the Trie
            public void Insert(string word)
            {
                TrieNode node = root;

                // Traverse through the word's characters and update the Trie
                foreach (char c in word)
                {
                    int index = c - 'a';

                    // If there is no node for this character, create a new one
                    if (node.Children[index] == null)
                    {
                        node.Children[index] = new TrieNode();
                    }

                    // Move to the next node and increment the prefix count
                    node = node.Children[index];
                    node.PrefixCount++;
                }
            }

            // Get the total prefix score for a given word
            public int GetPrefixScore(string word)
            {
                TrieNode node = root;
                int score = 0;

                // Traverse the word and accumulate the prefix score
                foreach (char c in word)
                {
                    int index = c - 'a';

                    // If the character is not in the Trie, break out
                    if (node.Children[index] == null)
                    {
                        break;
                    }

                    // Move to the next node and add its prefix count to the score
                    node = node.Children[index];
                    score += node.PrefixCount;
                }

                return score;
            }
        }
    }
}
