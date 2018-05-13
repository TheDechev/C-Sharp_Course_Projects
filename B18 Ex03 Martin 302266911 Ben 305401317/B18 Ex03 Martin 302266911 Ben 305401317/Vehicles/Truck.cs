using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const int k_TruckNumberOfWheels = 12;
        private const float k_TruckMaxWheelPressure = 28f;

        private bool m_IsTrunkCooled;
        private float m_TrunkCapacity;


        public Truck(string i_LicensePlate, Energy i_EnergyType): base(i_LicensePlate, i_EnergyType)
        {
            UpdateWheelsInfo(k_TruckNumberOfWheels, i_WheelsPressure, k_TruckMaxWheelPressure);
        }

        public bool isTrunkCooled
        {
            get
            {
                return this.m_IsTrunkCooled;
            }
        }

        public float TrunkCapacity
        {
            get
            {
                return this.m_TrunkCapacity;
            }
        }

        public override void UpdateUniqueProperties(string i_FirstProperty, string i_SecondProperty, eVehicleType i_VehicleType)
        {
            bool isTrunkCooled;
            float trunkCapacity;
               
            if (bool.TryParse(i_FirstProperty, out isTrunkCooled))
            {
                throw new FormatException("Invalid engine volume!");
            }

            this.m_IsTrunkCooled = isTrunkCooled;

            if (float.TryParse(i_SecondProperty, out trunkCapacity))
            {
                throw new FormatException("Invalid engine volume!");
            }

            this.m_TrunkCapacity = trunkCapacity;
        }

        public override string GetUniqueProperties()
        {
            return String.Format(
@"Trunk capacity: {0}
Cooled trunk: {1}", m_LicensePlate, m_IsTrunkCooled);
        }

        public override void UpdateWheelsInfo(float i_CurrentPreasure)
        {
            this.m_WheelsList = new List<Wheel>(k_TruckNumberOfWheels);

            foreach (Wheel wheel in m_WheelsList)
            {
                wheel.CurrentAirPressure = i_CurrentPreasure;
                wheel.MaxManufacturerAirPressure = k_TruckMaxWheelPressure;
            }
        }

    }
}
