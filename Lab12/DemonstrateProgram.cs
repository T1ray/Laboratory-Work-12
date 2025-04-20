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
}