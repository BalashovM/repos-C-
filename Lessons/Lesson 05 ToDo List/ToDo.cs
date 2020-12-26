using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson_05_ToDo_List
{
    public class ToDo
    {
        public int Number { get; set; }

        public string Title {get; set;}

        public bool IsDone { get; set; }

        public List<ToDo> ToDoList { get; }

        public ToDo()
        {
            Number = 0;
            Title = "";
            IsDone = false;
            if (ToDoList == null)
                ToDoList = new List<ToDo>();
        }

        public ToDo(string title)
        {
            Title = title;
            IsDone = false;
        }

        public ToDo(int number, string title, bool isDone)
        {
            Number = number;
            Title = title;
            IsDone = isDone;
        }

        public void Add(string title, bool isDone)
        {
            int qtyToDo;

            if (ToDoList == null)
                qtyToDo = 0;
            else
                qtyToDo = ToDoList.Count();
            
            int number = ++qtyToDo;

            ToDoList.Add(new ToDo(number, title, isDone));
        }

        public void ShowList()
        {
            Console.WriteLine();
            Console.WriteLine("Номер | Выполнена |              Задача           ");
            Console.WriteLine("--------------------------------------------------");
            foreach (var td in ToDoList)
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
