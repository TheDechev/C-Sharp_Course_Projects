using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelEnergy : Energy
    {
        public enum eFuelType
        {
            Octan95,
            Octan96,
            Octan98,
            Soler
        }
        private eFuelType m_FuelType;
        private readonly string k_FuelUnits = "Liters of fuel";

        public eFuelType FuelType
        {
            get
            {
                return this.m_FuelType;
            }
        }
        public float CurrentFuelAmount
        {
            get
            {
                return this.m_CurrentEnergy;
            }

            set
            {
                if(value + m_CurrentEnergy > m_MaxCapacity)
                {
                    throw new ValueOutOfRangeException("Exceeded max capacity of the fuel system!", m_CurrentEnergy, m_MaxCapacity);
                }

                m_CurrentEnergy = value;
            }
        }
        public float MaxFuelAmount
        {
            get
            {
                return this.m_MaxCapacity;
            }
        }

        public FuelEnergy(eFuelType i_FuelType, float i_MaxFuelCapacity) : base (i_MaxFuelCapacity)
        {
            m_FuelType = i_FuelType;
        }

        public void Refuel(float i_FuelAmountToAdd, eFuelType i_FuelType)
        {
            if(i_FuelType != this.FuelType)
            {
                throw new ArgumentException("Wrong fuel type!");
            }

            this.CurrentFuelAmount += i_FuelAmountToAdd;
        }

        public override string EnergyUnits()
        {
            return k_FuelUnits;
        }



    }
}
