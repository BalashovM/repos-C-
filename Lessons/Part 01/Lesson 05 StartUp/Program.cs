using System;
using System.IO;

namespace Lesson_05_StartUp
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "startup.txt";
            string[] fileText = { };
            if (!File.Exists(fileName))
            {
                Console.WriteLine($"Файл с именем {fileName} не найден, и будет создан заново.");
            }
            else
            {
               
                int count = File.ReadAllLines(fileName).Length;

                Console.WriteLine($"Найден файл {fileName} в котором количество строк = {File.ReadAllLines(fileName).Length} ");
            }

            File.AppendAllLines(fileName, new[] { $"Запуск программы был произведен {DateTime.Today.ToString("d")} в {(DateTime.Now).ToString("HH:mm:ss")}. " });
           
            Console.WriteLine("Время запуска программы добавлно в файл.");
            Console.WriteLine("Для выхода из программы нажмите любую клавишу.");
            Console.ReadKey();
        }
    }
}
