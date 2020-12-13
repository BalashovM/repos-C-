﻿using System;
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

            decimal minTemp;
            decimal maxTemp;
            decimal avgTemp;
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
                {
                    isMintemp = true;
                    minTemp = decimal.Parse(value);
                }
            }
            avgTemp = minTemp;


            while (!isMaxTemp)
            {
                Console.Write("Введите максимальную темепературу за сутки: ");
                String value = Console.ReadLine();

                isDecimal = decimal.TryParse(value, NumberStyles.Currency, CultureInfo.InvariantCulture, out maxTemp);
                if (!isDecimal)
                {
                    Console.WriteLine("Формат введенных данных неверен.");
                    Console.WriteLine("Попробуйте ещё раз.");
                }
                else
                {
                    isMaxTemp = true;
                    //avgTemp = avgTemp + maxTemp;
                }
            }

            //avgTemp = avgTemp/2;
            
            Console.WriteLine($"Среденесуточная температура{ avgTemp  }");
            Console.WriteLine("Для выхода нажмите любую клавишу.");
            Console.ReadKey();
        }
    }
}
