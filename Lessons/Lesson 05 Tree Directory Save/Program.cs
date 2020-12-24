using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lesson_05_Tree_Directory_Save
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Вас приветсвует программа, сохранения в текстовый файл дерево каталогов и файлов .");

            string startkDir = Environment.GetEnvironmentVariable("USERPROFILE");
            string workDir =  Path.Combine(startkDir, "source\\repos\\Lessons");
            string fileNameRecursive = "tree_recursive.txt";
            string fileNameNotRecursive = "tree_not_recursive.txt";

            bool isEnd = false;

            while (!isEnd)
            {
                if (Directory.Exists(workDir))
                {
                    Console.WriteLine($"Сохраняем в файл дерево каталогов начиная с {workDir}");

                    if (!UserDialog("Сохранить с использованием рекурсии? Да (1), Нет (2)"
                        , $"Сохранияем в файл {fileNameRecursive}"
                        , $"Сохранияем в файл {fileNameNotRecursive}"))
                    {
                        File.Create(Path.Combine(workDir, "fileNameRecursive"));

                        List<string> treeList = GetTreeRecursive(workDir);
                        foreach (string element in treeList)
                        {
                            File.AppendAllLines(fileNameRecursive, new[] { element });
                        }
                    }
                    else
                    {
                        File.Create(Path.Combine(workDir, "fileNameRecursive"));

                        List<string> treeList = GetTreeNotRecursive(workDir);
                        foreach (string element in treeList)
                        {
                            File.AppendAllLines(fileNameNotRecursive, new[] { element });
                        }
                    }
                    Console.WriteLine("Готово.");
                }

                isEnd = UserDialog();
            }
            Console.WriteLine("Для выхода нажмите любую клавишу.");
            Console.ReadKey();
        }
        
        static List<string> GetTreeRecursive(string startPath)
        {
            List<string> listTree = new List<string>();
            try
            {
                string[] folders = Directory.GetDirectories(startPath);
                
                foreach (string folder in folders)
                {
                    listTree.Add("Папка: " + folder);
                    listTree.AddRange(GetTreeRecursive(folder));
                }

                string[] files = Directory.GetFiles(startPath);

                foreach (string file in files)
                {
                    listTree.Add("Файл: " + file);
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"Ошибка - {e.Message}");
            }

            return listTree;
        }

        static List<string> GetTreeNotRecursive(string startPath)
        {
            List<string> listTree = new List<string>();
            try
            {
                string[] folders = Directory.GetDirectories(startPath, "*.*", SearchOption.AllDirectories);

                foreach (string folder in folders)
                {
                    listTree.Add("Папка: " + folder);

                    string[] files = Directory.GetFiles(folder);

                    foreach (string file in files)
                    {
                        listTree.Add("Файл: " + file);
                    }
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"Ошибка - {e.Message}");
            }

            return listTree;
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
