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
            Two = 2,
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
        

        public Car(string i_LicensePlate, string i_ModelName, int i_TiresNumber, float i_EnergyLeftPercentage, Energy i_EnergyType) 
            : base(i_LicensePlate, i_ModelName, i_TiresNumber, i_EnergyLeftPercentage, i_EnergyType) 
        {
        }

        public eColor Color
        {
            get
            {
                return this.m_Color;
            }

            set
            {
                this.m_Color = value;
            }
        }

    }
}
