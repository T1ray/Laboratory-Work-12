namespace Tests;

using Lab12;
using Lab10;

[TestClass]
public class MyHashTableTests
    {
        private MyHashTable<int, MusicalInstrument>? baseTable;
        private MyHashTable<int, MusicalInstrument>? completedTable;
        private MusicalInstrument? instrument1;
        private MusicalInstrument? instrument2;
        private MusicalInstrument? instrument3;
        private MusicalInstrument? instrument4;

        [TestInitialize]
        public void Initialize()
        {
            int capacity = 5;
            baseTable = new MyHashTable<int, MusicalInstrument>(capacity);
            completedTable = new MyHashTable<int, MusicalInstrument>(capacity);

            instrument1 = new MusicalInstrument("Барабан", 17);
            instrument2 = new Guitar(6, 24);
            instrument3 = new Piano(88, 115);
            instrument4 = new ElectricGuitar("usb", 8, 7);

            completedTable.Add(instrument1.Id.Id, instrument1);
            completedTable.Add(instrument2.Id.Id, instrument2);
            completedTable.Add(instrument3.Id.Id, instrument3);
            completedTable.Add(instrument4.Id.Id, instrument4);
        }
        
        [TestMethod]
        public void TestConstructor()
        {
            Assert.IsTrue(baseTable != null);
            Assert.AreEqual(0, baseTable.Count);
        }

        [TestMethod]
        public void TestAdd()
        {
            MusicalInstrument instrument = new MusicalInstrument("Саксофон", 1);
            baseTable.Add(instrument.Id.Id, instrument);

            Assert.AreEqual(1, baseTable.Count);
            Assert.IsTrue(baseTable.ContainsKey(instrument.Id.Id));

            Guitar instrumentNext = new Guitar(7, 9);
            baseTable.Add(instrumentNext.Id.Id, instrumentNext);

            Assert.AreEqual(2, baseTable.Count);
            Assert.IsTrue(baseTable.ContainsKey(instrumentNext.Id.Id));
            Assert.IsTrue(baseTable.ContainsKey(instrument.Id.Id));
        }

        [TestMethod]
        public void TestAddChain()
        {
            int key1 = instrument1.Id.Id;
            int key2 = instrument4.Id.Id;
            
            Assert.IsTrue(completedTable.ContainsKey(key1));
            Assert.IsTrue(completedTable.ContainsKey(key2));
        }
        

        [TestMethod]
        public void TestAddNull()
        {
            try
            {
                 baseTable.Add(1, null);
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void TestCount()
        {
            Assert.AreEqual(0, baseTable.Count);
            Assert.AreEqual(4, completedTable.Count);
        }

        [TestMethod]
        public void TestClear()
        {
            completedTable.Clear();
            Assert.AreEqual(0, completedTable.Count);
        }

        [TestMethod]
        public void TestRemoveNonExisting()
        {
            int nonExistingKey = 999;
            bool result = completedTable.Remove(nonExistingKey);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestRemoveFirstInChain()
        {
            int toRemove = instrument1.Id.Id;
            bool result = completedTable.Remove(toRemove);

            Assert.IsTrue(result);
            Assert.IsFalse(completedTable.ContainsKey(toRemove));
        }

        [TestMethod]
        public void TestRemoveLastInChain()
        {
            int toRemove = instrument4.Id.Id;
            bool result = completedTable.Remove(toRemove);

            Assert.IsTrue(result);
            Assert.IsFalse(completedTable.ContainsKey(toRemove));
        }

        [TestMethod]
        public void TestRemoveOnlyInChain()
        {
            int keyToRemove = instrument2.Id.Id;
            int initialCount = completedTable.Count;

            bool result = completedTable.Remove(keyToRemove);

            Assert.IsTrue(result);
            Assert.AreEqual(initialCount, completedTable.Count);
            Assert.IsFalse(completedTable.ContainsKey(keyToRemove));
        }

        [TestMethod]
        public void TestContains()
        {
            int key1 = instrument1.Id.Id;
            int key2 = instrument4.Id.Id;
            int key3 = instrument2.Id.Id;
            int nonExistingKey = 999;

            Assert.IsTrue(completedTable.ContainsKey(key1));
            Assert.IsTrue(completedTable.ContainsKey(key2));
            Assert.IsTrue(completedTable.ContainsKey(key3));
            Assert.IsFalse(completedTable.ContainsKey(nonExistingKey));
        }

        [TestMethod]
        public void TestSearchElement()
        {
            int key1 = instrument1.Id.Id;
            int key2 = instrument4.Id.Id;
            int nonExistingKey = 999;

            PointHS<int, MusicalInstrument>? searchedElement1 = completedTable.SearchElemnt(key1);
            PointHS<int, MusicalInstrument>? searchedElement2 = completedTable.SearchElemnt(key2);
            PointHS<int, MusicalInstrument>? nonExistingElement = completedTable.SearchElemnt(nonExistingKey);
            
            Assert.IsTrue(searchedElement1.Value.Equals(instrument1));
            Assert.IsTrue(searchedElement2.Value.Equals(instrument4));
            Assert.AreEqual(null, nonExistingElement);
        }
    }