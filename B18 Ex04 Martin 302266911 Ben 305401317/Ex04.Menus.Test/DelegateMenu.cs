using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex04.Menus.Delegates;

namespace Ex04.Menus.Test
{
    public class DelegateMenu
    {
        private MainMenu m_MainMenu;

        public void Show()
        {
           m_MainMenu.Show();
        }

        public DelegateMenu()
        {
            m_MainMenu = new MainMenu("Main Menu");
            initDelegateMenu();
        }

        private void initDelegateMenu()
        {
            TestFunctions newTest = new TestFunctions();

            Menu LevelOne_SubOne = new Menu("Show Data/Time");
            MenuItem LevelTwo_SubOne_ItemOne = new MenuItem("Show Time",newTest.ShowTime);
            MenuItem LevelTwo_SubOne_ItemTwo = new MenuItem("Show Date", newTest.ShowDate);
            LevelOne_SubOne.AddItem(LevelTwo_SubOne_ItemOne);
            LevelOne_SubOne.AddItem(LevelTwo_SubOne_ItemTwo);


            Menu LevelOne_SubTwo = new Menu("Version and Capitals");
            MenuItem LevelTwo_SubTwo_ItemOne = new MenuItem("Count Capitals",newTest.CapitalCount);
            MenuItem LevelTwo_SubTwo_ItemTwo = new MenuItem("Show Version", newTest.ShowVersion);
            LevelOne_SubTwo.AddItem(LevelTwo_SubTwo_ItemOne);
            LevelOne_SubTwo.AddItem(LevelTwo_SubTwo_ItemTwo);

            m_MainMenu.AddItem(LevelOne_SubOne);
            m_MainMenu.AddItem(LevelOne_SubTwo);

        }
    }
}
