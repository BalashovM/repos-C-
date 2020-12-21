using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Lesson_04_Return_Concat_FIO
{
    class Program
    {
        enum NameParts
        {
            [Description("Имя")]
            FirstName = 0,
            [Description("Фамилия")]
            LastName,
            [Description("Отчество")]
            Patronymic
        }
        static void Main(string[] args)
        {
            bool isEnd = false;

            while (!isEnd)
            {
                string[] namePartValues = new string[Enum.GetValues(typeof(NameParts)).Cast<int>().Max() + 1];
                string value;

                Console.WriteLine("Вас приветсвует программа, вывода ФИО как одной строки из отдельных строк.");

                for (int namePart = 0; namePart <= Enum.GetValues(typeof(NameParts)).Cast<int>().Max(); namePart++)
                {
                    Console.Write("Введите ");
                    Console.Write($"{GetDescription((NameParts)namePart)} - ");
                    value = Console.ReadLine();
                    namePartValues[namePart] = value;
                }
                Console.WriteLine($"Ввод закончен, вот результат: {GetFullName(namePartValues[0], namePartValues[1], namePartValues[2])}");

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

        static string GetFullName(string firstName, string lastName, string patronymic)
        {
            return lastName + " " + firstName + " " + patronymic;
        }

        public static string GetDescription(Enum enumElement)
        {
            Type type = enumElement.GetType();

            MemberInfo[] memInfo = type.GetMember(enumElement.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return enumElement.ToString();
        }
    }
}
