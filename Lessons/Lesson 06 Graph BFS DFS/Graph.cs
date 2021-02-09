using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lesson_06_Graph_BFS_DFS
{
    public class Node 
    {
        public int Value { get; set; }
        public List<Edge> Edges { get; set; }
        public Node(int value) 
        {
            Value = value;
            Edges = new List<Edge>();
        }
    }
    public class Edge 
    { 
        public int Weight { get; set; }
        public Node Node { get; set; }
    }
    class Graph
    {
        public List<Node> Nodes { get; set; }

        public Graph(Matrix _matrix)
        {
            Nodes = new List<Node>();

            //Добавляем вершины графа
            for (int i = 0; i < _matrix.Size; i++)
            {
                Node newNode = new Node(i);
                Nodes.Add(newNode);
            }

            for (int i = 0; i < _matrix.Size; i++)
            {
                Node currentNode = Nodes.Find(x => x.Value == i);

                for (int j = 0; j < _matrix.Size; j++)
                {
                    if (_matrix.array[i, j] != 0)
                    {
                        Edge edge = new Edge();
                        edge.Node = Nodes.Find(x => x.Value == j);
                        edge.Weight = _matrix.array[i, j];
                        currentNode.Edges.Add(edge);
                    }
                }
            }
        }
        enum WaveState //2 - Прошли, 1- Гребень, 0-Ждём
        {
            Wait,
            Peak,
            Done,
        }
        class Wave 
        {
            public Node Node { get; set; }
            public WaveState State { get; set; }
            public int Number { get; set; }
        }
        public Node BFS(Node start, int _search)
        {
            var q = new Queue<Node>();
            var visited = new bool[Nodes.Count];
            var peaks = new List<Wave>();

            //Добавили в коллекцию все вершины с статусом ожидания
            foreach(var x in Nodes)
            {
                peaks.Add(new Wave { Node = x, State = WaveState.Wait, Number = 0 });
            }

            q.Enqueue(start);
            //Начальную вершину помечаем как гребень
            foreach (var x in peaks.Where(p => p.Node == start))
            {
                x.State = WaveState.Peak;
                x.Number = 1;
            }

            visited[start.Value] = true;

            Node current = null;
            int step = 1;
            StringBuilder stepValue = new StringBuilder();
            StringBuilder printAll = new StringBuilder();

            Console.WriteLine($"\nВывод очереди по шагам \n{step}: {start.Value}");
            while (q.Count > 0)
            {
                current = q.Dequeue();
                int curNumber = 0;

                if (current == null)
                    continue;
                else
                {
                    printAll.Append($"{current?.Value.ToString()} ");
                    //Список волны 
                    foreach (var x in peaks.Where(p => p.Node == current && p.State == WaveState.Peak))
                    {
                        x.State = WaveState.Done;
                        curNumber = x.Number;
                    }
                }
                
                if (current.Value == _search)
                {
                    Console.WriteLine($"Список обхода узлов в ширину : {printAll.ToString()}");
                    return current;
                }

                foreach (var x in current.Edges)
                {
                    if (!visited[x.Node.Value])
                    {
                        q.Enqueue(x.Node);
                        visited[x.Node.Value] = true;

                        foreach (var t in peaks.Where(p => p.Node == x.Node && p.State == WaveState.Wait))
                        {
                            t.State = WaveState.Peak;
                            t.Number = curNumber + 1;
                        }
                    }
                }

                Node[] ar = q.ToArray();

                stepValue.Clear();
                foreach (var x in ar)
                {
                    if (x != null)
                        stepValue.Append($"{x?.Value.ToString()} ");
                }

                if (stepValue.Length > 0)
                    Console.WriteLine($"{++step}: {stepValue.ToString()}");
            }

            stepValue.Clear();
            
            foreach (var x in peaks.OrderBy(u => u.Number).ThenBy(u => u.Node.Value))
            {
                stepValue.Append($"Волна {x?.Number.ToString()} - узел {x?.Node.Value.ToString()} \n");
            }
            Console.WriteLine($"Вывод по волнам:\n{stepValue.ToString()}");

            Console.WriteLine($"Список обхода узлов в ширину : {printAll.ToString()}");
            return null;
        }

        public Node DFS(Node start, int _search)
        {
            Stack<Node> stack = new Stack<Node>();
            stack.Push(start);
            var visited = new bool[Nodes.Count];
            visited[start.Value] = true;

            StringBuilder printAll = new StringBuilder();
            StringBuilder stepValue = new StringBuilder();
            int step = 1;

            Console.WriteLine($"\nВывод очереди по шагам \n{step}: {start.Value}");

            while (stack.Count > 0)
            {
                var current = stack.Pop();

                if (current == null)
                    continue;
                else
                    printAll.Append($"{current?.Value.ToString()} ");

                if (current.Value == _search)
                {
                    Console.WriteLine($"Список обхода узлов в глубину : {printAll.ToString()}");
                    return current;
                }

                foreach (var x in current.Edges)
                {
                    if (!visited[x.Node.Value])
                    {
                        stack.Push(x.Node);
                        visited[x.Node.Value] = true;
                    }
                }

                Node[] ar = stack.ToArray();

                stepValue.Clear();
                foreach (var x in ar)
                {
                    if (x != null)
                        stepValue.Append($"{x?.Value.ToString()} ");
                }

                if (stepValue.Length > 0)
                    Console.WriteLine($"{++step}: {stepValue.ToString()}");
            }

            Console.WriteLine($"Список обхода узлов в глубину : {printAll.ToString()}");

            return null;
        }

    }

   
}
