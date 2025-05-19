using Lab12;
using Lab10;

namespace Tests;

[TestClass]
public class AVLTreeCollectionTests
{
    private MusicalInstrument instrument10, instrument5, instrument15, instrument3, instrument7, instrument12;
    private AVLTree<int, MusicalInstrument> tree;
    
    [TestInitialize]
    public void Initialize()
    {
        tree = new AVLTree<int, MusicalInstrument>();
        
        instrument10 = new MusicalInstrument("Маракасы", 10);
        instrument5 = new Piano(88, "октавная", 5);
        instrument15 = new Guitar(6, 15);
        instrument3 = new MusicalInstrument("Барабаны", 3);
        instrument7 = new ElectricGuitar("usb", 7, 7);
        instrument12 = new ElectricGuitar("батарейки", 8, 12);
    }

    [TestMethod]
    public void AVLBalancedTreeAddingOneElementToRoot()
    {
        tree.Insert(instrument10.Id.Id, instrument10);
        Assert.IsNotNull(tree.Root);
        Assert.AreEqual(instrument10.Id.Id, tree.Root.Key);
        Assert.AreEqual(instrument10, tree.Root.Value);
        Assert.AreEqual(1, tree.GetHeight(tree.Root));
    }

    [TestMethod]
    public void AVLBalancedTreeCheckBalance()
    {
        tree.Insert(instrument10.Id.Id, instrument10);
        tree.Insert(instrument5.Id.Id, instrument5);
        tree.Insert(instrument7.Id.Id, instrument7);
        
        Assert.AreEqual(instrument7.Id.Id, tree.Root.Key);
        Assert.AreEqual(instrument7, tree.Root.Value);
    }

    [TestMethod]
    public void AVLBalancedTreeCheckBalanceRLandLR()
    {
        tree.Insert(instrument10.Id.Id, instrument10);
        tree.Insert(instrument5.Id.Id, instrument5);
        tree.Insert(instrument7.Id.Id, instrument7);
        tree.Insert(instrument3.Id.Id, instrument3);
        tree.Insert(instrument15.Id.Id, instrument15);
        tree.Insert(instrument12.Id.Id, instrument12);
        
        Assert.AreEqual(instrument7.Id.Id, tree.Root.Key);
    }

    [TestMethod]
    public void AVLBalancedTreeTestAddDuplicate()
    {
        tree.Insert(instrument10.Id.Id, instrument10);
        tree.Insert(instrument5.Id.Id, instrument5);
        tree.Insert(instrument7.Id.Id, instrument7);
        
        StringWriter writer = new StringWriter();
        Console.SetOut(writer);
        Piano duplicatePiano = new Piano(88, 10);
        tree.Insert(duplicatePiano.Id.Id, duplicatePiano);
        writer.Close();
        Assert.AreEqual("Уже есть значение в дереве с данным ключом!\r\n", writer.ToString());
        
    }

    [TestMethod]
    public void AVLBalancedTreeMinNode()
    {
        tree.Insert(instrument10.Id.Id, instrument10);
        tree.Insert(instrument5.Id.Id, instrument5);
        tree.Insert(instrument3.Id.Id, instrument3);
        
        Assert.AreEqual(instrument3.Id.Id, tree.GetMinNode().Key);
        Assert.AreEqual(instrument3, tree.GetMinNode().Value);
    }

    [TestMethod]
    public void AVLBalancedTreeClear()
    {
        tree.Insert(instrument10.Id.Id, instrument10);
        tree.Insert(instrument5.Id.Id, instrument5);
        tree.Insert(instrument3.Id.Id, instrument3);
        tree.Clear();
        
        Assert.IsNull(tree.Root);
    }
}