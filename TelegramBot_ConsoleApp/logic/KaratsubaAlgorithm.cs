//
// Karatsuba multiplication algorithm
//
//            +------+------+
//            |  x1  |  x0  |
//            +------+------+
//                   *
//            +------+------+
//            |  y1  |  y0  |
//            +------+------+
//                   =
//     +-------------+-------------+
//  +  |    x1*y1    |    x0*y0    |
//     +----+-+------+------+------+
//          . .             .
//          . .             .
//          +-+------+------+
//       +  | x0*y1 + x1*y0 |
//          +-+------+------+
//

using System;

namespace TelegramBot_ConsoleApp
{
    
    
    public class KaratsubaAlgorithm
    {
        private int A { get; set; }
        private int B { get; set; }

        public KaratsubaAlgorithm(int a, int b)
        {
            A = a;
            B = b;
        }

        public string GetSolve()
        {
            var solve = GetSolveRec(A, B, A.ToString().Length);
            return solve;
        }

        public static string GetSolveRec(int a, int b, int n)
        {
            var m = (int)Math.Pow(10, n / 2);
            var a2 = a % m;
            var a1 = (a - a2) / m;
            var b2 = b % m;
            var b1 = (b - b2) / m;

            var solveString = string.Empty;
            solveString += "___________________________\n\n";

            if (n <= 2)
            {
                var solve = a1 * b1 * Math.Pow(10, n) + (a1 * b2 + b1 * a2) * Math.Pow(10, n / 2) + b2 * a2;
                //solveString += $"{a} * {b} = {a1} * {b1} * 10^{n} + ({a1} * {b2} + {b1} * {a2}) * 10^{n / 2} + {a2} * {b2} = ";
                solveString += $"<b>{a}</b> * <b>{b}</b> = {a1} * {b1} * 10^{n} + ({a1} * {b2} + {b1} * {a2}) * 10^{n / 2} + {a2} * {b2} = {solve}\n";
                return solveString;
                
                //Console.WriteLine("{0} * {1} * 10 ^ {2} + ({0} * {3} + {1} * {4}) * 10 ^ {5} + {4} * {3} =", a1, b1, n, b2, a2, n / 2);
                //Console.WriteLine("{0}", a1 * b1 * Math.Pow(10, n) + (a1 * b2 + b1 * a2) * Math.Pow(10, n / 2) + b2 * a2);
            } 
            else
            {
                var solve = a1 * b1 * Math.Pow(10, n) + a2 * b2 +
                            ((a1 + a2) * (b1 + b2) - a1 * b1 - a2 * b2) * Math.Pow(10, n / 2);
                
                solveString += $"<b>{a}</b> * <b>{b}</b> = {a1} * {b1} * 10^{n} + ({a1} * {b2} + {b1} * {a2}) * 10^{n / 2} + <a href=\"http://www.example.com/\">{a2} * {b2}</a> = \n";
                
                solveString +=
                    $"<a href=\"http://www.example.com/\">{a1} * {b1}</a> * 10^{n} + {a2} * {b2} + (<a href=\"http://www.example.com/\">({a1} + {a2}) * ({b1} + {b2})</a> - {a1} * {b1} - {a2} * {b2}) * 10^{n / 2} = \n{solve}\n";

                solveString += GetSolveRec(a1, b1, n / 2);
                solveString += GetSolveRec(a2, b2, n / 2);
                solveString += GetSolveRec(a1 + a2, b1 + b2, n / 2);
            }

            return solveString;
        }

/*        public string GetSolve(int a, int b)
        {
            
        }*/
    }
}