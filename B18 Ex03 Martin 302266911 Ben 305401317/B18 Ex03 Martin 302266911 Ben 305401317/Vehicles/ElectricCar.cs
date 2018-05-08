using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricCar : Car
    {
        private Engine m_Engine = new ElectricEngine();


        public void bla()
        {
            ElectricEngine x = m_Engine as ElectricEngine;
            if (x != null)
            {
                // fuel this motherfucker
            }
            else
            {
                //throw exception to the motherfucker
            }
        }
    }
}
