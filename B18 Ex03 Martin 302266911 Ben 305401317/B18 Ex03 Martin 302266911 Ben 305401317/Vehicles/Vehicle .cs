using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        public enum eVehicleType
        {
            ElectricCar,
            FuelCar,
            ElectricMotorcycle,
            FuelMotorcycle,
            FuelTruck
        }

        protected string m_ModelName;
        protected string m_LicensePlate;
        protected float m_EnergyLeftPrecentage;
        protected Energy m_Energy;
        protected List<Tire> m_Tires;

        internal Vehicle(string i_LicensePlate, string i_ModelName, int i_TiresNumber, float i_EnergyLeftPercentage, Energy i_EnergyType)
        {
            this.m_LicensePlate = i_LicensePlate;
            this.m_ModelName = i_ModelName;
            this.m_Tires = new List<Tire>(i_TiresNumber);
            this.m_Energy = i_EnergyType;
            this.m_EnergyLeftPrecentage = i_EnergyLeftPercentage;
        }

        private void UpdateTireInfo(string i_ManufacturerName,float i_MaxAirPressure,float  i_SetAirPressure)
        {
            foreach(Tire tire in m_Tires)
            {
                tire.ManufacturerName = i_ManufacturerName;
                tire.MaxManufacturerAirPressure = i_MaxAirPressure;
                tire.CurrentAirPressure = i_SetAirPressure;
            }
        }

        public string ModelName
        {
            get
            {
                return this.m_ModelName;
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
        }

        public void InflateTiersToMax()
        {
            foreach(Tire tire in m_Tires)
            {
                tire.Inflate(tire.MaxManufacturerAirPressure - tire.CurrentAirPressure);
            }
        }

        public abstract void UpdateUniqueProperties(string i_FirstProperty, string i_SecondProperty, eVehicleType i_VehicleType);



    }
}
