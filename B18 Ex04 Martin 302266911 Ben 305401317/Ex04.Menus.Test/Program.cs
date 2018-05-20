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

            Delegates.MainMenu newMenu = new Delegates.MainMenu();
            newMenu.Show();

        }
    }
}
