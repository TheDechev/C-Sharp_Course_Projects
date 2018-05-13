using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_ManufacturerName;
        private float m_MaxAirPressure;
        private float m_CurrentAirPressure;

        public string ManufacturerName
        {
            get
            {
                return m_ManufacturerName;
            }
            set
            {
                m_ManufacturerName = value;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }
            set
            {
                if(value > m_MaxAirPressure)
                {
                    throw new ValueOutOfRangeException("Air preassure exceeding the maximum value!");
                }
                m_CurrentAirPressure = value;
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return m_MaxAirPressure;
            }
            set
            {
                m_MaxAirPressure = value;
            }
        }

        public void Inflate(float i_AmountToAdd)
        {
           if(m_CurrentAirPressure + i_AmountToAdd > m_MaxAirPressure)
            {
                throw new ValueOutOfRangeException(m_CurrentAirPressure, m_MaxAirPressure);
            }

            m_CurrentAirPressure += i_AmountToAdd;
        }

    }
}
