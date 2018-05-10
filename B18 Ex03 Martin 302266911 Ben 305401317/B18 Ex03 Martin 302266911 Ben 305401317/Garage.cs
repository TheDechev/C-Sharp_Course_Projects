using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, ClientVehicle> m_Vehicle;

        public enum eStatus
        {
            InProcess,
            Repaired,
            Paid
        }

        private bool IsVehicleInGarage()
        {
            bool isExist = false;

            return isExist;
        }

        public bool insertVehicle()
        {// By RegistrationPlateNume, if the vhicle is already in the garage the system will
            // write a MSG (UI) and used the Vehicle thats in the garage.


            // The UI asks the user for the vehicle details
            // sends the vehicle to the garage (--> insertVehicle)
            // insertVehicle() func will cheak if the vehicle in the grage (--> IsVehicleInGarage())
            // 
            bool res = false;

            return res;

        }

        public void InflateTire(float i_AirAmountToAdd)
        {   // The function need to add the AirAmountToAdd to the CurrentAirPressure 
            // if its not exceed from the MaxManufacturerAirPressure

        }

        public void InflateTireToMax()
        { // By registration Plate

        }

        public void UpdateVehicleProcessStatus(string i_RegistrationPlateNum, )
        {

        }

        public void DisplayVehiclesList()
        {   // fillter option by process status
            // 1. InProcess
            // 2. Repaired
            // 3. Paid
            // 4. no fillter
        }

        public void DisplayVehicleFullDeatails()
        {

        }
    }
}
