using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Представьтесь пожалуйста, как вас зовут: ");
            String UserName = Console.ReadLine();
            Console.Clear();
            Console.WriteLine($"Привет, {UserName}, сегодня {DateTime.Today.ToString("d")} текущее время {(DateTime.Now).ToString("HH:mm:ss")}!");
            Console.ReadKey(); 
        }
    }
}
