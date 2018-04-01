using System;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace TelegramBot_ConsoleApp
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class DVector
    {
        /// <summary>
        /// Вектор
        /// </summary>
        // ReSharper disable once FieldCanBeMadeReadOnly.Global
        public double[] Vector;


        /// <summary>
        /// Пустой вектор, размерностью i
        /// </summary>
        public DVector(int i)
        {
            Vector = new double[i];
            for (var j = 0; j < i; j++) Vector[j] = 0;
        }


        /// <summary>
        /// Заполнение вектора путем передачи массива
        /// </summary>
        public DVector(params double[] vector)
        {
            this.Vector = new double[vector.Length];
            this.Vector = vector;
        }


        /// <summary>
        /// Операция разности векоторв
        /// </summary>
        public static DVector operator -(DVector v1, DVector v2) 
        {
            var v3 = new DVector(v1.Size);          
            
            for (var i = 0; i < v1.Size; i++) v3[i] = v1[i] - v2[i];           
            
            return v3;
        }


        /// <summary>
        /// Инвертируем значения полей вектора
        /// </summary>
        public static DVector operator -(DVector v1) 
        {
            for (var i = 0; i < v1.Size; i++) v1[i] *= -1;
            
            return v1;
        }


        /// <summary>
        /// Операция сложения векторов
        /// </summary>
        public static DVector operator +(DVector v1, DVector v2) 
        {
            var v3 = new DVector(v1.Size);
            
            for (var i = 0; i < v1.Size; i++) v3.Vector[i] = v1.Vector[i] + v2.Vector[i];
            
            return v3;
        }

        /// <summary>
        /// Сложение числа с вектором
        /// </summary>
        public static DVector operator +(double c, DVector v1)
        {
            var v3 = new DVector(v1.Size);
                       
            for (var i = 0; i < v1.Size; i++) v3.Vector[i] = c + v1.Vector[i];
            
            return v3;
        }


        /// <summary>
        /// Операция умножения числа на вектор
        /// </summary>
        public static DVector operator *(double c, DVector v1)
        {
            var v3 = new DVector(v1.Size);
            
            for (var i = 0; i < v1.Size; i++) v3.Vector[i] = c * v1.Vector[i];
            
            return v3; 
        }


        /// <summary>
        /// Операция умножения вектора на вектор
        /// </summary>
        public static double operator *(DVector v1, DVector v2)
        {
            double v3 = 0;
            
            for (var i = 0; i < v1.Size; i++) v3 += v1[i] * v2[i];
            
            return v3;
        }

        public static DVector operator /(DVector v1, double c)
        {
            var v3 = new DVector(v1.Size);
                       
            for (var i = 0; i < v3.Size; i++) v3[i] = v1[i] / c;
            
            return v3;
        }

        /// <summary>
        /// Умножение вектора на транспонированный вектор
        /// </summary>
        public static Matrix Multiplication(DVector v1, DVector v2)
        {
            var m = new Matrix(v1.Size);

            if (v1.Size != v2.Size)
                throw new ArgumentException("Число столбцов матрицы А не равно числу строк матрицы В.");
            
            for (var i = 0; i < m.Size; i++)
                for (var j = 0; j < m.Size; j++)
                    m[i, j] = v1[i] * v2[j];
            
            return m;
        }

        /// <summary>
        /// Строковое представление вектора
        /// </summary>
        public override string ToString()
        {
            var vector = string.Empty;
            
            for (var i = 0; i < Size - 1; i++) vector += this[i] + " : ";
            
            vector += this[Size - 1];
            
            return vector;
        }

        /// <summary>
        ///Норма вектора  
        /// </summary>
        public double Norma
        {
            get
            {
                double sum = 0;
                
                for (var i = 0; i < Size; i++) sum += Math.Pow(this[i], 2);
                
                return Math.Sqrt(sum);
            }
        }
        /// <summary>
        /// Значение ячейки вектора
        /// </summary>
        public double this[int index]
        {
            get => Vector[index]; // Аксессор для получения данных
            set => Vector[index] = value; // Аксессор для установки данных
        }
        /// <summary>
        /// Длина вектора
        /// </summary>
        public int Size => Vector.Length;
    }
}
