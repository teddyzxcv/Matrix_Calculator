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

        static int ChoosenMethodInput = 0;
        static string MenuName = "";
        static List<string> MenuInfo = new List<string>();
        static int[,] MatrixA = new int[1, 1];
        static int[,] MatrixB = new int[1, 1];
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
        static void InputMethod(int ChoosenMethodInput)
        {

            switch (ChoosenMethodInput)
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

                if (ChoosenOperation == 1 || ChoosenOperation == 0 || ChoosenOperation == 6)
                {

                    int n = 0;
                    int m = 0;
                    string[] FileInput = File.ReadAllLines(FilePath);

                    if (FileInput[0].Contains('*') & FileInput[0].Split('*').Length == 2)
                    {
                        string[] SizeOfMatrix = FileInput[0].Split('*');
                        if (!(int.TryParse(SizeOfMatrix[0], out n) & int.TryParse(SizeOfMatrix[1], out m) & FileInput.Length == n + 1))
                            throw new Exception();
                    }
                    else
                        throw new Exception();
                    MatrixA = new int[n, m];
                    for (int i = 1; i < n + 1; i++)
                    {
                        string[] FileLine = FileInput[i].Split(' ');
                        if (FileLine.Length != m)
                            throw new Exception();
                        for (int j = 0; j < FileLine.Length; j++)
                        {
                            MatrixA[i - 1, j] = int.Parse(FileLine[j]);
                        }
                    }
                    return;
                }
                else if (ChoosenOperation == 2 || ChoosenOperation == 3 || ChoosenOperation == 4)
                {
                    Console.WriteLine(28348324);


                    int n1 = 0;
                    int n2 = 0;
                    int m1 = 0;
                    int m2 = 0;
                    string[] FileInput = File.ReadAllLines(FilePath);

                    if (FileInput[0].Contains('*') & FileInput[0].Split('*').Length == 3)
                    {
                        string[] SizeOfMatrix = FileInput[0].Split(' ');
                        string st1 = SizeOfMatrix[0].Split('*')[0];
                        string st2 = SizeOfMatrix[0].Split('*')[1];
                        string st3 = SizeOfMatrix[1].Split('*')[0];
                        string st4 = SizeOfMatrix[1].Split('*')[1];
                        if (!(int.TryParse(st1, out n1) & int.TryParse(st2, out m1) & int.TryParse(st1, out n2) & int.TryParse(st2, out m2)))
                            throw new Exception();
                    }
                    else
                        throw new Exception();
                    MatrixA = new int[n1, m1];
                    MatrixB = new int[n1, m1];
                    int DivPos = 0;
                    for (int i = 0; i < FileInput.Length; i++)
                    {
                        if (FileInput[i] == "-")
                        {
                            DivPos = i;
                            break;
                        }
                        else if (i == FileInput.Length - 1)
                        {
                            throw new Exception();
                        }
                    }
                    if (DivPos != FileInput.Length - 2 - n1)
                        throw new Exception();
                    for (int i = 1; i < DivPos; i++)
                    {
                        string[] FileLine = FileInput[i].Split(' ');
                        if (FileLine.Length != m1)
                            throw new Exception();
                        for (int j = 0; j < FileLine.Length; j++)
                        {
                            MatrixA[i - 1, j] = int.Parse(FileLine[j]);
                        }
                    }
                    for (int i = DivPos + 1; i < FileInput.Length; i++)
                    {
                        string[] FileLine = FileInput[i].Split(' ');
                        if (FileLine.Length != m2)
                            throw new Exception();
                        for (int j = 0; j < FileLine.Length; j++)
                        {
                            MatrixB[i - 1, j] = int.Parse(FileLine[j]);
                        }
                    }



                    PrintMatrix(MatrixA);
                    PrintMatrix(MatrixB);
                    return;
                }
                else if (ChoosenOperation == 5)
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
        static void PrintMatrix(int[,] A)
        {
            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    Console.Write(A[i, j] + " ");
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
                    InputMethod(ChoosenMethodInput);
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
                        if (MenuPosition <= 2)
                        {
                            MenuPosition += 1;


                            ChoosenMethodInput = ChoosenOne;
                            if (MenuPosition == 1)
                            {
                                MenuName = MenuInfo[ChoosenOne];
                                ChoosenOperation = ChoosenOne;
                            }
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
