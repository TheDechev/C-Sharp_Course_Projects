using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test
{
    class ShowVersion : ILastItem
    {
        public void Execute()
        {
            Console.WriteLine("Version: 18.2.4.0");
        }
    }
}
