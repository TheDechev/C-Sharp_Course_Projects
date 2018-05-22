using System;
using System.Collections.Generic;

namespace Ex04.Menus.Interfaces
{
    public class MenuItem
    {
        protected List<MenuItem> m_MenuItems = null;
        protected Menu m_Parent = null;
        public string Title { get; set; }
        private ILastItem m_LastItem = null;

        public void Execute()
        {
            if(this.m_LastItem == null)
            {
                throw new ArgumentException("Item doesn't have implemented execute function.");
            }

            this.m_LastItem.Execute();
        }

        public MenuItem(string i_Title)
        {
            this.Title = i_Title;
        }

        public MenuItem(string i_Title, ILastItem i_LastItemFunction)
        {
            this.Title = i_Title;
            this.m_LastItem = i_LastItemFunction;
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
    }
}
