using System.Diagnostics;
using Lab10;

namespace Lab12;
using static AdditionalFunctions.AdditionalFunctions;

public class DoubleLinkedListLaboratory
{
    private static void MenuDoubleLinkedList()
    {
        Console.WriteLine("1. Вывод в консоль элементов коллекции");
        Console.WriteLine("2. Добавление элемента в конец списка");
        Console.WriteLine("3. Вставка элемента после указанного элемента");
        Console.WriteLine("4. Удаление элемента");
        Console.WriteLine("5. Выход");
    }

    private static MusicalInstrument ElementGenerator()
    {
        Console.WriteLine("Введите, какой тип элемента вы хотите создать:");
        Console.WriteLine("1. Музыкальный инструмент");
        Console.WriteLine("2. Гитара");
        Console.WriteLine("3. Электрическая гитара");
        Console.WriteLine("4. Фортепиано");
        
        MusicalInstrument instrument = new MusicalInstrument();
        Console.Write("> ");
        string input = Console.ReadLine();
        switch (input)
        {
            case "1":
                instrument = new MusicalInstrument();
                Console.WriteLine("Введите 1, если хотите рандомно сгенерировать элемент");
                Console.WriteLine("Введите 2, если хотите задать элемент вручную");
                Console.Write("> ");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        instrument.RandomInit();
                        break;
                    case "2":
                        instrument.Init();
                        break;
                    default:
                        Console.WriteLine("Нет такой команды!");
                        break;
                }
                break;
            
            case "2":
                instrument = new Guitar();
                Console.WriteLine("Введите 1, если хотите рандомно сгенерировать элемент");
                Console.WriteLine("Введите 2, если хотите задать элемент вручную");
                Console.Write("> ");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        instrument.RandomInit();
                        break;
                    case "2":
                        instrument.Init();
                        break;
                    default:
                        Console.WriteLine("Нет такой команды!");
                        break;
                }
                break;
            
            case "3":
                instrument = new ElectricGuitar();
                Console.WriteLine("Введите 1, если хотите рандомно сгенерировать элемент");
                Console.WriteLine("Введите 2, если хотите задать элемент вручную");
                Console.Write("> ");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        instrument.RandomInit();
                        break;
                    case "2":
                        instrument.Init();
                        break;
                    default:
                        Console.WriteLine("Нет такой команды!");
                        break;
                }
                break;
            
            case "4":
                instrument = new Piano();
                Console.WriteLine("Введите 1, если хотите рандомно сгенерировать элемент");
                Console.WriteLine("Введите 2, если хотите задать элемент вручную");
                Console.Write("> ");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        instrument.RandomInit();
                        break;
                    case "2":
                        instrument.Init();
                        break;
                    default:
                        Console.WriteLine("Нет такой команды!");
                        break;
                }
                break;
            
            default:
                Console.WriteLine("Нет такой опции!");
                break;
        }
        return instrument;
    }

    public static void Experiments()
    {
        Console.WriteLine("Экспериментальная зона");
        Console.WriteLine("Можно поэкспериментировать с двухсвязным списком");
        TextSeparator();
        
        DoubleLinkedList<MusicalInstrument> instrumentsList = new DoubleLinkedList<MusicalInstrument>();
        MusicalInstrument instrument;
        bool isNeedExit = false;
        while (!isNeedExit)
        {
            MenuDoubleLinkedList();
            Console.Write("Введите команду: ");
            string input = Console.ReadLine();
            TextSeparator();
            switch (input)
            {
                case "1":
                    if (instrumentsList.Count == 0)
                    {
                        Console.WriteLine("Список пуст!");
                        break;
                    }
                    Console.WriteLine("Список:");
                    instrumentsList.PrintList();
                    break;
                case "2":
                    Console.WriteLine("Добавление элемента в список");
                    instrument = ElementGenerator();
                    if (!instrument.Equals(new MusicalInstrument())) instrumentsList.Add(instrument);
                    break;
                case "3":
                    Console.WriteLine("Вставка элемента после указанного элемента");
                    if (instrumentsList.Count == 0)
                    {
                        Console.WriteLine("Список пуст!");
                        break;
                    }
                    Console.WriteLine("Элемент после которого будет вставляться элемент");
                    MusicalInstrument insertedInstrument = ElementGenerator();
                    if (!instrumentsList.Contains(insertedInstrument))
                    {
                        Console.WriteLine("В списке нет элемента, после которого необходимо вставлять элемент!");
                        break;
                    }
                    Console.WriteLine("Элемент, который будем вставлять");
                    instrument = ElementGenerator();
                    instrumentsList.Insert(insertedInstrument, instrument);
                    Console.WriteLine("Элемент добавлен в список!");
                    break;
                case "4":
                    Console.WriteLine("Удаление элемента");
                    if (instrumentsList.Count == 0)
                    {
                        Console.WriteLine("Список пуст!");
                        break;
                    }
                    instrument = ElementGenerator();
                    bool isInstrumentDeleted = instrumentsList.RemoveLast(instrument);
                    if (!isInstrumentDeleted) Console.WriteLine("Элемент не найден в списке");
                    else Console.WriteLine("Элемент удален из списка");
                    break;
                case "5":
                    isNeedExit = true;
                    break;
                default:
                    Console.WriteLine("Нет такой команды!");
                    break;
            }
            TextSeparator();
        }
    }
}