using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Interfaces
{
    public class MenuItem
    {
        protected List<MenuItem> m_MenuItems = null;
        protected Menu m_Parent = null;
        private string m_Title;
        private ILastItem m_LastItem = null;

        public void Execute()
        {
            if(m_LastItem == null)
            {
                throw new ArgumentException("Item doesn't have implemented execute function.");
            }

            m_LastItem.Execute();
        }

        public MenuItem(string i_Title)
        {
            this.Title = i_Title;
        }

        public MenuItem(string i_Title, ILastItem i_LastItemFunction)
        {
            this.Title = i_Title;
            m_LastItem = i_LastItemFunction;
        }

        public Menu ParentMenu
        {
            get
            {
                return m_Parent;
            }
            set
            {
                m_Parent = value;
            }
        }

        public string Title
        {
            get
            {
                return m_Title;
            }
            set
            {
                if (value == string.Empty)
                {
                    throw new ArgumentException("Cannot add empty title name!");
                }

                m_Title = value;
            }
        }

    }
}
