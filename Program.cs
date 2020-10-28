using System;
using System.Text;
using System.Collections.Generic;

namespace Matrix_Calculator
{
    class Program
    {

        static bool TraceM()
        {
            return false;
        }
        static List<string> MainMenu()
        {
            List<string> MainMenuInfo = new List<string>();

            MainMenuInfo.Add("Find trace matrix.");
            MainMenuInfo.Add("Find transpose matrix.");
            MainMenuInfo.Add("Find sum of two matrices.");
            MainMenuInfo.Add("Find difference of two matrices.");
            MainMenuInfo.Add("Find product of the two matrices.");
            MainMenuInfo.Add("Find product of the matrix on number. ");
            MainMenuInfo.Add("Find determinant of the matrix.");
            return MainMenuInfo;
        }
        static List<string> MethodMenu()
        {
            List<string> MethodMenuInfo = new List<string>();

            MethodMenuInfo.Add("Use text file");
            MethodMenuInfo.Add("Use console input");
            MethodMenuInfo.Add("Randomize");
            return MethodMenuInfo;
        }
        static void InputPath()
        {

        }
        static void InputMatrix()
        {

        }
        static bool CheckMatrix()
        {
            return false;
        }
        static short PositionClear(short Position)
        {
            if (Position < 0)
                return 0;
            if (Position > 3)
                return 0;
            return Position;
        }


        public static void PrintOutUpAndDown(List<string> InputList, int ChoosenOne)
        {
            // The loop that goes through all of the menu items.
            for (int i = 0; i < InputList.Count; i++)
            {
                // Print all output;
                if (i == ChoosenOne)
                {
                    // HighLight the choosen one.
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine($">>{InputList[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {InputList[i]}");
                }
            }
        }
        static void Main(string[] args)
        {
            int ChoosenOne = 0;
            short MenuPosition = 0;
            int ChoosenOperation = 0;
            string MenuName = "";
            List<string> MenuInfo = new List<string>();



            /* 1. нахождение следа матрицы;  KV
            2. транспонирование матрицы; every
            3. сумма двух матриц; Same
            4. разность двух матриц; Same
            5. произведение двух матриц; n*k k*p
            6. умножение матрицы на число; every
            7. нахождение определителя матрицы.n*n*/

            do
            {
                MenuPosition = PositionClear(MenuPosition);
                Console.Clear();
                if (MenuPosition == 0)
                {
                    Console.WriteLine("Main Menu");

                    MenuInfo = MainMenu();

                }
                if (MenuPosition == 1)
                {
                    Console.WriteLine(MenuName);
                    MenuInfo = MethodMenu();
                }

                PrintOutUpAndDown(MenuInfo, ChoosenOne);
                ConsoleKeyInfo key = Console.ReadKey(true);

                // Simple switch, if uparrow then decrease, downarrow then increase.
                switch (key.Key)
                {
                    case (ConsoleKey.UpArrow):
                        // Move cursor up.
                        if (ChoosenOne == 0)
                            ChoosenOne = MenuInfo.Count - 1;
                        else
                            ChoosenOne--;
                        break;
                    case (ConsoleKey.DownArrow):
                        // Move cursor down.
                        if (ChoosenOne == MenuInfo.Count - 1)
                            ChoosenOne = 0;
                        else
                            ChoosenOne++;
                        break;
                    case (ConsoleKey.RightArrow):
                        MenuPosition += 1;
                        MenuName = MenuInfo[ChoosenOne];
                        ChoosenOperation = ChoosenOne;
                        ChoosenOne = 0;
                        break;
                    case (ConsoleKey.LeftArrow):
                        MenuPosition -= 1;
                        ChoosenOne = 0;
                        break;
                }
            } while (true);
        }
    }
}
