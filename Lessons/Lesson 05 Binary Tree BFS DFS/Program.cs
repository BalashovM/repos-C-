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

            var elements = rand.Next(6,15);

            for (int i = 0; i < elements; i++)
            {
                binaryTree.Add(rand.Next(1, 99));
            }

            Console.WriteLine("Дерево :");
            Console.WriteLine($"{binaryTree.PrintTree()}");
            TreeBFS(binaryTree, 16);
            TreeDFS(binaryTree, 16);
            Console.ReadLine();
        }
        public static void TreeBFS(Tree _tree, int _search) 
        {
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(_tree.RootNode);
            Node level = _tree.RootNode;
            //Node last = null;
            Node current = null;
            StringBuilder printLevels = new StringBuilder();
            StringBuilder printAll = new StringBuilder();
            
            Console.WriteLine("Список обхода по уровням:");
            
            while (q.Count > 0)
            {
                current = q.Dequeue();

                if (current == null)
                    continue;

                if (current.Data.ToString() == _search.ToString())
                    break;

                printLevels.Append($"{current?.Data.ToString()} ");

                if (current == level || ((level == null) /*&& (current.LeftNode == null) && (current.RightNode == null)*/))
                {
                    Console.WriteLine($"{printLevels.ToString()}");
                    printLevels.Clear();
                    level = null;
                }

                q.Enqueue(current.LeftNode);
                q.Enqueue(current.RightNode);

                if (level == null )
                {
                    if (current.RightNode != null)
                    {
                        level = current.RightNode;
                    }
                    else
                    {
                        level = current.LeftNode;
                    }
                }

                printAll.Append($"{current?.Data.ToString()} ");
            }
            Console.WriteLine($"Список обхода в строку : {printAll.ToString()}");
        }
        public static void TreeDFS(Tree _tree, int _search)
        {
            Stack<Node> stack = new Stack<Node>();
            stack.Push(_tree.RootNode);
            StringBuilder printAll = new StringBuilder();
            var added = new HashSet<Node>();
            
            while (stack.Count>0)
            {
                var current = stack.Peek();
                
                if (current == null)
                    continue;

                if (current.Data.ToString() == _search.ToString())
                    break;

                if (current.LeftNode != null && !added.Contains(current.LeftNode))
                {
                    stack.Push(current.LeftNode);
                    added.Add(current.LeftNode);
                }
                else if (current.RightNode != null && !added.Contains(current.RightNode))
                {
                    stack.Push(current.RightNode);
                    added.Add(current.LeftNode);
                }
                else
                    stack.Pop();

                printAll.Append($"{current?.Data.ToString()} ");
            }
            Console.WriteLine($"Список обхода в строку : {printAll.ToString()}");
        }
    }
}
