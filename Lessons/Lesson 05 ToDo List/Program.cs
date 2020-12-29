using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Lesson_05_ToDo_List
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Вас приветсвует программа список задач .");

            string fileToDo = "task.json";
            string path = Path.Combine(Environment.CurrentDirectory, fileToDo);

            ToDos toDoList = new ToDos();

            if (File.Exists(path))
            {
                string extToDoList = File.ReadAllText(fileToDo);
                toDoList.ToDoList = JsonSerializer.Deserialize<List<ToDo>>(extToDoList);
            }
            else
                ToDoManualAdd(toDoList);

            toDoList.ShowList();

            bool isExit = false;
            while (!isExit)
            {
                bool isSelect = false;

                while (!isSelect)
                {
                    Console.WriteLine("\n Выберите действие : 1 - Добавить запись, 2 - Выбрать запись для отметки, 3 - Выйти");
                    string value = Console.ReadLine();

                    if (int.TryParse(value, out int action))
                    {
                        switch (action)
                        {
                            case 1: isSelect = true; ToDoManualAdd(toDoList); break;
                            case 2: isSelect = true; ToDoCheck(toDoList); break;
                            case 3: isSelect = true; isExit = true; break;
                            default: break;
                        }
                    }
                }

                ToDoReload(toDoList);
            }

            string json = JsonSerializer.Serialize(toDoList.ToDoList);
            File.WriteAllText(fileToDo,  json );
        }

        static void ToDoManualAdd(ToDos toDoList)
        {
            bool isManualFinish = false;
            while (!isManualFinish)
            {
                toDoList.NewToDo();

                bool isSelect = false;
                while (!isSelect)
                {
                    Console.Write("Добавить ещё задачу? (1 - Да, 2 - Нет) : ");
                    string value = Console.ReadLine();

                    if (int.TryParse(value, out int intIsDone))
                    {
                        switch (intIsDone)
                        {
                            case 1: isSelect = true; isManualFinish = false; break;
                            case 2: isSelect = true; isManualFinish = true; break;
                            default: Console.WriteLine("Необходимо выбрать (1 - Да, 2 - Нет)."); break;
                        }
                    }
                    else
                        Console.WriteLine("Необходимо выбрать (1 - Да, 2 - Нет).");
                }
            }
        }
        static void ToDoCheck(ToDos toDoList)
        {
            bool isEnd = false;
            while (!isEnd)
            {
                bool isSelect = false;
                while (!isSelect)
                {
                    Console.Write("Укажите номер задания : ");
                    string value = Console.ReadLine();

                    if (int.TryParse(value, out int intToDoNum))
                    {
                        foreach (ToDo toDoItem in toDoList.ToDoList)
                        {
                            if (toDoItem.Number == intToDoNum)
                            {
                                toDoItem.isChange();
                                isSelect = true;
                                break;
                            }
                        }
                    }
                    else
                        Console.WriteLine("Необходимо ввести целое число.");
                    
                    if(!isSelect)
                        Console.WriteLine("Указаный вами номер задания отсутствует в списке ");

                    Console.Write("Отметить ещё одно задание? (1 - Да, 2 - Нет): ");
                    value = Console.ReadLine();

                    isSelect = false;
                    while (!isSelect)
                    {
                        if (int.TryParse(value, out int intSelect))
                        {
                            switch (intSelect)
                            {
                                case 1: isSelect = true; break;
                                case 2: isSelect = true; isEnd = true; break;
                                default: Console.WriteLine("Необходимо ввести целое число."); break;
                            }
                        }
                    }
                }
            }
        }

        static void ToDoReload(ToDos toDoList)
        {
            Console.Clear();
            toDoList.ShowList();
        }
    }
}
 