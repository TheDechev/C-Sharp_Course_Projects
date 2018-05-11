using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class ElectricEnergy : Energy
    {

        public float BatteryTimeLeft
        {
            get
            {
                return this.m_CurrentEnergy;
            }

            set
            {
                if (value + m_CurrentEnergy > m_MaxCapacity)
                {
                    throw new ValueOutOfRangeException("Exceeded max battery time!", m_CurrentEnergy, m_MaxCapacity);
                }

                m_CurrentEnergy = value;
            }
        }

        public float MaxBatteryTime
        {
            get
            {
                return this.m_MaxCapacity;
            }
        }

        public ElectricEnergy(float i_StartBatteryTime, float i_MaxBatteryTime) : base(i_StartBatteryTime,i_MaxBatteryTime)
        {
        }

        public void Charge(float i_ChargeAmount) 
        {
            this.BatteryTimeLeft += i_ChargeAmount;
        }

    }
}
