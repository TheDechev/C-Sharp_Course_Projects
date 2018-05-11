using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic 
{
    public class Car : Vehicle
    {
        public enum eDoorsNumber
        {
            Two,
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

        private eColor m_Color;
        private eDoorsNumber m_DoorsNumber = eDoorsNumber.Four;
        

        public Car(string i_ModelName, string i_PlateNumber, eColor i_CarColor, eDoorsNumber i_DoorsNumber) : base(i_ModelName,i_PlateNumber) 
        {
            this.m_Color = i_CarColor;
            this.m_DoorsNumber = i_DoorsNumber;
        }

    }
}
