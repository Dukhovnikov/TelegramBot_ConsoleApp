using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TelegramBot_ConsoleApp;

namespace TelegramBot_ConsoleApp
{
    public enum TypeMethod
    {
        AmericanDeterminant,
        DivisibilityCriteria,
        VolkerStrassenAlhoritm,
        Karatsuba,
        Default 
        
    }
    
    public class Executor
    {
        private Dictionary<TypeMethod, Func<string, string>> MethoDictionary { get; set; } =
            new Dictionary<TypeMethod, Func<string, string>>()
            {
                {TypeMethod.AmericanDeterminant, OnGetDeterminantOfAmericanMethod},
                {TypeMethod.Default, GetDefault}
                
            };
        
        public Func<string, string> GetSolveFunc { get; set; }
        
        public static string OnGetDeterminantOfAmericanMethod(string data)
        {
            var matrix = new Matrix(data);

            if (((INullable) matrix).IsNull)
            {
                return "Error! An invalid string was specified.\n\n/keyboard - назад\n";
            }
            
            var determinantOfAmericanMethod = new DeterminantOfAmericanMethod(matrix);
            determinantOfAmericanMethod.GetDeterminant();

            return determinantOfAmericanMethod.Logger + "\n/keyboard - назад\n";
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

                return criterionDivibility.Logger + "\n/keyboard - назад\n";

            }
            catch (Exception e)
            {
                return "Error! An invalid string was specified.\n\n/keyboard - назад\n";
            }
        }

        public static string GetDefault(string data)
        {
            return "Take your pick!\n";
        }
        

        public Executor(TypeMethod typeMethod)
        {
            GetSolveFunc = MethoDictionary[typeMethod];
        }
    }
}