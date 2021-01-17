using System;
using System.IO;
using System.Threading;

namespace Lesson_05_Any_Text_To_File
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "text.txt";
            string[] fileText = { };

            Console.WriteLine("Вас приветсвует программа, записи текста в файл.");

            bool isEnd = false;

            while (!isEnd)
            {
                Console.Write("Введите текс для записи : ");
                string text = Console.ReadLine();
                
                fileText = File.ReadAllLines(fileName);

                if (fileText.Length == 0)
                    File.WriteAllText(fileName, text);
                else
                {
                    if (!UserDialog($"Файл {fileName} уже содержит {fileText.Length} строк, добавить (1) или перезаписать (2)"
                        , $"Добавляем в файл {fileName} текст {text}"
                        , $"Перезаписываем файл {fileName}"))
                    {
                        File.AppendAllLines(fileName, new[] { text });
                    }
                    else
                    {
                        File.WriteAllText(fileName, text);
                        File.AppendAllText(fileName, Environment.NewLine);
                    }
                }

                isEnd = UserDialog();
            }
            
            Console.Clear();
            Console.WriteLine($"Количество строк в файле {fileName} равно {fileText.Length} : ");
            Console.WriteLine($"{File.ReadAllText(fileName)}");
            Console.ReadKey();
        }
        static bool UserDialog()
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
                        Thread.Sleep(500);
                        Console.Clear();
                        ret = false;
                    }
                    else
                    {
                        Console.WriteLine("До скорой встречи.");
                        Thread.Sleep(500);
                        ret = true;
                    }
                }
                else
                    Console.WriteLine("Введите 1 или 2.");
            }
            return ret;
        }
        static bool UserDialog(string questions, string answerOne, string answerTwo)
        {
            bool isAnswer = false, ret = false;

            while (!isAnswer)
            {
                Console.WriteLine($"{questions}");
                string choice = Console.ReadLine();

                if (choice == "1" || choice == "2")
                {
                    isAnswer = true;

                    if (choice == "1")
                    {
                        Console.WriteLine($"{answerOne}");
                        Thread.Sleep(500);
                        Console.Clear();
                        ret = false;
                    }
                    else
                    {
                        Console.WriteLine($"{answerTwo}");
                        Thread.Sleep(500);
                        ret = true;
                    }
                }
                else
                    Console.WriteLine("Введите 1 или 2.");
            }
            return ret;
        }
    }
}
