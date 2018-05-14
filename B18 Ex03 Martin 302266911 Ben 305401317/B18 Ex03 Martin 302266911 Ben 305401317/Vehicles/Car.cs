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
            White = 1,
            Black,
            Gray,
            Blue,
        }

        public enum eDoorsNumber
        {
            Two = 1,
            Three,
            Four,
            Five
        }

        private const int k_CarNumberOfWheels = 4;
        private const float k_FuelCarMaxWheelPressure = 30f;
        private const float k_ElectricCarMaxWheelPressure = 32f;
        private const string k_ColorKey = "Color";
        private const string k_DoorsNumKey = "Doors number";
        private eColor m_Color;
        private eDoorsNumber m_DoorsNumber = eDoorsNumber.Four;

        public Car(string i_LicensePlate, Energy i_EnergyType) : base(i_LicensePlate, i_EnergyType) 
        {
            float maxPressure = k_FuelCarMaxWheelPressure;

            if (i_EnergyType is ElectricEnergy)
            {
                maxPressure = k_ElectricCarMaxWheelPressure;
            }

            base.AddNewWheels(k_CarNumberOfWheels, maxPressure);
        }

        public override void UpdateUniqueProperties(string i_Key, string i_Value)
        {
            if (i_Key == k_ColorKey)
            {
               this.m_Color = LogicUtils.EnumValidation<eColor>(i_Value, k_ColorKey);
            }
            else if (i_Key == k_DoorsNumKey)
            {
                this.m_DoorsNumber = LogicUtils.EnumValidation<eDoorsNumber>(i_Value, k_DoorsNumKey);
            }
            else
            {
                throw new ArgumentException("Invalid key");
            }
        }

        public override Dictionary<string, string[]> GetUniqueAtttributesDictionary()
        {
            Dictionary<string,string[]> stringAttributes = new Dictionary<string, string[]>();

            stringAttributes.Add(k_ColorKey, Enum.GetNames(typeof(eColor)));
            stringAttributes.Add(k_DoorsNumKey, Enum.GetNames(typeof(eDoorsNumber)));

            return stringAttributes;
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

        public override string GetUniquePropertiesInfo()
        {
            return String.Format(
@"Color: {0}
Number of doors: {1}", m_Color, m_DoorsNumber);
        }


    }
}
