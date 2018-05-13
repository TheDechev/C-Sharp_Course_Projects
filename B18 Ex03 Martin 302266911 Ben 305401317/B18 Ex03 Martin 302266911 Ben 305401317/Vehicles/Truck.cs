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

        public override void UpdateUniqueProperties(string i_FirstProperty, string i_SecondProperty, e_VehicleType i_VehicleType)
        {
            bool isTrunkCooled;
            float trunkCapacity;
               
            if (bool.TryParse(i_FirstProperty, out isTrunkCooled))
            {
                throw new FormatException("Invalid engine volume!");
            }

            this.m_IsTrunkCooled = isTrunkCooled;

            if (float.TryParse(i_SecondProperty, out trunkCapacity))
            {
                throw new FormatException("Invalid engine volume!");
            }

            this.m_TrunkCapacity = trunkCapacity;
        }

    }
}
