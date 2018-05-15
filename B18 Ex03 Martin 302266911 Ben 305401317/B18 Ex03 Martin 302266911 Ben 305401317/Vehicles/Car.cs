using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic 
{
    public class Car : Vehicle
    {
        private const int k_CarNumberOfTires = 4;
        private const float k_FuelCarMaxTriePressure = 30f;
        private const float k_ElectricCarMaxTirePressure = 32f;
        private const string k_ColorKey = "Color";
        private const string k_DoorsNumKey = "Doors number";
        private eColor m_Color = eColor.White;
        private eDoorsNumber m_DoorsNumber = eDoorsNumber.Four;

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

        internal Car(string i_LicensePlate, Energy i_EnergyType) : base(i_LicensePlate, i_EnergyType) 
        {
            float maxPressure = k_FuelCarMaxTriePressure;

            if (i_EnergyType is ElectricEnergy)
            {
                maxPressure = k_ElectricCarMaxTirePressure;
            }

            this.AddNewTires(k_CarNumberOfTires, maxPressure);
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
            Dictionary<string, string[]> stringAttributes = new Dictionary<string, string[]>();
            stringAttributes.Add(k_ColorKey, Enum.GetNames(typeof(eColor)));
            stringAttributes.Add(k_DoorsNumKey, Enum.GetNames(typeof(eDoorsNumber)));

            return stringAttributes;
        }

        public override string GetUniquePropertiesInfo()
        {
            return string.Format(
@"Color: {0}
Number of doors: {1}", 
            this.m_Color, 
            this.m_DoorsNumber);
        }
    }
}
