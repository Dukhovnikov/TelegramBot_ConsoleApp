using System;

namespace TelegramBot_ConsoleApp
{
    public class StrassenAlgorithm
    {
        public static string Logger { get; set; }

        public static void WriteLogger(string s)
        {
            Logger += s;
        }
        public static void WriteLineLogger(string s = "")
        {
            Logger += s + "\n";
        }

        public StrassenAlgorithm()
        {
            Logger = string.Empty;
        }

        private static (Matrix matrix, string solve) getD(Matrix aMatrix, Matrix bMatrix) 
        {                
            var d1 = (aMatrix[0, 0] + aMatrix[1, 1]) * (bMatrix[0, 0] + bMatrix[1, 1]);
            var d2 = (aMatrix[0, 1] - aMatrix[1, 1]) * (bMatrix[1, 0] + bMatrix[1, 1]);
            var d3 = (aMatrix[1, 0] - aMatrix[0, 0]) * (bMatrix[0, 0] + bMatrix[0, 1]);
            var d4 = (aMatrix[0, 0] + aMatrix[0, 1]) * bMatrix[1, 1];
            var d5 = (aMatrix[1, 0] + aMatrix[1, 1]) * bMatrix[0, 0];
            var d6 = aMatrix[0, 0] * (bMatrix[0, 1] - bMatrix[1, 1]);
            var d7 = aMatrix[1, 1] * (bMatrix[1, 0] - bMatrix[0, 0]);

            var result = (matrix: new Matrix(2)
                {
                    [0, 0] = d1 + d2 - d4 + d7,
                    [0, 1] = d4 + d6,
                    [1, 0] = d5 + d7,
                    [1, 1] = d1 + d3 - d5 + d6
                },
                solve: ""); 
            
            
/*            var m = new Matrix(2)
            {
                [0, 0] = d1 + d2 - d4 + d7,
                [0, 1] = d4 + d6,
                [1, 0] = d5 + d7,
                [1, 1] = d1 + d3 - d5 + d6
            };*/
            
            //WriteLineLogger($"{d1}\t\t{d2}\t\t{d3}\t\t{d4}\t\t{d5}\t\t{d6}\t\t{d7}")

            result.solve = "{\n\n";           
            result.solve += $"(d1 ... d3 ... d7) = {d1}\t\t{d2}\t\t{d3}\t\t{d4}\t\t{d5}\t\t{d6}\t\t{d7}\n\n";           
            result.solve += $"c11 = {d1} + {d2} - {d4} + {d7} = {result.matrix[0,0]}\n";
            result.solve += $"c12 = {d4} + {d6} = {result.matrix[0,1]}\n";
            result.solve += $"c21 = {d5} + {d7} = {result.matrix[1,0]}\n";
            result.solve += $"c22 = {d1} + {d3} - {d5} + {d6} = {result.matrix[1,1]}\n\n";          
            result.solve += $"Результирующая матрица:\n\n{result.matrix}";
            result.solve += "\n}\n\n";

            return result;
        }

        static int[,] d(int[,] A, int[,] B)
        {
            int d1, d2, d3, d4, d5, d6, d7;
            int[,] M = new int[2, 2];
            d1 = (A[0, 0] + A[1, 1]) * (B[0, 0] + B[1, 1]);
            d2 = (A[0, 1] - A[1, 1]) * (B[1, 0] + B[1, 1]);
            d3 = (A[1, 0] - A[0, 0]) * (B[0, 0] + B[0, 1]);
            d4 = (A[0, 0] + A[0, 1]) * B[1, 1];
            d5 = (A[1, 0] + A[1, 1]) * B[0, 0];
            d6 = A[0, 0] * (B[0, 1] - B[1, 1]);
            d7 = A[1, 1] * (B[1, 0] - B[0, 0]);

            M[0, 0] = d1 + d2 - d4 + d7;
            M[0, 1] = d4 + d6;
            M[1, 0] = d5 + d7;
            M[1, 1] = d1 + d3 - d5 + d6;

            WriteLineLogger($"{d1} {d2} {d3} {d4} {d5} {d6} {d7}");

            return M;
        }
        
        static int[,] plus(int[,] M1, int[,] M2)
        {
            int[,] M = new int[2, 2];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    M[i,j] = M1[i, j] + M2[i, j];
                }
            }
            return M;
        }

        static int[,] minus(int[,] M1, int[,] M2)
        {
            int[,] M = new int[2, 2];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    M[i, j] = M1[i, j] - M2[i, j];
                }
            }
            return M;
        }

        public static string GetSolveRefactored(Matrix aMatrix, Matrix bMatrix)
        {
            var a11 = aMatrix.Exclude(aMatrix.Size - 1, aMatrix.Size - 1).Exclude(aMatrix.Size - 2, aMatrix.Size - 2);
            var a12 = aMatrix.Exclude(aMatrix.Size - 1, 0).Exclude(aMatrix.Size - 2, 0);
            var a21 = aMatrix.Exclude(0, aMatrix.Size - 1).Exclude(0, aMatrix.Size - 2);
            var a22 = aMatrix.Exclude(0, 0).Exclude(0, 0);            

            var b11 = bMatrix.Exclude(bMatrix.Size - 1, bMatrix.Size - 1).Exclude(bMatrix.Size - 2, bMatrix.Size - 2);
            var b12 = bMatrix.Exclude(bMatrix.Size - 1, 0).Exclude(bMatrix.Size - 2, 0);
            var b21 = bMatrix.Exclude(0, bMatrix.Size - 1).Exclude(0, bMatrix.Size - 2);
            var b22 = bMatrix.Exclude(0, 0).Exclude(0, 0);

            
            WriteLineLogger("______________D1____________");
            var d1 = getD(a11 + a22, b11 + b22);
            WriteLogger(a11.ToString());
            WriteLineLogger("<a href=\"http://www.example.com/\">   +</a>");
            WriteLogger(a22.ToString());
            WriteLineLogger("<a href=\"http://www.example.com/\">   *</a>");
            WriteLogger(b11.ToString());
            WriteLineLogger("<a href=\"http://www.example.com/\">   +</a>");
            WriteLogger(b22.ToString());
            WriteLineLogger("<a href=\"http://www.example.com/\">   =</a>");
            WriteLogger((a11 + a22).ToString());
            WriteLineLogger("<a href=\"http://www.example.com/\">   *</a>");
            WriteLogger((b11 + b22) + "\n");
            WriteLogger(d1.solve);           
            
            WriteLineLogger("______________D2____________");
            var d2 = getD(a12 - a22, b21 + b22);
            WriteLogger(a12.ToString());
            WriteLineLogger("<a href=\"http://www.example.com/\">   -</a>");
            WriteLogger(a22.ToString());
            WriteLineLogger("<a href=\"http://www.example.com/\">   *</a>");
            WriteLogger(b21.ToString());
            WriteLineLogger("<a href=\"http://www.example.com/\">   +</a>");
            WriteLogger(b22.ToString());
            WriteLineLogger("<a href=\"http://www.example.com/\">   =</a>");
            WriteLogger((a12 - a22).ToString());
            WriteLineLogger("<a href=\"http://www.example.com/\">   *</a>");
            WriteLogger((b21 + b22) + "\n");
            WriteLogger(d2.solve);
            
            WriteLineLogger("______________D3____________");
            var d3 = getD(a21 - a11, b11 + b12);
            WriteLineLogger(a21.ToString());
            WriteLineLogger("<a href=\"http://www.example.com/\">   -</a>");
            WriteLineLogger(a11.ToString());
            WriteLineLogger("<a href=\"http://www.example.com/\">   *</a>");
            WriteLineLogger(b11.ToString());
            WriteLineLogger("<a href=\"http://www.example.com/\">   +</a>");
            WriteLineLogger(b12.ToString());
            WriteLineLogger("<a href=\"http://www.example.com/\">   =</a>");
            WriteLineLogger((a21 - a11).ToString());
            WriteLineLogger("<a href=\"http://www.example.com/\">   *</a>");
            WriteLineLogger((b11 + b12) + "\n");
            WriteLineLogger(d3.solve);
            
            WriteLineLogger("______________D4____________");
            var d4 = getD(a11 + a12, b22);
            WriteLineLogger(a11.ToString());
            WriteLineLogger("<a href=\"http://www.example.com/\">   +</a>");
            WriteLineLogger(a12.ToString());
            WriteLineLogger("<a href=\"http://www.example.com/\">   *</a>");
            WriteLineLogger(b22.ToString());
            WriteLineLogger("<a href=\"http://www.example.com/\">   =</a>");
            WriteLineLogger((a11 + a12).ToString());
            WriteLineLogger("<a href=\"http://www.example.com/\">   *</a>");
            WriteLineLogger((b22) + "\n");
            WriteLineLogger(d4.solve);
            
            
            WriteLineLogger("______________D5____________");
            var d5 = getD(a21 + a22, b11);
            WriteLineLogger(a21.ToString());
            WriteLineLogger("<a href=\"http://www.example.com/\">   +</a>");
            WriteLineLogger(a22.ToString());
            WriteLineLogger("<a href=\"http://www.example.com/\">   *</a>");
            WriteLineLogger(b11.ToString());
            WriteLineLogger("<a href=\"http://www.example.com/\">   =</a>");
            WriteLineLogger((a21 + a22).ToString());
            WriteLineLogger("<a href=\"http://www.example.com/\">   *</a>");
            WriteLineLogger((b11) + "\n");
            WriteLineLogger(d5.solve);
            
            WriteLineLogger("______________D6____________");
            var d6 = getD(a11, b12 - b22);
            WriteLineLogger(a11.ToString());
            WriteLineLogger("<a href=\"http://www.example.com/\">   *</a>");
            WriteLineLogger(b12.ToString());
            WriteLineLogger("<a href=\"http://www.example.com/\">   -</a>");
            WriteLineLogger(b22.ToString());
            WriteLineLogger("<a href=\"http://www.example.com/\">   =</a>");
            WriteLineLogger(a11.ToString());
            WriteLineLogger("<a href=\"http://www.example.com/\">   *</a>");
            WriteLineLogger((b12 - b22) + "\n");
            WriteLineLogger(d6.solve);
            
            WriteLineLogger("______________D7____________");
            var d7 = getD(a22, b21 - b11);                       
            WriteLineLogger(a22.ToString());
            WriteLineLogger("<a href=\"http://www.example.com/\">   *</a>");
            WriteLineLogger(b21.ToString());
            WriteLineLogger("<a href=\"http://www.example.com/\">   -</a>");
            WriteLineLogger(b11.ToString());
            WriteLineLogger("<a href=\"http://www.example.com/\">   =</a>");
            WriteLineLogger(a22.ToString());
            WriteLineLogger("<a href=\"http://www.example.com/\">   *</a>");
            WriteLineLogger((b21 - b11) + "\n");
            WriteLineLogger(d7.solve);
            
            
            var c11 = d1.matrix + d2.matrix - d4.matrix + d7.matrix;
            var c12 = d4.matrix + d6.matrix;
            var c21 = d5.matrix + d7.matrix;
            var c22 = d1.matrix + d3.matrix - d5.matrix + d6.matrix;
          
            WriteLineLogger("______________C11____________");
            WriteLineLogger(c11.ToString());            
            WriteLineLogger("______________C12____________");
            WriteLineLogger(c12.ToString());
            WriteLineLogger("______________C21____________");
            WriteLineLogger(c21.ToString());
            WriteLineLogger("______________C22____________");
            WriteLineLogger(c22.ToString());           
            WriteLineLogger("_____________________________\n");

            WriteLineLogger("<b>Ответ</b>:\n");

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (j < 2 && i < 2)
                    {
                        WriteLogger($"{c11[i, j]}   ");
                    }
                    else if (j > 1 && i < 2)
                    {
                        WriteLogger($"{c12[i, j - 2]}   ");
                    }
                    else if (j < 2 && i > 1)
                    {
                        WriteLogger($"{c21[i - 2, j]}   ");
                    }
                    else if (j > 1 && i > 1)
                    {
                        WriteLogger($"{c22[i - 2, j - 2]}   ");
                    }
                }
                WriteLineLogger("\n");
            }
            
            WriteLineLogger("Cложность (L) = 49");

            return Logger;

        }
        public static string GetSolve(Matrix aMatrix, Matrix bMatrix)
        {
            int[,] A = new int [4, 4];
            int[,] B = new int [4, 4];
            int[,] A11 = new int[2, 2];
            int[,] A12 = new int[2, 2];
            int[,] A21 = new int[2, 2];
            int[,] A22 = new int[2, 2];
            int[,] B11 = new int[2, 2];
            int[,] B12 = new int[2, 2];
            int[,] B21 = new int[2, 2];
            int[,] B22 = new int[2, 2];
            int[,] C11 = new int[2, 2];
            int[,] C12 = new int[2, 2];
            int[,] C21 = new int[2, 2];
            int[,] C22 = new int[2, 2];
            int[,] D1 = new int[2, 2];
            int[,] D2 = new int[2, 2];
            int[,] D3 = new int[2, 2];
            int[,] D4 = new int[2, 2];
            int[,] D5 = new int[2, 2];
            int[,] D6 = new int[2, 2];
            int[,] D7 = new int[2, 2];
            int[,] temp1 = new int[2, 2];
            int[,] temp2 = new int[2, 2];
            //WriteLineLogger("Для матриц, где n = 4");
            //WriteLineLogger("A:");
/*            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    A[i, j] = int.Parse(Console.ReadLine());
                    if (j < 2 && i < 2)
                    {
                        A11[i, j] = A[i, j];
                    } else if (j > 1 && i < 2)
                    {
                        A12[i, j-2] = A[i, j];+
                    } else if (j < 2 && i > 1)
                    {
                        A21[i-2, j] = A[i, j];
                    } else if (j > 1 && i > 1)
                    {
                        A22[i-2, j-2] = A[i, j];
                    }
                }
            }*/

            A = aMatrix.ToIntMass();
            
            A11 = aMatrix.Exclude(aMatrix.Size - 1, aMatrix.Size - 1)
                .ToIntMass();


            A11 = aMatrix.Exclude(aMatrix.Size - 1, aMatrix.Size - 1).Exclude(aMatrix.Size - 2, aMatrix.Size - 2)
                .ToIntMass();

            A12 = aMatrix.Exclude(aMatrix.Size - 1, 0).Exclude(aMatrix.Size - 2, 0)
                .ToIntMass();

            A21 = aMatrix.Exclude(0, aMatrix.Size - 1).Exclude(0, aMatrix.Size - 2)
                .ToIntMass();

            A22 = aMatrix.Exclude(0, 0).Exclude(0, 0).ToIntMass();
            
            
            /*WriteLineLogger("B:");
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    B[i, j] = int.Parse(Console.ReadLine());
                    if (j < 2 && i < 2)
                    {
                        B11[i, j] = B[i, j];
                    }
                    else if (j > 1 && i < 2)
                    {
                        B12[i, j - 2] = B[i, j];
                    }
                    else if (j < 2 && i > 1)
                    {
                        B21[i - 2, j] = B[i, j];
                    }
                    else if (j > 1 && i > 1)
                    {
                        B22[i - 2, j - 2] = B[i, j];
                    }
               }
            }*/

            B = bMatrix.ToIntMass();

            B11 = bMatrix.Exclude(bMatrix.Size - 1, bMatrix.Size - 1).Exclude(bMatrix.Size - 2, bMatrix.Size - 2)
                .ToIntMass();

            B12 = bMatrix.Exclude(bMatrix.Size - 1, 0).Exclude(bMatrix.Size - 2, 0)
                .ToIntMass();

            B21 = bMatrix.Exclude(0, bMatrix.Size - 1).Exclude(0, bMatrix.Size - 2)
                .ToIntMass();

            B22 = bMatrix.Exclude(0, 0).Exclude(0, 0).ToIntMass();
            
            
            temp1 = plus(A11, A22);
            temp2 = plus(B11, B22);
            D1 = d(plus(A11, A22), plus(B11, B22));
            temp1 = minus(A12, A22);
            temp2 = plus(B21, B22);
            D2 = d(temp1, temp2);
            temp1 = minus(A21, A11);
            temp2 = plus(B11, B12);
            D3 = d(temp1, temp2);
            temp1 = plus(A11, A12);
            D4 = d(temp1, B22);
            temp1 = plus(A21, A22);
            D5 = d(temp1, B11);
            temp1 = minus(B12, B22);
            D6 = d(A11, temp1);
            temp1 = minus(B21, B11);
            D7 = d(A22, temp1);

            C11 = plus(minus(plus(D1,D2),D4),D7);
            C12 = plus(D4, D6);
            C21 = plus(D5, D7);
            C22 = plus(minus(plus(D1, D3), D5), D6);

            WriteLineLogger("------------------------");

            WriteLineLogger("D1");
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    WriteLogger($"{D1[i, j]} ");
                }
                WriteLineLogger();
            }

            WriteLineLogger("------------------------");

            WriteLineLogger("D2");
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    WriteLogger($"{D2[i, j]} ");
                }
                WriteLineLogger();
            }

            WriteLineLogger("------------------------");

            WriteLineLogger("D3");
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    WriteLogger($"{D3[i, j]} ");
                }
                WriteLineLogger();
            }
            WriteLineLogger("------------------------");

            WriteLineLogger("D4");
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    WriteLogger($"{D4[i, j]} ");
                }
                WriteLineLogger();
            }
            WriteLineLogger("------------------------");

            WriteLineLogger("D5");
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    WriteLogger($"{D5[i, j]} ");
                }
                WriteLineLogger();
            }
            WriteLineLogger("------------------------");

            WriteLineLogger("D6");
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    WriteLogger($"{D6[i, j]} ");
                }
                WriteLineLogger();
            }
            WriteLineLogger("------------------------");

            WriteLineLogger("D7");
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    WriteLogger($"{D7[i, j]} ");
                }
                WriteLineLogger();
            }
            WriteLineLogger("------------------------");

            WriteLineLogger("C11");
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    WriteLogger($"{C11[i,j]} ");
                }
                WriteLineLogger();
            }

            WriteLineLogger("------------------------");
            WriteLineLogger("C12");
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    WriteLogger($"{C12[i, j]} ");
                }
                WriteLineLogger();
            }

            WriteLineLogger("------------------------");
            WriteLineLogger("C21");
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    WriteLogger($"{C21[i, j]} ");
                }
                WriteLineLogger();
            }

            WriteLineLogger("------------------------");
            WriteLineLogger("C22");
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    WriteLogger($"{C22[i, j]} ");
                }
                WriteLineLogger();
            }

            WriteLineLogger("------------------------");

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (j < 2 && i < 2)
                    {
                        WriteLogger($"{C11[i, j]} ");
                    }
                    else if (j > 1 && i < 2)
                    {
                        WriteLogger($"{C12[i, j - 2]} ");
                    }
                    else if (j < 2 && i > 1)
                    {
                        WriteLogger($"{C21[i - 2, j]} ");
                    }
                    else if (j > 1 && i > 1)
                    {
                        WriteLogger($"{C22[i - 2, j - 2]} ");
                    }
                }
                WriteLineLogger();
            }
            
            WriteLineLogger("Cложность (L) = 49");

            return Logger;
        }
    }
}