using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic 
{
    public class Car : Vehicle
    {
        public enum eColor
        {
            White,
            Black,
            Gray,
            Blue,
        }

        public enum eDoorsNumber
        {
            Two = 2,
            Three,
            Four,
            Five
        }


        private eColor m_Color;
        private eDoorsNumber m_DoorsNumber = eDoorsNumber.Four;
        

        public Car(string i_LicensePlate, string i_ModelName, int i_TiresNumber, float i_EnergyLeftPercentage, Energy i_EnergyType) 
            : base(i_LicensePlate, i_ModelName, i_TiresNumber, i_EnergyLeftPercentage, i_EnergyType) 
        {
        }

        public override void UpdateUniqueProperties(string i_FirstProperty, string i_SecondProperty, eVehicleType i_VehicleType)
        {
            eColor carColor = (eColor)Enum.ToObject(typeof(eColor), i_FirstProperty);

            if (!Enum.IsDefined(typeof(eColor), carColor))
            {
                throw new ValueOutOfRangeException("Invalid license plate!");
            }

            this.m_Color = carColor;

            eDoorsNumber doorsNum = (eDoorsNumber)Enum.ToObject(typeof(eDoorsNumber), i_SecondProperty);

            if (!Enum.IsDefined(typeof(eColor), doorsNum))
            {
                throw new ValueOutOfRangeException("Invalid license plate!");
            }

            this.m_DoorsNumber = doorsNum;
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

        public override string GetUniqueProperties()
        {
            return String.Format(
@"Color: {0}
Number of doors: {1}", m_Color, m_DoorsNumber);
        }

    }
}
