using System;
using System.Text;

namespace B18_Ex01_05
{
    public class Program
    {
        public static void Main()
        {
            int maxDigit, minDigit;
            int evenDigitsCounter, lowerThenFirstCounter;

            Console.WriteLine("Please enter a positive 6 digit intiger: ");
            int userInput = int.Parse(Console.ReadLine());

            maxDigit = getMaxDigit(userInput);
            minDigit = getMinDigit(userInput);
            evenDigitsCounter = countEvenDigitsInNum(userInput);
            lowerThenFirstCounter = countLowerThanFirstigitsInNum(userInput);
        }

        private static int getMaxDigit(int i_number)
        {
            int max = i_number % 10;
            int currDigit;

            i_number /= 10;
 
            while (i_number != 0)
            {
                currDigit = i_number % 10;
                max = Math.Max(currDigit, max);
                i_number /= 10;
            }   
                 
            return max;        
         }

        private static int getMinDigit(int i_number)
        {
            int min = i_number % 10;
            int currDigit;

            i_number /= 10;

            while (i_number != 0)
            {
                currDigit = i_number % 10;
                min = Math.Min(currDigit, min);
                i_number /= 10;
            }

            return min;
        }

        private static int countEvenDigitsInNum(int i_number)
        {
            int evenCounter = 0, currDigit;

            while (i_number != 0)
            {
                currDigit = i_number % 10;
                if (currDigit % 2 == 0)
                {
                    evenCounter++;
                }
              
                i_number /= 10;
            }

            return evenCounter;
        }

        private static int countLowerThanFirstigitsInNum(int i_number)
        {
            int lowerThenFirstCounter = 0, currDigit;
            int firstDigit = i_number % 10;

            while (i_number != 0)
            {
                currDigit = i_number % 10;
   
                if (firstDigit > currDigit)
                {
                    lowerThenFirstCounter++;
                }

                i_number /= 10;
            }

            return lowerThenFirstCounter;
        }
    }
}
