using System;
using System.Linq;
using System.Text;

namespace B18_Ex01_04
{
    class Program
    {

        public enum checkValid { onlyNumbers , onlyLetters, notValid}

        public static void Main()
        {
            string userInput = Console.ReadLine();
            int inputStatus = (int)checkValid.notValid;

            while (inputStatus == (int)checkValid.notValid)
            {
                Console.WriteLine("Invalid Input, try again . . .\n");
                userInput = Console.ReadLine();
                inputStatus = checkInput(userInput); 
            }

            string stringAnalysis = getStringAnalysis(userInput , inputStatus == (int)checkValid.onlyNumbers);

            Console.WriteLine(stringAnalysis);

        }

        private static int checkInput(string i_userInput)
        {
            int userNumber;

            if(int.TryParse(i_userInput, out userNumber)) // it's a number
            {
                return (int) checkValid.onlyNumbers;
            }
            else if (i_userInput.All(char.IsDigit))
            {
                return (int)checkValid.onlyLetters;
            }
            else
            {
                return (int) checkValid.notValid;
            }
        }

        private static string getStringAnalysis(string i_userInput, bool isNumber)
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


            if (isNumber) // it's a number
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
            else // it's a string
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
