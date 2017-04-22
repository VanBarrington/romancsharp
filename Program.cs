using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace romancsharp
{
    class Program
    {
        static class ArgPositions
        {
            public static int Value { get { return 0; } }
        }

        private static readonly Dictionary<int, char> IntegerToRomanMap = new Dictionary<int, char>
        {
            [1] = 'I',
            [5] = 'V',
            [10] = 'X',
            [50] = 'L',
            [100] = 'C',
            [500] = 'D',
            [1_000] = 'M'
        };

        private static readonly Dictionary<char, int> RomanToIntegerMap = IntegerToRomanMap.
            ToDictionary(
                mapping => mapping.Value,
                mapping => mapping.Key
            );

        static void Main(string[] args)
        {
            var input = args[ArgPositions.Value];

            var(isNumericInput, numericValue) = IsNumeric(input);

            if (isNumericInput)
            {
                var reverseOps = new[]
                {
                    new {Value = 1, Roman = "I"},
                    new {Value = 4, Roman = "IV"},
                    new {Value = 5, Roman = "V"},
                    new {Value = 9, Roman = "IX"},
                    new {Value = 10, Roman = "X"},
                    new {Value = 40, Roman = "XL"},
                    new {Value = 50, Roman = "L"},
                    new {Value = 90, Roman = "XC"},
                    new {Value = 100, Roman = "C"},
                    new {Value = 400, Roman = "CD"},
                    new {Value = 500, Roman = "D"},
                    new {Value = 900, Roman = "CM"},
                    new {Value = 1000, Roman = "M"}
                }.Reverse().ToList();

                var strOutput = "";
                while (numericValue > 0)
                {
                    for (var i = 0; i < reverseOps.Count; i++)
                    {
                        var key = reverseOps[i];
                        while (key.Value <= numericValue)
                        {
                            strOutput += key.Roman;
                            numericValue -= key.Value;
                        }
                    }
                }
                Console.Write(strOutput);
            }
            else
            {
                var output = ToInteger(input);
                Console.WriteLine(output);
            }
            Console.ReadLine();
        }

        private static int ToInteger(string value)
        {
            var aggregate = 0;
            for (var i = 0; i < value.Length; i++)
            {
                var currentValue = RomanToIntegerMap[value[i]];
                var next = i + 1;
                if (next < value.Length)
                {
                    var nextValue = RomanToIntegerMap[value[next]];
                    if (currentValue < nextValue)
                    {
                        var toAdd = nextValue - currentValue;
                        aggregate += toAdd;
                        i++;
                    }
                    else
                    {
                        aggregate += currentValue;
                    }
                }
                else
                {
                    aggregate += currentValue;
                }
            }

            return aggregate;
        }

        private static string ToRomanNumerals(int value)
        {
            throw new NotImplementedException();
        }

        private static (bool, int) IsNumeric(string input)
        {
            int value;
            return (int.TryParse(input, out value), value);
        }
    }
}