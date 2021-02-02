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
            var searchIndex = rand.Next(0, elements-1);

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
            StringBuilder printAll = new StringBuilder();
            
            while (q.Count > 0)
            {
                current = q.Dequeue();

                if (current == null)
                    continue;
                else
                    printAll.Append($"{current?.Data.ToString()} ");

                if (current.Data.ToString() == _search.ToString())
                    break;

                q.Enqueue(current.LeftNode);
                q.Enqueue(current.RightNode);
            }
            Console.WriteLine($"Список обхода в ширину : {printAll.ToString()}");
        }
        public static void TreeDFS(Tree _tree, int _search)
        {
            Stack<Node> stack = new Stack<Node>();
            stack.Push(_tree.RootNode);
            
            var added = new HashSet<Node>();
            added.Add(_tree.RootNode);

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
                    added.Add(current.RightNode);
                }
                else
                    stack.Pop();
            }
            
            StringBuilder printAll = new StringBuilder();

            foreach ( var x in added)
                printAll.Append($"{x.Data.ToString()} ");

            Console.WriteLine($"Список обхода в глубину : {printAll.ToString()}");
        }
    }
}
