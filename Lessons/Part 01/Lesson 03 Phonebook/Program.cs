using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_03_Phonebook
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Вас приветсвует программа телефонный справочник");
            
            string[,] phonebook = new string[5, 2];
            string[] nameSorting = new string[5];
            string value = "", name;
            int maxNameLength = 0;
            bool nameExist = false;
            bool phoneExist = false;

            Console.WriteLine("Введите пять контактов для телефонной книги.");

            for (int i = 0; i < phonebook.GetLength(0); i++)
            {
                do
                {
                    Console.Write($"Введите имя {i+1} из {phonebook.GetLength(0)}: ");
                    value = Console.ReadLine();

                    if (i > 0)
                    {
                        for (int k = 0; k < i; k++)
                        {
                            nameExist = phonebook[k, 0] == value;

                            if (nameExist)
                            {
                                Console.WriteLine("Такое имя уже существует в телефонной книге, введите другое имя.");
                                break;
                            }
                        }
                    }

                     if (!nameExist)
                        phonebook[i, 0] = value;
                }
                while (nameExist);

                if(value.Length > maxNameLength) 
                    maxNameLength = value.Length;
                name = value;

                do
                {
                    Console.Write($"Введите номер телефона/email для контакта {name} : ");
                    value = Console.ReadLine();

                    if (i > 0)
                    {
                        for (int k = 0; k < i; k++)
                        {
                            phoneExist = phonebook[k, 1] == value;

                            if (phoneExist)
                            {
                                Console.WriteLine($"Такой номер телефона/email уже существует в телефонной книге у {phonebook[k, 0]}, введите другой номер телефона/email.");
                                break;
                            }
                        }
                    }

                    if (!phoneExist)
                        phonebook[i, 1] = value;
                }
                while (phoneExist);
            }
            
            Console.Clear();

            for (int i = 0; i < phonebook.GetLength(0); i++)
            {
                nameSorting[i] = phonebook[i, 0];
            }

            Array.Sort(nameSorting);
           
            Console.WriteLine("Ваша записная книжка отсортированная по контактам по возрастанию: ");
            for (int i = 0; i < nameSorting.GetLength(0); i++)
            {
                for (int j = 0; j < phonebook.GetLength(0); j++)
                {
                    if (phonebook[j, 0] == nameSorting[i])
                        value = phonebook[j, 1];
                } 

                Console.WriteLine($"{i + 1} - {nameSorting[i].PadRight(maxNameLength, ' ')} - {value}");
            }

            Console.WriteLine("Для выхода нажмите любую клавишу.");
            Console.ReadKey();
        }
    }
}
