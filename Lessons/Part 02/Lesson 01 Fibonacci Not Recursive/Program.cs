using System;
using System.Threading;

namespace Lesson_01_Fibonacci_Not_Recursive
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Вас приветсвует программа, вычисления числа Фибоначчи через рекурсию.");

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
        static long Fibonacci(int n)
        {
            long first = 0;
            long second = 1;
            long fib = 0;

            if (Math.Abs(n) == 1)
                fib = second;

            for (int i = 0; i < Math.Abs(n) - 1; i++)
            {
                fib = first + second;
                first = second;
                second = fib;
            }
            return Math.Sign(n) * fib;
        }
    }
}
