using System;
using System.IO;
using System.Collections.Generic;

namespace Matrix_Calculator
{
    class Program
    {

        static double ProductToMake = 0;
        static int ChoosenOne = 0;
        static short MenuPosition = 0;
        static int ChoosenOperation = 0;
        static double Det = 0;

        static int ChoosenMethodInput = 0;
        static string MenuName = "";
        static List<string> MenuInfo = new List<string>();
        static double[,] MatrixA = new double[1, 1];
        static double[,] MatrixB = new double[1, 1];
        static string FilePath = "";
        static bool Error = false;

        static double TraceM()
        {
            double Output = 0;
            for (int i = 0; i < MatrixA.GetLength(0); i++)
            {
                Output += MatrixA[i, i];
            }
            return Output;
        }
        static double[,] TranposeM()
        {
            double[,] Output = new double[MatrixA.GetLength(1), MatrixA.GetLength(0)];
            for (int i = 0; i < MatrixA.GetLength(0); i++)
            {
                for (int j = 0; j < MatrixA.GetLength(1); j++)
                {
                    Output[j, i] = MatrixA[i, j];
                }
            }
            return Output;

        }
        static double[,] Sum2M()
        {
            double[,] Output = new double[MatrixA.GetLength(0), MatrixA.GetLength(1)];
            for (int i = 0; i < MatrixA.GetLength(0); i++)
            {
                for (int j = 0; j < MatrixA.GetLength(1); j++)
                {
                    Output[i, j] = MatrixA[i, j] + MatrixB[i, j];
                }
            }
            return Output;
        }
        static double[,] Dif2M()
        {
            double[,] Output = new double[MatrixA.GetLength(0), MatrixA.GetLength(1)];
            for (int i = 0; i < MatrixA.GetLength(0); i++)
            {
                for (int j = 0; j < MatrixA.GetLength(1); j++)
                {
                    Output[i, j] = MatrixA[i, j] - MatrixB[i, j];
                }
            }
            return Output;
        }
        static double[,] Pro2M()
        {
            double[,] Output = new double[MatrixA.GetLength(0), MatrixB.GetLength(1)];
            for (int i = 0; i < MatrixA.GetLength(0); i++)
            {
                for (int j = 0; j < MatrixB.GetLength(1); j++)
                {
                    double SumOfIJ = 0;
                    for (int k = 0; k < MatrixA.GetLength(1); k++)
                    {
                        SumOfIJ += MatrixA[i, k] * MatrixB[k, j];
                    }
                    Output[i, j] = SumOfIJ;
                }
            }
            return Output;

        }
        static double[,] Pro1N()
        {
            double[,] Output = new double[MatrixA.GetLength(0), MatrixA.GetLength(1)];
            for (int i = 0; i < MatrixA.GetLength(0); i++)
            {
                for (int j = 0; j < MatrixA.GetLength(1); j++)
                {
                    Output[i, j] = MatrixA[i, j] * ProductToMake;
                }
            }
            return Output;
        }
        static int InvertN(int[] b)
        {
            int Invert = 0;
            for (int i = 0; i < b.Length - 1; i++)
            {
                for (int j = i + 1; j < b.Length; j++)
                {
                    if (b[i] < b[j])
                        Invert++;
                }
            }
            return Invert;
        }
        static void Permutation(int[] b, int size, int n)
        {
            // if size becomes 1 then prints the obtained
            // permutation
            if (size == 1)
            {
                double ProEachPerm = 1;
                for (int i = 0; i < MatrixA.GetLength(0); i++)
                {
                    ProEachPerm *= MatrixA[i, b[i] - 1];
                }
                Det -= Math.Pow(-1, InvertN(b)) * ProEachPerm;
            }

            for (int i = 0; i < size; i++)
            {
                Permutation(b, size - 1, n);

                // if size is odd, swap 0th i.e (first) and
                // (size-1)th i.e (last) element
                if (size % 2 == 1)
                {
                    int temp = b[0];
                    b[0] = b[size - 1];
                    b[size - 1] = temp;
                }

                // If size is even, swap ith and
                // (size-1)th i.e (last) element
                else
                {
                    int temp = b[i];
                    b[i] = b[size - 1];
                    b[size - 1] = temp;
                }
            }
        }

        static void CalculationM()
        {
            if (!Error)
                switch (ChoosenOperation)
                {
                    case (0):
                        Console.WriteLine($"Trace:{TraceM()}");
                        break;
                    case (1):
                        Console.WriteLine("Tranpose matrix:");
                        PrintMatrix(TranposeM(), 3);
                        break;
                    case (2):
                        Console.WriteLine("Sum of two matrices:");
                        PrintMatrix(Sum2M(), 3);
                        break;
                    case (3):
                        Console.WriteLine("Dif of two matrices:");
                        PrintMatrix(Dif2M(), 3);
                        break;
                    case (4):
                        Console.WriteLine("Product of two matrices:");
                        PrintMatrix(Pro2M(), 3);
                        break;
                    case (5):
                        Console.WriteLine($"This is the number which is gonna multiply on the matrix:{ProductToMake}");
                        Console.WriteLine("Product of matrices on number:");
                        PrintMatrix(Pro1N(), 3);
                        break;
                    case (6):
                        Det = 0;
                        int[] b1 = new int[MatrixA.GetLength(0)];
                        for (int i = 0; i < MatrixA.GetLength(0); i++)
                        {
                            b1[i] = i + 1;
                        }
                        Permutation(b1, b1.Length, b1.Length);
                        Console.WriteLine($"Determinant:{Det}");
                        break;
                }
            else
            {
                Console.WriteLine("Error!");
                MenuPosition = 0;
            }
        }


        public static string[] AddElementToArray(string[] array, string Addthing)
        {
            /// Add element of array from the certain index.
            /// Resources https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.removeat?view=netcore-3.1;
            List<string> lst = new List<string>(array);
            lst.Add(Addthing);
            return lst.ToArray();
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
            try
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
                if (!CheckMatrix())
                    throw new Exception();
            }
            catch
            {
                Console.WriteLine("Worng input, Plz try again! Press Enter to continue...");
                Error = true;
                MenuPosition = 0;
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
                Console.WriteLine("Incorrect Input path! Press Enter to continue...");
                Error = true;
                MenuPosition = 0;
            }

            MenuInfo = new List<string>();
        }
        static void InputMatrix()
        {
            Console.WriteLine("Plz, input correct matrix(ces), type 'exit' to exit the input mode");
            string[] ConsoleInput = new string[1];
            string InputLine = "";
            ConsoleInput[0] = Console.ReadLine();
            do
            {
                InputLine = Console.ReadLine();
                if (InputLine == "exit")
                    break;
                ConsoleInput = AddElementToArray(ConsoleInput, InputLine);
            } while (true);

            ReadMatrix(ConsoleInput);
            MenuInfo = new List<string>();
        }
        static void ReadOneMatrix(string[] FileInput)
        {
            int n = 0;
            int m = 0;


            if (FileInput[0].Contains('*') & FileInput[0].Split('*').Length == 2)
            {
                string[] SizeOfMatrix = FileInput[0].Split('*');
                if (!(int.TryParse(SizeOfMatrix[0], out n) & int.TryParse(SizeOfMatrix[1], out m) & FileInput.Length == n + 1))
                    throw new Exception();
            }
            else
                throw new Exception();

            MatrixA = new double[n, m];
            for (int i = 1; i < n + 1; i++)
            {
                string[] FileLine = FileInput[i].Split(' ');

                if (FileLine.Length != m)
                    throw new Exception();

                for (int j = 0; j < FileLine.Length; j++)
                {
                    MatrixA[i - 1, j] = double.Parse(FileLine[j]);
                }
            }

        }
        static void ReadTwoMatrix(string[] FileInput)
        {
            int n1 = 0;
            int n2 = 0;
            int m1 = 0;
            int m2 = 0;
            if (FileInput[0].Contains('*') & FileInput[0].Split('*').Length == 3)
            {
                string[] SizeOfMatrix = FileInput[0].Split(' ');
                string st1 = SizeOfMatrix[0].Split('*')[0];
                string st2 = SizeOfMatrix[0].Split('*')[1];
                string st3 = SizeOfMatrix[1].Split('*')[0];
                string st4 = SizeOfMatrix[1].Split('*')[1];
                if (!(int.TryParse(st1, out n1) & int.TryParse(st2, out m1) & int.TryParse(st3, out n2) & int.TryParse(st4, out m2)))
                    throw new Exception();
            }
            else
                throw new Exception();
            MatrixA = new double[n1, m1];
            MatrixB = new double[n2, m2];
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
            if (DivPos != FileInput.Length - 1 - n2)
                throw new Exception();
            for (int i = 1; i < DivPos; i++)
            {
                string[] FileLine = FileInput[i].Split(' ');
                if (FileLine.Length != m1)
                    throw new Exception();
                for (int j = 0; j < FileLine.Length; j++)
                {
                    MatrixA[i - 1, j] = double.Parse(FileLine[j]);
                }
            }

            for (int i = DivPos + 1; i < FileInput.Length; i++)
            {
                string[] FileLine = FileInput[i].Split(' ');
                if (FileLine.Length != m2)
                    throw new Exception();
                for (int j = 0; j < FileLine.Length; j++)
                {
                    MatrixB[i - 1 - DivPos, j] = double.Parse(FileLine[j]);
                }
            }
        }

        static void ReadOneAndPrMatrix(string[] FileInput)
        {
            int n = 0;
            int m = 0;

            if (FileInput[0].Contains('*') & FileInput[0].Split('*').Length == 2)
            {
                string[] SizeOfMatrix = FileInput[0].Split('*');
                if (!(int.TryParse(SizeOfMatrix[0], out n) & int.TryParse(SizeOfMatrix[1], out m) & FileInput.Length == n + 2))
                    throw new Exception();

            }
            else
                throw new Exception();
            ProductToMake = double.Parse(FileInput[1]);
            MatrixA = new double[n, m];
            for (int i = 2; i < n + 2; i++)
            {
                string[] FileLine = FileInput[i].Split(' ');
                if (FileLine.Length != m)
                    throw new Exception();
                for (int j = 0; j < FileLine.Length; j++)
                {
                    MatrixA[i - 2, j] = double.Parse(FileLine[j]);
                }
            }


        }
        static void ReadMatrix(string[] Input)
        {
            try
            {
                if (ChoosenOperation == 1 || ChoosenOperation == 0 || ChoosenOperation == 6)
                {
                    Console.WriteLine("1m");
                    ReadOneMatrix(Input);
                    PrintMatrix(MatrixA, 1);
                    return;
                }
                else if (ChoosenOperation == 2 || ChoosenOperation == 3 || ChoosenOperation == 4)
                {
                    Console.WriteLine("2m");
                    ReadTwoMatrix(Input);
                    PrintMatrix(MatrixA, 1);
                    Console.WriteLine();
                    PrintMatrix(MatrixB, 2);
                    return;
                }
                else if (ChoosenOperation == 5)
                {
                    Console.WriteLine("1m+1n");
                    ReadOneAndPrMatrix(Input);
                    PrintMatrix(MatrixA, 1);
                    return;
                }
            }
            catch
            {
                Console.WriteLine("Incorrect input! (Error in ReadMode) Press Enter to continue...");
                Error = true;
                MenuPosition = 0;
            }
        }
        static void RandomMatrix()
        {
            string[] RandomInput = new string[1];
            try
            {
                Random rnd = new Random();
                Console.WriteLine("Plz, input range of random number in matrix and number(n-m)");
                int[] RandomRange = new int[2];
                string RangeInput = Console.ReadLine();
                RandomRange[0] = int.Parse(RangeInput.Split('-')[0]);
                RandomRange[1] = int.Parse(RangeInput.Split('-')[1]);
                switch (ChoosenOperation)
                {
                    case (0):
                        Console.WriteLine("Plz, input correct size of matrix (n*n)");
                        RandomInput[0] = Console.ReadLine();
                        goto case (100);
                    case (1):
                        Console.WriteLine("Plz, input correct size of matrix (n*m)");
                        RandomInput[0] = Console.ReadLine();
                        goto case (100);
                    case (2):
                        Console.WriteLine("Plz, input correct size of matrices (n*m)");
                        RandomInput[0] = Console.ReadLine();
                        goto case (200);
                    case (3):
                        Console.WriteLine("Plz, input correct size of matrices (n*m)");
                        RandomInput[0] = Console.ReadLine();
                        goto case (200);
                    case (4):
                        Console.WriteLine("Plz, input correct size of matrix (n*k)");
                        RandomInput[0] = Console.ReadLine();
                        Console.WriteLine("Plz, input correct size of matrix (k*m)");
                        RandomInput[0] = RandomInput[0] + " " + Console.ReadLine();
                        goto case (200);
                    case (5):
                        Console.WriteLine("Plz, input correct size of matrix (n*m)");
                        RandomInput[0] = Console.ReadLine();
                        RandomInput = AddElementToArray(RandomInput, rnd.Next(RandomRange[0], RandomRange[1]).ToString());
                        goto case (100);
                    case (6):
                        Console.WriteLine("Plz, input correct size of matrix (n*n)");
                        RandomInput[0] = Console.ReadLine();
                        goto case (100);
                    case (100):

                        int n = int.Parse(RandomInput[0].Split('*')[0]);
                        int m = int.Parse(RandomInput[0].Split('*')[1]);

                        for (int i = 0; i < n; i++)
                        {
                            string Line = "";
                            for (int j = 0; j < m; j++)
                            {
                                if (j != m - 1)
                                    Line += rnd.Next(RandomRange[0], RandomRange[1]) + " ";
                                else
                                    Line += rnd.Next(RandomRange[0], RandomRange[1]);


                            }
                            RandomInput = AddElementToArray(RandomInput, Line);
                        }
                        break;
                    case (200):
                        if (ChoosenOperation != 4)
                            RandomInput[0] = RandomInput[0] + " " + RandomInput[0];
                        int n1 = int.Parse(RandomInput[0].Split(' ')[0].Split('*')[0]);
                        int m1 = int.Parse(RandomInput[0].Split(' ')[0].Split('*')[1]);
                        int n2 = int.Parse(RandomInput[0].Split(' ')[1].Split('*')[0]);
                        int m2 = int.Parse(RandomInput[0].Split(' ')[1].Split('*')[1]);
                        for (int i = 0; i < n1; i++)
                        {
                            string Line = "";
                            for (int j = 0; j < m1; j++)
                            {
                                if (j != m1 - 1)
                                    Line += rnd.Next(RandomRange[0], RandomRange[1]) + " ";
                                else
                                    Line += rnd.Next(RandomRange[0], RandomRange[1]);
                            }
                            RandomInput = AddElementToArray(RandomInput, Line);
                        }
                        RandomInput = AddElementToArray(RandomInput, "-");
                        for (int i = 0; i < n2; i++)
                        {
                            string Line = "";
                            for (int j = 0; j < m2; j++)
                            {
                                if (j != m2 - 1)
                                    Line += rnd.Next(RandomRange[0], RandomRange[1]) + " ";
                                else
                                    Line += rnd.Next(RandomRange[0], RandomRange[1]);
                            }
                            RandomInput = AddElementToArray(RandomInput, Line);
                        }

                        break;
                    default:
                        throw new Exception();


                }
                ReadMatrix(RandomInput);


            }
            catch
            {
                Console.WriteLine("Incorrect input! (Error in RandMode) Press Enter to continue...");
                Error = true;
                MenuPosition = 0;
            }

            MenuInfo = new List<string>();
        }
        static bool CheckMatrix()
        {
            try
            {
                switch (ChoosenOperation)
                {
                    case (0):
                        if (MatrixA.GetLength(0) != MatrixA.GetLength(1))
                            throw new Exception();
                        break;
                    case (1):
                        break;
                    case (2):
                        if (MatrixA.GetLength(0) != MatrixB.GetLength(0) || MatrixA.GetLength(1) != MatrixB.GetLength(1))
                            throw new Exception();
                        break;
                    case (3):
                        if (MatrixA.GetLength(0) != MatrixB.GetLength(0) || MatrixA.GetLength(1) != MatrixB.GetLength(1))
                            throw new Exception();
                        break;
                    case (4):
                        if (MatrixA.GetLength(1) != MatrixB.GetLength(0))
                            throw new Exception();
                        break;
                    case (5):
                        break;
                    case (6):
                        if (MatrixA.GetLength(0) != MatrixA.GetLength(1))
                            throw new Exception();
                        break;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        static short PositionClear(short Position)
        {
            if (Position < 0)
                return 0;
            if (Position >= 2)
                return 2;
            return Position;
        }
        static void PrintMatrix(double[,] A, int Id)
        {
            if (Id == 1)
                Console.WriteLine("MatrixA:");
            else if (Id == 2)
                Console.WriteLine("MatrixB:");
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
            // try
            // {
            do
            {
                Error = false;
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
                    CalculationM();
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
            // }
            // catch
            // {
            // Console.WriteLine("Something Went Wrong, Restart the program plz...");
            //  }

        }
    }
}
