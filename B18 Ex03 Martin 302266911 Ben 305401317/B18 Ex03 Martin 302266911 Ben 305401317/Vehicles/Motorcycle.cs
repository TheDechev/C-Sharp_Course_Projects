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

        private const int k_MotorcycleNumberOfWheels = 2;
        private const float k_MotorcycleMaxWheelPressure = 30f;
        private int m_EngineVolume;
        private eLicenseType m_LicenseType;



        public Motorcycle(string i_LicensePlate, Energy i_EnergyType): base(i_LicensePlate, i_EnergyType, k_MotorcycleNumberOfWheels)
        {
        }

        public override void UpdateUniqueProperties(string i_FirstProperty, string i_SecondProperty, eVehicleType i_VehicleType)
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

        public override string GetUniqueProperties()
        {
            return String.Format(
@"License type: {0}
Engine Volume: {1}", m_LicensePlate, m_EngineVolume);
        }

        public override void UpdateWheelsInfo(float i_CurrentPreasure)
        {
            this.m_WheelsList = new List<Wheel>(k_MotorcycleNumberOfWheels);

            foreach (Wheel wheel in m_WheelsList)
            {
                wheel.MaxManufacturerAirPressure = k_MotorcycleMaxWheelPressure;
                wheel.CurrentAirPressure = i_CurrentPreasure;
            }
        }
    }
}
