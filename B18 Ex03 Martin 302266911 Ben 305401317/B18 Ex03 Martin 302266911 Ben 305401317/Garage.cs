using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<eStatus/* Or RegistrationPlateNum ? */, VehicleInGarage> m_Vehicle;

        public enum eStatus
        {
            InProcess,
            Repaired,
            Payed
        }

        public void InflateToMax()
        { // By registration Plate

        }

        public void insertVehicle()
        {// By RegistrationPlateNume, if the vhicle is already in the garage the system will
            // write a MSG (UI) and used the Vehicle thats in the garage.

        }
    }
}
