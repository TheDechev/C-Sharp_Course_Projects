using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class ElectricEngine : Engine
    {
        private float m_BatteryTimeLeft;
        private readonly float m_TotalBatteryTime;

        public float BatteryTimeLeft
        {
            get
            {
                return this.BatteryTimeLeft;
            }
        }

        public float TotalBatteryTime
        {
            get
            {
                return this.m_TotalBatteryTime;
            }
        }

        public void Charge(float i_FuelAmountToAdd)
        { // Add FuelAmountToAdd to  CurrentFuelAmount if the Fuel type is correct else exception

        }

    }
}
