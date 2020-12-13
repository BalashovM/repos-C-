using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_02_Month
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Вас приветсвует программа вывода названия месяца по его номеру.");
            int num;
            bool isNum = false;
            bool isMonth = false;
            String value;

            while (!isMonth)
            {
                Console.Write("Введите номер месяца: ");
                value = Console.ReadLine();

                isNum = int.TryParse(value, out num);
                if (isNum && (num > 0  && num < 13))
                {
                    isMonth = true;
                    Console.WriteLine($"Название месяца - {System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(num)}");
                }
                else
                {
                    Console.WriteLine("Формат введенных данных неверен, для проверки нужно целое число в диапазоне от 1 до 12.");
                    Console.WriteLine("Попробуйте ещё раз.");
                }
            }
            Console.WriteLine("Для выхода нажмите любую клавишу.");
            Console.ReadKey();
        }
    }
}
