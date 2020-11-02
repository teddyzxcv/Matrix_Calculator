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
        /// <summary>
        /// Calculate the trace.
        /// </summary>
        /// <returns></returns>
        static double TraceM()
        {
            double Output = 0;
            for (int i = 0; i < MatrixA.GetLength(0); i++)
            {
                Output += MatrixA[i, i];
            }
            return Output;
        }
        /// <summary>
        /// Calculate the tranpose matrix.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Calculate the sum of two matrix
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Calculatethe different of two matrix.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Calculate the product of two matrix.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Calculate product of one matrix on number.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Find the invert number of permutation.
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Find all permutaion and all determinant.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="size"></param>
        /// <param name="n"></param>
        /// <param name="A"></param>
        static void PermutationAndDet(int[] b, int size, int n, double[,] A)
        {
            // if size becomes 1 then prints the obtained
            // permutation
            if (size == 1)
            {
                // Get determinanat 
                double ProEachPerm = 1;
                for (int i = 0; i < A.GetLength(0); i++)
                {
                    ProEachPerm *= A[i, b[i] - 1];
                }
                Det -= Math.Pow(-1, InvertN(b)) * ProEachPerm;
            }
            for (int i = 0; i < size; i++)
            {
                PermutationAndDet(b, size - 1, n, A);

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
        /// <summary>
        /// Find determinant.
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        static double Deter(double[,] A)
        {
            Det = 0;
            Console.WriteLine("Plz, wait...");
            int[] b1 = new int[A.GetLength(0)];
            for (int i = 0; i < A.GetLength(0); i++)
            {
                b1[i] = i + 1;
            }
            PermutationAndDet(b1, b1.Length, b1.Length, A);
            if (b1.Length == 1)
                Det *= -1;
            return Det;
        }
        /// <summary>
        /// Find solution for liner equation use Cramer rule.
        /// </summary>
        /// <returns></returns>

        static double[] CramerRule()
        {
            double[] OutPut = new double[MatrixA.GetLength(0)];
            double MainDet = Deter(MatrixA);
            if (MainDet == 0)
                // if Delta == 0, liner equation have more than one solution.
                throw new Exception();
            Console.WriteLine("Delta: " + MainDet);
            double[] SubDet = new double[MatrixA.GetLength(0)];
            for (int i = 0; i < MatrixA.GetLength(0); i++)
            {
                double[,] SubMat = new double[MatrixA.GetLength(0), MatrixA.GetLength(0)];

                for (int i1 = 0; i1 < MatrixA.GetLength(0); i1++)
                {

                    for (int j1 = 0; j1 < MatrixA.GetLength(0); j1++)
                    {
                        if (i1 != i)
                            SubMat[j1, i1] = MatrixA[j1, i1];
                        else
                            SubMat[j1, i1] = MatrixB[0, j1];
                    }

                }
                // Find all delta[i].
                SubDet[i] = Deter(SubMat);
                Console.WriteLine($"Delta[{i + 1}]: " + SubDet[i]);
            }

            for (int i = 0; i < MatrixA.GetLength(0); i++)
            {
                // Find Xi.
                OutPut[i] = SubDet[i] / MainDet;
            }


            return (OutPut);
        }
        /// <summary>
        /// Do all the calculation.
        /// </summary>
        static void CalculationM()
        {
            try
            {
                if (!Error)
                {
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
                            Deter(MatrixA);
                            Console.WriteLine($"Determinant:{Det}");
                            break;
                        case (7):
                            double[] Solution = CramerRule();
                            Console.WriteLine("Solution is here!");
                            for (int i = 0; i < Solution.Length; i++)
                            {
                                Console.WriteLine($"X{i + 1} = {Solution[i]} ");
                            }
                            Console.WriteLine();
                            PrintMatrix(MatrixA, 7);
                            break;
                    }
                    Console.WriteLine("Plz, press enter to continue...");
                    MenuPosition = 0;

                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                Console.WriteLine("Error! Plz try again!");
                MenuPosition = 0;
            }
        }

        /// <summary>
        /// Add element to array.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="Addthing"></param>
        /// <returns></returns>
        public static string[] AddElementToArray(string[] array, string Addthing)
        {
            /// Add element of array from the certain index.
            /// Resources https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.removeat?view=netcore-3.1;
            List<string> lst = new List<string>(array);
            lst.Add(Addthing);
            return lst.ToArray();
        }
        /// <summary>
        /// Main Menu input.
        /// </summary>
        /// <returns></returns>
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
            MainMenuInfo.Add("Find the solution of System of linear equations use Cramer's rule");
            return MainMenuInfo;
        }
        /// <summary>
        /// Method Input menu.
        /// </summary>
        /// <returns></returns>
        static List<string> MethodMenu()
        {
            List<string> MethodMenuInfo = new List<string>();

            MethodMenuInfo.Add("Use text file");
            MethodMenuInfo.Add("Use console input");
            MethodMenuInfo.Add("Randomize");
            return MethodMenuInfo;
        }
        /// <summary>
        /// Let user choose input method.
        /// </summary>
        /// <param name="ChoosenMethodInput"></param>
        static void InputMethod(int ChoosenMethodInput)
        {
            try
            {
                switch (ChoosenMethodInput)
                {
                    case (0):
                        Console.WriteLine(MainMenu()[ChoosenOperation]);
                        PrintInstructionMatrix();
                        InputPath();
                        break;
                    case (1):
                        Console.WriteLine(MainMenu()[ChoosenOperation]);
                        PrintInstructionMatrix();
                        InputMatrix();
                        break;
                    case (2):
                        Console.WriteLine(MainMenu()[ChoosenOperation]);
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
                Console.WriteLine("Wrong input, Plz try again! Press Enter to continue...");
                Error = true;
                MenuPosition = 0;
            }
        }
        // Size of matrix;
        /// <summary>
        /// File input method.
        /// </summary>
        static void InputPath()
        {

            Console.WriteLine("Plz, input correct full path to your file with matrix(ces)");

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
        /// <summary>
        /// Input from console.
        /// </summary>
        static void InputMatrix()
        {
            Console.WriteLine("Plz, input correct matrix(ces), type 'end' in the new line to end the input");
            string[] ConsoleInput = new string[1];
            string InputLine = "";
            ConsoleInput[0] = Console.ReadLine();
            do
            {
                InputLine = Console.ReadLine();
                if (InputLine == "end")
                    break;
                ConsoleInput = AddElementToArray(ConsoleInput, InputLine);
            } while (true);

            ReadMatrix(ConsoleInput);
            MenuInfo = new List<string>();
        }
        /// <summary>
        /// Read only one matrix.
        /// </summary>
        /// <param name="FileInput"></param>
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
        /// <summary>
        /// Read two matrix.
        /// </summary>
        /// <param name="FileInput"></param>
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
            // Find "-" which differece the first and second matrix.
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
        /// <summary>
        /// Read one matrix and number.
        /// </summary>
        /// <param name="FileInput"></param>
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
        /// <summary>
        /// Read matrix in all situation.
        /// </summary>
        /// <param name="Input"></param>
        static void ReadMatrix(string[] Input)
        {
            try
            {
                if (ChoosenOperation == 1 || ChoosenOperation == 0 || ChoosenOperation == 6)
                {
                    ReadOneMatrix(Input);
                    PrintMatrix(MatrixA, 1);
                    return;
                }
                else if (ChoosenOperation == 2 || ChoosenOperation == 3 || ChoosenOperation == 4)
                {
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
                else if (ChoosenOperation == 7)
                {
                    Console.WriteLine("1m|b");
                    ReadTwoMatrix(Input);
                    PrintMatrix(MatrixA, 7);
                    Console.WriteLine();
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
        /// <summary>
        /// Generate random matrix.
        /// </summary>
        static void RandomMatrix()
        {
            string[] RandomInput = new string[1];
            try
            {
                Random rnd = new Random();
                Console.WriteLine("Plz, input range of random number in matrix and number(n~m)");
                int[] RandomRange = new int[2];
                string RangeInput = Console.ReadLine();
                RandomRange[0] = int.Parse(RangeInput.Split('~')[0]);
                RandomRange[1] = int.Parse(RangeInput.Split('~')[1]);
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
                        Console.WriteLine("Plz, input correct size of matrix (n*n) P.S but not more than 10*10");
                        RandomInput[0] = Console.ReadLine();
                        goto case (100);
                    case (7):
                        Console.WriteLine("Plz, input correct size of matrix (n*n) P.S but not more than 10*10");
                        RandomInput[0] = Console.ReadLine();
                        RandomInput[0] = RandomInput[0] + " " + $"1*{RandomInput[0].Split('*')[0]}";
                        goto case (200);
                    case (100):
                        RandomOneMatrix(ref RandomInput, RandomRange, ref rnd);
                        break;
                    case (200):
                        RandomTwoMatrix(ref RandomInput, RandomRange, ref rnd);
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
        /// <summary>
        /// Generate one random matrix.
        /// </summary>
        /// <param name="RandomInput"></param>
        /// <param name="RandomRange"></param>
        /// <param name="rnd"></param>
        static void RandomOneMatrix(ref string[] RandomInput, int[] RandomRange, ref Random rnd)
        {
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
        }
        /// <summary>
        /// Generate two random matrix.
        /// </summary>
        /// <param name="RandomInput"></param>
        /// <param name="RandomRange"></param>
        /// <param name="rnd"></param>
        static void RandomTwoMatrix(ref string[] RandomInput, int[] RandomRange, ref Random rnd)
        {
            if (ChoosenOperation != 4 && ChoosenOperation != 7)
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
        }
        /// <summary>
        /// Check input.
        /// </summary>
        /// <returns></returns>
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
                        if (MatrixA.GetLength(0) != MatrixA.GetLength(1) || (MatrixA.GetLength(0) == MatrixA.GetLength(1) && MatrixA.GetLength(0) > 10))
                            throw new Exception();
                        break;
                    case (7):
                        if ((MatrixA.GetLength(0) == MatrixA.GetLength(1) && MatrixA.GetLength(0) > 10) || MatrixA.GetLength(0) != MatrixA.GetLength(1) || MatrixB.GetLength(0) != 1 || MatrixB.GetLength(1) != MatrixA.GetLength(0))
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
        /// <summary>
        /// Menu position correction.
        /// </summary>
        /// <param name="Position"></param>
        /// <returns></returns>
        static short PositionClear(short Position)
        {
            if (Position < 0)
                return 0;
            if (Position >= 2)
                return 2;
            return Position;
        }
        /// <summary>
        /// Print Matrix.
        /// </summary>
        /// <param name="A"></param>
        /// <param name="Id"></param>
        static void PrintMatrix(double[,] A, int Id)
        {
            if (Id == 1)
                Console.WriteLine("MatrixA:");
            else if (Id == 2)
                Console.WriteLine("MatrixB:");
            if (Id != 7)
                for (int i = 0; i < A.GetLength(0); i++)
                {
                    for (int j = 0; j < A.GetLength(1); j++)
                    {
                        Console.Write(A[i, j] + " ");
                    }
                    Console.WriteLine();
                }
            else if (Id == 7)
            {
                Console.WriteLine("MatrixA|b: ");
                for (int i = 0; i < A.GetLength(0); i++)
                {
                    for (int j = 0; j < A.GetLength(1); j++)
                    {
                        if (j != A.GetLength(1) - 1)
                            Console.Write(A[i, j] + " ");
                        else
                            Console.Write(A[i, j] + " | " + MatrixB[0, i]);
                    }
                    Console.WriteLine();
                }
            }
        }
        /// <summary>
        /// Use to generate the graphic menu.
        /// </summary>
        /// <param name="InputList"></param>
        /// <param name="ChoosenOne"></param>
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
        public static void PrintInstructionMatrix()
        {
            Console.WriteLine("This is how you must input matrix:");
            if (ChoosenMethodInput == 0)
                Console.WriteLine("Write your file in .txt like this: (in the end of line must without the whitespace)");
            else if (ChoosenMethodInput == 1)
                Console.WriteLine("Input on console like this (press enter after completed line, in the end of line must without whitespace)");
            Console.WriteLine("Example:");
            if (ChoosenOperation == 2 || ChoosenOperation == 3 || ChoosenOperation == 4)
            {
                Console.WriteLine("2*2 2*3      // <---- Input your size of matrix, between them must have ' '.");
                Console.WriteLine("1 2");
                Console.WriteLine("1 2      // <---- Input here your first matrix. One ' ' between two elements");
                Console.WriteLine("-       //<---- Input here '-' to divide two matrix.");
                Console.WriteLine("1 2 3");
                Console.WriteLine("1 4 3    // <---- Input here your second matrix. One ' ' between two elements");
            }
            else
            {
                Console.WriteLine("2*3     // <---- Input your size of matrix.");
                if (ChoosenOperation == 5)
                    Console.WriteLine("5   // <--- Input which number you want to multiply.");
                Console.WriteLine("1 2 3");
                Console.WriteLine("1 4 3    // <---- Input here your matrix. One ' ' between two elements");
            }
            if (ChoosenMethodInput == 1)
                Console.WriteLine("end           //<---- Input here end to end the input.");
        }
        /// <summary>
        /// Main method.
        /// </summary>
        /// <param name="args"></param>
        static bool ExitCode = false;
        static void Main(string[] args)
        {
            do
            {
                // Check the menu position.
                Error = false;
                MenuPosition = PositionClear(MenuPosition);
                Console.Clear();
                if (MenuPosition == 0)
                {
                    // Main menu.
                    Console.WriteLine("Main Menu      Press ESC to exit programm");
                    MenuInfo = MainMenu();
                }
                if (MenuPosition == 1)
                {
                    // Choose method menu.
                    Console.WriteLine(MenuName);
                    MenuInfo = MethodMenu();
                }
                if (MenuPosition == 2)
                {
                    // Input menu.
                    InputMethod(ChoosenMethodInput);
                    // And calculation.
                    CalculationM();
                }
                // Print the current menu.
                PrintOutUpAndDown(MenuInfo, ChoosenOne);
                // Read the nect choise of user.
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
                    case (ConsoleKey.Escape):
                        ExitCode = true;
                        break;
                }
            } while (!ExitCode);
        }
    }
}