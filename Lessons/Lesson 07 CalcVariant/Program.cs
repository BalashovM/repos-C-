using System;
using System.Collections.Generic;

namespace Lesson_07_CalcVariant
{
    class Program
    {
        static void Main(string[] args)
        {
            int m, n;
            Console.WriteLine("Задача:\nДля прямоугольного поля M на N клеток нужно подсчитать количество путей из верхней левой клетки в правую нижнюю.");
            Console.WriteLine("Известно, что ходить можно только на одну клетку вправо или вниз.");
            Console.WriteLine("Решение: в клетку (x, y) можно попасть из клеток (x-1, y) или (x, y-1).");
            Console.WriteLine("Т.е. F(x, y) = F(x-1, y) + F(x, y-1), а клетки вида (1, y) и (x, 1) - имеют только один вариант.");
            Console.WriteLine("Проверим ;-)");

            bool isEnd = false;
            while (!isEnd)
            {
                m = GetUserInt("Введите длину M: ");
                n = GetUserInt("Введите ширину N: ");

                //Реализация через массив значений
                ulong[,] field = new ulong[m, n];
                for (int i = 0; i < m; i++)
                    for (int j = 0; j < n; j++)
                        if (i == 0 || j == 0)
                            field[i, j] = 1;
                        else
                            field[i, j] = field[i - 1, j] + field[i, j - 1];

                Console.WriteLine($"Количество вариантов : {field[m - 1, n - 1]}");
                
                int maxSize = 10;
                Console.WriteLine($"Посчитаем вариант с перпятствиями, для упрощения восприятия размеры поля не более {maxSize}");

                Random rand = new Random();

                m = rand.Next(maxSize / 2, maxSize);
                n = rand.Next(maxSize / 2, maxSize);
                Console.WriteLine($"Размер поля : {m} на {n}");
                int blockQty = (int)Math.Truncate((m * n) * 0.1);
                Console.WriteLine($"Количество препятствий : {blockQty}");
                int[,] blockCoord = new int[blockQty, 2];
                HashSet<Tuple<int, int>> blocks = new HashSet<Tuple<int, int>>();
                while (blocks.Count != blockQty)
                {
                    blocks.Add(Tuple.Create(rand.Next(1, m),rand.Next(1, n)));
                }
                // Распологаем препятствия
                int el = 0;
                foreach (var x in blocks)
                {
                    blockCoord[el, 0] = x.Item1;
                    blockCoord[el, 1] = x.Item2;
                    el = el + 1;
                }
                
                //Определяем можитель/делитель
                int k = (int)Math.Pow(10, Math.Log10(maxSize));

                //Для сортировки переводим в дроби
                decimal[] blockCoordSort = new decimal[blockQty];
                for (int i = 0; i < blockQty; i++)
                    blockCoordSort[i] = (decimal)((decimal)blockCoord[i, 0] + (decimal)blockCoord[i, 1] / (decimal)k);
                //Использум QuickSort для float
                QuickSort(blockCoordSort,0, blockQty-1);
                //Переводим обратно в int
                for (int i = 0; i < blockQty; i++)
                {
                    blockCoord[i, 0] = (int)Math.Truncate(blockCoordSort[i]);
                    blockCoord[i, 1] = (int)Math.Truncate((blockCoordSort[i] - Math.Truncate(blockCoordSort[i])) * k); ;
                }
                
                Console.WriteLine("Координаты препятствий");
                for (int i = 0; i < blockQty; i++)
                { Console.WriteLine($" {i+1} - {blockCoord[i, 0]+1} : {blockCoord[i, 1]+1}"); }

                int blockCount = 0;

                //Реализация через массив значений
                int[,] fieldWithBlocks = new int[m, n];
                for (int i = 0; i < m; i++)
                    for (int j = 0; j < n; j++)
                        if (blockCount < blockQty && i == blockCoord[blockCount, 0] && j == blockCoord[blockCount, 1])
                        {
                            fieldWithBlocks[i, j] = 0;
                            blockCount++;
                        }
                        else if (i == 0 || j == 0)
                        {
                            if((i == 0 && j > 0 && fieldWithBlocks[i, j - 1] == 0) || (i > 0 && j == 0 && fieldWithBlocks[i-1, j] == 0))
                                fieldWithBlocks[i, j] = 1;
                            else
                                fieldWithBlocks[i, j] = 1;
                        }
                        else
                            fieldWithBlocks[i, j] = fieldWithBlocks[i - 1, j] + fieldWithBlocks[i, j - 1];

                
                int maxValue = fieldWithBlocks[m - 1, n - 1];
                //Если препятствие в конце тогда определяем максимальный элемент
                if(maxValue ==0)
                    //Ищем максимальный элемент массива
                    foreach (int num in fieldWithBlocks)
                    {
                        maxValue = maxValue < num ? num : maxValue;
                    }

                int numbers = (int)Math.Log10(maxValue) + 2;

                Console.WriteLine("Массив значений:");
                for (int i = 0; i < m + 1; i++)
                {
                    for (int j = 0; j < n + 1; j++)
                    {
                        if (i == 0 && j == 0)
                            Console.Write("".PadLeft(numbers, ' '));
                        else if (i == 0 && j > 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(j.ToString().PadLeft(numbers, ' '));
                        }
                        else if (j == 0 && i > 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(i.ToString().PadLeft(numbers, ' '));
                        }
                        else
                        {
                            if (fieldWithBlocks[i - 1, j - 1] == 0 )
                                if(blocks.Contains(Tuple.Create(i - 1, j - 1)))
                                    Console.ForegroundColor = ConsoleColor.Red;
                                else
                                    Console.ForegroundColor = ConsoleColor.Blue;
                            else 
                                Console.ResetColor();
                            Console.Write(fieldWithBlocks[i - 1, j - 1].ToString().PadLeft(numbers, ' '));
                        }
                    }
                    Console.ResetColor();
                    Console.Write("\n");
                }
                Console.Write("\n");
                Console.WriteLine("Красные 0 - препятствия, Синие 0 - нет вариантов пройти, Белые цифры - кол-во вариантов добраться до точки.");
                Console.WriteLine($"Количество вариантов с препятствиями : {fieldWithBlocks[m - 1, n - 1]}");

                bool isChoise = false;
                while (!isChoise)
                {
                    int choise = GetUserInt("Закончить = 1, Повторить - 2 \nВаш выбор: ");

                    switch (choise)
                    {
                        case 1: isChoise = true; isEnd = true; break;
                        case 2: isChoise = true; break;
                        default: Console.WriteLine("Необходимо выбрать 1 или 2"); break;
                    }
                }
            }
        }
        static void QuickSort(decimal[] array, int first, int last)
        {
            int i = first, j = last;
            decimal x = array[(first + last) / 2];

            do
            {
                while (array[i] < x)
                    i++;
                while (array[j] > x)
                    j--;

                if (i <= j)
                {
                    if (array[i] > array[j])
                    {
                        var tmp = array[i];
                        array[i] = array[j];
                        array[j] = tmp;
                    }
                    i++;
                    j--;
                }
            } while (i <= j);

            if (i < last)
                QuickSort(array, i, last);
            if (first < j)
                QuickSort(array, first, j);
        }

        static int GetUserInt(string question)
        {
            bool isValue;
            Console.Write(question);
            isValue = false;
            while (!isValue)
            {
                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    if (value > 0)
                    {
                        isValue = true;
                        return value;
                    }
                    else
                        Console.WriteLine("Число должно быть больше нуля.");
                }
                else
                    Console.WriteLine("Необходимо ввести целое число.");
            }

            return 0; 
        }
    }
}
