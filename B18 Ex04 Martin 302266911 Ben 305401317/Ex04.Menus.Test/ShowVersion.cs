using System;
using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test
{
    public class ShowVersion : ILastItem
    {
        public void Execute()
        {
            Console.WriteLine("Version: 18.2.4.0");
        }
    }
}
