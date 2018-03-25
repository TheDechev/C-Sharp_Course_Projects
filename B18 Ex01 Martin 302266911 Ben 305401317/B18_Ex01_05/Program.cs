using System;
using System.Text;


namespace B18_Ex01_05
{
    class Program
    {
        public static void Main()
        {
            int maxDigit, minDigit = 0;
            int evenDigitsCounter, lowerThenFirstCounter = 0;

            Console.WriteLine("Please enter a positive 6 digit intiger: ");
            int userInput = int.Parse(Console.ReadLine());

            maxDigit = getMaxAndMinDigits(userInput,ref minDigit);
            evenDigitsCounter = countEvenDigitsInNum(userInput, ref lowerThenFirstCounter);

        }

        private static int getMaxAndMinDigits(int i_number, ref int io_min)
        {
            int max = i_number % 10;
            io_min = i_number % 10;

            int currDigit;
            i_number /= 10;

            while (i_number != 0)
            {
                currDigit = i_number % 10;
                max =  Math.Max(currDigit, max);
                io_min = Math.Min(currDigit, io_min);
                i_number /= 10;
            }
        
            return max;        
         }


        private static int countEvenDigitsInNum(int i_number, ref int io_lowerThenFirstCounter)
        {
            int evenCounter = 0, currDigit;
            int firstDigit = i_number % 10;
            

            while (i_number != 0)
            {
                currDigit = i_number % 10;
                if (i_number % 2 == 0)
                {
                    evenCounter++;
                }
                if (firstDigit > currDigit)
                {
                    io_lowerThenFirstCounter++;
                }
                i_number /= 10;
            }

            return evenCounter;
        }
    }
}
