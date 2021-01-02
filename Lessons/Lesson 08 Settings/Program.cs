using System;

namespace Lesson_08_Settings
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Properties.Settings.Default.Greeting);

            if (string.IsNullOrEmpty(Properties.Settings.Default.UserName))
            {
                SetUserSetting();
            }
            else
            {
                Console.WriteLine($"{Properties.Settings.Default.UserName}");
                Console.WriteLine($"Возраст {Properties.Settings.Default.UserAge}");
                Console.WriteLine($"Сфера деятельности {Properties.Settings.Default.UserCareer}");

                bool isSelect = false;
                while (!isSelect)
                {
                    Console.Write("Хотите изменить данные? (1 - Да, 2 - Нет) : ");
                    string value = Console.ReadLine();

                    if (int.TryParse(value, out int intIsDone))
                    {
                        switch (intIsDone)
                        {
                            case 1: isSelect = true; SetUserSetting(); break;
                            case 2: isSelect = true; break;
                            default: Console.WriteLine("Необходимо выбрать (1 - Да, 2 - Нет)."); break;
                        }
                    }
                    else
                        Console.WriteLine("Необходимо выбрать (1 - Да, 2 - Нет).");
                }
            }

            Console.WriteLine("До скорой встречи.");
            Console.ReadKey();

        }

        static void SetUserSetting()
        {
            Console.WriteLine("Введите имя пользователя:");
            Properties.Settings.Default.UserName = Console.ReadLine();


            bool isSelect = false;
            while (!isSelect)
            {
                Console.WriteLine("Введите возраст:");
                string value = Console.ReadLine();

                if (int.TryParse(value, out int intIsDone))
                {
                    Properties.Settings.Default.UserAge = intIsDone;
                    isSelect = true;
                }
                else
                    Console.WriteLine("Необходимо ввести целое число");

            }

            Console.WriteLine("Введите род деятельности:");
            Properties.Settings.Default.UserCareer = Console.ReadLine();

            Console.WriteLine("Балгодарю Вас!");

            Properties.Settings.Default.Save();

        }

    }
}
