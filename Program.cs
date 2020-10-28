using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace Matrix_Calculator
{
    class Program
    {

        static int ChoosenOne = 0;
        static short MenuPosition = 0;
        static int ChoosenOperation = 0;
        static string MenuName = "";
        static List<string> MenuInfo = new List<string>();
        static int[][] MatrixA = new int[1][];
        static int[][] MatrixB = new int[1][];
        static string FilePath = "";

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
            MainMenuInfo.Add("Find product of the matrix on number.");
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
        static void InputMethod(int ChoosenOperation)
        {
            switch (ChoosenOperation)
            {
                case (0):
                    InputPath();
                    break;
                case (1):
                    InputMatrix();
                    break;
                case (2):
                    RandomMatrix();
                    break;
                default:
                    break;
            }
        }
        // Size of matrix;
        static void InputPath()
        {
            Console.WriteLine("Plz, input correct path to your file with matrix(ces)");
            FilePath = Console.ReadLine();
            try
            {
                if (File.Exists(FilePath))
                {
                    ReadMatrix(File.ReadAllLines(FilePath));
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                Console.WriteLine("Incorrect Input path!");
            }

            MenuInfo = new List<string>();
        }
        static void InputMatrix()
        {
            Console.WriteLine("Plz, input correct matrix(ces)");
            MenuInfo = new List<string>();
        }

        static void ReadMatrix(string[] Input)
        {
            try
            {
                int n = 0;
                int m = 0;
                if (ChoosenOne == 1 || ChoosenOne == 0 || ChoosenOne == 6)
                {
                    string[] FileInput = File.ReadAllLines(FilePath);

                    if (FileInput[0].Contains('*') & FileInput[0].Split('*').Length == 2)
                    {
                        string[] SizeOfMatrix = FileInput[0].Split('*');
                        if (!(int.TryParse(SizeOfMatrix[0], out n) & int.TryParse(SizeOfMatrix[1], out m) & FileInput.Length == n + 1))
                        {
                            throw new Exception();

                        }

                    }
                    else
                    {
                        throw new Exception();
                    }
                    MatrixA = new int[n][];
                    MatrixA[n] = new int[m];
                    for (int i = 1; i < n + 1; i++)
                    {
                        string[] FileLine = FileInput[i].Split(' ');
                        for (int j = 0; j < FileLine.Length; j++)
                        {
                            MatrixA[i - 1][j] = int.Parse(FileLine[j]);

                        }
                        if (MatrixA[i - 1].Length != m)
                        {
                            throw new Exception();
                        }


                    }
                    PrintMatrix(MatrixA);
                    return;
                }
                else if (ChoosenOne == 2 || ChoosenOne == 3 || ChoosenOne == 4)
                {

                }
                else if (ChoosenOne == 5)
                {

                }
            }
            catch
            {
                Console.WriteLine("Incorrect input in the file!");
            }


        }
        static void RandomMatrix()
        {
            Console.WriteLine("Plz, input correct size of matrix");
            MenuInfo = new List<string>();
        }
        static bool CheckMatrix()
        {
            return false;
        }
        static short PositionClear(short Position)
        {
            if (Position < 0)
                return 0;
            if (Position >= 2)
                return 2;
            return Position;
        }
        static void PrintMatrix(int[][] A)
        {
            for (int i = 0; i < A.Length; i++)
            {
                for (int j = 0; j < A[i].Length; j++)
                {
                    Console.Write(A[i][j] + " ");
                }
                Console.WriteLine();
            }
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
                if (MenuPosition == 2)
                {
                    InputMethod(ChoosenOperation);
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
                        if (MenuPosition < 2)
                        {
                            MenuPosition += 1;
                            MenuName = MenuInfo[ChoosenOne];
                            ChoosenOperation = ChoosenOne;
                            ChoosenOne = 0;
                        }
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
