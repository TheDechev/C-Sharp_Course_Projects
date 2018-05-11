using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        public enum e_VehicleType
        {
            ElectricCar,
            FuelCar,
            ElectricMotorcycle,
            FuelMotorcycle,
            FuelTruck
        }
        protected readonly string m_ModelName;
        protected readonly string m_RegistrationPlateNumber;
        protected float m_EnergyLeftPrecentage;
        protected Energy m_Energy;
        protected List<Tire> m_Tires;
        
      
        protected Vehicle(string i_ModelName, string i_PlateNumber)
        {
            this.m_ModelName = i_ModelName;
            this.m_RegistrationPlateNumber = i_PlateNumber;
        }

        public string ModelName
        {
            get
            {
                return this.m_ModelName;
            }
        }

        public string RegistrationPlateNumber
        {
            get
            {
                return this.m_RegistrationPlateNumber;
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

                return m_Energy.MaxCapacity;
            }
        }
    }
}
