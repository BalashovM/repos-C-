using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_03_Backwards
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Вас приветсвует программа вывода строки задом наперёд.");

            Console.Write("Введите строку: ");
            string value = Console.ReadLine();

            Console.WriteLine($"Строка задом наперёд : {new string(value.ToCharArray().Reverse().ToArray())}");
            Console.WriteLine("Для выхода нажмите любую клавишу.");
            Console.ReadKey();
        }
    }
}
