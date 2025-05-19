namespace Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab12;
using Lab10;
using System;
using System.Collections.Generic;
using System.IO;

[TestClass]
public class MyCollectionTests
{
    private MyCollection<int, MusicalInstrument> collection;
    private MusicalInstrument instrument1;
    private Piano instrument2AsPiano;

    [TestInitialize]
    public void Initialize()
    {
        collection = new MyCollection<int, MusicalInstrument>();
        instrument1 = new MusicalInstrument("Guitar", 1);
        instrument2AsPiano = new Piano(88, "октавная", 2);
    }

    [TestMethod]
    public void AddNewItem()
    {
        collection.Add(instrument1.Id.Id, instrument1);
        Assert.AreEqual(1, collection.Count);
        Assert.IsTrue(collection.ContainsKey(instrument1.Id.Id));

        MusicalInstrument retrievedValue;
        bool success = collection.TryGetValue(instrument1.Id.Id, out retrievedValue);
        Assert.IsTrue(success);
        Assert.IsNotNull(retrievedValue);
        Assert.AreNotSame(instrument1, retrievedValue, "Значение из TryGetValue должно быть клонированым");
        Assert.AreEqual(instrument1, retrievedValue);
    }

    [TestMethod]
    public void AddExistingKeyDoesNotAddItem()
    {
        collection.Add(instrument1.Id.Id, instrument1);
        int initialCount = collection.Count;
        MusicalInstrument retrievedBefore;
        collection.TryGetValue(instrument1.Id.Id, out retrievedBefore);

        MusicalInstrument instrumentDuplicate = new MusicalInstrument("Барабаны", instrument1.Id.Id); 
        collection.Add(instrumentDuplicate.Id.Id, instrumentDuplicate);
        
        MusicalInstrument retrievedAfter;
        collection.TryGetValue(instrument1.Id.Id, out retrievedAfter);
        Assert.AreEqual(retrievedBefore, retrievedAfter, "Значение не должно изменяться при том же ключе.");
    }


    [TestMethod]
    public void ClearRemovesAllItems()
    {
        collection.Add(instrument1.Id.Id, instrument1);
        collection.Add(instrument2AsPiano.Id.Id, instrument2AsPiano);

        StringWriter sw = new StringWriter();
        Console.SetOut(sw);
        collection.Clear();
        string expectedOutput = "АВЛ-дерево очищено!\r\n";
        Assert.AreEqual(expectedOutput, sw.ToString());
        Assert.AreEqual(0, collection.Count);
        Assert.IsFalse(collection.ContainsKey(instrument1.Id.Id));
    }

    [TestMethod]
    public void ContainsKeyExistingKeyReturnsTrue()
    {
        collection.Add(instrument1.Id.Id, instrument1);
        Assert.IsTrue(collection.ContainsKey(instrument1.Id.Id));
    }

    [TestMethod]
    public void ContainsKeyNonExistingKeyReturnsFalse()
    {
        Assert.IsFalse(collection.ContainsKey(999));
    }

    [TestMethod]
    public void RemoveExistingKeyRemovesItemReturnsTrue()
    {
        collection.Add(instrument1.Id.Id, instrument1);
        bool removed = collection.Remove(instrument1.Id.Id);
        Assert.IsTrue(removed);
        Assert.AreEqual(0, collection.Count);
        Assert.IsFalse(collection.ContainsKey(instrument1.Id.Id));
    }

    [TestMethod]
    public void RemoveNonExistingKeyReturnsFalse()
    {
        collection.Add(instrument1.Id.Id, instrument1);
        int initialCount = collection.Count;
        bool removed = collection.Remove(999);
        Assert.IsFalse(removed);
        Assert.AreEqual(initialCount, collection.Count);
    }


    [TestMethod]
    public void TryGetValueExistingKeyReturnsTrue()
    {
        collection.Add(instrument2AsPiano.Id.Id, instrument2AsPiano);
        MusicalInstrument valueFromTryGetValue;
        bool retrieved = collection.TryGetValue(instrument2AsPiano.Id.Id, out valueFromTryGetValue);

        Assert.IsTrue(retrieved);
        Assert.IsNotNull(valueFromTryGetValue);
        Assert.AreEqual(instrument2AsPiano, valueFromTryGetValue);
    }

    [TestMethod]
    public void TryGetValueNonExistingKeyReturnsFalse()
    {
        MusicalInstrument value;
        bool retrieved = collection.TryGetValue(999, out value);
        Assert.IsFalse(retrieved);
        Assert.IsNull(value);
    }

    [TestMethod]
    public void IndexerGetExistingKeyReturnsStoredClone()
    {
        collection.Add(instrument2AsPiano.Id.Id, instrument2AsPiano);

        MusicalInstrument valueFromIndexer1 = collection[instrument2AsPiano.Id.Id];
        MusicalInstrument valueFromIndexer2 = collection[instrument2AsPiano.Id.Id];

        Assert.IsNotNull(valueFromIndexer1);
        Assert.AreEqual(instrument2AsPiano, valueFromIndexer1);
    }

    [TestMethod]
    public void IndexerSetNewKeyAddsClonedItem()
    {
        collection[instrument2AsPiano.Id.Id] = instrument2AsPiano;
        Assert.AreEqual(1, collection.Count);

        MusicalInstrument retrievedValue = collection[instrument2AsPiano.Id.Id];
        Assert.AreEqual(instrument2AsPiano, retrievedValue);
    }

    [TestMethod]
    public void IndexerTryGetValueCloning()
    {
        collection.Add(instrument2AsPiano.Id.Id, instrument2AsPiano);

        MusicalInstrument valFromIndexer = collection[instrument2AsPiano.Id.Id];

        MusicalInstrument valFromTryGetValue;
        collection.TryGetValue(instrument2AsPiano.Id.Id, out valFromTryGetValue);
        
        Assert.AreEqual(instrument2AsPiano, valFromIndexer);
        Assert.AreEqual(instrument2AsPiano, valFromTryGetValue);
    }

    [TestMethod]
    public void KeysReturnsClonedKeysFromEnumerator()
    {
        collection.Add(instrument1.Id.Id, instrument1);
        collection.Add(instrument2AsPiano.Id.Id, instrument2AsPiano);
        ICollection<int> keys = collection.Keys;

        Assert.AreEqual(2, keys.Count);
        bool foundKey1 = false;
        bool foundKey2 = false;
        foreach (int k in keys)
        {
            if (k == instrument1.Id.Id) foundKey1 = true;
            else if (k == instrument2AsPiano.Id.Id) foundKey2 = true;
        }
        Assert.IsTrue(foundKey1, $"Ключ {instrument1.Id.Id} не найден в Keys");
        Assert.IsTrue(foundKey2, $"Key {instrument2AsPiano.Id.Id} не найден в Keys");
    }

    [TestMethod]
    public void ValuesReturnsClonedValuesFromEnumerator()
    {
        collection.Add(instrument1.Id.Id, instrument1);
        collection.Add(instrument2AsPiano.Id.Id, instrument2AsPiano);
        ICollection<MusicalInstrument> values = collection.Values;

        Assert.AreEqual(2, values.Count);

        MusicalInstrument valForInst1 = null;
        MusicalInstrument valForInst2 = null;

        foreach (MusicalInstrument v in values)
        {
            if (v.Id.Id == instrument1.Id.Id) valForInst1 = v;
            else if (v.Id.Id == instrument2AsPiano.Id.Id) valForInst2 = v;
        }

        Assert.IsNotNull(valForInst1);
        Assert.AreEqual(instrument1, valForInst1);

        Assert.IsNotNull(valForInst2);
        Assert.AreEqual(instrument2AsPiano, valForInst2);
    }

    [TestMethod]
    public void GetEnumeratorIteratesAndYieldsClonedPairs()
    {
        collection.Add(instrument1.Id.Id, instrument1);
        collection.Add(instrument2AsPiano.Id.Id, instrument2AsPiano);

        int iteratedCount = 0;
        foreach (KeyValuePair<int, MusicalInstrument> pair in collection)
        {
            iteratedCount++;
            if (pair.Key == instrument1.Id.Id)
            {
                Assert.AreEqual(instrument1, pair.Value);
            }
            else if (pair.Key == instrument2AsPiano.Id.Id)
            {
                Assert.AreEqual(instrument2AsPiano, pair.Value);
            }
        }
    }

    [TestMethod]
    public void CopyToCopiesClonedPairsToArray()
    {
        collection.Add(instrument1.Id.Id, instrument1);
        collection.Add(instrument2AsPiano.Id.Id, instrument2AsPiano);
        KeyValuePair<int, MusicalInstrument>[] array = new KeyValuePair<int, MusicalInstrument>[2];
        collection.CopyTo(array, 0);

        Assert.AreEqual(2, array.Length);
        KeyValuePair<int, MusicalInstrument> pairForInst1 = default;
        KeyValuePair<int, MusicalInstrument> pairForInst2 = default;

        foreach(var p in array)
        {
            if (p.Key == instrument1.Id.Id) pairForInst1 = p;
            else if (p.Key == instrument2AsPiano.Id.Id) pairForInst2 = p;
        }

        Assert.AreNotEqual(default(KeyValuePair<int, MusicalInstrument>), pairForInst1, "Пары для instrument1 не найдено в коллекции");
        Assert.AreEqual(instrument1, pairForInst1.Value);

        Assert.AreNotEqual(default(KeyValuePair<int, MusicalInstrument>), pairForInst2, "Пары для instrument2AsPiano не найдено в коллекции");
        Assert.AreEqual(instrument2AsPiano, pairForInst2.Value);
    }

    [TestMethod]
    public void ContainsKeyValuePairExistingItemReturnsTrue()
    {
        collection.Add(instrument1.Id.Id, instrument1);
        KeyValuePair<int, MusicalInstrument> pairToFind =
            new KeyValuePair<int, MusicalInstrument>(instrument1.Id.Id, instrument1);

        Assert.IsTrue(collection.Contains(pairToFind));
    }

    [TestMethod]
    public void RemoveKeyValuePairExistingItemRemovesItem()
    {
        collection.Add(instrument1.Id.Id, instrument1);
        KeyValuePair<int, MusicalInstrument> pairToRemove =
            new KeyValuePair<int, MusicalInstrument>(instrument1.Id.Id, instrument1);

        bool removed = collection.Remove(pairToRemove);
        Assert.IsTrue(removed);
        Assert.AreEqual(0, collection.Count);
    }

    [TestMethod]
    public void AddKeyValuePairExistingItem()
    {
        collection.Add(new KeyValuePair<int, MusicalInstrument>(instrument1.Id.Id, instrument1));
        Assert.AreEqual(instrument1, collection[instrument1.Id.Id]);
    }

    [TestMethod]
    public void InitializeNewCollectionWithLengthParametr()
    {
        MyCollection<int, MusicalInstrument> collection = new MyCollection<int, MusicalInstrument>(10);
        Assert.AreEqual(10, collection.Count);
    }
}

