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
}