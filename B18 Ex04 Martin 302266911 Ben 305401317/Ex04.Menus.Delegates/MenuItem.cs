using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex04.Menus.Delegates
{
    public delegate void MenuChoiceDelegate();

    public class MenuItem
    {
        protected MenuChoiceDelegate m_ItemChoiceDelegate;
        protected List<MenuItem> m_MenuItems = null;
        protected Menu m_Parent = null;
        private string m_Title;

        public MenuItem(string i_Title)
        {
            this.Title = i_Title;
        }

        public MenuItem(string i_Title, MenuChoiceDelegate i_Function)
        {
            this.Title = i_Title;
            m_ItemChoiceDelegate += i_Function;
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
                if(value  == string.Empty)
                {
                    throw new ArgumentException("Cannot add empty title name!");
                }

                m_Title = value;
            }
        }

        public void OnChoice()
        {
            if(m_ItemChoiceDelegate != null)
            {
                m_ItemChoiceDelegate.Invoke();
            }
        }
    }
}
