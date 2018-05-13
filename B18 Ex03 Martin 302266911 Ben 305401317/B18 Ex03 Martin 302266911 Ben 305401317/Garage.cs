using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {

        public const string k_VehicleStatus = "Vehicle status"

        private Dictionary<string, ClientVehicle> m_Vehicle = new Dictionary<string, ClientVehicle>();

        public enum eVehicleStatus
        {
            InProcess = 1,
            Repaired,
            Paid,
            None
        }

        public void insertVehicle(Vehicle i_VehicleToAdd, string i_ClientName, string i_ClientPhoneNumber)
        {
            CheckLicensePlate(i_VehicleToAdd.LicensePlate);

            m_Vehicle.Add(i_VehicleToAdd.LicensePlate, new ClientVehicle(i_VehicleToAdd,i_ClientName, i_ClientPhoneNumber)); 
        }

        public bool isVehicleInGarage(string i_LicenseToCheck)
        {
            return this.m_Vehicle.ContainsKey(i_LicenseToCheck);
        }

        public void InflateTireToMax(string i_LicensePlate)
        {
            CheckLicensePlate(i_LicensePlate);

            m_Vehicle[i_LicensePlate].Vehicle.InflateWheelsToMax();
        }
        
        public void RefuelFuelVehicle(string i_LicensePlate, FuelEnergy.eFuelType i_FuelType, float i_FuelToAdd)
        {
            CheckLicensePlate(i_LicensePlate);

            FuelEnergy currentEnergy = m_Vehicle[i_LicensePlate].Vehicle.Energy as FuelEnergy;

            if(currentEnergy == null)
            {
                throw new ArgumentException("Vehicle doesn't have fuel type energy!");
            }

            currentEnergy.Refuel(i_FuelToAdd, i_FuelType);
        }

        public void RechargeElectricVehicle(string i_LicensePlate, float i_MinutesToAdd)
        {
            CheckLicensePlate(i_LicensePlate);

            ElectricEnergy currentEnergy = m_Vehicle[i_LicensePlate].Vehicle.Energy as ElectricEnergy;

            if (currentEnergy == null)
            {
                throw new ArgumentException("Vehicle doesn't have electric type energy!");
            }

            currentEnergy.Charge(i_MinutesToAdd);
        }

        public void UpdateVehicleStatus(string i_LicensePlate, eVehicleStatus i_NewStatus)
        {
            CheckLicensePlate(i_LicensePlate);

            m_Vehicle[i_LicensePlate].Status = i_NewStatus;
        }

        public string DisplayVehicleFullDeatails(string i_LicensePlate)
        {
            StringBuilder vehicleInfo = new StringBuilder();
            Vehicle vehicleToCheck = this.m_Vehicle[i_LicensePlate].Vehicle;

            vehicleInfo.Append(String.Format(
@"License Plate: {0},
Model Name: {1}
Owner Name: {2}
Status in garage: {3}
Air pressure in tires: {4}
Tiers manufacturer: {5}
", vehicleToCheck.LicensePlate, vehicleToCheck.ModelName, m_Vehicle[i_LicensePlate].Status, vehicleToCheck.TiresList[0].CurrentAirPressure,
vehicleToCheck.TiresList[0].ManufacturerName));

            if (vehicleToCheck.Energy is FuelEnergy)
            {
                vehicleInfo.Append(String.Format(
 @"Fuel Type: {0}
Fuel level: {1}", ((FuelEnergy)vehicleToCheck.Energy).FuelType, vehicleToCheck.Energy.CurrentEnergy));
            }
            else if (vehicleToCheck.Energy is ElectricEnergy)
            {
                vehicleInfo.Append(String.Format(
 @"Battery level: {0}", vehicleToCheck.Energy.CurrentEnergy));
            }

            vehicleInfo.Append(vehicleToCheck.GetUniquePropertiesInfo());

            return vehicleInfo.ToString();
        }

        public string DisplayVehiclesList(eVehicleStatus i_FilterByStatus)
        {
            StringBuilder licensePlatesStr = new StringBuilder();

            foreach (ClientVehicle client in m_Vehicle.Values)
            {
                if (i_FilterByStatus == eVehicleStatus.None || client.Status == i_FilterByStatus)
                {
                    licensePlatesStr.Append(string.Format("{0}{1}", client.Vehicle.LicensePlate, Environment.NewLine).ToString());
                }
            }

            return licensePlatesStr.ToString();
        }

        private void CheckLicensePlate(string i_LicensePlate)
        {
            if (!isVehicleInGarage(i_LicensePlate))
            {
                throw new Exception("License plate not found!");
            }
        }

    }
}
