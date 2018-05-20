using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Test
{
    public class TestFunctions
    {
        public void ShowTime()
        {
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss"));
        }

        public void ShowDate()
        {
            Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy"));
        }

        public void CapitalCount()
        {
            Console.WriteLine("Please enter a sentence:");
            string userInput = Console.ReadLine().Trim();
            int amountOfCapital = 0;

            for (int i = 0; i < userInput.Length; i++)
            {
                if (char.IsUpper(userInput[i]))
                {
                    amountOfCapital++;
                }
            }

            Console.WriteLine("There are {0} capital letters in the sentence.",amountOfCapital);
        }

        public void ShowVersion()
        {
            Console.WriteLine("Version: 18.2.4.0");
        }

    }
}
