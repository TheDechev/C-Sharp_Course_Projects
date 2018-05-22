using System;
using System.Collections.Generic;

namespace Ex04.Menus.Delegates
{
    public class Menu : MenuItem
    {
        public Menu(string i_Title) : base(i_Title)
        {
            this.m_MenuItems = new List<MenuItem>();
            this.m_ItemChoiceDelegate += new MenuChoiceDelegate(this.Show);
        }

        public void Show()
        {
            Console.Clear();
            int index = 1;
            string zeroChoice = "Back";
            Console.WriteLine("Current Level : {0} {1}", this.Title, Environment.NewLine);

            foreach (MenuItem item in this.m_MenuItems)
            {
                Console.WriteLine("{0}. {1}", index++, item.Title);
            }

            if(this is MainMenu)
            {
                zeroChoice = "Exit";
            }

            Console.WriteLine("0. {0} {1}", zeroChoice, Environment.NewLine);
            this.manageUserChoice(this.getUserChoice());
        }

        public void AddItem(MenuItem i_ItemToAdd)
        {
            if (i_ItemToAdd != null)
            {
                i_ItemToAdd.ParentMenu = this;
                this.m_MenuItems.Add(i_ItemToAdd);
            }
        }

        private void manageUserChoice(int i_UserChoice)
        {
            const int k_BackChoice = 0;

            if (i_UserChoice != k_BackChoice)
            {
                MenuItem userItemChoice = m_MenuItems[i_UserChoice - 1];
                userItemChoice.OnChoice();
                if(!(userItemChoice is Menu))
                { 
                    Console.Write("{0}<Press any key to return to menu.>", Environment.NewLine);
                    Console.ReadLine();
                    this.Show();
                }
            } 
            else
            {
                if(!(this is MainMenu))
                {
                    ParentMenu.Show();
                }
            }
        }

        private int getUserChoice()
        {
            int userInput = 0;
            bool isInputValid = false;

            do
            {
                try
                {
                    Console.Write("Enter your choice: ");
                    userInput = int.Parse(Console.ReadLine());
                    if(userInput >= 0 && userInput <= this.m_MenuItems.Count)
                    {
                        isInputValid = true;
                    }
                    else
                    {
                        throw new ArgumentException();
                    } 
                }
                catch (Exception)
                {
                    Console.WriteLine("<Invalid input. Please enter one of the options above.>");
                }
            }
            while (!isInputValid);

            return userInput;
        }
    }
}
