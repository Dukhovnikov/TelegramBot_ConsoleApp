using System;

// ReSharper disable once CheckNamespace
namespace TelegramBot_ConsoleApp
{
 public class DeterminantOfAmericanMethod
    {
        public string Logger { get; private set; }
        
        private Matrix Matrix { get; set; }
        private Matrix[] MatrixOfDenominators { get; set; } = new Matrix[2];

        public DeterminantOfAmericanMethod(Matrix matrix)
        {
            Matrix = matrix;
            Logger = string.Empty;
        }        
        
        public void GetDeterminant()
        {
            var countIteration = 1;
            var determinant = Matrix.Determinant;
            // Получаем матрицу делителей
            MatrixOfDenominators[0] = GetMatrixOfDenominators(Matrix);
            // Получаем матрицу делителей второго порядка (пониженной матрицы)
            MatrixOfDenominators[1] = GetMatrixOfDenominators(DownToRise(Matrix));
            
            
            Logger += "<code>Американский метод вычисления определителя</code>\n\n";
            Logger += "Начальная матрица: \n";
            Logger += Matrix + Environment.NewLine;
            Logger += "Матрица делителей: \n";
            Logger += MatrixOfDenominators[0];
            Logger += Environment.NewLine;
            Logger += "___________________________\n";
            
            
            while (Matrix.Size != 1)
            {
                // Понижаем порядок матрицы
                Matrix = DownToRise(Matrix);

                Logger += $"Итерация {countIteration}: \n\n";
                
                if (countIteration > 1)
                {

                    Logger += "Матрица делителей: \n";
                    Logger += MatrixOfDenominators[0] + Environment.NewLine;
                    Logger += "Матрица пониженного порядка (решение): \n";
                    Logger += GetCleavageMatrixOfStringImplementation(Matrix, MatrixOfDenominators[0]);
                    
                    for (var i = 0; i < Matrix.Size; i++)
                    {
                        for (var j = 0; j < Matrix.Size; j++)
                        {
                            Matrix[i, j] = Matrix[i, j] / MatrixOfDenominators[0][i, j];
                        }
                    }

                    MatrixOfDenominators[0] = MatrixOfDenominators[1];
                    
                    Logger += "\nМатрица пониженного порядка (ответ): \n";
                }
                else
                {
                    Logger += "Матрица пониженного порядка: \n";
                }
                
                // Получаем матрицу делителей пониженной матрицы
                MatrixOfDenominators[1] = GetMatrixOfDenominators(Matrix);
                
                

                Logger += Matrix;
                Logger += "___________________________\n";
                
                countIteration++;
            }

            Logger += $"Стандартное решение: <b>{determinant}</b>\n";
            Logger += $"Американским методом: <b>{Matrix[0, 0]}</b>\n";
            
            //return Matrix[0, 0];
        }

        private static Matrix DownToRise(Matrix matrix)
        {
            var rezMatrix = new Matrix(matrix.Size - 1);


            for (var i = 0; i < rezMatrix.Size; i++)
            {
                for (var j = 0; j < rezMatrix.Size; j++)
                {
                    rezMatrix[i, j] = matrix[i, j] * matrix[i + 1, j + 1] - matrix[i + 1, j] * matrix[i, j + 1];
                }
            }
            
            
            return rezMatrix;
        }

        private static Matrix GetMatrixOfDenominators(Matrix matrix)
        {
            return matrix.Size.Equals(1) ? Matrix.E(1) : matrix.Exclude(matrix.Size - 1, matrix.Size - 1).Exclude(0, 0);
        }

        private static string GetCleavageMatrixOfStringImplementation(Matrix a, Matrix b)
        {
            var cleavageMatrixOfStringImplementation = string.Empty;

            for (var i = 0; i < a.Size; i++)
            {
                for (var j = 0; j < a.Size; j++)
                {
                    cleavageMatrixOfStringImplementation += $"{a[i, j]}/{b[i, j]}   ";
                    //cleavageMatrixOfStringImplementation += $"{a[i, j]}/{b[i, j]}\t";
                }

                cleavageMatrixOfStringImplementation += Environment.NewLine;
            }

            return cleavageMatrixOfStringImplementation;
        }
    }
}