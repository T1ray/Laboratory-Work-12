namespace Tests;

using Lab10;
using Lab12;

[TestClass]
public class SearchTreeTests
{
    private SearchTree<int, MusicalInstrument> tree;
    private MusicalInstrument instrument10, instrument5, instrument15, instrument3, instrument7, instrument12;

    [TestInitialize]
    public void Initialize()
    {
        BalancedTree<int, MusicalInstrument> balancedTree = new BalancedTree<int, MusicalInstrument>();
        
        instrument10 = new MusicalInstrument("Маракасы", 10);
        instrument5 = new Piano(88, "октавная", 5);
        instrument15 = new Guitar(6, 15);
        instrument3 = new MusicalInstrument("Барабаны", 3);
        instrument7 = new ElectricGuitar("usb", 7, 7);
        instrument12 = new ElectricGuitar("батарейки", 8, 12);
        
        balancedTree.Insert(instrument10.Id.Id, instrument10);
        balancedTree.Insert(instrument5.Id.Id, instrument5);
        balancedTree.Insert(instrument7.Id.Id, instrument7);
        balancedTree.Insert(instrument3.Id.Id, instrument3);
        balancedTree.Insert(instrument15.Id.Id, instrument15);
        balancedTree.Insert(instrument12.Id.Id, instrument12);
        
        tree = balancedTree.ConvertToSearchTree();
    }

    [TestMethod]
    public void TestSearchTreeFind()
    {
        SearchTreeNode<int, MusicalInstrument> findedNode = tree.Find(instrument10.Id.Id);
        Assert.IsTrue(findedNode.Value.Equals(instrument10));
    }

    [TestMethod]
    public void TestSearchTreeDeleteWithOneEmptyOffspring()
    {
        tree.Delete(instrument5.Id.Id);
        tree.Delete(instrument10.Id.Id);
        
        Assert.IsNull(tree.Find(instrument10.Id.Id));
        Assert.IsNull(tree.Find(instrument5.Id.Id));
    }
    
    [TestMethod]
    public void TestSearchTreeDeleteWithTwoOffspring()
    {
        tree.Delete(instrument7.Id.Id); // это root
        
        Assert.IsNull(tree.Find(instrument7.Id.Id));
    }

    [TestMethod]
    public void TestSearchTreeDeleteNonExistent()
    {
        MusicalInstrument newInstrumnet = new Guitar(8, 22);
        
        StringWriter writer = new StringWriter();
        Console.SetOut(writer);
        
        tree.Delete(newInstrumnet.Id.Id);
        writer.Close();
        Assert.AreEqual("Элемент с данным ключом не найден в дереве!\r\n", writer.ToString());
    }

    [TestMethod]
    public void TestSearchTreeClear()
    {
        tree.Clear();
        Assert.IsNull(tree.Root);
    }
}