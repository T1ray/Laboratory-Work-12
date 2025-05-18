using System.Threading.Channels;

namespace Lab12;
using Lab10;
using static AdditionalFunctions.AdditionalFunctions;

public class DemonstrateProgram
{
    public static void DemonstrateDoubleLinkedList()
    {
        #region Инициализация переменных
        MusicalInstrument basicMusicalInstrumnet = new MusicalInstrument();
        basicMusicalInstrumnet.RandomInit();
        Guitar gutar = new Guitar(6, 700);
        ElectricGuitar electricGuitar = new ElectricGuitar("аккумулятор", 9, 123);
        Piano piano = new Piano(88, "шкальная", 234);
        MusicalInstrument musicalInstrument = new MusicalInstrument("Саксофон", 15);
        #endregion

        #region Создание и добавление элементов в список
        DoubleLinkedList<MusicalInstrument> instrumentsList = new DoubleLinkedList<MusicalInstrument>();
        instrumentsList.Add(basicMusicalInstrumnet);
        instrumentsList.Add(gutar);
        instrumentsList.Add(electricGuitar);
        instrumentsList.Add(piano);
        instrumentsList.Add(musicalInstrument);
        DoubleLinkedList<MusicalInstrument> copyInstrumentsList = (DoubleLinkedList<MusicalInstrument>)instrumentsList.Clone();
        #endregion
        
        #region Печать списка
        instrumentsList.PrintList();
        TextSeparator();
        #endregion
        
        #region Выполнение задания
        instrumentsList.RemoveLast((Piano)piano.Clone());
        Console.WriteLine("Список элементов после удаления элемента пианино");
        instrumentsList.PrintList();
        TextSeparator();

        ElectricGuitar addedElement = new ElectricGuitar("usb", 9, 69);
        Console.WriteLine($"Добавление элемента: {addedElement}\nпосле элемента {gutar}");
        instrumentsList.Insert(gutar, addedElement);
        Console.WriteLine("Список элементов добавления элемента");
        instrumentsList.PrintList();
        TextSeparator();
        #endregion
        
        #region Копия списка
        Console.WriteLine("Копия изначального списка инструментов");
        copyInstrumentsList.PrintList();
        TextSeparator();
        #endregion
        
        #region Удаление списка из памяти
        Console.WriteLine("Удаление списка из памяти");
        instrumentsList.Clear();
        instrumentsList.PrintList();
        TextSeparator();
        #endregion
    }

    public static void DemonstrateHashTable()
    {
        #region Инициализация переменных
        MyHashTable<int, MusicalInstrument> hashTable = new MyHashTable<int, MusicalInstrument>(10);
        
        MusicalInstrument violin = new MusicalInstrument("Скрипка", 510);
        Guitar gutar = new Guitar(18, 700);
        ElectricGuitar electricGuitar = new ElectricGuitar("аккумулятор", 9, 123);
        Piano piano = new Piano(88, "шкальная", 234);
        MusicalInstrument musicalInstrument = new MusicalInstrument("Саксофон", 15);
        #endregion

        #region Заполнение таблицы
        hashTable.Add(violin.Id.Id, violin);
        hashTable.Add(gutar.Id.Id, gutar);
        hashTable.Add(electricGuitar.Id.Id, electricGuitar);
        hashTable.Add(piano.Id.Id, piano);
        hashTable.Add(musicalInstrument.Id.Id, musicalInstrument);

        Console.WriteLine("Таблица заполнена:");
        hashTable.PrintHashTable();
        TextSeparator();
        #endregion

        #region Поиск элемента
        Console.WriteLine($"Поиск элемента: {piano} по ID");
        PointHS<int, MusicalInstrument>? searchedElemnt = hashTable.SearchElemnt(piano.Id.Id);
        if (searchedElemnt == null) Console.WriteLine("Элемент не найден!");
        else Console.WriteLine($"Элемент найден: {searchedElemnt.Value}");
        Console.WriteLine();
        
        ElectricGuitar specialElectricGuitar = new ElectricGuitar("usb", 9, 874);
        Console.WriteLine($"Поиск элемента: {specialElectricGuitar} - по ID");
        searchedElemnt = hashTable.SearchElemnt(specialElectricGuitar.Id.Id);
        if (searchedElemnt == null) Console.WriteLine("Элемент не найден!");
        else Console.WriteLine($"Элемент по ключу {searchedElemnt.Key} найден: {searchedElemnt.Value}");
        TextSeparator();
        #endregion

        #region Удаление элемента
        Console.WriteLine("Хеш-таблица до удаления");
        hashTable.PrintHashTable();
        Console.WriteLine();

        Console.WriteLine($"Удаление элмента: {piano}");
        hashTable.Remove(piano.Id.Id);
        Console.WriteLine();

        Console.WriteLine("Хеш-таблица после удаления");
        hashTable.PrintHashTable();
        TextSeparator();
        #endregion
    }
    
    public static void DemonstrateBinaryTrees()
    {
        #region Инициализация переменных
        BalancedTree<int, MusicalInstrument> balancedTree = new BalancedTree<int, MusicalInstrument>();
        
        Guitar guitar = new Guitar(6, 20);
        ElectricGuitar electricGuitar = new ElectricGuitar("аккумулятор", 9, 17);
        Piano piano = new Piano(88, "шкальная", 35);
        MusicalInstrument musicalInstrument = new MusicalInstrument("Саксофон", 30);
        Piano piano1 = new Piano(97, 82);
        ElectricGuitar electricGuitar1 = new ElectricGuitar("usb", 8, 62);
        #endregion

        #region Добавление элементов в идеально сбалансированное дерево
        balancedTree.Insert(guitar.Id.Id, electricGuitar);
        balancedTree.Insert(electricGuitar.Id.Id, electricGuitar);
        balancedTree.Insert(piano.Id.Id, piano);
        balancedTree.Insert(musicalInstrument.Id.Id, musicalInstrument);
        balancedTree.Insert(piano1.Id.Id, piano1);
        balancedTree.Insert(electricGuitar1.Id.Id, electricGuitar1);

        Console.WriteLine("Элементы добавлены в идеально сбалансированное дерево!");
        Console.WriteLine();
        #endregion

        #region Печать идеально сбалансированного дерева
        Console.WriteLine("Печать идеально сбалансированного дерева");
        balancedTree.PrintByLevel(3, balancedTree.Root);
        TextSeparator();
        #endregion

        #region Поиск минимального ключа в дереве
        Console.WriteLine("Поиск минимального ключа в дереве");
        BalancedTreeNode<int, MusicalInstrument> minNode = balancedTree.GetMinNode();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Ключ: ");
        Console.ResetColor();
        Console.Write($"{minNode.Key.ToString()}; ");
            
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Значение: ");
        Console.ResetColor();
        Console.WriteLine(minNode.Value.ToString());
        TextSeparator();
        Console.WriteLine();
        #endregion

        #region Преобразование сбалансированного дерева в дерево поиска

        Console.WriteLine("Преобразование сбалансированного дерева в дерево поиска");
        SearchTree<int, MusicalInstrument> searchTree = balancedTree.ConvertToSearchTree();
        TextSeparator();

        #endregion

        #region Удаление элемента из дерева поиска
        
        Console.WriteLine("Удаление элемента из дерева поиска");
        Console.WriteLine("Вывод дерева поиска до удаления элемента");
        searchTree.PrintByLevel(3, searchTree.Root);
        Console.WriteLine();

        Console.WriteLine($"Удаление элемента: {piano.ToString()}");
        searchTree.Delete(piano.Id.Id);

        Console.WriteLine("Вывод дерева поиска после удаления элемента");
        searchTree.PrintByLevel(3, searchTree.Root);
        TextSeparator();

        #endregion

        #region Демонстрация выделения отдельной памяти для дерева поиска

        Console.WriteLine("Исходное сбалансированное дерево");
        balancedTree.PrintByLevel(3, balancedTree.Root);
        Console.WriteLine();
        
        Console.WriteLine("Дерево поиска (после удаления элемента)");
        searchTree.PrintByLevel(3, searchTree.Root);
        TextSeparator();

        #endregion

        #region Очистка деревьев из памяти

        Console.WriteLine("Очистка деревьев из памяти");
        Console.WriteLine("Идеально сбалансированное дерево:");
        balancedTree.Clear();
        balancedTree.PrintByLevel(3, balancedTree.Root);

        Console.WriteLine();

        Console.WriteLine("Дерево поиска:");
        searchTree.Clear();
        searchTree.PrintByLevel(3, searchTree.Root);
        Console.WriteLine();

        #endregion
    }
}