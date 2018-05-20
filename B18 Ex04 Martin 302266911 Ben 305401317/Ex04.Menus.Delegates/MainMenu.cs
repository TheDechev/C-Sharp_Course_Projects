﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Delegates
{
    public class MainMenu : Menu
    {
        public MainMenu(string i_Title): base(i_Title)
        {
        }

        public MainMenu() : base("Main Menu")
        {
        }
    }
}