using System;

namespace Lesson_06_Graph_BFS_DFS
{
    class Matrix
    {
        public const int maxWeight = 99;
        public Matrix(int nodeQty)
        {
            Size = nodeQty;
            array = new int[nodeQty, nodeQty];
        }
        public int Size { get; set; }
        public bool isDirected { get; set; }
        public bool isWeighted { get; set; }
        public bool allowedLoops { get; set; }
        public bool revertEqually { get; set; }
        public int[,] array; //матрица смежности

        public void Clear()
        {
            array = null;
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
                                if (!revertEqually || (revertEqually && array[j, i] == 0))
                                    array[i, j] = rand.Next(0, maxWeight);
                                else
                                    array[i, j] = array[j, i];
                            }
                            else
                                array[i, j] = 0;
                        }
                        else
                        {
                            if (j < i)
                                j = i;

                            if ((i != j || (i == j && allowedLoops)) && rand.Next(0, 2) != 0)
                                array[i, j] = rand.Next(0, maxWeight);
                            else
                                array[i, j] = 0;

                            if (j != i)
                                array[j, i] = array[i, j];
                        }
                    }
                    else
                    {
                        if (isDirected)
                        {
                            if ((i != j || (i == j && allowedLoops)) && rand.Next(0, 2) != 0)
                                array[i, j] = 1;
                            else
                                array[i, j] = 0;
                        }
                        else
                        {
                            if (j < i)
                                j = i;

                            if ((i != j || (i == j && allowedLoops)) && rand.Next(0, 2) != 0)
                                array[i, j] = 1;
                            else
                                array[i, j] = 0;

                            if (j != i)
                                array[j, i] = array[i, j];

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
                        Console.Write("{0,4}", array[i - 1, j - 1]);
                    }
                }
                Console.WriteLine();
            }
        }
    }

}

