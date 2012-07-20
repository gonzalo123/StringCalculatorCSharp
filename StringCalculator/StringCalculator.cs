using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringCalculator
{
    public class StringCalculator
    {
        private string separator = ",";
        private string myString;
        private const int maxNumber = 1000;
        private List<string> separators = new List<string>() {"\n"};

        public int Add(string numbers)
        {
            if (numbers == "") return 0;
            myString = numbers;
            if (hasCustomSeparator())
            {
                appendToSeparators(hasMultibyteSeparator());
                myString = getNewString();
            }

            unifySeparators();
            if (myString.IndexOf(separator) > 0)
                return processStingWithSeparator();
            else
                return Convert.ToInt32(myString);
        }

        private int processStingWithSeparator()
        {
            string[] pieces = splitInput(myString, separator);
            int output = 0;
            foreach (string piece in pieces)
            {
                int value = Convert.ToInt32(piece);
                if (value < 0) throw new ArgumentException("negatives not allowed");
                if (value < maxNumber) output += value;
            }
            return output;
        }

        private void unifySeparators()
        {
            foreach (string _separator in separators) 
                myString = myString.Replace(_separator, separator);
        }

        private static string[] splitInput(string numbers, string separator)
        {
            return numbers.Split(new string[] { separator }, StringSplitOptions.None);
        }

        private string getNewString()
        {
            int start = myString.IndexOf("\n") + 1;
            int length = myString.Length - start;
            return myString.Substring(start, length);
        }

        private void appendToSeparators(bool withMultibyteSeparator = false)
        {
            int start = withMultibyteSeparator ? 3 : 2;
            string endString = withMultibyteSeparator ? "]\n" : "\n";
            string mySeparator = myString.Substring(start, myString.IndexOf(endString) - start);

            if (hasMultipleSeparators(mySeparator))
            {
                foreach (string multipleSeparatorElement in getMultipleSeparators(mySeparator))
                {
                    separators.Add(multipleSeparatorElement);
                }
            }
            else
            {
                separators.Add(mySeparator);
            }
        }

        private string[] getMultipleSeparators(string mySeparator)
        {
            return mySeparator.Split(new string[] { "][" }, StringSplitOptions.None);
        }

        private bool hasMultipleSeparators(string mySeparator)
        {
            return mySeparator.IndexOf("][") >= 0;
        }

        private bool hasCustomSeparator()
        {
            return myString.IndexOf("//") >= 0;
        }

        private bool hasMultibyteSeparator()
        {
            return myString.IndexOf("//[") >= 0;
        }
    }
}
