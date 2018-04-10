using System;

namespace B18_Ex01_01
{
    public class Program
    {
        public static void Main()
        {
            int totalNumberSum;
            short powerOfTwoCounter, decreasingSequenceCounter, oneDigitCounter;
            string theThreeNumbers;

            getInputStatistics(out decreasingSequenceCounter, out oneDigitCounter, out powerOfTwoCounter, out totalNumberSum, out theThreeNumbers);
            printInputStatistics(oneDigitCounter, totalNumberSum, decreasingSequenceCounter, powerOfTwoCounter, theThreeNumbers);
        }

        private static void getInputStatistics(out short o_DecreasingSequenceCounter, out short o_OneDigitCounter, out short o_PowerOfTwoCounter, out int o_TotalNumberSum, out string o_TheThreeNumbers)
        {
            string userInput;
            int binaryNumber, decimalNumber;
            o_TotalNumberSum = 0;
            o_DecreasingSequenceCounter = 0;
            o_OneDigitCounter = 0;
            o_PowerOfTwoCounter = 0;
            o_TheThreeNumbers = "";

            Console.WriteLine("Please enter 3 binary numbers ( 9 digits each ): ");
            for (int i = 0; i < 3; i++)
            {
                userInput = Console.ReadLine();

                if (!isInputValid(userInput))
                {
                    Console.WriteLine("Invalid Input! Try again . . .");
                    i--;
                    continue;
                }

                binaryNumber = int.Parse(userInput);
                o_OneDigitCounter += getNumberOfOnes(binaryNumber);
                decimalNumber = convertBinaryToDec(binaryNumber);
                o_DecreasingSequenceCounter += isDecreasingSequence(decimalNumber);
                o_TheThreeNumbers += " " + decimalNumber.ToString();
                o_PowerOfTwoCounter += isPowerOfTwo(binaryNumber);
                o_TotalNumberSum += decimalNumber;
            }
        }

        private static int convertBinaryToDec(int binaryNumber)
        {
            int decimalNum = 0, currDig;
            double currPow = 0;

            while (binaryNumber != 0)
            {
                currDig = binaryNumber % 10;
                decimalNum += (int)Math.Pow(2.0, currPow) * currDig;   
                currPow++;
                binaryNumber /= 10;                            
            }

            return decimalNum;
        }

        private static void printInputStatistics(short i_OneDigitCounter, int i_TotalNumberSum, short i_DecreasingSequenceCounter, short i_PowerOfTwoCounter, string i_TheThreeNumbers)
        {
            string statisticsMessage = string.Format(
@"
The numbers are:{7}
{0} zeroes is: {2:.00} 
{0} ones is: {3:.00}
{1} power of two: {4}
{1} a decreasing sequence: {5} 
The total average is: {6:00.00}",
                "The total average number of", 
                "Numbers that are", 
                (float)((9 * 3) - i_OneDigitCounter) / 3, 
                (float)i_OneDigitCounter / 3, 
                i_PowerOfTwoCounter, 
                i_DecreasingSequenceCounter, 
                (float)i_TotalNumberSum / 3, 
                i_TheThreeNumbers);
            Console.WriteLine(statisticsMessage);
        }

        private static short getNumberOfOnes(int i_CurrentNumber)
        {
            int currentDigit = 0;
            short numOfOnes = 0;
            while (i_CurrentNumber != 0)
            {
                currentDigit = i_CurrentNumber & 1;
                i_CurrentNumber /= 10;
                if (currentDigit == 1)
                {
                    numOfOnes++;
                }
            }

            return numOfOnes;
        }

        private static short isPowerOfTwo(int i_CurrentNumber)
        {
            int x = i_CurrentNumber & i_CurrentNumber - 1;
            if (x == 0)
            {
                return 1; //represents true
            }

            return 0; //represents false
        }

        private static short isDecreasingSequence(int i_CurrentNumber)
        {
            int previousDigit, currentDigit;
            previousDigit = i_CurrentNumber % 10;
            i_CurrentNumber /= 10;
            while(i_CurrentNumber > 0)
            {
                currentDigit = i_CurrentNumber % 10;
                i_CurrentNumber /= 10;

                if (!(currentDigit > previousDigit))
                { 
                    return 0; //represents false
                }

                previousDigit = currentDigit;
            }

            return 1; //represents true
        }

        private static bool isInputValid(string i_CurrentNumber)
        {
            int inputSize = i_CurrentNumber.Length;

            if (inputSize == 9)
            {
                foreach (char currentChar in i_CurrentNumber)
                {
                    if (!(currentChar == '0' || currentChar == '1'))
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}
