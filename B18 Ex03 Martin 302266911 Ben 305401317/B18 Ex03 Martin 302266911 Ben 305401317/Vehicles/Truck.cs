using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_IsTrunkCooled;
        private float m_TrunkCapacity;


        public Truck(string i_LicensePlate, string i_ModelName, int i_TiresNumber, float i_EnergyLeftPercentage, Energy i_EnergyType)
            : base(i_LicensePlate, i_ModelName, i_TiresNumber, i_EnergyLeftPercentage, i_EnergyType)
        {
        }

        public bool isTrunkCooled
        {
            get
            {
                return this.m_IsTrunkCooled;
            }
        }

        public float TrunkCapacity
        {
            get
            {
                return this.m_TrunkCapacity;
            }
        }


    }
}
