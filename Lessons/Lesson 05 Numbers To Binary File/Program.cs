using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Lesson_05_Numbers_To_Binary_File
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "bytes.bin";

            Console.WriteLine("Вас приветсвует программа, записи чисел в двоичный файл.");
            
            bool isEnd = false;

            while (!isEnd)
            {
                Console.WriteLine("Введите числа для записи от 0 до 255 в произвольном порядке : ");
                string strNumbers = Console.ReadLine();

                string[] values = strNumbers.Split(' ');

                List<byte> list = new List<byte>();

                foreach (var value in values)
                {
                    if (byte.TryParse(value, out byte curNumber))
                        try
                        {
                            list.Add(curNumber);
                        }
                        catch (OverflowException) 
                        { }
                }

                byte[] arrayNum = list.ToArray();
                File.WriteAllBytes(fileName, arrayNum);

                arrayNum = File.ReadAllBytes(fileName);

                Console.WriteLine($"Удалось записать в двоичный файл {fileName} : ");
                bool begin = true;
                foreach (var value in arrayNum)
                {
                    if (!begin)
                        Console.Write(", ");
                    else
                        begin = false;

                    Console.Write("{0}",String.Format("{0:X2}", value));
                    Console.Write($" ({Convert.ToInt32(value)})");
                }
                
                Console.Write("\n");
                isEnd = UserDialog();
            }
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
    }
}
