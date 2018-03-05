using System;
using System.Data.SqlTypes;

// ReSharper disable ConvertIfStatementToSwitchStatement

// ReSharper disable once CheckNamespace
namespace TelegramBot_ConsoleApp
{
    public class Matrix : INullable
    {
        /// <summary>
        /// Двумерный массив
        /// </summary>
        private readonly double[,] _matrix;

        private bool _isNull;


        /// <summary>
        /// Задает пустую матрицу размером N
        /// </summary>
        public Matrix(int n)
        {
            _matrix = new double[n, n];
        }


        /// <summary>
        /// Задает матрицу с помощью двумерного массива
        /// </summary>
        public Matrix(double[,] matrix)
        {
            _matrix = matrix;
        }

        public Matrix(string matrix)
        {
            try
            {
                var stringsMatrix = matrix.Split('\n');
                var size = stringsMatrix[0].Split(' ').Length;
                _matrix = new double[size, size];
                var i = 0;
            
                foreach (var stringMatrix in stringsMatrix)
                {
                    var rowMatrix = stringMatrix.Split(' ');

                    for (var j = 0; j < size; j++)
                    {
                        _matrix[i, j] = int.Parse(rowMatrix[j]);
                    }

                    i++;
                }
            }
            catch (Exception e)
            {
                _matrix = null;
            }

        }


        /// <summary>
        /// Получение значение ячейки матрицы
        /// </summary>
        public double this[int row, int column]
        {
            get => _matrix[row, column]; // Аксессор для получения данных
            set => _matrix[row, column] = value; // Аксессор для установки данных
        }


        #region Operators       

        /// <summary>
        /// Сложение матриц
        /// </summary>
        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            var m3 = new Matrix(m1.Size);
            
            
            for (var i = 0; i < m3.Size; i++)
            for (var j = 0; j < m3.Size; j++)
                m3[i, j] = m1[i, j] + m2[i, j];
            
            
            return m3;
        }


        /// <summary>
        /// Разность матриц
        /// </summary>
        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            var m3 = new Matrix(m1.Size);
            
            
            for (var i = 0; i < m3.Size; i++)
            for (var j = 0; j < m3.Size; j++)
                m3[i, j] = m1[i, j] - m2[i, j];
            
            
            return m3;
        }


        /// <summary>
        /// Умножение матрицы на число
        /// </summary>
        public static Matrix operator *(double c, Matrix m1)
        {
            var m3 = new Matrix(m1.Size);
            
            
            for (var i = 0; i < m3.Size; i++)
            for (var j = 0; j < m3.Size; j++)
                m3[i, j] = c * m1[i, j];
            
            
            return m3;
        }


        /// <summary>
        /// Умножение матрицы на вектор
        /// </summary>
        public static DVector operator *(Matrix m, DVector v)
        {
            if (m.Size != v.Size) throw new ArgumentException("Число столбцов матрицы А не равно числу элементов вектора В.");
            
            var vector = new DVector(v.Size);
            
            
            for (var i = 0; i < vector.Size; i++)
            for (var j = 0; j < vector.Size; j++)
                vector[i] += m[i, j] * v[j];
            
            
            return vector;
        }

        /// <summary>
        /// Умножение матриц
        /// </summary>
        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            var matrix = new Matrix(m1.Size);
            
            
            for (var i = 0; i < matrix.Size; i++)
            for (var j = 0; j < matrix.Size; j++)
            for (var k = 0; k < matrix.Size; k++)
                matrix[i, j] += m1[i, k] * m2[k, j];
            
            
            return matrix;
        }

        /// <summary>
        /// Деление матрицы на константу
        /// </summary>
        public static Matrix operator /(Matrix m, double c)
        {
            var matrix = new Matrix(m.Size);
            
            
            for (var i = 0; i < m.Size; i++)
            for (var j = 0; j < m.Size; j++)
                matrix[i, j] = m[i, j] / c;
            
            
            return matrix;
        }

        #endregion
        
        /// <summary>
        /// Возвращает матрицу без указанных строки и столбца. Исходная матрица не изменяется.
        /// </summary>
        public Matrix Exclude(int row, int column)
        {
            if (row > Size || column > Size) throw new IndexOutOfRangeException("Строка или столбец не принадлежат матрице.");
            
            var m1 = new Matrix(Size - 1);
            var x = 0;
            
            
            for (var i = 0; i < Size; i++)
            {
                var y = 0;
                if (i == row) { x++; continue; }
                for ( var j = 0; j < Size; j++)
                {
                    if (j == column) { y++; continue; }
                    m1[i - x, j - y] = this[i, j];
                }
            }
            
            
            return m1;
        }

        /// <summary>
        /// Единичная матрица размером NxN
        /// </summary>
        public static Matrix E(int n)
        {
            var matrix = new Matrix(n);
            
            
            for (var i = 0; i < n; i++)
            for (var j = 0; j < n; j++)
                matrix[i, j] = 1;
            
            
            return matrix;
        }

        /// <summary>
        /// Детерминант матрицы
        /// </summary>
        public double Determinant
        {
            get
            {
                var m1 = this;
                if (m1.Size == 1) return m1[0, 0];
                if (m1.Size == 2) return m1[0, 0] * m1[1, 1] - m1[0, 1] * m1[1, 0];
                if (m1.Size == 3) return m1[0, 0] * m1[1, 1] * m1[2, 2] + m1[0, 1] * m1[1, 2] * m1[2, 0] + m1[0, 2] * m1[1, 0] * m1[2, 1] - m1[0, 2] * m1[1, 1] * m1[2, 0] - m1[0, 0] * m1[1, 2] * m1[2, 1] - m1[0, 1] * m1[1, 0] * m1[2, 2];
                double det = 0;
                
                
                for (var i = 0; i < m1.Size; i++)
                {
                    det += Math.Pow(-1, i) * m1[0, i] * m1.Exclude(0, i).Determinant;
                }
                
                
                return det;
            } 
        }


        /// <summary>
        /// Определитель матрицы
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public static double determinant(Matrix m1)
        {
            if (m1.Size == 1) return m1[0, 0];
            if (m1.Size == 2) return m1[0, 0] * m1[1, 1] - m1[0, 1] * m1[1, 0];
            if (m1.Size == 3) return m1[0, 0] * m1[1, 1] * m1[2, 2] + m1[0, 1] * m1[1, 2] * m1[2, 0] + m1[0, 2] * m1[1, 0] * m1[2, 1] - m1[0, 2] * m1[1, 1] * m1[2, 0] - m1[0, 0] * m1[1, 2] * m1[2, 1] - m1[0, 1] * m1[1, 0] * m1[2, 2];
            double det = 0;
            for (var i = 0; i < m1.Size; i++)
            {
                det += Math.Pow(-1, i) * m1[0, i] * determinant(m1.Exclude(0, i));
            }
            return det;
        }


        /// <summary>
        /// Обратная матрица. Обратная матрица существует только для квадратных, невырожденных, матриц.
        /// </summary>
        public Matrix Inverse
        {
            get
            {
                var matrix = new Matrix(Size);
                var determinant = Determinant;
                
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (determinant == 0) return matrix; //Если определитель == 0 - матрица вырожденная
                
                
                for (var i = 0; i < Size; i++)
                {
                    for (var j = 0; j < Size; j++)
                    {
                        var tmp = Exclude(i, j);
                        matrix[j, i] = (Math.Pow(-1, i + 1 + j + 1) / determinant) * tmp.Determinant;
                    }
                }
                
                
                return matrix;
            }
        }


        /// <summary>
        /// Обратная матрица. Обратная матрица существует только для квадратных, невырожденных, матриц.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public static Matrix inverse(Matrix m, int round = 0)
        {
            var matrix = new Matrix(m.Size);
            var determinant = m.Determinant;
            
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (determinant == 0) return matrix; //Если определитель == 0 - матрица вырожденная
            
            
            for (var i = 0; i < m.Size; i++)
            {
                for (var j = 0; j < m.Size; j++)
                {
                    var tmp = m.Exclude(i, j);
                    matrix[j, i] = round == 0 ? (Math.Pow(-1, i + 1 + j + 1) / determinant) * tmp.Determinant : Math.Round(((1 / determinant) * tmp.Determinant), round, MidpointRounding.ToEven);
                }
            }
            
            
            return matrix;
        }


        /// <summary>
        /// Транспонирование матицы
        /// </summary>
        public Matrix Transpose
        {
            get
            {
                var matrix = new Matrix(Size);
                
                
                for (var i = 0; i < Size; i++)
                for (var j = 0; j < Size; j++)
                    matrix[j, i] = this[i, j];
                
                
                return matrix;
            }
        }


        /// <summary>
        /// Транспонирование матицы
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public Matrix transpose(Matrix m)
        {
            var matrix = new Matrix(m.Size);
            
            
            for (var i = 0; i < m.Size; i++)
            for (var j = 0; j < m.Size; j++)
                matrix[j, i] = m[i, j];
            
            
            return matrix;
        }

        /// <summary>
        /// Размерность матрицы
        /// </summary>
        public int Size => _matrix.GetLength(0);
        
        /// <summary>
        /// Строковое представление матрицы
        /// </summary>
        public override string ToString()
        {
            var stringMatrix = string.Empty;
            
            
            for (var i = 0; i < _matrix.GetLength(0); i++)
            {
                for (var j = 0; j < _matrix.GetLength(1); j++)
                {
                    //stringMatrix += matrix[i, j] + "\t";
                    stringMatrix += _matrix[i, j] + "    ";
                }

                stringMatrix += Environment.NewLine;
            }

            
            return stringMatrix;
        }

        /// <inheritdoc />
        /// <summary>
        /// Реализация интерфейса
        /// </summary>
        bool INullable.IsNull => _matrix == null;
    }
}