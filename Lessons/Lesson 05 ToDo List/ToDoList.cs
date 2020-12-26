using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson_05_ToDo_List
{
    public class ToDoList 

        public ToDoList()
        { 
        
        }

        public ToDo ToDoItem { get; set; }

        public void Add(string title, bool isDone)
        {
            int qtyToDo = this.Count();
            int number = qtyToDo++;

            this.Add(new ToDo(number, title, isDone));

        }

        public void ShowList()
        {
            Console.WriteLine();
            Console.WriteLine("Номер | Выполнена |              Задача           ");
            Console.WriteLine("--------------------------------------------------");
            foreach (var td in this)
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
                    if (intIsDone == 1)
                    {
                        isDone = true;
                        isSelect = true;
                    }
                    else
                    {
                        isDone = false;
                        isSelect = true;
                    }
                }
                else
                    Console.WriteLine("Необходимо выбрать (1 - Да, 2 - Нет).");
            }

            this.Add(title, isDone);
        }
    }
}
