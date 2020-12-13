using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_02_Weather_Outside
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Вас приветсвует программа вычисления погоды на улице.");
            string[] month =
            {   "Empty",
                "Зима",
                "Зима",
                "Весна",
                "Весна",
                "Весна",
                "Лето",
                "Лето",
                "Лето",
                "Осень",
                "Осень",
                "Осень",
                "Зима"
            };
            
            decimal minTemp = -1000000;
            decimal maxTemp = 1000000;
            decimal avgTemp;

            bool isMintemp = false, isMaxTemp = false;
            bool isDecimal;

            int num = 0;
            bool isNum = false;
            bool isMonth = false;

            String value, messageText = "";

            while (!isMonth)
            {
                Console.Write("Введите номер месяца: ");
                value = Console.ReadLine();

                isNum = int.TryParse(value, out num);
                if (isNum && (num > 0 && num < 13))
                {
                    isMonth = true;
                    Console.WriteLine($"Название месяца - {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(num)}");
                    Console.WriteLine($"Время года - {month[num]}");
                }
                else
                {
                    Console.WriteLine("Формат введенных данных неверен, для проверки нужно целое число в диапазоне от 1 до 12.");
                    Console.WriteLine("Попробуйте ещё раз.");
                }
            }

            while (!isMintemp)
            {
                Console.Write("Введите минимальную темепературу за месяц: ");
                value = Console.ReadLine();

                isDecimal = decimal.TryParse(value, NumberStyles.Currency, CultureInfo.InvariantCulture, out minTemp);
                if (!isDecimal)
                {
                    Console.WriteLine("Формат введенных данных неверен.");
                    Console.WriteLine("Попробуйте ещё раз.");
                }
                else
                    isMintemp = true;
            }

            while (!isMaxTemp)
            {
                Console.Write("Введите максимальную темепературу за месяц: ");
                value = Console.ReadLine();

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

            avgTemp = (minTemp + maxTemp) / 2;
            Console.WriteLine($"Средняя температура за месяц {avgTemp}");

            switch (num)
            {

                case 1:
                case 2:
                case 12:
                    if (avgTemp > 0)
                        messageText = "Дождливая зима";
                    else if (avgTemp <= 0 && avgTemp > -10)
                        messageText = "Тёплая зима";
                    else if (avgTemp <= -10 && avgTemp > -20)
                        messageText = "Холодная зима";
                    else
                        messageText = "Суровая зима";
                    break;
                case 3:
                case 4:
                case 5:
                    if (avgTemp < 0)
                        messageText = "Холодная весна";
                    else if (avgTemp >= 0 && avgTemp < 10)
                        messageText = "Дожливая весна";
                    else if (avgTemp > 10)
                        messageText = "Тёплая весна";
                    break;
                case 6:
                case 7:
                case 8:
                    if (avgTemp <20)
                        messageText = "Холодное лето ";
                    else if (avgTemp > 20 && avgTemp < 30)
                        messageText = "Мягкое лето";
                    else if (avgTemp >30)
                        messageText = "Засушливое лето";
                    break;
                case 9:
                case 10:
                case 11:
                    if (avgTemp < 0)
                        messageText = "Холодное осень ";
                    else if (avgTemp > 0 && avgTemp < 10)
                        messageText = "Дождливая осень";
                    else if (avgTemp > 10)
                        messageText = "Золотая осень";
                    break;
            }
            Console.WriteLine(messageText);
            Console.WriteLine("Для выхода нажмите любую клавишу.");
            Console.ReadKey();
        }
    }
}
