using System;
using System.Linq;
using System.Text;

namespace B18_Ex01_04
{
    class Program
    {
        public static void Main()
        {
            string userInput = Console.ReadLine();
            string stringAnalysis = getStringAnalysis(userInput);

            Console.WriteLine(stringAnalysis);

        }

        private static string getStringAnalysis(string i_userInput)
        {
            
            int userNumber;
            StringBuilder stringAnalysis = new StringBuilder();

            
            if (isPalindrome(i_userInput))
            {
                stringAnalysis.AppendLine("The input is a palindrome.");
            }
            else
            {
                stringAnalysis.AppendLine("The input is NOT a palindrome.");
            }


            if (int.TryParse(i_userInput, out userNumber)) // it's a number
            {
                if (isEven(i_userInput))
                {
                    stringAnalysis.AppendLine("The number is even.");
                }
                else
                {
                    stringAnalysis.AppendLine("The number is odd.");
                }

            }
            else // it's NOT a number
            {
                stringAnalysis.AppendLine("Number of lowercase characters is: " + getLowerCaseNumber(i_userInput));
            }

            return stringAnalysis.ToString();
        }

        private static bool isPalindrome(string i_userInput)
        {
            return i_userInput.SequenceEqual(i_userInput.Reverse());
        }

        private static bool isEven(string i_userInput)
        {
            return int.Parse(i_userInput) % 2 == 0;
        }

        private static short getLowerCaseNumber(string i_userInput)
        {
            short numberOfLowerCase = 0;
            string lowerUserInput = i_userInput.ToLower();

            for(int i = 0; i < i_userInput.Length ; i++)
            {
                if (i_userInput[i] == lowerUserInput[i])
                {
                    numberOfLowerCase++;
                }
            }
            
            return numberOfLowerCase;
        }

    }
}
