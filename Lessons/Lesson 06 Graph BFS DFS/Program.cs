using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_06_Graph_BFS_DFS
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isEnd = false;

            int minVertex = 5;
            int maxVertex = 20;
            Matrix matrix = null;

            while (!isEnd)
            {
                Console.Write($"Введите число вершин графа (от {minVertex} до {maxVertex}):");
                if (int.TryParse(Console.ReadLine(), out int graphSize) && 
                        graphSize >= minVertex && 
                            graphSize <= maxVertex)
                {
                    matrix?.Clear();

                    matrix = new Matrix(graphSize);

                    matrix.allowedLoops = YesNo("Вершины графа могут иметь петли?");
                    matrix.isDirected = YesNo("Граф должен быть ориентированным?");
                    matrix.isWeighted = YesNo("Граф должен быть взвешенным?");

                    if (matrix.isWeighted)
                        matrix.revertEqually = YesNo("Вес дуг между одними вершинами в разных направлениях должен быть одинаковым?");
                    else
                        matrix.revertEqually = false;

                    matrix.Generate();
                    Console.WriteLine("Сгенерированная матрица смежности для графа:");
                    matrix.Show();

                    Graph graph = new Graph(matrix);
                         

                    
                    isEnd = YesNo("Закончить?");
                }
                else
                    Console.WriteLine($"Введено неверное значение, необходимо ввести число от {minVertex} до {maxVertex}.");
                
            }
        }

        public static bool YesNo(string questionText)
        {
            Console.WriteLine($"{questionText}");
            Console.Write("Да - 1, Нет - 0 :");
            bool isAnswer = false;
            while (!isAnswer)
            {
                if(int.TryParse(Console.ReadLine(),out int value) && (value == 1 || value == 0))
                {
                    isAnswer = true;
                    if (value == 1)
                        return true;
                }
                else
                    Console.WriteLine($"Введено неверное значение, необходимо ввести Да - 1 или Нет - 0.");
            }

            return false;
        }
    }
}
