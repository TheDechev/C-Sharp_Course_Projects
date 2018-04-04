using System;
using System.Linq;
using System.Text;

namespace B18_Ex01_04
{
    public class Program
    {
        public enum stringStatus
        {
            onlyNumbers, onlyLetters, notValid
        }

        public static void Main()
        {
            string userInputAnalysis = getValidUserInput();
            Console.WriteLine(userInputAnalysis);
        }

        private static string getValidUserInput()
        {
            string userInput = Console.ReadLine();
            stringStatus inputStatus = checkInput(userInput);

            while (inputStatus == stringStatus.notValid)
            {
                Console.WriteLine("Invalid Input, try again . . .\n");
                userInput = Console.ReadLine();
                inputStatus = checkInput(userInput);
            }

            string stringAnalysis = getStringAnalysis(userInput, inputStatus == stringStatus.onlyNumbers);

            return stringAnalysis;
        }

        private static stringStatus checkInput(string i_UserInput)
        {
            int userNumber;

            if (int.TryParse(i_UserInput, out userNumber)) 
            {
                return stringStatus.onlyNumbers;
            }
            else if (!i_UserInput.Any(char.IsDigit)) 
            {
                return stringStatus.onlyLetters;
            }
            else 
            {
                return stringStatus.notValid;
            }
        }

        private static string getStringAnalysis(string i_UserInput, bool isNumber)
        {
            StringBuilder stringAnalysis = new StringBuilder();
  
            if (isPalindrome(i_UserInput))
            {
                stringAnalysis.AppendLine("The input is a palindrome.");
            }
            else
            {
                stringAnalysis.AppendLine("The input is NOT a palindrome.");
            }

            if (isNumber)
            {
                if (isEven(i_UserInput))
                {
                    stringAnalysis.AppendLine("The number is even.");
                }
                else
                {
                    stringAnalysis.AppendLine("The number is odd.");
                }
            }
            else 
            {
                stringAnalysis.AppendLine("Number of lowercase characters in it are: " + getLowerCaseNumber(i_UserInput));
            }

            return stringAnalysis.ToString();
        }

        private static bool isPalindrome(string i_UserInput)
        {
            return i_UserInput.SequenceEqual(i_UserInput.Reverse());
        }

        private static bool isEven(string i_UserInput)
        {
            return int.Parse(i_UserInput) % 2 == 0;
        }

        private static short getLowerCaseNumber(string i_UserInput)
        {
            short numberOfLowerCase = 0;
            string lowerUserInput = i_UserInput.ToLower();

            for(int i = 0; i < i_UserInput.Length; i++)
            {
                if (i_UserInput[i] == lowerUserInput[i])
                {
                    numberOfLowerCase++;
                }
            }
            
            return numberOfLowerCase;
        }
    }
}
