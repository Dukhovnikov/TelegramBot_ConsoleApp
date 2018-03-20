using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Runtime.InteropServices.ComTypes;

namespace TelegramBot_ConsoleApp
{
    public class CriterionsForDivisibility
    {
        public string Logger { get; private set; }

        private readonly string _n;
        private readonly string _d;

        public CriterionsForDivisibility(string n, string d)
        {
            _n = n;
            _d = d;
            
            Logger = string.Empty;
        }        
        public CriterionsForDivisibility(int n, int d)
        {
            _n = n.ToString();
            _d = d.ToString();
            
            Logger = string.Empty;
        }

        public void GetPaskalSolve()
        {
            var decomposedN = new DecomposedNumber(_n);
            var countIteration = 1;

            Logger += "<code>Признак делимости Паскаля</code>\n\n";
            //Logger += "<pre>Признак делимости Паскаля</pre>\n\n";
            //Logger += "<a href=\"https://ru.wikipedia.org/wiki/Признак_Паскаля\">Признак делимости Паскаля</a>\n\n";
            Logger += $"Число: {decomposedN.Value()}\n";
            Logger += $"Делимое: {_d}\n";
            
            Logger += "___________________________\n";
            Logger += $"<i>Итерация {countIteration}</i>: {decomposedN.Value()}/{_d}\n\n";
            Logger += $"N = {decomposedN.Value()} = {decomposedN}\n\n";
            Logger += $"R = {decomposedN.GetPowerResidue(_d)} = {decomposedN.Value()}\n";

            int flag;
            do
            {
                countIteration++;
                
                flag = decomposedN.Value();
                Logger += "___________________________\n";
                Logger += $"<i>Итерация {countIteration}</i>: {decomposedN.Value()}/{_d}\n\n";
                Logger += $"N = {decomposedN.Value()} = {decomposedN}\n\n";
                Logger += $"R = {decomposedN.GetPowerResidue(_d)} = {decomposedN.Value()}\n";

            } while (flag != decomposedN.Value() && flag != 0);

            Logger += "___________________________\n";

            if (decomposedN.Value().Equals(int.Parse(_d)) || flag.Equals(0))
            {
                Logger += "\nОтвет: <b>делится</b>\n";
            }
            else
            {
                Logger += "\nОтвет: <b>не делится</b>\n";
            }

            //return Logger;
        }

        public string GetRachinskiyFirstSolve()
        {
            var n = int.Parse(_n); // Число
            var d = int.Parse(_d); // Делимое
            
            var a = 0;
            var b = 0;
            var m = 0;
            var k = 0;
            var n1 = 0;
            
            var countIteration = 1;
            var solveString = string.Empty;

            solveString += "<code>Признак делимости Рачинского №1</code>\n\n";
            solveString += $"Число: {n}\n";
            solveString += $"Делимое: {d}\n";            

            while(n > d && n1 != n)
            {
                solveString += "___________________________\n";
                solveString += $"<i>Итерация {countIteration}</i>: {n}/{d}\n\n";
                countIteration++;
                
                k = n % 10;
                m = (n - k) / 10;
                b = d % 10;
                a = (d - b) / 10;
                n1 = n;
                n = m * b - a * k;
                solveString += $"{m} * {b} - {a} * {k} = {n}\n";
            }
            
            solveString += "___________________________\n";
            
            if (n < d && n != 0)
            {
                solveString += "\nОтвет: <b>не делится</b>\n";
            }
            else
            {
                solveString += "\nОтвет: <b>делится</b>\n";
            }

            return solveString;
        }

        public string GetRachinskiySecondSolve()
        {
            var n = int.Parse(_n); // Число
            var d = int.Parse(_d); // Делимое
            
            var b = 0;
            var m = 0;
            var k = 0;
            var q = 0;
            var n1 = 0;
            
            var countIteration = 1;
            var solveString = string.Empty;
            
            solveString += "<code>Признак делимости Рачинского №2</code>\n\n";
            solveString += $"Число: {n}\n";
            solveString += $"Делимое: {d}\n";  
            
            while (n > d && n1 != n)
            {
                solveString += "___________________________\n";
                solveString += $"<i>Итерация {countIteration}</i>: {n}/{d}\n\n";
                countIteration++;
                
                k = n % 10;
                m = (n - k) / 10;
                b = d % 10;
                if (b == 1 || b == 9)
                {
                    b = 10 - b;
                }
                q = (d * b + 1) / 10;
                n1 = n;
                n = Math.Abs(m + k * q);

                solveString += $"q = ({d} * {b} + 1) / 10\n";
                solveString += $"{m} + {k} * {q} = {n}\n";
            }
            
            solveString += "___________________________\n";
            
            if (n < d && n != 0)
            {
                solveString += "\nОтвет: <b>не делится</b>\n";
            }
            else
            {
                solveString += "\nОтвет: <b>делится</b>\n";
            }

            return solveString;
        }

        public string GetRachinskiyThirdSolve()
        {
            var n = int.Parse(_n); // Число
            var d = int.Parse(_d); // Делимое
            
            var b = 0;
            var m = 0;
            var k = 0;
            var q = 0;
            var q1 = 0;
            var n1 = 0;
            
            var countIteration = 1;
            var solveString = string.Empty;
            
            solveString += "<code>Признак делимости Рачинского №3</code>\n\n";
            solveString += $"Число: {n}\n";
            solveString += $"Делимое: {d}\n"; 
            
            while (n > d && n1 != n)
            {
                solveString += "___________________________\n";
                solveString += $"<i>Итерация {countIteration}</i>: {n}/{d}\n\n";
                countIteration++;
                
                k = n % 10;
                m = (n - k) / 10;
                b = d % 10;
                if (b == 1 || b == 9)
                {
                    b = 10 - b;
                }
                q = (d * b + 1) / 10;
                q1 = Math.Abs(d - q);
                n1 = n;
                n = Math.Abs(m - k * q1);

                solveString += $"q = ({d} * {b} + 1) / 10 = {q}\n";
                solveString += $"q* = |{d} - {q}| = {q1}\n";
                solveString += $"{m} - {k} * {q1} = {n}\n";
            }
            
            solveString += "___________________________\n";
            
            if (n < d && n != 0)
            {
                solveString += "\nОтвет: <b>не делится</b>\n";
            }
            else
            {
                solveString += "\nОтвет: <b>делится</b>\n";
            }

            return solveString;
        }

        #region OldestCode

/*        public string GetRachinskiyFirstSolve()
        {
            var n = int.Parse(_n); // Число
            var d = int.Parse(_d); // Делимое
            
            var a = 0;
            var b = 0;
            var m = 0;
            var k = 0;
            var n1 = 0;
            
            var countIteration = 1;
            var solveString = string.Empty;

            solveString += "<code>Признак делимости Рачинского №1</code>\n\n";
            solveString += $"Число: {n}\n";
            solveString += $"Делимое: {d}\n";            

            while(n > d && n1 != n)
            {
                solveString += "___________________________\n";
                solveString += $"<i>Итерация {countIteration}</i>: {n}/{d}\n\n";
                countIteration++;
                
                k = n % 10;
                m = (n - k) / 10;
                b = d % 10;
                a = (d - b) / 10;
                n1 = n;
                n = m * b - a * k;
                solveString += $"{m} * {b} - {a} * {k} = {n}\n";
            }
            
            solveString += "___________________________\n";
            
            if (n < d && n != 0)
            {
                solveString += "\nОтвет: <b>не делится</b>\n";
            }
            else
            {
                solveString += "\nОтвет: <b>делится</b>\n";
            }

            return solveString;
        }*/
        
        /*public static string GetPaskalSolve(string N, string D)
        {
            var decomposedN = new DecomposedNumber(N);
            
            var stringsOfList = new List<string>()
            {
                $"N = {decomposedN}",
                $"R = {decomposedN.GetPowerResidue(D)} = {decomposedN.Value()}"
            };
            
            

            int flag;
            do
            {
                flag = decomposedN.Value();
                stringsOfList.Add($"N = {decomposedN}");
                stringsOfList.Add($"R = {decomposedN.GetPowerResidue(D)} = {decomposedN.Value()}");

            } while (flag != decomposedN.Value() && flag != 0);

            if (decomposedN.Value().Equals(int.Parse(D)) || flag.Equals(0))
            {
                stringsOfList.Add("Делится");
            }
            else
            {
                stringsOfList.Add("Не делится");
            }
            
            return stringsOfList;
        }*/

        #endregion
        
    }
}