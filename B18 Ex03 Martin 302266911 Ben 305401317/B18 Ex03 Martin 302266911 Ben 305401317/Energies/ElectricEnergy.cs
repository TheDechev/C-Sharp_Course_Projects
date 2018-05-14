using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricEnergy : Energy
    {
        public const string k_ElectricUnits = "Battery time in hours";

        public float BatteryTimeLeft
        {
            get
            {
                return this.m_CurrentEnergy;
            }

            set
            {
                if (value > this.m_MaxCapacity)
                {
                    throw new ValueOutOfRangeException("Exceeded max battery time", this.m_CurrentEnergy, this.m_MaxCapacity);
                }

                this.CurrentEnergy = value;
            }
        }

        public float MaxBatteryTime
        {
            get
            {
                return this.m_MaxCapacity;
            }
        }

        internal ElectricEnergy(float i_MaxBatteryTime) : base(i_MaxBatteryTime)
        {
        }

        public void Charge(float i_ChargeAmount) 
        {
            if (this.BatteryTimeLeft == this.MaxBatteryTime)
            {
                throw new ValueOutOfRangeException("Battery fully charged, can't charge more!");
            }

            this.BatteryTimeLeft += i_ChargeAmount;
        }

        public override string EnergyUnits()
        {
            return k_ElectricUnits;
        }
    }
}
