using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_02_Even_Number
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Вас приветсвует программа проверки числа, является ли оно чётным или нет.");
            int num;
            bool isNum = false;
            String value;

            while (!isNum)
            {
                Console.Write("Введите число для проверки: ");
                value = Console.ReadLine();

                isNum = int.TryParse(value, out num);
                if (!isNum)
                {
                    Console.WriteLine("Формат введенных данных неверен, для проверки нужно целое число.");
                    Console.WriteLine("Попробуйте ещё раз.");
                }
                else
                {
                    if ((num % 2) == 0)
                        Console.WriteLine($"Введенное вами число {num} - четное");
                    else
                        Console.WriteLine($"Введенное вами число {num} - нечетное");
                }
            }
            Console.WriteLine("Для выхода нажмите любую клавишу.");
            Console.ReadKey();
        }
    }
}
