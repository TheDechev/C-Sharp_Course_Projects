using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex04.Menus;

namespace Ex04.Menus.Test
{
    class Program
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
