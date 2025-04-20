using System.Collections;
using System.Drawing;
using Lab10;

namespace Lab12;

public class Point<T> 
{
    public T? Data { get; set; }
    public Point<T>? Next { get; set; }
    public Point<T>? Prev { get; set; }

    // public Point()
    // {
    //     Data = default(T);
    //     Next = null;
    //     Prev = null;
    // }

    public Point(T info)
    {
        Data = info;
        Next = null;
        Prev = null;
    }

    public override string ToString()
    {
        return Data?.ToString() ?? "null"; 
    }
}

public class DoubleLinkedList<T> : ICloneable where T : ICloneable
{
    public Point<T>? head;
    public Point<T>? tail;

    public DoubleLinkedList()
    {
        head = null;
        tail = null;
    }

    public void Add(T item)
    {
        AddLast(item);
    }

    public void Clear()
    {
        head = null;
        tail = null;
    }

    public bool Contains(T item)
    {
        Point<T>? current = head;
        while (current != null && !current.Data.Equals(item)) current = current.Next;
        return current != null;
    }
    
    // public void CopyTo(T[] array, int arrayIndex)
    // {
    //     if (array == null)
    //         throw new Exception("array is null");
    //     if (arrayIndex < 0)
    //         throw new Exception("arrayIndex is negative");
    //     if (array.Length - arrayIndex < Count)
    //         throw new Exception("array is too small");
    //     
    //     Point<T> current = head;
    //     int i = arrayIndex;
    //     while (current != null && i < array.Length)
    //     {
    //         array[i++] = current.Data;
    //         current = current.Next;
    //     }
    // }

    public bool RemoveLast(T item)
    {
        if (head == null) return false;
        
        Point<T>? toRemove = null;
        Point<T>? current = tail;
        while (current != null)
        {
            if (current.Data.Equals(item))
            {
                toRemove = current;
                break;
            }
            current = current.Prev;
        }
        
        if (toRemove == null) return false;

        Point<T>? previous = toRemove.Prev;
        Point<T>? next = toRemove.Next;
        
        if (previous != null)
        {
            previous.Next = next;
        }
        else
        {
            head = next;
        }

        if (next != null)
        {
            next.Prev = previous;
        }
        else
        {
            tail = previous;
        }
        return true;
    }

    public int Count
    {
        get
        {
            int count = 0;
            if (head == null) return count;
            Point<T>? current = head;
            while (current != null)
            {
                count++;
                current = current.Next;
            }
            return count;
        }
    }

    // private void AddFirstElement(T info)
    // {
    //     head = tail = new Point<T>(info);
    // }
    
    // public void AddFirst(T info)
    // {
    //     if (head == null)
    //     {
    //         AddFirstElement(info);
    //         return;
    //     }
    //     Point<T> current = new Point<T>(info);
    //     current.Next = head;
    //     head.Prev = current;
    //     head = current;
    // }

    public void AddLast(T info)
    {
        if (tail == null)
        {
            head = tail = new Point<T>(info);
            return;
        }
        Point<T> current = new Point<T>(info);
        current.Prev = tail;
        tail.Next = current;
        tail = current;
    }

    public bool Insert(T insertAfter, T insertedItem)
    {
        if (insertAfter == null) throw new Exception("Элемент, после которого необходимо вставить " +
                                                     "элемент не может быть null");
        if (insertedItem == null) throw new Exception("Вставляемый элемент должен быть отличен от null");
        
        Point<T>? current = head;
        while (current != null && !current.Data.Equals(insertAfter)) current = current.Next;
        if (current == null) return false;
        
        //Console.WriteLine($"Элемент {insertAfter}, после которого необходимо вставить элемент, не найден в двусвязном списке");
        
        Point<T> newPoint = new Point<T>(insertedItem);
        if (current == tail)
        {
            newPoint.Prev = current;
            current.Next = newPoint;
            tail = newPoint;
            return true;
        }
        
        newPoint.Next = current.Next;
        newPoint.Prev = current;
        current.Next.Prev = newPoint;
        current.Next = newPoint;
        return true;
    }

    /*
    public void Insert(int index, T item)
    {
        if (index < 0 || index >= Count) throw new IndexOutOfRangeException();
        int curIndex = 0;
        Point<T>? curPoint = head;
        while (curIndex < index)
        {
            curIndex++;
            curPoint = curPoint.Next;
        }
        Point <T> insertedElement = new Point<T>(item);
        if (Count == 0) AddFirstElement(item);
        else if (index == 0)
        {
            insertedElement.Next = head;
            head.Prev = insertedElement;
            head = insertedElement;
        }
        else if (index == Count)
        {
            insertedElement.Prev = tail;
            tail.Next = insertedElement;
            tail = insertedElement;
        }
        insertedElement.Next = curPoint;
        insertedElement.Prev = curPoint.Prev;
        (curPoint.Prev).Next = insertedElement;
        curPoint.Prev = insertedElement;
    }
    */

    public object Clone()
    {
        DoubleLinkedList<T> newList = new DoubleLinkedList<T>();
        if (head == null) return null;
        Point<T>? current = head;
        while (current != null)
        {
            newList.Add((T)(current.Data).Clone());
            current = current.Next;
        }
        return newList;
    }
    
    public void PrintList()
    {
        Point<T>? current = head;
        int count = 1;
        while (current != null)
        {
            Console.WriteLine($"{count++}) {current.Data}");
            current = current.Next;
        }
    }  
}