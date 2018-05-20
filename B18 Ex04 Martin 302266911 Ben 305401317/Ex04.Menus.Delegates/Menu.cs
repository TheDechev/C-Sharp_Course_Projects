using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Delegates
{
    public class Menu : MenuItem
    {
        public Menu()
        {
            this.m_ItemChoiceDelegate += new MenuChoiceDelegate(Show);
        }

        public void Show()
        {
            int userInput, index = 1;
            string zeroChoice = "Back";
            bool isInputValid = false;
            Console.WriteLine("Current Level : {0} {1}",this.Title , Environment.NewLine);

            foreach (MenuItem item in m_MenuItems)
            {
                Console.WriteLine("{0}. {1}", index++, item.Title);
            }

            if(this is MainMenu)
            {
                zeroChoice = "Exit";
            }
            Console.WriteLine("0. {0} {1}",zeroChoice, Environment.NewLine);

            do
            {
                try
                {
                    Console.Write("Enter your choice: ");
                    userInput = int.Parse(Console.ReadLine());
                    if (userInput != 0)
                    {
                        m_MenuItems[userInput - 1].OnChoice();
                    }
                    isInputValid = true;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            } while (!isInputValid);
        }


    }
}
