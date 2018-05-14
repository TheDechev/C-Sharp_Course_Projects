using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        public enum eLicenseType
        {
            A = 1,
            A1,
            B1,
            B2
        }

        private const string k_EngineVolumeKey = "Engine Volume";
        private const string k_LicenseTypeKey = "License type";
        private const int k_MotorcycleNumberOfTires = 2;
        private const float k_MotorcycleMaxTirePressure = 30f;
        private int m_EngineVolume;
        private eLicenseType m_LicenseType;

        internal Motorcycle(string i_LicensePlate, Energy i_EnergyType) : base(i_LicensePlate, i_EnergyType)
        {
            AddNewTires(k_MotorcycleNumberOfTires, k_MotorcycleMaxTirePressure);
        }

        public override void UpdateUniqueProperties(string i_Key, string i_Value)
        {
            if (i_Key == k_EngineVolumeKey)
            {
                this.m_EngineVolume = (int)LogicUtils.NumericValueValidation(i_Value, int.MaxValue);
            }
            else if (i_Key == k_LicenseTypeKey)
            {
                this.m_LicenseType = LogicUtils.EnumValidation<eLicenseType>(i_Value, i_Key);
            }
            else
            {
                throw new ArgumentException("Invalid key");
            }
        }

        public override string GetUniquePropertiesInfo()
        {
            return string.Format(
@"License type: {0}
Engine Volume: {1}", 
            this.m_LicensePlate, 
            this.m_EngineVolume);
        }

        public override Dictionary<string, string[]> GetUniqueAtttributesDictionary()
        {
            Dictionary<string, string[]> stringAttributes = new Dictionary<string, string[]>();

            stringAttributes.Add(k_LicenseTypeKey, Enum.GetNames(typeof(eLicenseType)));
            stringAttributes.Add(k_EngineVolumeKey, new string[] { });

            return stringAttributes;
        }
    }
}
