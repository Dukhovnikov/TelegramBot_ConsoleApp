using System;
using System.Data.SqlTypes;
using System.Linq;

namespace TelegramBot_ConsoleApp
{
    public class DecomposedNumber : INullable
    {
        private int[] Number { get; set; }

        public DecomposedNumber(string number)
        {
            try
            {
                Number = new int[number.Length];

                for (var i = 0; i < number.Length; i++)
                {
                    Number[i] = (int) char.GetNumericValue(number[i]);
                }
            }
            catch (Exception e)
            {
                Number = null;
            }

        }

        public DecomposedNumber(int number) : this(number.ToString())
        {
            
        }

        public int Value()
        {
            var number = Number.Aggregate(string.Empty, (current, t) => current + t.ToString());

            return int.Parse(number);
        }
        
        public string GetPowerResidue(string d)
        {
            var rezultString = string.Empty;
            var rezultInt = 0;
            
            for (var i = 0; i < Number.Length; i++)
            {
                var riseToPower = 1;
                
                for (var j = 0; j < Number.Length - i - 1; j++)
                {
                    riseToPower *= 10;
                }

                var powerResidue = riseToPower % int.Parse(d);
               
                rezultString += Number[i] + "*" + powerResidue;
                rezultInt += Number[i] * powerResidue;
                
                if (i != Number.Length - 1)
                    rezultString += (" + ");
            }

            Number = new DecomposedNumber(rezultInt.ToString()).Number;
            
            return rezultString;
        }

        public override string ToString()
        {
            var rezultString = string.Empty;

            for (var i = 0; i < Number.Length; i++)
            {
                rezultString += Number[i] + "*10^" + (Number.Length - i - 1);
                
                if (i != Number.Length - 1)
                    rezultString += (" + ");
            }

            return rezultString;
        }

        public bool IsNull => Number == null;
    }
}