using Microsoft.VisualStudio.TestTools.UnitTesting;
using DynamicArray;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicArray.Tests
{
    [TestClass()]
    public class DynamicArrayTests
    {
        [TestMethod()]
        [DataRow(0, 0)]
        [DataRow(2, 2)]
        public void DynamicArrayWithCapacity(int capacity, int expectedResult)
        {
            DynamicArray<int> array = new DynamicArray<int>(capacity);
            var result = array.Capacity;
            Assert.AreEqual(expectedResult, result, "Ёмкость массива неверная");
        }

        [TestMethod()]
        [DataRow(8)]
        public void DynamicArrayWithDefaultCapacity(int capacity)
        {
            DynamicArray<int> array = new DynamicArray<int>();
            var result = array.Capacity == capacity;
            Assert.IsTrue(result, "Ёмкость массива неверная");
        }

        [TestMethod()]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        [DataRow(11)]
        [DataRow(9)]
        public void DynamicArrayWithDefaultExeptionCapacity(int capacity)
        {
            DynamicArray<int> array = new DynamicArray<int>();
            for (int i = 0; i <= capacity - 1; i++)
            {
                array[i] = i;
            }
        }

        [TestMethod()]
        [DataRow(new int[] { 1, 2, 3 })]
        public void DynamicArrayWithCollection(int[] collection)
        {
            DynamicArray<int> array = new DynamicArray<int>(collection);
            var result = true;

            for (var i = 0; i < collection.Length; i++)
            {
                if (collection[i] != array[i])
                {
                    result = false;
                    break;
                }
            }

            Assert.IsTrue(result, "Динамический массив не равен коллекции");
        }

        [TestMethod()]
        [DataRow(23.1)]
        [DataRow(13.6)]
        [DataRow(54634.7)]
        public void AddTest(double number)
        {
            DynamicArray<double> array = new DynamicArray<double>();
            array.Add(number);
            Assert.AreEqual(array[array.Length - 1], number, "Проверка метода Add не прошла успешно");
        }

        [TestMethod()]
        [DataRow(new int[] { 1, 2, 3 }, 1)]
        public void RemoveTest(int[] collection, int index)
        {
            DynamicArray<int> array = new DynamicArray<int>(collection);
            var arrayLength = array.Length;
            array.Remove(index);
            var result = true;

            if (arrayLength == array.Length)
            {
                result = false;
            }

            Assert.IsTrue(result, "Проверка метода Remove не прошла успешно");

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [DataRow(11, 2)]
        [DataRow(2, 5)]
        public void InsertWithExeptionTest(int index, int item)
        {
            DynamicArray<int> array = new DynamicArray<int>();
            array.Insert(index, item);
        }

        [TestMethod()]
        [DataRow(3, 2, new int[] { 1, 2, 3, 4, 5 })]
        [DataRow(5, 1, new int[] { 2, 5, 2, 7, 3, 9 })]
        public void InsertTest(int index, int item, int[] collection)
        {
            DynamicArray<int> array = new DynamicArray<int>(collection);
            array.Insert(index, item);
            Assert.AreEqual(item, array[index], "Проверка метода Insert не прошла успешно");
        }
    }
}