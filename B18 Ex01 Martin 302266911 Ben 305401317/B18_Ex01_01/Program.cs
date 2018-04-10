﻿using System;

namespace B18_Ex01_01
{
    public class Program
    {
        public static void Main()
        {
            int totalNumberSum = 0;
            short powerOfTwoCounter = 0, decreasingSequenceCounter = 0, oneDigitCounter = 0;
            string theThreeNumbers = string.Empty;

            getInputStatistics(ref decreasingSequenceCounter, ref oneDigitCounter, ref powerOfTwoCounter, ref totalNumberSum, ref theThreeNumbers);
            printInputStatistics(oneDigitCounter, totalNumberSum, decreasingSequenceCounter, powerOfTwoCounter, theThreeNumbers);
        }

        private static void getInputStatistics(ref short io_DecreasingSequenceCounter, ref short io_OneDigitCounter, ref short io_PowerOfTwoCounter, ref int io_TotalNumberSum, ref string io_TheThreeNumbers)
        {
            string userInput;
            int binaryNumber, decimalNumber;

            Console.WriteLine("Please enter 3 binary numbers ( 9 digits each ): ");
            for (int i = 0; i < 3; i++)
            {
                userInput = Console.ReadLine();

                if (!isInputValid(userInput))
                {
                    Console.WriteLine("Invalid Input! Try again . . .\n");
                    i--;
                    continue;
                }

                binaryNumber = int.Parse(userInput);
                io_OneDigitCounter += getNumberOfOnes(binaryNumber);
                decimalNumber = convertBinaryToDec(binaryNumber);
                io_DecreasingSequenceCounter += isDecreasingSequence(decimalNumber);
                io_TheThreeNumbers += " " + decimalNumber.ToString();
                io_PowerOfTwoCounter += isPowerOfTwo(binaryNumber);
                io_TotalNumberSum += decimalNumber;
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
The numbers are: {7}
{0} zeroes is: {2:.00} 
{0} ones is: {3:.00}
{1} power of two: {4}
{1} a decreasing sequence: {5} 
The total average is: {6:00.00}",
                "The average number of ", 
                "Numbers that are ", 
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
                return 1;
            }

            return 0;
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
                    return 0;
                }

                previousDigit = currentDigit;
            }

            return 1;
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
