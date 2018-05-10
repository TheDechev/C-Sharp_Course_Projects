using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected readonly string m_ModelName;
        protected readonly string m_RegistrationPlateNumber;
        protected float m_EnergyLeftPrecentage;
        protected Engine m_Engine;
        protected List<Tire> m_Tires;

        //TODO: continue
        protected Vehicle(string i_ModleName)
        {
            this.m_ModelName = i_ModleName;
        }

        public string ModelName
        {
            get
            {
                return this.m_ModelName;
            }
        }
    }
}
