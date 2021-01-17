using System;
using System.Globalization;
using System.Threading;

namespace Lesson_04_Sum_Numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isEnd = false;

            while (!isEnd)
            {
                Console.WriteLine("Вас приветсвует программа, вывода суммы чисел введенных через пробел в одной строке.");
                Console.Write("Введите числа разделенные пробелом : ");
                string strNumbers = Console.ReadLine();
                decimal sum = 0;

                string[] values = strNumbers.Split(' ');
                foreach (var value in values)
                {
                    if (decimal.TryParse(value, NumberStyles.Currency, CultureInfo.InvariantCulture, out decimal curNumber))
                        sum += curNumber;
                }

                Console.WriteLine($"Сумма введенных чисел = {sum}.");

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

    }
}
