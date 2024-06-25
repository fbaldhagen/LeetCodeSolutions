namespace LeetCodeSolutions.Structures.Trie
{
    public class Trie
    {
        public readonly TrieNode root;

        public Trie()
        {
            root = new TrieNode();
        }

        public void Insert(string word, int index)
        {
            TrieNode current = root;

            for (int i = word.Length - 1; i >= 0; i--)
            {
                char c = word[i];
                if (!current.Children.TryGetValue(c, out TrieNode value))
                {
                    value = new TrieNode();
                    current.Children[c] = value;
                }
                if (IsPalindrome(word, 0, i))
                {
                    current.PalindromeSuffixes.Add(index);
                }
                current = value;
            }

            current.IsEndOfWord = true;
            current.WordIndex = index;
            current.PalindromeSuffixes.Add(index);
        }

        private bool IsPalindrome(string word, int left, int right)
        {
            while (left < right)
            {
                if (word[left++] != word[right--])
                {
                    return false;
                }
            }
            return true;
        }
    }

    public class TrieNode
    {
        public Dictionary<char, TrieNode> Children { get; } = [];
        public bool IsEndOfWord { get; set; } = false;
        public int WordIndex { get; set; } = -1;
        public List<int> PalindromeSuffixes { get; } = [];
    }
}
