using System;
using System.Diagnostics;

namespace Lesson_06_Task_Manager
{
    public class MyProcessException : Exception { }

    class Program
    {
        static bool keepRunning = true;

        static public int numOfId, numOfQty;

        static void Main(string[] args)
        {
            Console.WriteLine("Вас приветствует программа Диспетчер процессов");

            ProcessShowList();

            do
            {
                string lastCommand = Console.ReadLine();
                ProcessCommand(lastCommand);
            } while (keepRunning);
        }

        static void ProcessShowList()
        {
            Console.WriteLine("Текущий список процессов: ");

            Process[] processes = Process.GetProcesses();

            int maxId = 0;
            //Для красоты
            foreach (var process in processes)
                if (maxId < process.Id)
                    maxId = process.Id;
            numOfId = (int)Math.Log10(maxId) + 1;
            numOfQty = (int)Math.Log10(processes.Length) + 1;

            int qty = 1;
            foreach (var process in processes)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Num: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{(qty).ToString().PadRight(numOfQty, ' ')}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("| ID: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{(process.Id).ToString().PadRight(numOfId, ' ')}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("| Name: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{process.ProcessName}");
                Console.Write("\n");
                qty++;
            }

            Console.ForegroundColor = ConsoleColor.Gray;

            Console.WriteLine("Убить процесс - kill ID или kill Name");
            Console.WriteLine("Обновить список - refresh");
            Console.WriteLine("Выйти из программы - exit");

            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        static void ProcessCommand(string command)
        {
            if (string.IsNullOrEmpty(command))
                return;

            string[] commandParts = command.Split(' ');

            switch (commandParts[0])
            {
                case "exit":
                    keepRunning = false;
                    break;
                case "kill":
                    if (commandParts.Length < 2)
                    {
                        Console.WriteLine("Для команды kill необходимо указать name или ID процесса");
                        return;
                    }

                    int id;
                    if (int.TryParse(commandParts[1], out id))
                        try
                        {
                            KillProcessByID(id);
                        }
                        catch (ArgumentException e)
                        {
                            Console.WriteLine($"Произошла ошибка: {e.Message}");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Произошла ошибка: {e.Message}");
                        }
                    else
                        try
                        {
                            KillProcessByName(commandParts[1]);
                        }
                        catch (MyProcessException)
                        {
                            Console.WriteLine($"Процесс с именем {commandParts[1]} не найден");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Произошла ошибка: {e.Message}");
                        }
                    break;
                case "refresh":
                    Console.Clear();
                    ProcessShowList();
                    break;
                default:
                    Console.WriteLine($"Неизвестная команда {command}"); ;
                    break;
            }
        }

        static void KillProcessByName(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);

            int qty = 1;
            if (processes.Length > 0)
                foreach (Process process in processes)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Num: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{(qty).ToString().PadRight(numOfQty, ' ')}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("| ID: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{(process.Id).ToString().PadRight(numOfId, ' ')}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("| Name: ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"{process.ProcessName}");
                    Console.Write("\n");
                    qty++;

                    try
                    {
                        process.Kill();
                    }
                    catch (Exception e)
                    {
                        throw new Exception($"Возникла следующая ошибка при попытке убить процесс с ID = {process.Id} \n {e.Message}" ) ;
                    }
                }
            else
            {
                throw new MyProcessException();
            }
        }

        static void KillProcessByID(int processID)
        {
            Process process = Process.GetProcessById(processID);
            if (process != null)
            {
                Console.WriteLine("ID: " + process.Id + " | Name: " + process.ProcessName);
                process.Kill();
            }
        }
    }
}