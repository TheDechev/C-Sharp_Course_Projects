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
            this.m_MainMenu = new MainMenu("Main Menu");
            this.initDelegateMenu();
        }

        private void initDelegateMenu()
        {
            Menu LevelOne_SubOne = new Menu("Show Data/Time");
            MenuItem LevelTwo_SubOne_ItemOne = new MenuItem("Show Time", new MenuChoiceDelegate((new ShowTime()).Execute));
            MenuItem LevelTwo_SubOne_ItemTwo = new MenuItem("Show Date", new MenuChoiceDelegate((new ShowDate()).Execute));
            LevelOne_SubOne.AddItem(LevelTwo_SubOne_ItemOne);
            LevelOne_SubOne.AddItem(LevelTwo_SubOne_ItemTwo);

            Menu LevelOne_SubTwo = new Menu("Version and Capitals");
            MenuItem LevelTwo_SubTwo_ItemOne = new MenuItem("Count Capitals", new MenuChoiceDelegate((new CountCapitals()).Execute));
            MenuItem LevelTwo_SubTwo_ItemTwo = new MenuItem("Show Version", new MenuChoiceDelegate((new ShowVersion()).Execute));
            LevelOne_SubTwo.AddItem(LevelTwo_SubTwo_ItemOne);
            LevelOne_SubTwo.AddItem(LevelTwo_SubTwo_ItemTwo);

            this.m_MainMenu.AddItem(LevelOne_SubOne);
            this.m_MainMenu.AddItem(LevelOne_SubTwo);
        }
    }
}
