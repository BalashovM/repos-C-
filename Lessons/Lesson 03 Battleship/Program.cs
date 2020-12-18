using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_03_Battleship
{
    class Program
    {
        enum Ships 
        { 
            [Description("4x-палубный")]
            FourClells = 1,
            [Description("3x-палубный")]
            ThreeClells,
            [Description("2x-палубный")]
            TwoClells,
            [Description("1-палубный")]
            OneClell
        };

       static void Main(string[] args)
        {
            const int field = 10;
            const string letters = "АБВГДЕЖЗИКЛМНОПРСТУФХЦЧШЭЮЯ"; //27 русских символов
            
            string[,] battlefield = new string[field,field];
                        
            Console.WriteLine("Вас приветсвует программа вывода вариантов расстановки для игры в морской бой.");
            Console.WriteLine($"Размер поля {field}*{field}.");
            Console.WriteLine("При расстановке нельзя касаться краёв поля и других кораблей даже по диагонали.");

            string description = "";

            foreach (int i in Enum.GetValues(typeof(Ships))) 
                description = description + GetDescription((Ships)i) + " - " + i.ToString() + " шт \n";

            Console.WriteLine($"Корабли:\n{description}");
            
            //Вывод поля
            Console.WriteLine("Игровое поле:");
            for (int i = 0; i < field+1; i++)
            {
                for (int j = 0; j < field+1; j++)
                {
                    if(i==0 && j == 0)
                        Console.Write("  ");
                    else if (i == 0 && j > 0)
                        Console.Write($" {letters.Substring(j-1, 1)}");
                    else if (j == 0 && i > 0)
                        Console.Write(i.ToString().PadLeft(2, ' '));
                    else
                        Console.Write(" O");
                }
                Console.Write("\n");
            }
            Console.Write("\n");
            Console.WriteLine("Расставить в ручную или сгенерировать расстановку?");

            bool isAnswer = false;
            while (!isAnswer)
            {
                Console.Write("1 - вручную, 2 - автоматически, ваш выбор : ");
                string value = Console.ReadLine();

                if (value == "1" || value == "2")
                {
                    if (value == "1")
                    {
                        int x1 = 0, x2 = 0, y1 = 0 , y2 = 0;
                        
                        Console.WriteLine("Вы выбрали ручную расстановку кораблей.");
                        foreach (int shipType in Enum.GetValues(typeof(Ships)))
                        {
                            for (int shipCount = 0; shipCount < shipType; shipCount++)
                            {
                                bool isShipSetUp = false;
                                while (!isShipSetUp)
                                {
                                    bool isReset = false;

                                    Console.WriteLine($"Выберите начальную точку для {GetDescription((Ships)shipType)}");

                                    bool isCorrectPoint = false;

                                    while(!isCorrectPoint)
                                    {
                                        bool xSelect = false;

                                        while (!xSelect)
                                        {
                                            Console.Write("Введите столбец : ");
                                            value = Console.ReadLine().ToUpper();
                                            if (letters.Substring(1, field - 1).Contains(value))
                                            {
                                                x1 = letters.IndexOf(value) + 1;
                                                xSelect = true;
                                            }
                                            else
                                                Console.WriteLine("Не правильно выбран столбец, выберете столбец из списка, кроме крайних.");
                                        }

                                        bool ySelect = false;

                                        while (!ySelect)
                                        {
                                            Console.Write("Введите номер строки : ");
                                            value = Console.ReadLine();
                                            bool isNum = int.TryParse(value, out y1);
                                            if (isNum && y1 > 1 && y1 < field)
                                            {
                                                ySelect = true;
                                            }
                                            else
                                                Console.WriteLine("Не правильно выбрана строка, выберете строку из списка, кроме крайних.");
                                        }

                                        if (xSelect && ySelect)
                                        {
                                            bool isEmptyField = true;
                                            for (int i = x1 - 2; i <= x1; i++)
                                                for (int j = y1 - 2; j <= y1; j++)
                                                    if (battlefield[j, i] == "X")
                                                        isEmptyField = false;

                                            if (isEmptyField)
                                                isCorrectPoint = true;
                                            else
                                                Console.WriteLine("Не верно выбрана точка попробуйте ещё раз.");
                                        }
                                    }

                                    bool directionSelect = false;

                                    if (shipType == 4)
                                    {
                                        directionSelect = true;

                                        battlefield[y1 - 1, x1 - 1] = "X";
                                    }
                                    else
                                        directionSelect = false;

                                    while (!directionSelect)
                                    {
                                        Console.Write("Выберите направление, 1 - влево, 2 - вверх, 3 - вправо, 4 - вниз, 5 - сбросить :");
                                        value = Console.ReadLine();

                                        bool isNum = int.TryParse(value, out int d1);

                                        if (isNum && d1 == 5)
                                        {
                                            isReset = true;
                                            break;
                                        }

                                        int shipLen;

                                        if (isNum && d1 > 0 && d1 < 5)
                                        {
                                            switch ((Ships)shipType)
                                            {
                                                case Ships.FourClells: shipLen = 4; break;
                                                case Ships.ThreeClells: shipLen = 3; break;
                                                case Ships.TwoClells: shipLen = 2; break;
                                                case Ships.OneClell: shipLen = 1; break;
                                                default: shipLen = 0; break;
                                            }

                                            //Расчет координат второй точки корабля
                                            switch (d1)
                                            {
                                                case 1: 
                                                    x2 = x1 - (shipLen - 1); 
                                                    y2 = y1; 
                                                    break;
                                                case 2: 
                                                    x2 = x1; 
                                                    y2 = y1 - (shipLen - 1); 
                                                    break;
                                                case 3: 
                                                    x2 = x1 + (shipLen - 1); 
                                                    y2 = y1; 
                                                    break;
                                                case 4: 
                                                    x2 = x1; 
                                                    y2 = y1 + (shipLen - 1); 
                                                    break;
                                            }

                                            if ((x2 > 1 && x2 < field) && (y2 > 1 && y2 < field))
                                            {
                                                bool isCross = false;
                                                //Проверка на пересечение
                                                switch (d1)
                                                {
                                                    case 1:
                                                        for (int i = x2 - 1; i < x1; i++)
                                                            if (battlefield[y2 - 1, i] == "X")
                                                                isCross = true;
                                                        break;
                                                    case 2:
                                                        for (int i = y2 - 1; i < y1; i++)
                                                            if (battlefield[i, x2 - 1] == "X")
                                                                isCross = true;
                                                        break;
                                                    case 3:
                                                        for (int i = x1 - 1; i < x2; i++)
                                                            if (battlefield[y2 - 1, i] == "X")
                                                                isCross = true;
                                                        break;
                                                    case 4:
                                                        for (int i = y1 - 1; i < y2; i++)
                                                            if (battlefield[i, x2 - 1] == "X")
                                                                isCross = true;
                                                        break;
                                                }
                                                if (!isCross)
                                                {
                                                    bool isTouch = false;
                                                    //Проверка на касание
                                                    switch (d1)
                                                    {
                                                        case 1:
                                                            for (int i = x2 - 2; i <= x1; i++)
                                                                for (int j = y2 - 1; j <= y2; j++)
                                                                    if (battlefield[j, i] == "X")
                                                                        isTouch = true;

                                                            if (!isTouch)
                                                                for (int i = x2 - 1; i < x1; i++)
                                                                    battlefield[y2 - 1, i] = "X";
                                                            break;
                                                        case 2:
                                                            for (int i = x1 - 2; i <= x2; i++)
                                                                for (int j = y2 - 2; j <= y1; j++)
                                                                    if (battlefield[j, i] == "X")
                                                                        isTouch = true;

                                                            if (!isTouch)
                                                                for (int i = y2 - 1; i < y1; i++)
                                                                    battlefield[i, x2 - 1] = "X";
                                                            break;
                                                        case 3:
                                                            for (int i = x1 - 2; i <= x2; i++)
                                                                for (int j = y1 - 2; j <= y2; j++)
                                                                    if (battlefield[j, i] == "X")
                                                                        isTouch = true;

                                                            if (!isTouch)
                                                                for (int i = x1 - 1; i < x2; i++)
                                                                    battlefield[y2 - 1, i] = "X";
                                                            break;
                                                        case 4:
                                                            for (int i = x1 - 2; i <= x2; i++)
                                                                for (int j = y1 - 2; j <= y2; j++)
                                                                    if (battlefield[j, i] == "X")
                                                                        isTouch = true;

                                                            if (!isTouch)
                                                                for (int i = y1 - 1; i < y2; i++)
                                                                    battlefield[i, x2 - 1] = "X";
                                                            break;
                                                    }

                                                    if (!isTouch)
                                                        directionSelect = true;
                                                    else
                                                        Console.WriteLine("Неправильно выбрано направление, корабль косается другого корабля");
                                                }
                                                else
                                                    Console.WriteLine("Неправильно выбрано направление, корабль пересекается с другим кораблем");
                                            }
                                            else
                                                Console.WriteLine("Неправильно выбрано направление, корабль выходит за рамки поля или прикасается к краям поля.");
                                        }
                                        else
                                            Console.WriteLine("Неправильно выбрано направление, выберете из списка:");
                                    }

                                    if (!isReset)
                                        isShipSetUp = true;
                                }

                                //надо нарисовать корабль
                                Console.Clear();
                                Console.WriteLine("Вас приветсвует программа вывода вариантов расстановки для игры в морской бой.");
                                Console.WriteLine($"Размер поля {field}*{field}.");
                                Console.WriteLine("При расстановке нельзя касаться краёв поля и других кораблей даже по диагонали.");
                                Console.WriteLine($"Корабли:\n{description}");
                                
                                Console.WriteLine("Игровое поле:");
                                for (int i = 0; i < field + 1; i++)
                                {
                                    for (int j = 0; j < field + 1; j++)
                                    {
                                        if (i == 0 && j == 0)
                                            Console.Write("  ");
                                        else if (i == 0 && j > 0)
                                            Console.Write($" {letters.Substring(j - 1, 1)}");
                                        else if (j == 0 && i > 0)
                                            Console.Write(i.ToString().PadLeft(2, ' '));
                                        else
                                        {
                                            string curPoint = battlefield[i - 1, j - 1] == "X" ? " X" : " O";
                                            Console.Write("{0}", curPoint);
                                        }
                                    }
                                    Console.Write("\n");
                                }
                                Console.Write("\n");
                            }
                        }
                        isAnswer = true;
                    }
                    else 
                    {
                        Console.WriteLine("Вы выбрали автоматическую расстановку кораблей");
                        Console.WriteLine("Данный вариант ещё в разработке.");
                        isAnswer = false;
                    }
                }
            }
            Console.WriteLine("Расстановка кораблей на поле завершена.");
            Console.ReadKey();
        }

        public static string GetDescription(Enum enumElement)
        {
            Type type = enumElement.GetType();

            MemberInfo[] memInfo = type.GetMember(enumElement.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return enumElement.ToString();
        }
    }
}

