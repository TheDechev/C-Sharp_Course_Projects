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

            DelegateMenu test1 = new DelegateMenu();
            test1.Show();

            InterfaceMenu test2 = new InterfaceMenu();
            test2.Show();

        }
    }
}
