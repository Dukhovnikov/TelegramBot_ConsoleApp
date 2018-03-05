using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TelegramBot_ConsoleApp
{
    public class Paskal
    {
        private string N { get; set;  }
        private string D { get; set; }
        
        public Paskal(string n, string d)
        {
            this.N = n;
            this.D = d;
        }
        
        public IEnumerable<string> GetSolve()
        {
            var listOfString = new List<string>
            {
                $"n = {int.Parse(N)}",
                $"d = {int.Parse(D)}\n",
                $"{N} = {RazlozhenieChisla(N)}\n",
                $"r = {PeremnojenieVichetaAndChisla(N, D)}"
            };

            return listOfString;

        }

        public static string RazlozhenieChisla(string number)
        {
            var razlozhenie = string.Empty;
            
            for (var i = 0; i < number.Length; i++)
            {
                razlozhenie += (number[i] + " * 10^" + (number.Length - i - 1));
                
                if (i != number.Length - 1) 
                    razlozhenie += (" + ");
            }

            return razlozhenie;
        }

        public static string PeremnojenieVichetaAndChisla(string n, string d)
        {
            var nAsInt = ConvertToMass(n); // 4 0 8 8
            var r = StepennieVicheti(n, d); // 51 27 10 1

            var rezString = string.Empty;
            var rezInt = 0;

            for (var i = 0; i < nAsInt.Length; i++)
            {
                rezString += (nAsInt[i] + " * " + r[i]);
                rezInt += nAsInt[i] * r[i];
                
                if (i != nAsInt.Length - 1)
                    rezString += (" + ");
                else
                    rezString += ($" = {rezInt}");
            }

            return rezString;
        }
        
        public static int[] StepennieVicheti(string n, string d)
        {
            var stepennoiVichetAsMass = new int[n.Length];

            for (var i = 0; i < n.Length; i++)
            {
                var riseToPower = 1;
                
                for (var j = 0; j < n.Length - i - 1; j++)
                {
                    riseToPower *= 10;
                }

                stepennoiVichetAsMass[i] = riseToPower % int.Parse(d);
            }

            return stepennoiVichetAsMass;
        }
        
        public static int[] ConvertToMass(string number)
        {
            var numberAsMass = new int[number.Length];

            for (var i = 0; i < number.Length; i++)
            {
                numberAsMass[i] = int.Parse(number[i].ToString());
            }

            return numberAsMass;
        }
    }
}