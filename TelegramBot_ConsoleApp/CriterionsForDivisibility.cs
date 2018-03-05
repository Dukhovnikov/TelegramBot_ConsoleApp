using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Runtime.InteropServices.ComTypes;

namespace TelegramBot_ConsoleApp
{
    public class CriterionsForDivisibility
    {
        public string Logger { get; private set; }
        
        private string N { get; set; }
        private string D { get; set; }

        public CriterionsForDivisibility(string n, string d)
        {
            N = n;
            D = d;
            
            Logger = string.Empty;
        }        
        public CriterionsForDivisibility(int n, int d)
        {
            N = n.ToString();
            D = d.ToString();
            
            Logger = string.Empty;
        }

        public void GetPaskalSolve()
        {
            var decomposedN = new DecomposedNumber(N);
            var countIteration = 1;

            Logger += "<code>Признак делимости Паскаля</code>\n\n";
            //Logger += "<pre>Признак делимости Паскаля</pre>\n\n";
            //Logger += "<a href=\"https://ru.wikipedia.org/wiki/Признак_Паскаля\">Признак делимости Паскаля</a>\n\n";
            Logger += $"Число: {decomposedN.Value()}\n";
            Logger += $"Делимое: {D}\n";
            
            Logger += "___________________________\n";
            Logger += $"<i>Итерация {countIteration}</i>: <em>{decomposedN.Value()}/{D}</em>\n\n";
            Logger += $"N = {decomposedN.Value()} = {decomposedN}\n\n";
            Logger += $"R = {decomposedN.GetPowerResidue(D)} = {decomposedN.Value()}\n";

            int flag;
            do
            {
                countIteration++;
                
                flag = decomposedN.Value();
                Logger += "___________________________\n";
                Logger += $"<i>Итерация {countIteration}</i>: {decomposedN.Value()}/{D}\n\n";
                Logger += $"N = {decomposedN.Value()} = {decomposedN}\n\n";
                Logger += $"R = {decomposedN.GetPowerResidue(D)} = {decomposedN.Value()}\n";

            } while (flag != decomposedN.Value() && flag != 0);

            Logger += "___________________________\n";

            if (decomposedN.Value().Equals(int.Parse(D)) || flag.Equals(0))
            {
                Logger += "Ответ: <b>делится</b>\n";
            }
            else
            {
                Logger += "Ответ: <b>не делится</b>\n";
            }

            //return Logger;
        }

        #region OldestCode

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