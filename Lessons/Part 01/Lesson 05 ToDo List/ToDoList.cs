using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson_05_ToDo_List
{
    public class ToDos
    {
        
        public List<ToDo> ToDoList {get; set;}

        public ToDos()
        {
            ToDoList  = new List<ToDo>();
        }
        public ToDos(List<ToDo> toDoList)
        {
            ToDoList = toDoList;
        }

        public void Add(string title, bool isDone)
        {
            int qtyToDo = this.ToDoList.Count();
            int number = qtyToDo++;

            this.ToDoList.Add(new ToDo(number, title, isDone));

        }

        public void ShowList()
        {
            Console.WriteLine();
            Console.WriteLine("Номер | Выполнена |              Задача           ");
            Console.WriteLine("--------------------------------------------------");
            foreach (var td in this.ToDoList)
            {
                Console.WriteLine("   {0}        {1}   \t{2}", td.Number.ToString(), (td.IsDone) ? "X" : "O", td.Title);
            }
        }

        public void NewToDo()
        {
            string title = String.Empty;
            bool isDone = false;

            Console.WriteLine("Введите новую задачу : ");
            title = Console.ReadLine().Trim();

            bool isSelect = false;
            while (!isSelect)
            {
                Console.Write("Задача выполнена? (1 - Да, 2 - Нет) : ");
                string value = Console.ReadLine();

                if (int.TryParse(value, out int intIsDone))
                {
                    switch (intIsDone)
                    {
                        case 1: isSelect = true; isDone = true; break;
                        case 2: isSelect = true; isDone = false; break;
                        default: Console.WriteLine("Необходимо выбрать (1 - Да, 2 - Нет)."); break;
                    }
                }
                else
                    Console.WriteLine("Необходимо выбрать (1 - Да, 2 - Нет).");
            }

            this.Add(title, isDone);
        }
    }
}
