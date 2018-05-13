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
        private float m_MaxManufacturerAirPressure;
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
                if(value > m_MaxManufacturerAirPressure)
                {
                    throw new ValueOutOfRangeException("Air preassure exceeding the maximum value!");
                }
                m_CurrentAirPressure = value;
            }
        }

        public float MaxManufacturerAirPressure
        {
            get
            {
                return m_MaxManufacturerAirPressure;
            }
            set
            {
                m_MaxManufacturerAirPressure = value;
            }
        }

        public void Inflate(float i_AmountToAdd)
        {
           if(m_CurrentAirPressure + i_AmountToAdd > m_MaxManufacturerAirPressure)
            {
                throw new ValueOutOfRangeException(m_CurrentAirPressure, m_MaxManufacturerAirPressure);
            }

            m_CurrentAirPressure += i_AmountToAdd;
        }

    }
}
