using System;
using System.Text;
using System.Threading;

namespace Lesson_07_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            int stepRange = 20; 
            Random rndSteps = new Random();
            Random rndCups = new Random();

            bool isEnd = false;
            while (!isEnd)
            {
                int steps = rndSteps.Next(10, stepRange);

                Console.WriteLine("Игра Угадате где шарик ");
                Console.WriteLine("Д - стакан, о - шарик ");

                int cups = 10;

                bool selectCup = false;
                while (!selectCup)
                {
                    Console.Write("Выберите количество стаканов от 3 до 6 : ");
                    if (int.TryParse(Console.ReadLine(), out cups))
                        if (cups >= 3 && cups <= 6)
                            selectCup = true;
                        else
                            Console.WriteLine("Не правильно указано количество стаканов");
                }
                Console.Write("\n");

                StringBuilder sbCups = new StringBuilder();
                StringBuilder sbNumbers = new StringBuilder();
                for (int i = 1; i <= cups; i++)
                {
                    sbCups.Append("Д ");
                    sbNumbers.Append($"{i} ");
                }
                string strCups = sbCups.ToString();
                string strNumbers = sbNumbers.ToString();

                Console.WriteLine(strNumbers);
                Console.WriteLine(strCups);

                int cupBall = rndCups.Next(1, cups);
                string strBall = "о";
                Console.WriteLine(strBall.PadLeft((cupBall * 2) - 1, ' '));

                Console.WriteLine("Для начала нажмите любую клавишу");
                Console.ReadKey();

                for (int i = 0; i < steps; i++)
                {
                    Console.Clear();
                    Console.WriteLine(strNumbers);
                    cupBall = rndCups.Next(1, cups);
                    Console.WriteLine("Д".PadLeft((cupBall * 2) - 1, ' '));

                    StringBuilder sbBallCups = new StringBuilder();
                    for (int j = 0; j < cups; j++)
                    {
                        if(j + 1 == cupBall)
                            sbBallCups.Append("о ");
                        else 
                            sbBallCups.Append("Д ");

                    }
                    Console.WriteLine(sbBallCups.ToString());
                    Thread.Sleep(100);
                }

                Console.Clear();
                Console.WriteLine(strNumbers);
                Console.WriteLine(strCups);

                Console.Write("Выберите под каким стаканом мячик : ");
                if (int.TryParse(Console.ReadLine(), out cups))
                {
                    Console.Clear();
                    Console.WriteLine(strNumbers);
                    Console.WriteLine(strCups);
                    Console.WriteLine("о".PadLeft((cupBall * 2) - 1, ' '));

                    if (cups == cupBall)
                        Console.WriteLine("Вы угадали :-)");
                    else

                        Console.WriteLine("К сожалению вы ошиблись :-(");
                }

                bool select = false;
                while (!select)
                {
                    Console.Write("Сыграем ещё раз? (1 - да, 2- нет ) : ");
                    if (int.TryParse(Console.ReadLine(), out int intSelect))
                        switch (intSelect)
                        {
                            case 1: 
                                select = true; 
                                Console.Clear(); 
                                break;
                            case 2: 
                                select = true; 
                                isEnd = true; 
                                Console.WriteLine("Спасибо за игру!"); 
                                Thread.Sleep(1000); 
                                break;
                            default: 
                                Console.WriteLine("Не правильно сделан выбор."); 
                                break;
                        }
                }
                
            }
        }
    }
}
