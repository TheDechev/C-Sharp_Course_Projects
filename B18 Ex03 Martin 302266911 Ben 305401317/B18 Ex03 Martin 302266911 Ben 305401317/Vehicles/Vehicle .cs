using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected const int k_UniqueAttributesNum = 2;

        public enum eVehicleType
        {
            ElectricCar = 1,
            FuelCar,
            ElectricMotorcycle,
            FuelMotorcycle,
            FuelTruck
        }
        public const string k_VehicleTypeKey = "Vehicle Type";
        protected readonly string m_LicensePlate;
        protected readonly List<Wheel> m_WheelsList;
        protected float m_EnergyLeftPrecentage;
        protected string m_ModelName;
        protected Energy m_Energy;

        internal Vehicle(string i_LicensePlate, Energy i_EnergyType, int i_NumberOfWheels)
        {
            this.m_WheelsList = new List<Wheel>(i_NumberOfWheels);
            this.m_LicensePlate = i_LicensePlate;
            this.m_Energy = i_EnergyType;
        }

        public string ModelName
        {
            get
            {
                return this.m_ModelName;
            }
            set
            {
                m_ModelName = value;
            }
        }

        public string LicensePlate
        {
            get
            {
                return this.m_LicensePlate;
            }
        }

        public Energy Energy
        {
            get
            {
               return this.m_Energy;
            }
        }

        public float EnergyPercentageLeft
        {
            get
            {
                if(m_Energy == null)
                {
                    throw new Exception("Missing energy source!");
                }

                return m_EnergyLeftPrecentage;
            }

            set
            {
                this.Energy.CurrentEnergy = value;
            }
        }

        public void InflateWheelsToMax()
        {
            foreach(Wheel wheel in m_WheelsList)
            {
                wheel.Inflate(wheel.MaxManufacturerAirPressure - wheel.CurrentAirPressure);
            }
        }

        public abstract void UpdateUniqueProperties(string i_Key, string i_Value);

        public List<Wheel> TiresList
        {
            get
            {
                return this.m_WheelsList;
            }
        }

        public abstract string GetUniquePropertiesInfo();

        public abstract void UpdateWheelsInfo(float i_CurrentPreasure, string i_ManufacturerName);

        public abstract Dictionary<string, string[]> GetUniqueAtttributesDictionary();


    }
}
