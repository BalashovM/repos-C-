using System;

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
                //Распологаем препятствия
                for (int i = 0; i < blockQty; i++)
                {
                    blockCoord[i, 0] = rand.Next(1, m);
                    blockCoord[i, 1] = rand.Next(1, n);
                }
                //Для сортировки переводим в дроби
                float[] blockCoordSort = new float[blockQty];
                for (int i = 0; i < blockQty; i++)
                {
                    blockCoordSort[i] = (float)((float)blockCoord[i, 0] + (float)blockCoord[i, 1] / 10.0);
                }
                //Использум QuickSort для float
                QuickSort(blockCoordSort,0, blockQty-1);
                //Переводим обратно в int
                for (int i = 0; i < blockQty; i++)
                {
                    int xx = (int)Math.Truncate(blockCoordSort[i]);
                    blockCoord[i, 0] = (int)Math.Truncate(blockCoordSort[i]);

                    string yyy = System.Convert.ToString(blockCoordSort[i]);
                    string[] parts = yyy.Split(',');
                    int.TryParse(parts[1],out int yy);
                    blockCoord[i, 1] = yy;
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
                            fieldWithBlocks[i, j] = 1;
                        else
                            fieldWithBlocks[i, j] = fieldWithBlocks[i - 1, j] + fieldWithBlocks[i, j - 1];

                int numbers = (int)Math.Log10(fieldWithBlocks[m - 1, n - 1]) + 2;

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
                            if (fieldWithBlocks[i - 1, j - 1] == 0)
                                Console.ForegroundColor = ConsoleColor.Red;
                            else 
                                Console.ResetColor();
                            Console.Write(fieldWithBlocks[i - 1, j - 1].ToString().PadLeft(numbers, ' '));
                        }
                    }
                    Console.Write("\n");
                }
                Console.Write("\n");
                Console.WriteLine($"Количество вариантов с препятствиями : {fieldWithBlocks[m - 1, n - 1]}");

                bool isChoise = false;
                while (!isChoise)
                {
                    int choise = GetUserInt("Закончить = 1, Повторить - 0 \nВаш выбор: ");

                    switch (choise)
                    {
                        case 1: isChoise = true; isEnd = true; break;
                        case 0: isChoise = true; break;
                        default: Console.WriteLine("Необходимо выбрать 0 или 1"); break;
                    }
                }
            }
        }
        static void QuickSort(float[] array, int first, int last)
        {
            int i = first, j = last;
            float x = array[(first + last) / 2];

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
                    isValue = true;
                    return value;
                }
                else
                    Console.WriteLine("Необходимо ввести целое число.");
            }

            return 0; 
        }
    }
}
