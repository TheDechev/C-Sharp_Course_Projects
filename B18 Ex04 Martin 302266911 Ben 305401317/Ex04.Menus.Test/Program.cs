namespace Ex04.Menus.Test
{
    public class Program
    {
        public static void Main()
        {
            DelegateMenu delegateMenu = new DelegateMenu();
            delegateMenu.Show();
            InterfaceMenu interfaceMenu = new InterfaceMenu();
            interfaceMenu.Show();
        }
    }
}
