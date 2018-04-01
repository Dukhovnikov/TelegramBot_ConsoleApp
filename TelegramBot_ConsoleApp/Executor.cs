using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TelegramBot_ConsoleApp;
using static System.String;

namespace TelegramBot_ConsoleApp
{    
    public static class Executor
    {
        private static string ErrorTextAboutRachinskiy = @"
Ошибка! (Делитель не является простым числом)

Для алгоритмов Рачинского делитель должен быть простым.
";
    
        
        public static string OnGetDeterminantOfAmericanMethod(string data)
        {
            var matrix = new Matrix(data);
            if (((INullable) matrix).IsNull)
            {
                return "Error! An invalid string was specified.\n\n";
            }
            
            var determinantOfAmericanMethod = new DeterminantOfAmericanMethod(matrix);
            determinantOfAmericanMethod.GetDeterminant();

            return determinantOfAmericanMethod.Logger;
        }

        public static string OnGetPaskaleSolve(string data)
        {           
            try
            {
                var stringOfNumbers = data.Split("/");
                if (stringOfNumbers.Length != 2) throw new Exception();

                var n = int.Parse(stringOfNumbers[0]);
                var d = int.Parse(stringOfNumbers[1]);

                var criterionDivibility = new CriterionsForDivisibility(n, d);
                criterionDivibility.GetPaskalSolve();

                return criterionDivibility.Logger + "\n";

            }
            catch (Exception e)
            {
                return "Error! An invalid string was specified.\n\n";
            }
        }
        
        public static string OnGetRachinskiyOneSolve(string data)
        {           
            try
            {
                var stringOfNumbers = data.Split("/");
                if (stringOfNumbers.Length != 2) throw new Exception();

                var n = int.Parse(stringOfNumbers[0]);
                var d = int.Parse(stringOfNumbers[1]);

                if (!IsPrime(d))
                {
                    return ErrorTextAboutRachinskiy;
                }
                
                var criterionDivibility = new CriterionsForDivisibility(n, d);
                var solveString = criterionDivibility.GetRachinskiyFirstSolve();

                return solveString + "\n";

            }
            catch (Exception e)
            {
                return "Error! An invalid string was specified.\n\n";
            }
        }
        
        public static string OnGetRachinskiySecondSolve(string data)
        {           
            try
            {
                var stringOfNumbers = data.Split("/");
                if (stringOfNumbers.Length != 2) throw new Exception();

                var n = int.Parse(stringOfNumbers[0]);
                var d = int.Parse(stringOfNumbers[1]);

                if (!IsPrime(d))
                {
                    return ErrorTextAboutRachinskiy;
                }
                
                var criterionDivibility = new CriterionsForDivisibility(n, d);
                var solveString = criterionDivibility.GetRachinskiySecondSolve();

                return solveString + "\n";

            }
            catch (Exception e)
            {
                return "Error! An invalid string was specified.\n\n";
            }
        }

        public static string OnGetRachinskiyThridSolve(string data)
        {
            try
            {
                var stringOfNumbers = data.Split("/");
                if (stringOfNumbers.Length != 2) throw new Exception();

                var n = int.Parse(stringOfNumbers[0]);
                var d = int.Parse(stringOfNumbers[1]);

                if (!IsPrime(d))
                {
                    return ErrorTextAboutRachinskiy;
                }
                
                var criterionDivibility = new CriterionsForDivisibility(n, d);
                var solveString = criterionDivibility.GetRachinskiyThirdSolve();

                return solveString + "\n";

            }
            catch (Exception e)
            {
                return "Error! An invalid string was specified.\n\n";
            }
        }

        public static string OnGetMultiplicationNubmersKaratsubaSolve(string data)
        {
            try
            {
                var stringOfNumbers = data.Split("*");
                if (stringOfNumbers.Length != 2) throw new Exception();

                var n = int.Parse(stringOfNumbers[0]);
                var d = int.Parse(stringOfNumbers[1]);
                
                var karatsuba = new KaratsubaAlgorithm(n, d);

                return karatsuba.GetSolve();
            }
            catch (Exception e)
            {
                return "Error! An invalid string was specified.\n\n";
            }
        }

        public static string OnGetStrassenAlgorithmSolve(string aMatrixString, string bMatrixString)
        {
            var aMatrix = new Matrix(aMatrixString);
            var bMatrix = new Matrix(bMatrixString);
            
            if (((INullable) aMatrix).IsNull || ((INullable) bMatrix).IsNull)
            {
                return "Error! An invalid string was specified.\n\n";
            }

            if (aMatrix.Size != bMatrix.Size)
            {
                return "Ошибка! \n\nРазмерность матрицы А не совпадает с размерностью В";
            }

            if ((aMatrix.Size < 3 || bMatrix.Size < 3) && (aMatrix.Size > 4 || bMatrix.Size > 4))
            {
                return "Ошибка! \n\nАлгоритм работает только с матрицами 3х3 и 4х4";
            }

            if (aMatrix.Size == 3 && bMatrix.Size == 3)
            {
                aMatrix = aMatrix.UpSize();
                bMatrix = aMatrix.UpSize();
            }
            
            StrassenAlgorithm.Logger = Empty;
            return StrassenAlgorithm.GetSolve(aMatrix, bMatrix);
        }
        
        public static string OnGetStrassenAlgorithmSolve(Matrix aMatrix, Matrix bMatrix)
        {
            if (((INullable) aMatrix).IsNull || ((INullable) bMatrix).IsNull)
            {
                return "Error! An invalid string was specified.\n\n";
            }

            if (aMatrix.Size != bMatrix.Size)
            {
                return "Ошибка! \n\nРазмерность матрицы А не совпадает с размерностью В";
            }

            if ((aMatrix.Size < 3 || bMatrix.Size < 3) && (aMatrix.Size > 4 || bMatrix.Size > 4))
            {
                return "Ошибка! \n\nАлгоритм работает только с матрицами 3х3 и 4х4";
            }

            if (aMatrix.Size == 3 && bMatrix.Size == 3)
            {
                aMatrix = aMatrix.UpSize();
                bMatrix = aMatrix.UpSize();
            }
            
            StrassenAlgorithm.Logger = Empty;
            //return StrassenAlgorithm.GetSolve(aMatrix, bMatrix);
            return StrassenAlgorithm.GetSolveRefactored(aMatrix, bMatrix);
        }
        
        private static bool IsPrime(int number)
        {
            if (number < 2) return false;
            for (var i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
    }
}