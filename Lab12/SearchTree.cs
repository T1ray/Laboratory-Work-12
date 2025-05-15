    namespace Lab12;

    public class SearchTree<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        public SearchTreeNode<TKey, TValue>? Root { get; set; }
        
        public void Insert(TKey key, TValue value)
        {
            Root = Insert(Root, key, value);
        }
        
        private TKey CloneKeyIfPossible(TKey key)
        {
            if (key is ICloneable cloneableKey) return (TKey)cloneableKey.Clone();
            return key;
        }
        
        private TValue CloneValueIfPossible(TValue value)
        {
            if (value is ICloneable cloneableKey) return (TValue)cloneableKey.Clone();
            return value;
        }

        private SearchTreeNode<TKey, TValue> Insert(SearchTreeNode<TKey, TValue>? node, TKey key, TValue value)
        {
            if (node == null)
                return new SearchTreeNode<TKey, TValue>(CloneKeyIfPossible(key), CloneValueIfPossible(value));

            int compareResult = key.CompareTo(node.Key);
            if (compareResult < 0) node.Left = Insert(node.Left, key, value);
            else if (compareResult > 0) node.Right = Insert(node.Right, key, value);
            else Console.WriteLine($"Значение с ключом {key} уже существует. Значение проигнорировано!");
            return node;
        }

        public void PrintByLevel()
        {
            if (Root == null)
            {
                Console.WriteLine("Дерево пустое!");
                return;
            }
            
            Queue<SearchTreeNode<TKey,TValue>> queue = new Queue<SearchTreeNode<TKey,TValue>>();
            queue.Enqueue(Root);
            int level = 1;
            int numberElements = 1;
            
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Уровень №{level}");
            Console.ResetColor();
            
            while (queue.Count > 0)
            {
                if (numberElements == 0)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"\nУровень №{++level}");
                    Console.ResetColor();
                    numberElements = queue.Count;
                }
                
                SearchTreeNode<TKey,TValue> node = queue.Dequeue();
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Ключ: ");
                Console.ResetColor();
                Console.Write($"{node.Key.ToString()}; ");
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Значение: ");
                Console.ResetColor();
                Console.WriteLine(node.Value.ToString());
                
                if (node.Left != null) queue.Enqueue(node.Left);
                if (node.Right != null) queue.Enqueue(node.Right);
                numberElements--;
            }
        }

        public void Delete(TKey key)
        {
            Root = Delete(Root, key);
        }

        private SearchTreeNode<TKey, TValue>? Delete(SearchTreeNode<TKey, TValue>? node, TKey key)
        {
            if (node == null)
            {
                Console.WriteLine("Элемент с данным ключом не найден в дереве!");
                return node;
            }
            
            int resultCompare = key.CompareTo(node.Key);
            if (resultCompare < 0) node.Left = Delete(node.Left, key);
            else if (resultCompare > 0) node.Right = Delete(node.Right, key);
            else
            {
                if (node.Left == null) return node.Right;
                else if (node.Right == null) return node.Left;

                SearchTreeNode<TKey, TValue> minNode = node.Right;
                while (minNode.Left != null) minNode = minNode.Left;
                
                node.Key = minNode.Key;
                node.Value = minNode.Value;
                node.Right = Delete(node.Right, minNode.Key);
            }

            return node;
        }

        public SearchTreeNode<TKey, TValue>? Find(TKey key)
        {
            SearchTreeNode<TKey, TValue>? node = Root;

            while (node != null)
            {
                int compareResult = key.CompareTo(node.Key);
                if (compareResult < 0) node = node.Left;
                else if (compareResult > 0) node = node.Right;
                else return node;
            }
            return node;
        }
        
        public void Clear()
        {
            Root = null;
            Console.WriteLine("Дерево поиска очищено!");
        }
    }