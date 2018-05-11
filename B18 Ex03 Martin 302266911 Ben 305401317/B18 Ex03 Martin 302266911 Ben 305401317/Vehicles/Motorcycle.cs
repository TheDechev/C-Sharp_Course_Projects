using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private int m_EngineVolume;
        private eLicenseType m_LicenseType;

        public enum eLicenseType
        {
            A,
            A1,
            B1,
            B2
        }

        public Motorcycle(string i_ModelName, string i_PlateNumber, eLicenseType i_LicenseType, int i_EngineVolume) : base(i_ModelName,i_PlateNumber) 
        {
            this.m_LicenseType = i_LicenseType;
            this.m_EngineVolume = i_EngineVolume;
        }

    }
}
