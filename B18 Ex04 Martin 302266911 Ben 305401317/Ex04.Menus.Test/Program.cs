namespace Ex04.Menus.Test
{
    public class Program
    {
        public static void Main()
        {
            DelegateMenu delegateMenu = new DelegateMenu();
            InterfaceMenu interfaceMenu = new InterfaceMenu();
            delegateMenu.Show();
            interfaceMenu.Show();
        }
    }
}
