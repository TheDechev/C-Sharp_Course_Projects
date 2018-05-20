using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test
{
    class CountCapitals : ILastItem
    {
        public void Execute()
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

            Console.WriteLine("There are {0} capital letters in the sentence.", amountOfCapital);
        }
    }
}
