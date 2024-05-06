using LeetCodeSolutions.Structures;
namespace LeetCodeSolutions._0101_0200;

public class Problems_0141_0150
{
    /// <summary>
    /// Problem 141
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public static bool HasCycle(ListNode head)
    {
        if (head is null)
        {
            return false;

        }

        ListNode slow = head;
        ListNode fast = head;

        while (fast is not null && fast.next is not null)
        {
            slow = slow.next;
            fast = fast.next.next;
            if (fast == slow)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Problem 142
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public static ListNode DetectCycle(ListNode head)
    {
        // Turtoise & Hare
        ListNode fast = head;
        ListNode slow = head;

        while (fast is not null && fast.next is not null)
        {
            slow = slow.next;
            fast = fast.next.next;

            if (fast == slow)
            {
                // Cycle found, start looking for the starting node
                // slow is reset to the head of the list, fast remains at the meeting point where they collided.
                slow = head;

                // When the fast and slow pointers meet within the loop, they are at some distance k from the starting node of the loop. 
                // Fast was k steps ahead of slow when they met within the loop, both pointers are now moving towards the starting
                // node of the loop but with a relative distance of k between them.
                while (slow != fast)
                {
                    slow = slow.next;
                    fast = fast.next;
                }
                return slow;
            }
        }
        return null;
    }

    /// <summary>
    /// Problem 143
    /// </summary>
    /// <param name="head"></param>
    public static void ReorderList(ListNode head)
    {
        if (head is null || head.next is null)
        {
            return;
        }

        // Find the middle using slow and fast pointers
        ListNode slow = head;
        ListNode fast = head;

        while (fast.next is not null && fast.next.next is not null)
        {
            slow = slow.next;
            fast = fast.next.next;
        }

        // Separate the two halfs
        ListNode current = slow.next;
        ListNode previous = null;
        slow.next = null;

        // Reverse the second half
        while (current is not null)
        {
            ListNode next = current.next;
            current.next = previous;
            previous = current;
            current = next;
        }

        // Head is the head of the first half, previous is the head of the reversed list
        ListNode first = head;
        ListNode second = previous;

        // Merge the first and second halves alternatively
        while (second is not null)
        {
            ListNode nextFirst = first.next;
            ListNode nextSecond = second.next;

            first.next = second;
            second.next = nextFirst;
            first = nextFirst;
            second = nextSecond;
        }
    }

    /// <summary>
    /// Problem 144
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public static IList<int> PreorderTraversal(TreeNode root)
    {
        IList<int> result = [];
        RecursiveHelper(root);
        return result;

        void RecursiveHelper(TreeNode node)
        {
            if (node is null)
            {
                return;
            }

            result.Add(node.val);
            RecursiveHelper(node.left);
            RecursiveHelper(node.right);
        }
    }

    /// <summary>
    /// Problem 145
    /// </summary>
    /// <param name="root"></param>
    /// <returns></returns>
    public static IList<int> PostorderTraversal(TreeNode root)
    {
        IList<int> result = [];
        RecursiveHelper(root);
        return result;

        void RecursiveHelper(TreeNode node)
        {
            if (node is null)
            {
                return;
            }

            RecursiveHelper(node.left);
            RecursiveHelper(node.right);

            result.Add(node.val);
        }
    }

    // Problem 146 is solved in the separate file "LRUCache.cs"

    /// <summary>
    /// Problem 147
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public static ListNode InsertionSortList(ListNode head)
    {
        // If head is null, return null, if its only one node, return the node.
        if (head is null || head.next is null)
        {
            return head;
        }

        // Dummy node pointing to head
        ListNode dummy = new()
        {
            val = int.MinValue,
            next = head
        };

        // Keep track of the end of the sorted list and the last unsorted node
        ListNode sortedEnd = head;
        ListNode unsortedNode = head.next;

        // While there are still unsorted nodes left..
        while (unsortedNode is not null)
        {
            // Check if the value of the unsorted is larger than (or equal to)
            // the value of the last sorted node
            if (unsortedNode.val >= sortedEnd.val)
            {
                // update the end of the sorted node
                sortedEnd = sortedEnd.next;
            }
            else
            {
                // Find the place where the node should be placed
                ListNode prev = dummy;
                // Iterate over the list until we find the spot where we should place the node
                while (prev.next.val < unsortedNode.val)
                {
                    prev = prev.next;
                }

                // Remove the node from the end by setting the next pointer of the
                // sortedEnd node to the one unsortedNode points to
                sortedEnd.next = unsortedNode.next;

                // Insert the node in its updated position by updating the pointers
                unsortedNode.next = prev.next;
                prev.next = unsortedNode;
            }
            // Set unsortedNode to the first node after the sortedEnd
            unsortedNode = sortedEnd.next;
        }

        // Return the head of the list
        return dummy.next;
    }

    /// <summary>
    /// Problem 148
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    /// <remarks>Solves the problem using Merge sort. <br></br>
    /// Another approach is to traverse the linked list, adding the values to a list, sorting the list with built in methods, then creating a new linked list where you add new nodes with the values from the list.</remarks>
    public static ListNode SortList(ListNode head)
    {
        if (head is null || head.next is null)
        {
            return head;
        }

        // Split the list into two halves by using a slow and fast pointer
        // and a prev to separate the two halves
        ListNode prev = null;
        ListNode slow = head;
        ListNode fast = head;

        // Find the middle
        while (fast is not null && fast.next is not null)
        {
            prev = slow;
            slow = slow.next;
            fast = fast.next.next;
        }

        // Separate the two halves
        prev.next = null;

        ListNode left = SortList(head);
        ListNode right = SortList(slow);

        // Merge the two halves
        return Merge(left, right);

        // Merges two sub-lists into one list in ascending order
        static ListNode Merge(ListNode l1, ListNode l2)
        {
            ListNode dummy = new();
            ListNode current = dummy;

            while (l1 is not null && l2 is not null)
            {
                if (l1.val < l2.val)
                {
                    current.next = l1;
                    l1 = l1.next;
                }
                else
                {
                    current.next = l2;
                    l2 = l2.next;
                }
                current = current.next;
            }

            if (l1 is not null)
            {
                current.next = l1;
            }
            else
            {
                current.next = l2;
            }

            return dummy.next;
        }
    }

    /// <summary>
    /// Problem 149
    /// </summary>
    /// <param name="points"></param>
    /// <returns></returns>
    public static int MaxPoints(int[][] points)
    {
        // Since points is guaranteed to contain atleast one element, start with 1 as the current maximum
        int maxPoints = 1;

        for (int i = 0; i < points.Length; i++)
        {
            var slopes = new Dictionary<double, int>(); // Slope -> Count

            for (int j = i + 1; j < points.Length; j++)
            {
                // Calculate the slope between point[i] and point[j]
                int dx = points[j][0] - points[i][0];
                int dy = points[j][1] - points[i][1];

                // If there is dx is 0 (vertical line), set the slope to infinity (no division with 0), else normal dy/dx.
                double slope = dx == 0 ? double.PositiveInfinity : (double)dy / dx;

                // If the slope is the same between two points, increment by 1, else we set it to 1.
                if (!slopes.TryGetValue(slope, out int value))
                {
                    slopes[slope] = 1;
                }
                else
                {
                    slopes[slope] = ++value;
                }

                // Update the max points on a line
                maxPoints = Math.Max(maxPoints, slopes[slope] + 1); // Adding 1 for the current point
            }
        }

        return maxPoints;
    }

    /// <summary>
    /// Problem 150
    /// </summary>
    /// <param name="tokes"></param>
    /// <returns></returns>
    public static int EvalRPN(string[] tokens)
    {
        // Keep a stack of operands
        Stack<int> stack = new();

        // Go through all of the tokens
        foreach (string token in tokens)
        {
            if (!IsOperator(token))
            {
                // If the token isnt an operator, its an operand. Push it onto the stack.
                stack.Push(int.Parse(token));
            }
            else
            {
                // If the token is an operator, pop the two operands from the stack.
                int operand2 = stack.Pop();
                int operand1 = stack.Pop();

                // Perform the operation and store the result
                int result = PerformOperation(operand1, operand2, token);
                // Push the result onto the stack. It's either going to be used as an operand or get returend at the end of the method.
                stack.Push(result);
            }
        }

        return stack.Pop();

        // Check if a token is an operator or not.
        static bool IsOperator(string token)
        {
            return token == "+" || token == "-" || token == "*" || token == "/";
        }

        // Performs the operation based on the operator, and returns the result.
        static int PerformOperation(int operand1, int operand2, string op)
        {
            return op switch
            {
                "+" => operand1 + operand2,
                "-" => operand1 - operand2,
                "*" => operand1 * operand2,
                "/" => operand1 / operand2,
                _ => throw new ArgumentException("Invalid operator"),
            };
        }
    }
}


/// <summary>
/// Solution to Problem 146
/// </summary>
public class Problem_146
{
    /// <summary>
    /// My implementation of a Least Recently Used (LRU) cache.
    /// <br>Solution to Problem 146.</br>
    /// </summary>
    public class LRUCache
    {
        private int _capacity;
        private Dictionary<int, LRUNode> _cache;
        private LRUNode _head;
        private LRUNode _tail;

        public LRUCache(int capacity)
        {
            _capacity = Math.Abs(capacity);
            _cache = new Dictionary<int, LRUNode>();
            _head = new LRUNode(-1, -1);
            _tail = new LRUNode(-1, -1);
            _head.next = _tail;
            _tail.prev = _head;
        }

        public int Get(int key)
        {
            if (_cache.TryGetValue(key, out LRUNode value))
            {
                MoveToFront(value);
                return value.val;
            }
            return -1;
        }

        public void Put(int key, int valueToPut)
        {
            if (_cache.TryGetValue(key, out LRUNode? value))
            {
                LRUNode node = value;
                node.val = valueToPut;
                MoveToFront(node);
            }
            else
            {
                if (_cache.Count >= _capacity)
                {
                    RemoveLRU();
                }
                LRUNode newNode = new(key, valueToPut);
                _cache[key] = newNode;
                AddToFront(newNode);
            }
        }

        private void AddToFront(LRUNode node)
        {
            node.prev = _head;
            node.next = _head.next;
            _head.next.prev = node;
            _head.next = node;
        }

        private void RemoveNode(LRUNode node)
        {
            LRUNode prev = node.prev;
            LRUNode next = node.next;
            prev.next = next;
            next.prev = prev;
        }

        private void MoveToFront(LRUNode node)
        {
            RemoveNode(node);
            AddToFront(node);
        }

        private void RemoveLRU()
        {
            LRUNode lruNode = _tail.prev;
            RemoveNode(lruNode);
            _cache.Remove(lruNode.key);
        }
    }

    /// <summary>
    /// Node class that differs a bit from the other Nodes used so far. <br></br>
    /// Has 2 fields key and val, as well as two pointers (prev and next).
    /// </summary>
    public class LRUNode
    {
        public int key;
        public int val;
        public LRUNode prev;
        public LRUNode next;

        public LRUNode(int key, int val)
        {
            this.key = key;
            this.val = val;
        }
    }
}