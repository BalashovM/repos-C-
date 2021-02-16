using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lesson_08_BucketSort_and_ExternalSort
{
    class Program
    {
        static void Main(string[] args)
        {
            const int elementQty = 500;
            int maxValue = (int)Math.Pow(10, (int)Math.Log10(elementQty) + 1) - 1;

            int[] array = new int[elementQty];
            StringBuilder arrayStr = new StringBuilder();

            Random rand = new Random();

            //Заполняем массив случайными не повторяющимися значениями 
            HashSet<int> values = new HashSet<int>();
            while (values.Count != elementQty)
            {
                values.Add(rand.Next(1, maxValue));
            }
            //Переносим уникальные значения в массив
            int el = 0;
            foreach (var x in values)
            {
                array[el] = x;
                el = el + 1;
            }
            Console.WriteLine("Неотсортированный массив:");
            //Формируем строку значений для вывода
            foreach (int x in array)
            {
                arrayStr.Append($"{x.ToString().PadLeft(2, ' ')} ");
            }
            Console.WriteLine(arrayStr.ToString());

            //Создаём копию массива
            int[] arraySort = new int[array.Length]; 
            array.CopyTo(arraySort,0);

            Console.WriteLine("Применяем блочную сортировку:");
            //BucketSort_V1(ref array);
            BucketSort_V2(arraySort, arraySort.Length);
            arrayStr.Clear();
            foreach (int x in arraySort)
            {
                arrayStr.Append($"{x.ToString().PadLeft(2, ' ')} ");
            }
            Console.WriteLine(arrayStr.ToString());
            Console.WriteLine();
            Console.WriteLine("Выполним внешнюю сортировку массива. Для этого сохраним массив неотсортированных значений в бинарный файл");
            Console.WriteLine("Для сортировки будем использовать два вспомогательных файла");
            Console.WriteLine("Разделяем данные из исходного файла на два вспомогательных, и постепенно перемещаем данные из одного массива в другой");
            Console.WriteLine("Премещаяем по возрастанию, до тех пор пока один из файлов не станет пустым, значит второй файл полностью отсортирован.");
            string fileName = "array.bin";
            ArrayToFile(array, fileName);
            ExternalSort extSort = new ExternalSort(fileName);
            //PrintFileArray(array.Length, fileName);
            extSort.Run();
            Console.WriteLine();
            PrintFileArray(array.Length, fileName);

            Console.ReadKey();
        }

        static void ArrayToFile(int[] array, string file)
        {
            using (BinaryWriter bw = new BinaryWriter(File.Create(file, 65536)))
            {
                for (int i = 0; i < array.Length; i++)
                {
                    bw.Write(array[i]);
                }
            }
        }
        public static void PrintFileArray(int arrayLength, string file)
        {
            using (BinaryReader br = new BinaryReader(File.OpenRead(file)))
            {
                long length = br.BaseStream.Length;
                long position = 0;
                for (int i = 0; i < arrayLength; i++)
                {
                    if (position == length)
                    {
                        break;
                    }
                    else
                    {
                        Console.Write($"{br.ReadInt32()} ");
                        position += 4;
                    }
                }
                Console.WriteLine();
            }
        }

        static void BucketSort_V1(ref int[] items)
        {
            // Предварительная проверка элементов исходного массива
            if (items == null || items.Length < 2)
                return;

            // Поиск элементов с максимальным и минимальным значениями
            int maxValue = items[0];
            int minValue = items[0];

            for (int i = 1; i < items.Length; i++)
            {
                if (items[i] > maxValue)
                    maxValue = items[i];

                if (items[i] < minValue)
                    minValue = items[i];
            }

            // Создание временного массива "карманов" в количестве,
            // достаточном для хранения всех возможных элементов,
            // чьи значения лежат в диапазоне между minValue и maxValue.
            // Т.е. для каждого элемента массива выделяется "карман" List<int>.
            // При заполнении данных "карманов" элементы исходного не отсортированного массива
            // будут размещаться в порядке возрастания собственных значений "слева направо".

            List<int>[] bucket = new List<int>[maxValue - minValue + 1];

            for (int i = 0; i < bucket.Length; i++)
            {
                bucket[i] = new List<int>();
            }

            // Занесение значений в пакеты
            for (int i = 0; i < items.Length; i++)
            {
                bucket[items[i] - minValue].Add(items[i]);
            }

            // Восстановление элементов в исходный массив
            // из карманов в порядке возрастания значений
            int position = 0;
            for (int i = 0; i < bucket.Length; i++)
            {
                if (bucket[i].Count > 0)
                {
                    for (int j = 0; j < bucket[i].Count; j++)
                    {
                        items[position] = bucket[i][j];
                        position++;
                    }
                }
            }
        }
        static void BucketSort_V2(int[] sarray, int array_size)
        {
            // Используем дополнительный эелемнт массиваbucket[i,array_size] для хранения количества заполненных элементов
            int[,] bucket = new int[10, array_size + 1];
            // Заполняем массив
            for (var i = 0; i < 10; i++)
                bucket[i, array_size] = 0;

            // Поиск элементов с максимальным значением
            int maxValue = sarray[0];

            for (int i = 1; i < sarray.Length; i++)
            {
                if (sarray[i] > maxValue)
                    maxValue = sarray[i];
            }
            //Определяем максимум для цикла по разрядам
            maxValue = (int)Math.Pow(10, (int)Math.Log10(maxValue)+1);

            // Проходимся по всем элементам по разрядам
            for (int digit = 1; digit <= maxValue; digit *= 10)
            {
                // Массив по блокам
                for (int i = 0; i < array_size; i++)
                {
                    //Получаем цифру 0-9
                    int dig = (sarray[i] / digit) % 10;
                    //Добавляем в блоки
                    bucket[dig, bucket[dig, array_size]++] = sarray[i];
                }
                // Блоки обратно в массив
                int idx = 0;
                for (var i = 0; i < 10; i++)
                {
                    for (var j = 0; j < bucket[i, array_size]; j++)
                    {
                        sarray[idx++] = bucket[i, j];
                    }
                    //Скидываем счетчик элементов в блоке
                    bucket[i, array_size] = 0;
                }
            }
        }
    }
}
