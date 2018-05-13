using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        public enum eLicenseType
        {
            A,
            A1,
            B1,
            B2
        }

        private const string k_EngineVolumeKey = "Engine Volume";
        private const string k_LicenseTypeKey = "License type";
        private const int k_MotorcycleNumberOfWheels = 2;
        private const float k_MotorcycleMaxWheelPressure = 30f;
        private int m_EngineVolume;
        private eLicenseType m_LicenseType;



        public Motorcycle(string i_LicensePlate, Energy i_EnergyType): base(i_LicensePlate, i_EnergyType)
        {
            base.AddNewWheels(k_MotorcycleNumberOfWheels, k_MotorcycleMaxWheelPressure);
        }

        public override void UpdateUniqueProperties(string i_Key, string i_Value)
        {

            if (i_Key == k_EngineVolumeKey)
            {
                this.m_EngineVolume = int.Parse(i_Value);
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
            return String.Format(
@"License type: {0}
Engine Volume: {1}", m_LicensePlate, m_EngineVolume);
        }

        public override Dictionary<string, string[]> GetUniqueAtttributesDictionary()
        {
            Dictionary<string, string[]> stringAttributes = new Dictionary<string, string[]>();

            stringAttributes.Add("License Type", Enum.GetNames(typeof(eLicenseType)));
            stringAttributes.Add("Engine volume", new string[] { });

            return stringAttributes;
        }
    }
}
