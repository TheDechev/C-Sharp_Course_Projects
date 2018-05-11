using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Energy
    {
        protected float m_CurrentEnergy;
        protected float m_MaxCapacity;

        public Energy(float i_CurrentEnergy, float i_MaxCapacity)
        {
            m_CurrentEnergy = i_CurrentEnergy;
            m_MaxCapacity = i_MaxCapacity;
        }

        protected bool isExceedingMaxAmount(float i_AmountToAdd)
        {
            bool isExceeding = false;
            if (i_AmountToAdd + m_CurrentEnergy > m_MaxCapacity)
            {
                isExceeding = true;
            }

            return isExceeding;
        }

        public float CurrentEnergy
        {
            get
            {
                return this.m_CurrentEnergy;
            }
        }

        public float MaxCapacity
        {
            get
            {
                return this.m_MaxCapacity;
            }
        }
    }
}
