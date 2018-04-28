using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace B18_Ex02
{
    class Figure
    {
        int m_Row;
        int m_Col;

        public Figure(int i_Row, int i_Col)
        {
            m_Row = i_Row;
            m_Col = i_Col;
        }

        public int Row
        {
            set
            {
                m_Row = value;
            }

            get
            {
                return m_Row;
            }
        }

        public int Col
        {
            set
            {
                m_Col = value;
            }

            get
            {
                return m_Col;
            }
        }


    }
}
