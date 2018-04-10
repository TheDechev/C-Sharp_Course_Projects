using System;
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
            Console.WriteLine("Please enter a word <8 characters>:");
            string userInputAnalysis = getValidUserInput();
            Console.WriteLine(userInputAnalysis);
        }

        private static string getValidUserInput()
        {
            string userInput = Console.ReadLine();
            stringStatus inputStatus = checkInput(userInput);

            while (inputStatus == stringStatus.notValid)
            {
                Console.WriteLine("Invalid Input, try again . . .");
                userInput = Console.ReadLine();
                inputStatus = checkInput(userInput);
            }

            string stringAnalysis = getStringAnalysis(userInput, inputStatus == stringStatus.onlyNumbers);

            return stringAnalysis;
        }

        private static stringStatus checkInput(string i_UserInput)
        {
            int userNumber;
            stringStatus inputStatus;

            if (int.TryParse(i_UserInput, out userNumber)) 
            {
                inputStatus = stringStatus.onlyNumbers;
            }
            else if (!isThereADigit(i_UserInput)) 
            {
                inputStatus =  stringStatus.onlyLetters;
            }
            else 
            {
                inputStatus = stringStatus.notValid;
            }

            return inputStatus;
        }

        private static bool isThereADigit(string i_UserInput)
        {
            foreach (char currentChar in i_UserInput)
            {
                if (currentChar >= '1' && currentChar <= '9')
                {
                    return true;
                }
            }
            return false;
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
            int leftIndex = 0;
            int rightIndex = i_UserInput.Length - 1;

            while(leftIndex <= rightIndex)
            {
                if(i_UserInput[leftIndex] != i_UserInput[rightIndex])
                {
                    return false;
                }
                leftIndex++;
                rightIndex--;
            }
            return true;
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
