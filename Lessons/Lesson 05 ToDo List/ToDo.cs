﻿namespace Lesson_05_ToDo_List
{
    public class ToDo
    {
        public int Number { get; set; }

        public string Title {get; set;}

        public bool IsDone { get; set; }

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
    }
}
