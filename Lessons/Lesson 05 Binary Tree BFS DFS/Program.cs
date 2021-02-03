using Lesson_04_Binary_Tree;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson_05_Binary_Tree_BFS_DFS
{
    class Program
    {
        static void Main(string[] args)
        {
            var binaryTree = new Tree();
            Random rand = new Random();

            int nodeMin = 6;
            int nodeMax = 15;
            int valueMin = 1;
            int valueMax = 99;

            //Количество эелемнтов в дереве 
            var elements = rand.Next(nodeMin, nodeMax);
            var searchIndex = rand.Next(elements%2, elements-1);

            int searchValue = 0, currenValue = 0;

            for (int i = 0; i < elements; i++)
            {
                currenValue = rand.Next(valueMin, valueMax);
                binaryTree.Add(currenValue);

                if (searchIndex == i)
                    searchValue = currenValue;
            }

            Console.WriteLine("Дерево :");
            Console.WriteLine($"{binaryTree.PrintTree()}");

            Console.WriteLine("Выберите вариант : 1 - Полный обход дерева, 2 - Случайный выбор искомого элемента, 3 - Задать искомое значение в ручную");
            bool isSelect = false;
            while (!isSelect)
            {
                string val = Console.ReadLine();
                if (int.TryParse(val, out int selectValue))
                {
                    switch (selectValue)
                    {
                        case 1: searchValue = 0; isSelect = true;  break;
                        case 2: isSelect = true; break;
                        case 3:
                            bool isUserValue = false;
                            while (!isUserValue)
                            {
                                Console.Write("Введите искомое значение : ");
                                val = Console.ReadLine();
                                if (int.TryParse(val, out int userValue))
                                {
                                    searchValue = userValue;
                                    isUserValue = true;
                                    isSelect = true;
                                }
                                else
                                    Console.WriteLine("Необходимо ввести целое число.");
                            }
                            break;
                        default:
                            Console.WriteLine("Нет такого варианта. Необходимо выбрать только из 1,2,3");
                            break;
                    }
                }
                else
                    Console.WriteLine("Необходимо ввести целое число.");
            }


            Console.WriteLine($"Искомое значение в дереве: {searchValue}");
            TreeBFS(binaryTree, searchValue);
            TreeDFS(binaryTree, searchValue);
            Console.ReadLine();
        }
        public static void TreeBFS(Tree _tree, int _search) 
        {
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(_tree.RootNode);
            Node current = null;
            StringBuilder stepValue = new StringBuilder();
            StringBuilder printAll = new StringBuilder();
            int step = 1;
            bool isFind = false;

            Console.WriteLine($"\nВывод очереди по шагам \n{step}: {_tree.RootNode.Data.ToString()}");
            
            while (q.Count > 0)
            {
                current = q.Dequeue();

                if (current == null)
                    continue;
                else
                    printAll.Append($"{current?.Data.ToString()} ");

                if (current.Data.ToString() == _search.ToString())
                {
                    Console.WriteLine($"Искомое значение найдено на шаге - {step}");
                    isFind = true;
                    break;
                }

                if (current.LeftNode != null)
                    q.Enqueue(current.LeftNode);
                if (current.RightNode != null)
                    q.Enqueue(current.RightNode);
                
                Node[] ar = q.ToArray();

                stepValue.Clear();
                foreach (var x in ar)
                {
                    if (x != null)
                        stepValue.Append($"{x?.Data.ToString()} ");
                }

                if (stepValue.Length>0)
                    Console.WriteLine($"{++step}: {stepValue.ToString()}");
            }
            if (!isFind)
                Console.WriteLine($"Искомое значение {_search.ToString()} в дереве не найдено.");
            Console.WriteLine($"Список обхода узлов в ширину : {printAll.ToString()}");
        }
        public static void TreeDFS(Tree _tree, int _search)
        {
            Stack<Node> stack = new Stack<Node>();
            stack.Push(_tree.RootNode);
            
            var added = new HashSet<Node>();
            added.Add(_tree.RootNode);

            StringBuilder stepValue = new StringBuilder();
            int step = 1;

            bool isFind = false;

            Console.WriteLine($"\nВывод стека по шагам \n{step}: {_tree.RootNode.Data.ToString()}");

            while (stack.Count>0)
            {
                var current = stack.Peek();
                
                if (current == null)
                    continue;

                if (current.Data.ToString() == _search.ToString())
                {
                    Console.WriteLine($"Искомое значение найдено на шаге - {step}");
                    isFind = true;
                    break;
                }

                if (current.LeftNode != null && !added.Contains(current.LeftNode))
                {
                    stack.Push(current.LeftNode);
                    added.Add(current.LeftNode);
                }
                else if (current.RightNode != null && !added.Contains(current.RightNode))
                {
                    stack.Push(current.RightNode);
                    added.Add(current.RightNode);
                }
                else
                    stack.Pop();

                Node[] ar = stack.ToArray();

                stepValue.Clear();
                foreach (var x in ar)
                {
                    if (x != null)
                        stepValue.Append($"{x?.Data.ToString()} ");
                }

                if (stepValue.Length > 0)
                    Console.WriteLine($"{++step}: {stepValue.ToString()}");
            }
            
            StringBuilder printAll = new StringBuilder();

            foreach ( var x in added)
                printAll.Append($"{x.Data.ToString()} ");
            if (!isFind)
                Console.WriteLine($"Искомое значение {_search.ToString()} в дереве не найдено.");
            Console.WriteLine($"Список обхода узлов в глубину : {printAll.ToString()}");
        }
    }
}
