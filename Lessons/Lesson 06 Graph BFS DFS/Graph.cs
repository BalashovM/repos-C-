using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_06_Graph_BFS_DFS
{
    class Graph
    {
        public const int maxWeight = 99;
        public Graph(int nodeQty)
        {
            Size = nodeQty;
            matrix = new int[nodeQty, nodeQty];
        }
        public int Size { get; set; }
        public bool isDirected { get; set; }
        public bool isWeighted { get; set; }
        public bool allowedLoops { get; set; }
        public bool revertEqually { get; set; }
        private int[,] matrix; //матрица смежности

        public void Clear()
        {
            matrix = null;
            Size = 0;
            isDirected = false;
            isWeighted = false;
            allowedLoops = false;
            revertEqually = false;
        }

        public void Generate()
        {
            Random rand = new Random();

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (isWeighted)
                    {
                        if (isDirected)
                        {
                            if ((i != j || (i == j && allowedLoops)) && rand.Next(0, 2) != 0)
                            {
                                if (!revertEqually || (revertEqually && matrix[j, i] == 0))
                                    matrix[i, j] = rand.Next(0, maxWeight);
                                else
                                    matrix[i, j] = matrix[j, i];
                            }
                            else
                                matrix[i, j] = 0;
                        }
                        else
                        {
                            if (j < i)
                                j = i;

                            if ((i != j || (i == j && allowedLoops)) && rand.Next(0, 2) != 0)
                                matrix[i, j] = rand.Next(0, maxWeight);
                            else
                                matrix[i, j] = 0;

                            if (j != i)
                                matrix[j, i] = matrix[i, j];
                        }
                    }
                    else
                    {
                        if (isDirected)
                        {
                            if ((i != j || (i == j && allowedLoops)) && rand.Next(0, 2) != 0)
                                matrix[i, j] = 1;
                            else
                                matrix[i, j] = 0;
                        }
                        else
                        {
                            if (j < i)
                                j = i;

                            if ((i != j || (i == j && allowedLoops)) && rand.Next(0, 2) != 0)
                                matrix[i, j] = 1;
                            else
                                matrix[i, j] = 0;

                            if (j != i)
                                matrix[j, i] = matrix[i, j];

                        }
                    }

                }
            }
        }

        public void Show()
        {
            for (int i = 0; i <= Size; i++)
            {
                for (int j = 0; j <= Size; j++)
                {
                    if (i == 0 && j == 0)
                        Console.Write("    ");
                    else if (i == 0 && j != 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("{0,4}", j);
                    }
                    else if (i != 0 && j == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("{0,4}", i);
                    }
                    else
                    {
                        Console.ResetColor();
                        Console.Write("{0,4}", matrix[i - 1, j - 1]);
                    }
                }
                Console.WriteLine();
            }
        }
    }

}

