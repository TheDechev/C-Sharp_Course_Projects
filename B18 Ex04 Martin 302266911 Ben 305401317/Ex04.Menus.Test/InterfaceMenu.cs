using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test
{
    class InterfaceMenu
    {

        private MainMenu m_MainMenu;

        public void Show()
        {
            m_MainMenu.Show();
        }

        public InterfaceMenu()
        {
            m_MainMenu = new MainMenu("Main Menu");
            initInterfaceMenu();
        }

        private void initInterfaceMenu()
        {
            Menu LevelOne_SubOne = new Menu("Show Data/Time");
            MenuItem LevelTwo_SubOne_ItemOne = new MenuItem("Show Time", (ILastItem)new ShowTime());
            MenuItem LevelTwo_SubOne_ItemTwo = new MenuItem("Show Date", (ILastItem)new ShowDate());
            LevelOne_SubOne.AddItem(LevelTwo_SubOne_ItemOne);
            LevelOne_SubOne.AddItem(LevelTwo_SubOne_ItemTwo);

            Menu LevelOne_SubTwo = new Menu("Version and Capitals");
            MenuItem LevelTwo_SubTwo_ItemOne = new MenuItem("Count Capitals", (ILastItem)new CountCapitals());
            MenuItem LevelTwo_SubTwo_ItemTwo = new MenuItem("Show Version", (ILastItem)new ShowVersion());
            LevelOne_SubTwo.AddItem(LevelTwo_SubTwo_ItemOne);
            LevelOne_SubTwo.AddItem(LevelTwo_SubTwo_ItemTwo);

            m_MainMenu.AddItem(LevelOne_SubOne);
            m_MainMenu.AddItem(LevelOne_SubTwo);

        }
    }
}
