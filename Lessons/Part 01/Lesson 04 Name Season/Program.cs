using System;
using System.Globalization;
using System.Threading;

namespace Lesson_04_Name_Season
{
    class Program
    {
        enum Seasons
        {
            Winter = 1,
            Sprint,
            Summer,
            Autumn
        }
        static void Main(string[] args)
        {
            bool isEnd = false;

            while (!isEnd)
            {
                Console.WriteLine("Вас приветсвует программа, вывода названия времени года по номеру месяца.");

                bool isMonth = false;
                while (!isMonth)
                {
                    Console.Write("Введите номер месяца от 1 до 12 : ");
                    string monthNumber = Console.ReadLine();

                    bool isNum = int.TryParse(monthNumber, out int num);
                    if (isNum && (num > 0 && num < 13))
                    {
                        isMonth = true;
                        
                        string description = GetDescriptionBySeason(GetSeasonByMonth(num));
                        if (description != null)
                            Console.WriteLine($"Название сезона для выбранного месяца {CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(num)} - {description}");
                    }
                    else
                    {
                        Console.WriteLine("Формат введенных данных неверен, для проверки нужно целое число в диапазоне от 1 до 12.");
                        Console.WriteLine("Попробуйте ещё раз.");
                    }
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

        static Seasons GetSeasonByMonth(int monthNumber)
        {
            Seasons s = (Seasons)5;
    
            switch(monthNumber)
            {
                case 1:              
                case 2: s = Seasons.Winter; break;
                case 3:
                case 4:
                case 5: s = Seasons.Sprint; break;
                case 6:
                case 7:
                case 8: s = Seasons.Summer; break;
                case 9:
                case 10:
                case 11: s = Seasons.Autumn; break;
                case 12: s = Seasons.Winter; break;
            }

            return s;
        }

        static string GetDescriptionBySeason(Seasons s)
        {
            string ret = null;

            switch ((int)s)
            {
                case 1: ret = "Зима"; break;
                case 2: ret = "Весна"; break;
                case 3: ret = "Лето"; break;
                case 4: ret = "Осень"; break;
            }
            return ret;
        }
    }
}