using System;
using System.Threading;

namespace Lesson_04_Fibonacci
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Вас приветсвует программа, вычисления числа Фибоначчи.");

            bool isEnd = false;

            while (!isEnd)
            {
                bool isNum = false;
                while (!isNum)
                {
                    Console.Write("Введите вычисляемый член последовательности Фибоначчи : ");
                    string value = Console.ReadLine();

                    isNum = int.TryParse(value, out int num);
                    if (isNum)
                        Console.WriteLine($"Число Фибонначчи для члена {num} = {Fibonacci(num)}");
                    else
                        Console.WriteLine("Не правильный формат данных, введите целое число.");
                }
                
                isEnd = Enough();
            }
        }

        static bool Enough()
        {
            bool isAnswer = false, ret = false;

            while (!isAnswer)
            {
                Console.WriteLine("Повторить ввод? (1 - ДА, 2 - НЕТ )");
                string choice = Console.ReadLine();

                if (choice == "1" || choice == "2")
                {
                    isAnswer = true;

                    if (choice == "1")
                    {
                        Console.WriteLine("С удовольствием.");
                        Thread.Sleep(1000);
                        Console.Clear();
                        ret = false;
                    }
                    else
                    {
                        Console.WriteLine("До скорой встречи.");
                        Thread.Sleep(1000);
                        ret = true;
                    }
                }
                else
                    Console.WriteLine("Введите 1 или 2.");
            }
            return ret;
        }

        static int Fibonacci(int n)
        {
            if (n == 0 || n == 1)
                return n;
            else
                return Fibonacci(n - 1) + Fibonacci(n - 2);
        }
    }

}