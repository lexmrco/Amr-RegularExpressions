using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Learning.StepValidation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese el valor del primer argumento");
            string leftArgument = Console.ReadLine();
            Console.WriteLine("Ingrese el valor del segundo argumento");
            string rightArgument = Console.ReadLine();
            Console.WriteLine("Ingrese el operador a usar");
            string op = Console.ReadLine();

            if (ConditionalStepValidate(leftArgument, rightArgument, op))
                Console.WriteLine("Se cumple la condición: SI");
            else
                Console.WriteLine("Se cumple la condición: NO");          
        }

        private static bool ConditionalStepValidate(string leftArgument, string rightArgument, string op)
        {            
            NumberStyles styles = NumberStyles.Number;
            var cultureInfo = CultureInfo.InvariantCulture;
            bool validNumber = false;
            float lFloat = 0, rFloat = 0;
            // if the first regex matches, the number string is in us culture
            if (Regex.IsMatch(leftArgument, @"^(:?[\d,]+\.)*\d+$") && Regex.IsMatch(rightArgument, @"^(:?[\d,]+\.)*\d+$"))
            {
                cultureInfo = new CultureInfo("en-US");
                validNumber = true;
            }
            // if the second regex matches, the number string is in de culture
            else if (Regex.IsMatch(leftArgument, @"^(:?[\d.]+,)*\d+$") && Regex.IsMatch(rightArgument, @"^(:?[\d.]+,)*\d+$"))
            {
                cultureInfo = new CultureInfo("es-CO");
                validNumber = true;
            }
            if(validNumber)
            {
                if (!float.TryParse(leftArgument, styles, cultureInfo, out lFloat))
                    return false;
                if (!float.TryParse(rightArgument, styles, cultureInfo, out rFloat))
                    return false;
            }
            switch (op)
            {
                case "==":
                    return leftArgument == rightArgument;
                case "!=":
                    return leftArgument != rightArgument;
                case "<=":
                    if (!validNumber) return false;                    
                    return lFloat <= rFloat;
                case ">=":
                    if (!validNumber) return false;
                    return lFloat >= rFloat;
                case "<":
                    if (!validNumber) return false;
                    return lFloat < rFloat;
                case ">":
                    if (!validNumber) return false;
                    return lFloat > rFloat;
                default:
                    return false;
            }
        }
    }
}
