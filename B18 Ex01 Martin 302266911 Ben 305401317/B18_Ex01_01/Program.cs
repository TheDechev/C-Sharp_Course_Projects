using System;

namespace B18_Ex01_01
{
    class Program
    {
        private const short k_InputSize = 9;
        private const short k_NumberOfInputs = 3;

        public static void Main()
        {
            int totalNumberSum = 0;
            short powerOfTwoCounter = 0, decreasingSequenceCounter = 0, oneDigitCounter = 0;

            getInputStatistics(ref decreasingSequenceCounter, ref oneDigitCounter, ref powerOfTwoCounter, ref totalNumberSum);
            printInputStatistics(oneDigitCounter, totalNumberSum, decreasingSequenceCounter, powerOfTwoCounter);

        }

        private static void getInputStatistics(ref short io_decreasingSequenceCounter, ref short io_oneDigitCounter, ref short io_powerOfTwoCounter, ref int io_totalNumberSum)
        {
            string userInput;
            int binaryNumber, decimalNumber;

            for (int i = 0; i < k_NumberOfInputs; i++)
            {
                userInput = Console.ReadLine();

                if (!isInputValid(userInput))
                {
                    Console.WriteLine("Invalid Input! Try again . . .\n");
                    i--;
                    continue;
                }

                binaryNumber = int.Parse(userInput);
                io_oneDigitCounter += getNumberOfOnes(binaryNumber);

                decimalNumber = convertBinaryToDec(binaryNumber);
                io_decreasingSequenceCounter += isDecreasingSequence(decimalNumber);

                io_powerOfTwoCounter += isPowerOfTwo(binaryNumber);
                io_totalNumberSum += decimalNumber;
            }
        }

        private static int convertBinaryToDec(int binaryNumber)
        {
            int decimalNum = 0, currDig;
            double currPow = 0;

            while (binaryNumber != 0)
            {
                currDig = binaryNumber % 10;
                decimalNum += (int)Math.Pow(2.0, currPow)*currDig;   
                currPow ++;
                binaryNumber /= 10;                            
            }

            return decimalNum;
        }

        private static void printInputStatistics(short i_oneDigitCounter, int i_totalNumberSum, short i_decreasingSequenceCounter, short i_powerOfTwoCounter)
        {

            object[] args = new object[7];
            args[0] = "The average number of ";
            args[1] = "Numbers that are ";
            args[2] = (float)(k_InputSize * k_NumberOfInputs - i_oneDigitCounter) / k_NumberOfInputs;
            args[3] = (float)i_oneDigitCounter / k_NumberOfInputs;
            args[4] = i_powerOfTwoCounter;
            args[5] = i_decreasingSequenceCounter;
            args[6] = (float)i_totalNumberSum / k_NumberOfInputs;

            string statisticsMessage = String.Format(
@"
{0} zeroes is: {2:.00} 
{0} ones is: {3:.00}
{1} power of two: {4}
{1} a decreasing sequence: {5}
The total average is: {6:00.00}
", args);
            Console.WriteLine(statisticsMessage);
        }

        private static short getNumberOfOnes(int i_currentNumber)
        {
            int currentDigit = 0;
            short  numOfOnes = 0;
            while(i_currentNumber != 0)
            {
                currentDigit = i_currentNumber & 1;
                i_currentNumber /= 10;
                if (currentDigit == 1)
                {
                    numOfOnes++;
                }
            }
            return numOfOnes;
        }

        private static short isPowerOfTwo(int i_currentNumber)
        {
            int x = i_currentNumber & i_currentNumber - 1;
            if (x == 0)
            {
                return 1;
            }
            return 0;
        }

        private static short isDecreasingSequence(int i_currentNumber)
        {
         
            int previousDigit, currentDigit;
            previousDigit = i_currentNumber % 10;
            i_currentNumber /= 10;
            while(i_currentNumber > 0)
            {
                currentDigit = i_currentNumber % 10;
                i_currentNumber /= 10;

                if (!(currentDigit > previousDigit))
                    return 0;

                previousDigit = currentDigit;
            }
            return 1;
        }

        private static bool isInputValid(string i_currentNumber)
        {

            int inputSize = i_currentNumber.Length;

            if (inputSize == k_InputSize)
            {
                foreach (char currentChar in i_currentNumber)
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
