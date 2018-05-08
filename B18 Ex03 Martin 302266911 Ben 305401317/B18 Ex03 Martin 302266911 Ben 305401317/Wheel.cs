using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly string m_ManufacturerName;
        private readonly float r_MaxManufacturerAirPressure;
        private float m_CurrentAirPressure;

        public  string ManufacturerName
        {
            get
            {
                return m_ManufacturerName;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }
        }

        public float MaxManufacturerAirPressure
        {
            get
            {
                return MaxManufacturerAirPressure;
            }
        }

        public void Inflate(float i_AirAmountToAdd)
        {   // The function need to add the AirAmountToAdd to the CurrentAirPressure 
            // if its not exceed from the MaxManufacturerAirPressure

        }


        public override string ToString()
        {
            string wheelsDeatails = string.Empty;

            return wheelsDeatails;
        }

    }
}
