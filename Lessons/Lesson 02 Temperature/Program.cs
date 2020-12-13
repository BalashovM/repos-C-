using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_02_Temperature
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Вас приветсвует программа вычисления среднесуточной температуры.");

            decimal minTemp = -1000000;
            decimal maxTemp =  1000000;

            bool isMintemp = false, isMaxTemp = false;
            bool isDecimal;

            while (!isMintemp)
            {
                Console.Write("Введите минимальную темепературу за сутки: ");
                String value = Console.ReadLine();

                isDecimal = decimal.TryParse(value, NumberStyles.Currency, CultureInfo.InvariantCulture, out minTemp);
                if (!isDecimal)
                {
                    Console.WriteLine("Формат введенных данных неверен.");
                    Console.WriteLine("Попробуйте ещё раз.");
                }
                else
                    isMintemp = true;
            }

            while(!isMaxTemp)
            {
                Console.Write("Введите максимальную темепературу за сутки: ");
                String value = Console.ReadLine();

                isDecimal = decimal.TryParse(value, NumberStyles.Currency, CultureInfo.InvariantCulture, out maxTemp);
                if (!isDecimal)
                {
                    Console.WriteLine("Формат введенных данных неверен.");
                    Console.WriteLine("Попробуйте ещё раз.");
                }
                else if (minTemp > maxTemp)
                {
                    Console.WriteLine("Максимальная температура не может быть меньше минимальной.");
                    Console.WriteLine("Попробуйте ещё раз.");
                }
                else
                    isMaxTemp = true;
            }

            Console.WriteLine($"Среденесуточная температура { (minTemp + maxTemp)/2  }");
            Console.WriteLine("Для выхода нажмите любую клавишу.");
            Console.ReadKey();
        }
    }
}
