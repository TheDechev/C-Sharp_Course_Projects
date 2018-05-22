using System;
using System.Collections.Generic;

namespace Ex04.Menus.Delegates
{
    public delegate void MenuChoiceDelegate();

    public class MenuItem
    {
        protected MenuChoiceDelegate m_ItemChoiceDelegate;
        protected List<MenuItem> m_MenuItems = null;
        protected Menu m_Parent = null;

        public string Title { get; set; }

        public MenuItem(string i_Title)
        {
            this.Title = i_Title;
        }

        public MenuItem(string i_Title, MenuChoiceDelegate i_Function)
        {
            this.Title = i_Title;
            this.m_ItemChoiceDelegate += i_Function;
        }

        public Menu ParentMenu
        {
            get
            {
                return this.m_Parent;
            }

            set
            {
                this.m_Parent = value;
            }
        }

        public void OnChoice()
        {
            if(this.m_ItemChoiceDelegate != null)
            {
                this.m_ItemChoiceDelegate.Invoke();
            }
        }
    }
}
