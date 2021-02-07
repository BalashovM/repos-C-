using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }

   
}
