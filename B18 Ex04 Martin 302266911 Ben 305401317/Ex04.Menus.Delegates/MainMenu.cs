using System;
using System.Collections.Generic;

namespace Ex04.Menus.Delegates
{
    public class MainMenu : MenuItem
    {
        public MainMenu(string i_Title) : base(i_Title)
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

            if(this.m_Parent == null)
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

        public void RemoveItem(MenuItem i_ItemToRemove)
        {
            this.m_MenuItems.Remove(i_ItemToRemove);
        }

        private void manageUserChoice(int i_UserChoice)
        {
            const int k_BackChoice = 0;

            if (i_UserChoice != k_BackChoice)
            {
                MenuItem userItemChoice = m_MenuItems[i_UserChoice - 1];
                userItemChoice.OnChoice();
                if(!(userItemChoice is MainMenu))
                { 
                    Console.Write("{0}<Press any key to return to menu.>", Environment.NewLine);
                    Console.ReadLine();
                    this.Show();
                }
            } 
            else
            {
                if(!(this.m_Parent == null))
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
                Console.Write("Enter your choice: ");
                isInputValid = int.TryParse(Console.ReadLine(), out userInput);

                if (!isInputValid || userInput < 0 || userInput > this.m_MenuItems.Count)
                {
                    Console.WriteLine("<Invalid input. Please enter one of the options above.>");
                }
            }
            while (!isInputValid);

            return userInput;
        }
    }
}
