using Lab10;
using Lab12;

namespace Tests;
[TestClass]
public class DoubleLinkedListTests
{
    private DoubleLinkedList<MusicalInstrument>? baseList;
    private DoubleLinkedList<MusicalInstrument>? completedList;
    
    [TestInitialize]
    public void Initialize()
    {
        baseList = new DoubleLinkedList<MusicalInstrument>();
        completedList = new DoubleLinkedList<MusicalInstrument>();
        
        MusicalInstrument musicalInstrument = new MusicalInstrument("Барабан", 17);
        Guitar guitar = new Guitar(6, 24);
        Piano piano = new Piano(88, 115);
        completedList.AddLast(musicalInstrument);
        completedList.AddLast(guitar);
        completedList.AddLast(piano);
    }
    
    [TestMethod]
    public void TestConstructor()
    {
        Assert.AreEqual(baseList.head, null);
        Assert.AreEqual(baseList.tail, null);
    }

    [TestMethod]
    public void TestAdd()
    {
        MusicalInstrument instrument = new MusicalInstrument("Саксофон",1);
        baseList.Add(instrument);
        Assert.AreEqual(baseList.head.Data, (MusicalInstrument)instrument.Clone());
        Assert.AreEqual(baseList.tail.Data, (MusicalInstrument)instrument.Clone());

        instrument.Name = "Контробас";
        instrument.Id.Id = 2;
        baseList.Add(instrument);
        Assert.AreEqual(baseList.tail.Data, (MusicalInstrument)instrument.Clone());
    }

    [TestMethod]
    public void TestCount()
    {
        Assert.AreEqual(baseList.Count, 0);
        
        MusicalInstrument instrument = new MusicalInstrument("Саксофон",1);
        baseList.Add(instrument);
        Assert.AreEqual(baseList.Count, 1);
    }

    [TestMethod]
    public void TestClear()
    {
        completedList.Clear();
        Assert.AreEqual(completedList.head, null);
        Assert.AreEqual(completedList.tail, null);
    }

    [TestMethod]
    public void TestRemoveLast()
    {
        Guitar guitar = new Guitar(6, 24);
        Piano piano = new Piano(88, 115);
        completedList.RemoveLast(piano);
        Assert.AreEqual(completedList.tail.Data, guitar);
        Assert.AreEqual(completedList.Count, 2);
    }
    
    [TestMethod]
    public void TestRemoveMiddle()
    {
        MusicalInstrument musicalInstrument = new MusicalInstrument("Барабан", 17);
        Guitar guitar = new Guitar(6, 24);
        Piano piano = new Piano(88, 115);
        completedList.RemoveLast(guitar);
        Assert.AreEqual(completedList.tail.Prev.Data, musicalInstrument);
        Assert.AreEqual(completedList.head.Next.Data, piano);
        Assert.AreEqual(completedList.Count, 2);
    }
    
    [TestMethod]
    public void TestRemoveFirst()
    {
        MusicalInstrument musicalInstrument = new MusicalInstrument("Барабан", 17);
        Guitar guitar = new Guitar(6, 24);
        completedList.RemoveLast(musicalInstrument);
        Assert.AreEqual(completedList.head.Data, guitar);
        Assert.AreEqual(completedList.Count, 2);
    }

    [TestMethod]
    public void TestInsertNull()
    {
        try
        {
            MusicalInstrument musicalInstrument = new MusicalInstrument("Барабан", 17);
            baseList.Insert(musicalInstrument, musicalInstrument);
        }
        catch (Exception e)
        {
            Assert.AreEqual("Элемент не может быть null", e.Message);
        }
    }

    [TestMethod]
    public void TestInsertMiddle()
    {
        Guitar guitar = new Guitar(6, 24);
        ElectricGuitar eGuitar = new ElectricGuitar("usb", 8, 77);
        completedList.Insert(guitar, eGuitar);
        Assert.AreEqual(completedList.tail.Prev.Data, (ElectricGuitar)eGuitar.Clone());
    }
    
    [TestMethod]
    public void TestInsertLast()
    {
        Piano piano = new Piano(88, 115);
        ElectricGuitar eGuitar = new ElectricGuitar("usb", 8, 77);
        completedList.Insert(piano, eGuitar);
        Assert.AreEqual(completedList.tail.Data, (ElectricGuitar)eGuitar.Clone());
    }

    [TestMethod]
    public void TestClone()
    {
        DoubleLinkedList<MusicalInstrument> newList = (DoubleLinkedList<MusicalInstrument>)completedList.Clone();
        Assert.AreEqual(completedList.head.Data, newList.head.Data);
        Assert.AreEqual(completedList.tail.Data, newList.tail.Data);
    }

    [TestMethod]
    public void TestContains()
    {
        Guitar guitar = new Guitar(6, 24);
        Assert.IsTrue(completedList.Contains(guitar));
    }
}