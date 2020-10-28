using System;
using System.Text;
using System.Collections.Generic;

namespace Matrix_Calculator
{
    class Program
    {

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
            List<string> MenuInfo = new List<string>();
            MenuInfo.Add("Find trace matrix.");
            MenuInfo.Add("Find transpose matrix.");
            MenuInfo.Add("Find sum of two matrices.");
            MenuInfo.Add("Find difference of two matrices.");
            MenuInfo.Add("Find product of the two matrices.");
            MenuInfo.Add("Find product of the matrix on number. ");
            MenuInfo.Add("Find determinant of the matrix.");

            /* 1. нахождение следа матрицы;  KV
            2. транспонирование матрицы; every
            3. сумма двух матриц; Same
            4. разность двух матриц; Same
            5. произведение двух матриц; n*k k*p
            6. умножение матрицы на число; every
            7. нахождение определителя матрицы.n*n*/

            do
            {
                Console.Clear();
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
                }
            } while (true);
        }
    }
}
