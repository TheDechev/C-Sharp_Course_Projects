using System;
using Ex04.Menus.Interfaces;

namespace Ex04.Menus.Test 
{
    public class ShowDate : ILastItem
    {
        public void Execute()
        {
            Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy"));
        }
    }
}
