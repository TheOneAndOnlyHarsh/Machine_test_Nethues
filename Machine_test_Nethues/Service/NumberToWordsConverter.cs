namespace Machine_test_Nethues.Service
{
    public class NumberToWordsConverter
    {
        private static readonly string[] UnitsMap = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        private static readonly string[] TeensMap = { "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        private static readonly string[] TensMap = { "", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
        private static readonly string[] ThousandsMap = { "", "thousand", "million", "billion" };

        public string Convert(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + Convert(-number);

            var words = "";

            int thousandCounter = 0;
            while (number > 0)
            {
                int part = number % 1000;
                if (part > 0)
                {
                    words = ConvertThreeDigits(part) + (thousandCounter > 0 ? " " + ThousandsMap[thousandCounter] : "") + " " + words;
                }
                number /= 1000;
                thousandCounter++;
            }

            return words.Trim();
        }

        private string ConvertThreeDigits(int number)
        {
            string words = "";

            if (number >= 100)
            {
                words += UnitsMap[number / 100] + " hundred ";
                number %= 100;
            }

            if (number >= 20)
            {
                words += TensMap[number / 10] + " ";
                number %= 10;
            }
            else if (number >= 10)
            {
                words += TeensMap[number - 10] + " ";
                number = 0;
            }

            if (number > 0)
            {
                words += UnitsMap[number] + " ";
            }

            return words.Trim();
        }
    }

}
