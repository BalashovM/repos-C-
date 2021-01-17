using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_03_Array_Diagonal
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Вас приветсвует программа вывода заначений диагонали двумерного массива.");
                        
            int currentNumber, range;
            bool isNum, isArrayRange = false;
            String value;
            int[,] array;
            Random random = new Random();

            while (!isArrayRange)
            {
                Console.Write("Укажите размерность массива от 2 до 10 : ");
                value = Console.ReadLine();

                isNum = int.TryParse(value, out int num);
                if (isNum && (num > 1 && num < 11))
                {
                    Console.WriteLine($"Заполняем массив {num} до {num} случайными значениями: ");
                    isArrayRange = true;

                    if (num == 10)
                        range = 99;
                    else
                        range = num * num;

                    array = new int[num, num];

                    for (int i = 0; i < num; i++)
                    {
                        for (int j = 0; j < num ; j++)
                        {
                            currentNumber = random.Next(0, range);
                            array[i, j] = currentNumber;

                            Console.Write($"{currentNumber.ToString(CultureInfo.InvariantCulture),2} ");
                        }
                        Console.Write("\n");
                    }

                    Console.Write("Диагональ : ");

                    for (int i = 0; i < num; i++)
                        for (int j = 0; j < num; j++)
                        { 
                            if (i == j)
                                Console.Write($"{array[i, j].ToString(CultureInfo.InvariantCulture),2} ");
                        }
                    
                    Console.Write("\n");
                }
                else
                {
                    Console.WriteLine("Формат введенных данных неверен, для проверки нужно целое число в диапазоне от 2 до 10.");
                    Console.WriteLine("Попробуйте ещё раз.");
                }
            }
            Console.WriteLine("Для выхода нажмите любую клавишу.");
            Console.ReadKey();
        }
    }
}
