namespace Lab12;

public class PointHS<TKey, TValue>
{
    public TKey? Key { get; set; } 
    public TValue? Value { get; set; }
    public PointHS<TKey, TValue>? Next { get; set; }

    public PointHS()
    {
        Key = default(TKey);
        Value = default(TValue);
        Next = null;
    }
    
    public PointHS(TKey key, TValue value)
    {
        Key = key;
        Value = value;
        Next = null;
    }

    public override string ToString()
    {
        string keyString = Key?.ToString() ?? "null";
        string valueString = Value?.ToString() ?? "null";
        return $"({keyString}: {valueString})";
    }

    public override int GetHashCode()
    {
        return Key?.GetHashCode() ?? 0;
    }
}

public class MyHashTable<TKey, TValue>
{
    private const int defaultCapacity = 10;
    private PointHS<TKey, TValue>[] table;
    private int count;
    
    public int Count => count;

    public MyHashTable(int capacity = defaultCapacity)
    {
        table = new PointHS<TKey, TValue>[capacity];
        count = 0;
    }

    public void Add(TKey key, TValue value)
    {
        if (key == null) throw new ArgumentNullException(nameof(key));
        if (value == null) throw new ArgumentNullException(nameof(value));
        
        int index = Math.Abs(key.GetHashCode()) % table.Length;
        if (table[index] == null)
        {
            table[index] = new PointHS<TKey, TValue>(key, value);
            count++;
        }
        else
        {
            PointHS<TKey, TValue> current = table[index];
            while (current.Next != null)
            {
                if (current.Key.Equals(key)) return ;
                current = current.Next;
            }

            if (!current.Key.Equals(key))
            {
                current.Next = new PointHS<TKey, TValue>(key, value);
                count++;
            }
        }
    }

    public bool ContainsKey(TKey key)
    {
        if (key == null) throw new ArgumentNullException(nameof(key));
        
        int index = Math.Abs(key.GetHashCode()) % table.Length;
        if (table[index] == null) return false;
        
        PointHS<TKey, TValue>? current = table[index];
        while (current != null)
        {
            if (current.Key.Equals(key)) return true;
            current = current.Next;
        }
        return false;
    }

    public PointHS<TKey, TValue>? SearchElemnt(TKey key)
    {
        if (key == null) throw new ArgumentNullException(nameof(key));
        
        int index = Math.Abs(key.GetHashCode()) % table.Length;
        if (table[index] == null) return null;
        
        PointHS<TKey, TValue>? current = table[index];
        while (current != null)
        {
            if (current.Key.Equals(key)) return current;
            current = current.Next;
        }
        return null;
    }

    public bool Remove(TKey key)
    {
        if (key == null) throw new ArgumentNullException(nameof(key));
        int index = Math.Abs(key.GetHashCode()) % table.Length;
        
        if (table[index] == null) return false;
        PointHS<TKey, TValue>? current = table[index];
        if (current.Key.Equals(key))
        {
            table[index] = current.Next;
            return true;
        }
        PointHS<TKey, TValue> previous = current;
        current = current.Next;
        while (current != null)
        {
            if (current.Key.Equals(key))
            {
                previous.Next = current.Next;
                return true;
            }
            previous = current;
            current = current.Next;
        }
        return false;
    }

    public void Clear()
    {
        table = new PointHS<TKey, TValue>[defaultCapacity];
    }

    public void PrintHashTable()
    {
        if (table == null) throw new NullReferenceException("Таблица не создана!");

        for (int i = 0; i < table.Length; i++)
        {
            Console.Write($"{i}: ");
            if (table[i] == null) Console.WriteLine("null");
            else
            {
                PointHS<TKey, TValue> current = table[i];
                while (current != null)
                {
                    if (current.Next != null) Console.Write($"Key: {current.Key}, Value: {current.Value};\n   ");
                    else Console.WriteLine($"Key: {current.Key}, Value: {current.Value}.");
                    current = current.Next;
                }
            }
        }
    }

    // public object Clone()
    // {
    //     PointHS<TKey, TValue>[] newTable = new PointHS<TKey, TValue>[table.Length];
    //     if (table != null) return null;
    //
    //     for (int i = 0; i < table.Length; i++)
    //     {
    //         PointHS<TKey, TValue>? current = table[i].Clone();
    //         
    //     }
    //     
    //     return newTable;
    // }
}