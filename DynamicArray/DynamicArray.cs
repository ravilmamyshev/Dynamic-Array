using System.Collections;

namespace DynamicArray
{
    public class DynamicArray<T> : IEnumerable<T>
    {
        private T[] _array;
        private const int ArrayLength = 8;
        /// <summary>
        /// получение и установка длины массива
        /// </summary>
        public int Length { get; private set; }
        /// <summary>
        /// Получает емкость внутреннего массива
        /// </summary>
        public int Capacity
        {
            get
            {
                return _array.Length;
            }
        }
        /// <summary>
        /// создает массив указанного рамера
        /// </summary>
        /// <param name="capacity"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public DynamicArray(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), "Длина массива не может быть отрицательным");
            }
            else
            {
                _array = new T[capacity];
                Length = 0;
            }
        }
        /// <summary>
        /// создает массив ёмкостью равной 8 (по умолчанию)
        /// </summary>
        public DynamicArray()
        {
            _array = new T[ArrayLength];
            Length = 0;
        }
        /// <summary>
        /// создает массив по переданной коллекции
        /// </summary>
        /// <param name="collection"></param>
        /// <exception cref="ArgumentException"></exception>
        public DynamicArray(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentException(nameof(collection), "Задана пустая коллекция");
            }
            else
            {
                _array = collection.ToArray();
                Length = collection.Count();
            }
        }
        /// <summary>
        /// создает индексатор для доступа к элементам массива
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public T this[int index]
        {
            get
            {
                if (index < 0 && index >= Capacity)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), $"Индех {index} меньше нуля или вышел за границы массива");
                }
                else
                {
                    return _array[index];
                }
            }
            set
            {
                if (index < 0 && index >= Capacity)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), $"Индех {index} меньше нуля или вышел за границы массива");
                }
                else
                {
                    _array[index] = value;
                }
            }
        }
        /// <summary>
        /// добавляет в конец массива один элемент
        /// </summary>
        /// <param name="argument"></param>
        public void Add(T argument)
        {
            if (Capacity > Length)
            {
                _array[Length] = argument;
                Length++;
            }

            else
            {
                if (Capacity == 0)
                {
                    Array.Resize(ref _array, 1);
                }

                else
                {
                    Array.Resize(ref _array, Capacity * 2);
                }
                _array[Length] = argument;
                Length++;
            }
        }
        /// <summary>
        /// добавляет коллекцию в конец массива
        /// </summary>
        /// <param name="collection"></param>
        /// <exception cref="ArgumentException"></exception>
        public void AddRange(IEnumerable<T> collection)
        {
            if (collection.Count() == 0 || collection == null)
            {
                throw new ArgumentException(nameof(collection), $"Задана пустая коллекция {collection}");
            }

            int colCount = collection.Count();
            int arrCapacity = Capacity - Length;

            if (arrCapacity < colCount)
            {
                Array.Resize(ref _array, Length + colCount);
            }

            foreach (var item in collection)
            {
                _array[Length] = item;
                Length++;
            }
        }
        /// <summary>
        /// удаляет элемент по указанному индексу
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool Remove(int index)
        {
            if (index >= 0 && index <= Length)
            {
                for (var i = index; i < Length - 1; i++)
                {
                    _array[i] = _array[i + 1];
                }
                Length--;
                _array[Length] = default(T);
                return true;
            }
            return false;
        }
        /// <summary>
        /// добавляет значение по указанному индексу массива
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public bool Insert(int index, T item)
        {
            if (index >= 0 && index <= Length)
            {
                if (Length == Capacity)
                {
                    Array.Resize(ref _array, Length + 1);
                }
                Length++;
                T number = _array[index];
                _array[index] = item;
                T curNumber;

                for (var i = index + 1; i < Length; i++)
                {
                    curNumber = _array[i];
                    _array[i] = number;
                    number = curNumber;
                }
                return true;
            }

            else
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"Индекс {index} вышел за границу массива");
            }
        }

        /// <summary>
        /// добавление итератора для перебора набора значений
        /// </summary>
        /// <returns></returns>

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < _array.Length; i++)
            {
                yield return _array[i];
            }
        }

        /// <summary>
        /// возвращает результат сравнения объектов
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            else if (object.ReferenceEquals(this, obj))
            {
                return true;
            }
            return _array == obj;

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            return _array.GetHashCode();
        }
    }
}