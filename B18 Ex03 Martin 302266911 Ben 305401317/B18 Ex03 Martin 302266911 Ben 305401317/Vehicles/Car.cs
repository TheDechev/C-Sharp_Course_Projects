using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic 
{
    public class Car : Vehicle
    {
        private eColor m_Color;
        private eDoorsNumber m_DoorsNumber = eDoorsNumber.Four;
        
        public enum eDoorsNumber
        {
            Tow,
            Three,
            Four,
            Five
        }

        public enum eColor
        {
            White,
            Black,
            Gray,
            Blue,
        }

        public Car( eColor i_CarColor, string i_ModelName) : base (i_ModelName)
        {
            m_Color = i_CarColor;

        }
    }
}
