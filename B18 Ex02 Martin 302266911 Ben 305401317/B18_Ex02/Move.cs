using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B18_Ex02
{
    public class Move
    {
        private Figure m_figureFrom;

        private Figure m_figureTo;

        public Move(Figure from, Figure to)
        {
            this.m_figureFrom = from;
            this.m_figureTo = to;
        }

        public Move()
        {
            m_figureFrom = null;
            m_figureTo = null;
        }

        public Figure FigureFrom
        {
            get
            {
                return m_figureFrom;
            }
        }

        public Figure FigureTo
        {
            get
            {
                return m_figureTo;
            }
        }

        public static bool operator== (Move i_MoveOne, Move i_MoveTwo)
        {
            return (i_MoveOne.m_figureFrom == i_MoveTwo.m_figureFrom && i_MoveOne.m_figureTo == i_MoveTwo.m_figureTo);
        }

        public static bool operator!= (Move i_MoveOne, Move i_MoveTwo)
        {
            return (i_MoveOne.m_figureFrom != i_MoveTwo.m_figureFrom || i_MoveOne.m_figureTo != i_MoveTwo.m_figureTo);
        }

        public override bool Equals(object i_Move)
        {
            if (i_Move == null || i_Move.GetType() != GetType() )
            {
                return false;
            }

            Move movObj = (Move)i_Move;
            return this == movObj;
        }

        
    }
}
