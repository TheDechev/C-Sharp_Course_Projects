using Ex04.Menus.Delegates;

namespace Ex04.Menus.Test
{
    public class DelegateMenu
    {
        private MainMenu m_MainMenu;

        public void Show()
        {
            this.m_MainMenu.Show();
        }

        public DelegateMenu()
        {
            this.m_MainMenu = new MainMenu("Main Menu (Delegate)");
            this.initDelegateMenu();
        }

        private void initDelegateMenu()
        {
            MainMenu LevelOne_SubOne = new MainMenu("Show Data/Time");
            MenuItem LevelTwo_SubOne_ItemOne = new MenuItem("Show Time", (new ShowTime()).Execute);
            MenuItem LevelTwo_SubOne_ItemTwo = new MenuItem("Show Date", (new ShowDate()).Execute);
            LevelOne_SubOne.AddItem(LevelTwo_SubOne_ItemOne);
            LevelOne_SubOne.AddItem(LevelTwo_SubOne_ItemTwo);

            MainMenu LevelOne_SubTwo = new MainMenu("Version and Capitals");
            MenuItem LevelTwo_SubTwo_ItemOne = new MenuItem("Count Capitals", (new CountCapitals()).Execute);
            MenuItem LevelTwo_SubTwo_ItemTwo = new MenuItem("Show Version", (new ShowVersion()).Execute);
            LevelOne_SubTwo.AddItem(LevelTwo_SubTwo_ItemOne);
            LevelOne_SubTwo.AddItem(LevelTwo_SubTwo_ItemTwo);

            this.m_MainMenu.AddItem(LevelOne_SubOne);
            this.m_MainMenu.AddItem(LevelOne_SubTwo);
        }
    }
}
