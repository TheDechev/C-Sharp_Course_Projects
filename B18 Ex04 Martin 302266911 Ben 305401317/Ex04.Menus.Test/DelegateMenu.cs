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
            m_MainMenu = new MainMenu("Main Menu (Delegate)");
            initDelegateMenu();
        }

        private void initDelegateMenu()
        {
            Menu LevelOne_SubOne = new Menu("Show Data/Time");
            MenuItem LevelTwo_SubOne_ItemOne = new MenuItem("Show Time",(new ShowTime()).Execute);
            MenuItem LevelTwo_SubOne_ItemTwo = new MenuItem("Show Date", (new ShowDate()).Execute);
            LevelOne_SubOne.AddItem(LevelTwo_SubOne_ItemOne);
            LevelOne_SubOne.AddItem(LevelTwo_SubOne_ItemTwo);

            Menu LevelOne_SubTwo = new Menu("Version and Capitals");
            MenuItem LevelTwo_SubTwo_ItemOne = new MenuItem("Count Capitals", (new CountCapitals()).Execute);
            MenuItem LevelTwo_SubTwo_ItemTwo = new MenuItem("Show Version", (new ShowVersion()).Execute);
            LevelOne_SubTwo.AddItem(LevelTwo_SubTwo_ItemOne);
            LevelOne_SubTwo.AddItem(LevelTwo_SubTwo_ItemTwo);

            m_MainMenu.AddItem(LevelOne_SubOne);
            m_MainMenu.AddItem(LevelOne_SubTwo);
        }
    }
}
