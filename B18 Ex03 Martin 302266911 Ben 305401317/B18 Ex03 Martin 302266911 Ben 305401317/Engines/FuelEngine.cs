using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        private eFuelType m_FuelType;
        private float m_CurrentFuelAmount;
        private readonly float m_MaxFuelAmount; 

        public enum eFuelType
        {
            Octan95,
            Octan96,
            Octan98,
            Soler
        }


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
                return this.m_CurrentFuelAmount;
            }
        }

        public float MaxFuelAmount
        {
            get
            {
                return this.m_CurrentFuelAmount;
            }
        }

        public void Refuel(float i_FuelAmountToAdd) //Fuel Vehicle
        { // Add FuelAmountToAdd to  CurrentFuelAmount if the Fuel type is correct else exception

        }

    }
}
