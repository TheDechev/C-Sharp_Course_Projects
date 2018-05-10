using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class VehicleFactory
    {
        private List<Vehicle> m_SupportedVeihiclesInSystem;

        //TODO: continue 
        public Vehicle CreateVehicle()
        {
            Vehicle newVehicle = new Car(Car.eColor.White, "ss");

            return newVehicle;
        }
    }
}
