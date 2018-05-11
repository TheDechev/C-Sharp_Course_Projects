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


        public Truck(string i_ModelName ,string i_PlateNumber, bool i_IsTrunkCool, float i_CurrentTrunkCapacity): base(i_ModelName,i_PlateNumber) 
        {
            this.m_IsTrunkCooled = i_IsTrunkCool;
            this.m_TrunkCapacity = i_CurrentTrunkCapacity;
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
