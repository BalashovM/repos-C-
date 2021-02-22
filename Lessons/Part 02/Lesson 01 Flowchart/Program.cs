using System;

namespace Lesson_01_Flowchart
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Вас приветствует программа определения является ли введенное чило простым.");
            Console.Write($"Введите целое число : ");
            int.TryParse(Console.ReadLine(), out int n);
            int d = 0;
            int i = 2;
            while (i < n)
            {
                if (n % i == 0)
                {
                    d++;
                }
                i++;
            }

            if (d == 0)
            {
                Console.WriteLine("Введенное вами число является простым!");
            }
            else
            {
                Console.WriteLine("Введенное вами число не является простым");
            }
            Console.ReadKey();
        }
    }
}
