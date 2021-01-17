using System;

namespace Lesson_02_Doubly_linked_list
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList myList = new LinkedList();

            Console.WriteLine("Введите через пробел числа для списка.");
            string val = Console.ReadLine();
            string[] values = val.Split(' ');
            
            foreach (var value in values)
            {
                if (int.TryParse(value, out int curNumber))
                    try
                    {
                        myList.AddNode(curNumber);
                    }
                    catch (OverflowException)
                    { }
            }

            bool isEnd = false;
            while (!isEnd)
            {
                bool isSelect = false;

                while (!isSelect)
                {
                    Console.Clear();
                    myList.PrintList();

                    Console.WriteLine("Выберите действие:");
                    Console.WriteLine("1 - Добавить в конец");
                    Console.WriteLine("2 - Добавить после какого-то");
                    Console.WriteLine("3 - Удалить по номеру");
                    Console.WriteLine("4 - Удалить по значению");
                    Console.WriteLine("5 - Сортировать по возрастанию.");
                    Console.WriteLine("6 - Двоичный поиск индекса по значению");
                    Console.WriteLine("0 - Выход");
                    val = Console.ReadLine();
                    if (int.TryParse(val, out int choise))
                    {
                        bool isValue = false;

                        switch (choise)
                        {
                            case 0: isSelect = true; isEnd = true; break;
                            case 1: 
                                isSelect = true;
                                while (!isValue)
                                {
                                    Console.Write("Введите значение: ");
                                    val = Console.ReadLine();
                                    if (int.TryParse(val, out int value))
                                    {
                                        if (myList.FindNode(value) == null)
                                        {
                                            isValue = true;
                                            myList.AddNode(value);
                                        }
                                        else
                                        {
                                            isSelect = false;
                                            Console.WriteLine("Элемент с таким значением уже есть в списке, введите другое число.");
                                        }
                                    }
                                    else
                                        Console.WriteLine("Введите целое число");
                                }
                                break;
                            case 2:
                                isSelect = true;
                                isValue = false;
                                int newValue = 0, index = -1;
                                while (!isValue)
                                {
                                    Console.Write("Введите значение: ");
                                    val = Console.ReadLine();
                                    if (int.TryParse(val, out newValue))
                                        if (myList.FindNode(newValue) == null)
                                            isValue = true;
                                        else
                                        {
                                            isSelect = false;
                                            Console.WriteLine("Элемент с таким значением уже есть в списке, введите другое число.");
                                        }
                                    else
                                        Console.WriteLine("Введите целое число");
                                }
                                isValue = false;
                                while (!isValue)
                                {
                                    Console.Write("Введите номер элемента после которого необходимо добавить значение: ");
                                    val = Console.ReadLine();
                                    if (int.TryParse(val, out index))
                                        isValue = true;
                                    else
                                        Console.WriteLine("Введите целое число");
                                }
                                myList.AddNodeAfter(myList.FindNodeByIndex(index), newValue);
                                break;
                            case 3:
                                isSelect = true;
                                while (!isValue)
                                {
                                    Console.Write("Введите номер элемента который необходимо удалить: ");
                                    val = Console.ReadLine();
                                    if (int.TryParse(val, out int value))
                                    {
                                        isValue = true;
                                        myList.RemoveNode(value);
                                    }
                                    else
                                        Console.WriteLine("Введите целое число");
                                }
                                break;
                            case 4:
                                isSelect = true;
                                while (!isValue)
                                {
                                    Console.Write("Введите элемент который необходимо удалить: ");
                                    val = Console.ReadLine();
                                    if (int.TryParse(val, out int value))
                                    {
                                        isValue = true;
                                        myList.RemoveNode(myList.FindNode(value));
                                    }
                                    else
                                        Console.WriteLine("Введите целое число");
                                }
                                break;
                            case 5:
                                isSelect = true;
                                myList.Sort();
                                break;
                            case 6:
                                isSelect = true;
                                Console.Write("Для двоичного поиска список будет отсортирован по возрастанию.");
                                myList.Sort();
                                myList.PrintList();

                                while (!isValue)
                                {
                                    Console.Write("Введите значение: ");
                                    val = Console.ReadLine();
                                
                                    if (int.TryParse(val, out int value))
                                    {
                                        int indexValue = myList.FindBinary(value);

                                        if (indexValue != -1)
                                        {
                                            isValue = true;
                                            Console.WriteLine($"Индекс {indexValue}");
                                            Console.ReadKey();
                                        }
                                        else
                                        {
                                            isValue = false;
                                            Console.WriteLine("Элемент с таким значением в списке не найден.");
                                        }
                                    }
                                    else
                                        Console.WriteLine("Введите целое число");
                                }
                                break;
                            default: Console.WriteLine("Необходимо выбрать только из предложенных вариантов"); break;
                        }
                    }
                    else
                        Console.WriteLine("Введите целое число из предложенных вариантов.");
                }
            }
        }
    }
    public class Node
    {
        public int Value { get; set; }
        public Node NextNode { get; set; }
        public Node PrevNode { get; set; }
    }

    //Начальную и конечную ноду нужно хранить в самой реализации интерфейса
    public interface ILinkedList
    {
        int GetCount(); // возвращает количество элементов в списке
        void AddNode(int value);  // добавляет новый элемент списка
        void AddNodeAfter(Node node, int value); // добавляет новый элемент списка после определённого элемента
        void RemoveNode(int index); // удаляет элемент по порядковому номеру
        void RemoveNode(Node node); // удаляет указанный элемент
        Node FindNode(int searchValue); // ищет элемент по его значению
        Node FindNodeByIndex(int index); //ищет элемент по его индексу
        void PrintList(); // вывод на экран
        void Sort();//Сортировка по возрастанию
        int FindBinary(int searchValue);//Двоичный поиск 
    }

    public class LinkedList : ILinkedList
    {
        private int _count = 0;

        private Node _startNode;
        private Node _endNode;

        public void AddNode(int value)
        {
            if (_startNode == null)
            {
                _startNode = new Node { Value = value };
                _endNode = _startNode;
            }
            else
            { 
                Node nodeAdded = new Node { Value = value };
                
                _endNode.NextNode = nodeAdded;
                nodeAdded.PrevNode = _endNode;
                _endNode = nodeAdded;
            }
            _count++;
        }

        public void AddNodeAfter(Node node, int value)
        {
            

            Node nodeAdded = new Node { Value = value };

            if (node == _endNode)
            {
                _endNode.NextNode = nodeAdded;
                nodeAdded.PrevNode = _endNode;
                _endNode = nodeAdded;
            }
            
            nodeAdded.NextNode = node.NextNode;
            node.NextNode.PrevNode = nodeAdded;
            node.NextNode = nodeAdded;
            nodeAdded.PrevNode = node;

            _count++;
        }

        public int FindBinary(int searchValue)
        {
            //Sort();

            int min = 1;
            int max = _count;

            while (min <= max)
            {
                int mid = (min + max) / 2;
                Node nodeTarget = FindNodeByIndex(mid);

                if (searchValue == nodeTarget.Value)
                    return mid;
                else if (searchValue < nodeTarget.Value)
                    max = mid - 1;
                else
                    min = mid + 1;
            }

            return -1;
        }

        public Node FindNode(int searchValue)
        {
            Node nodeTarget = null;

            bool isFind = false;
            while (!isFind)
            {
                if (nodeTarget == null)
                    nodeTarget = _startNode;
                else
                    nodeTarget = nodeTarget.NextNode;

                if (nodeTarget.Value == searchValue)
                    return nodeTarget;

                if(nodeTarget == _endNode && nodeTarget.Value != searchValue)
                    isFind = true;
            }

            return null;
        }

        public Node FindNodeByIndex(int index)
        {
            Node nodeTarget = null;

            if (index == 1)
                return _startNode;
            else if (index == _count)
                return _endNode;
            else
            {
                int i = 1;
                bool isFind = false;
                nodeTarget = _startNode;

                while (!isFind)
                {
                    nodeTarget = nodeTarget.NextNode;
                    i++;
                    if (i == index )
                        isFind = true;
                    if (i == _count && i != index)
                    {
                        isFind = true;
                        nodeTarget = null;
                    }
                }
            }
            return nodeTarget;
        }

        public int GetCount()
        {
            return _count;
        }

        public void PrintList()
        {
            Console.WriteLine($"Список из {_count} элементов: ");

            for (int i = 1; i <= _count; i++)
            {
                Node nodeTarget = FindNodeByIndex(i);
                Console.Write($"{nodeTarget.Value} ");
            }
            Console.Write("\n");
        }

        public void RemoveNode(int index)
        {
            Node nodeTarget = FindNodeByIndex(index);

            RemoveNode(nodeTarget);
        }

        public void RemoveNode(Node node)
        {
            if (node == _endNode)
            {
                node.PrevNode.NextNode = null;
                _endNode = node.PrevNode;
                node.PrevNode = null;
            }
            else if (node == _startNode)
            {
                node.NextNode.PrevNode = null;
                _startNode = node.NextNode;
                node.NextNode = null;
            }
            else
            {
                node.PrevNode.NextNode = node.NextNode;
                node.NextNode.PrevNode = node.PrevNode;
                node.PrevNode = null;
                node.NextNode = null;
            }

            _count--;
        }

        public void Sort()
        {
            for (int i = 1; i <= _count; i++)
            {
                for (int j = 1; j <= _count - 1; j++)
                {
                    Node nodeTarget = FindNodeByIndex(j);
                    if (nodeTarget.Value > nodeTarget.NextNode.Value)
                    {
                        int tempValue = nodeTarget.Value;
                        RemoveNode(j);
                        AddNodeAfter(FindNodeByIndex(j), tempValue);
                    }
                }
            }
        }
    }
}
