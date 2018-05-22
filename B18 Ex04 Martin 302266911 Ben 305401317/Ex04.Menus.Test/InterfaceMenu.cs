using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test
{
    public class InterfaceMenu
    {
        private MainMenu m_MainMenu;

        public void Show()
        {
            this.m_MainMenu.Show();
        }

        public InterfaceMenu()
        {
            this.m_MainMenu = new MainMenu("Main Menu (Interface)");
            this.initInterfaceMenu();
        }

        private void initInterfaceMenu()
        {
            MainMenu LevelOne_SubOne = new MainMenu("Show Data/Time");
            MenuItem LevelTwo_SubOne_ItemOne = new MenuItem("Show Time", (ILastItem)new ShowTime());
            MenuItem LevelTwo_SubOne_ItemTwo = new MenuItem("Show Date", (ILastItem)new ShowDate());
            LevelOne_SubOne.AddItem(LevelTwo_SubOne_ItemOne);
            LevelOne_SubOne.AddItem(LevelTwo_SubOne_ItemTwo);

            MainMenu LevelOne_SubTwo = new MainMenu("Version and Capitals");
            MenuItem LevelTwo_SubTwo_ItemOne = new MenuItem("Count Capitals", (ILastItem)new CountCapitals());
            MenuItem LevelTwo_SubTwo_ItemTwo = new MenuItem("Show Version", (ILastItem)new ShowVersion());
            LevelOne_SubTwo.AddItem(LevelTwo_SubTwo_ItemOne);
            LevelOne_SubTwo.AddItem(LevelTwo_SubTwo_ItemTwo);

            this.m_MainMenu.AddItem(LevelOne_SubOne);
            this.m_MainMenu.AddItem(LevelOne_SubTwo);
        }
    }
}
