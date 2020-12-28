﻿using System;
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

            ToDo toDo = new ToDo();

            //Сделать чтение из файла
            if (File.Exists(path))
            {
                string extToDoList = File.ReadAllText(fileToDo);

                toDo.ToDoList = JsonSerializer.Deserialize<List<ToDo>>(extToDoList);
            }
            else
                ToDoManualAdd(toDo);

            toDo.ShowList();

            //Сделать проверку на выполненные задания.
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
                            case 1: isSelect = true; ToDoManualAdd(toDo); break;
                            case 2: isSelect = true; ToDoCheck(toDo);  break;
                            case 3: isExit = true;  isSelect = true; break;
                            default: break;
                        }
                    }
                }

                ToDoReload(toDo);
            }

            string json = JsonSerializer.Serialize(toDo.ToDoList);

            File.WriteAllText(fileToDo, json);
        }

        static void ToDoManualAdd(ToDo toDo)
        {
            bool isManualFinish = false;
            while (!isManualFinish)
            {
                toDo.NewToDo();

                bool isSelect = false;
                while (!isSelect)
                {
                    Console.Write("Добавить ещё задачу? (1 - Да, 2 - Нет) : ");
                    string value = Console.ReadLine();

                    if (int.TryParse(value, out int intIsDone))
                    {
                        if (intIsDone == 2)
                        {
                            isManualFinish = true;
                            isSelect = true;
                        }
                        else if (intIsDone == 1)
                        {
                            isManualFinish = false;
                            isSelect = true;
                        }
                    }
                    else
                        Console.WriteLine("Необходимо выбрать (1 - Да, 2 - Нет).");
                }
            }
        }
        static void ToDoCheck(ToDo toDo)
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
                        foreach (ToDo toDoItem in toDo.ToDoList)
                        {
                            if (toDoItem.Number == intToDoNum)
                            {
                                toDoItem.IsDone = true;
                                isSelect = true;
                                break;
                            }
                        }
                    }
                    else
                        Console.WriteLine("Необходимо ввести целое число.");
                    
                    if(!isSelect)
                        Console.WriteLine("Указаный вами номер задания отсутствует в списке ");

                    isSelect = false;
                    while (!isSelect)
                    {
                        Console.Write("Отметить ещё одно задание? (1 - Да, 2 - Нет): ");
                        value = Console.ReadLine();

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

        static void ToDoReload(ToDo toDo)
        {
            Console.Clear();
            toDo.ShowList();
        }
    }
}
 