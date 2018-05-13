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

        private int m_EngineVolume;
        private eLicenseType m_LicenseType;



        public Motorcycle(string i_LicensePlate, string i_ModelName, int i_TiresNumber, float i_EnergyLeftPercentage, Energy i_EnergyType)
            : base(i_LicensePlate, i_ModelName, i_TiresNumber, i_EnergyLeftPercentage, i_EnergyType)
        {
        }

        public override void UpdateUniqueProperties(string i_FirstProperty, string i_SecondProperty, e_VehicleType i_VehicleType)
        {
            int engineVolume;
            eLicenseType licenseType = (eLicenseType)Enum.ToObject(typeof(eLicenseType), i_FirstProperty);

            if(!Enum.IsDefined(typeof(eLicenseType), licenseType))
            {
                throw new ValueOutOfRangeException("Invalid license plate!");
            }

            m_LicenseType = licenseType;

            if (int.TryParse(i_SecondProperty, out engineVolume))
            {
                throw new FormatException("Invalid engine volume!");
            }

            this.m_EngineVolume = engineVolume;

        }

    }
}
