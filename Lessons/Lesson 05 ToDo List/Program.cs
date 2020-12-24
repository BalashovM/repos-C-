using System;

namespace Lesson_05_ToDo_List
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Вас приветсвует программа список задач .");

            var toDoList = new ToDoList();

            //Сделать чтение из файла
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

            toDoList.ShowList();
            
            //Сделать проверку на выполненные задания.

            Console.ReadKey();

        }
    }
}
