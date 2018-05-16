using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        public enum eVehicleStatus
        {
            InProcess = 1,
            Repaired,
            Paid
        }

        private const string k_VehicleStatusKey = "Vehicle status";
        private string k_NoStatusFilter = (Enum.GetNames(typeof(eVehicleStatus)).Length + 1).ToString();
        private Dictionary<string, ClientVehicle> m_Vehicle = new Dictionary<string, ClientVehicle>();

        public static string VehicleStatusKey
        {
            get
            {
                return k_VehicleStatusKey;
            }
        }

        public void insertVehicle(Vehicle i_VehicleToAdd, string i_ClientName, string i_ClientPhoneNumber)
        {
            this.m_Vehicle.Add(i_VehicleToAdd.LicensePlate, new ClientVehicle(i_VehicleToAdd, i_ClientName, i_ClientPhoneNumber)); 
        }

        public bool isVehicleInGarage(string i_LicenseToCheck)
        {
            return this.m_Vehicle.ContainsKey(i_LicenseToCheck);
        }

        public void InflateTireToMax(string i_LicensePlate)
        {
            this.checkLicensePlate(i_LicensePlate);
            this.m_Vehicle[i_LicensePlate].Vehicle.InflateTiresToMax();
        }
        
        public void RefuelFuelVehicle(string i_LicensePlate, FuelEnergy.eFuelType i_FuelType, float i_FuelToAdd)
        {
            this.checkLicensePlate(i_LicensePlate);
            FuelEnergy currentEnergy = this.m_Vehicle[i_LicensePlate].Vehicle.Energy as FuelEnergy;

            if (currentEnergy == null)
            {
                throw new ArgumentException("Vehicle doesn't have fuel type energy!");
            }

            currentEnergy.Refuel(i_FuelToAdd, i_FuelType);
        }

        public void RechargeElectricVehicle(string i_LicensePlate, float i_MinutesToAdd)
        {
            this.checkLicensePlate(i_LicensePlate);
            ElectricEnergy currentEnergy = this.m_Vehicle[i_LicensePlate].Vehicle.Energy as ElectricEnergy;

            if (currentEnergy == null)
            {
                throw new ArgumentException("Vehicle doesn't have electric type energy!");
            }

            currentEnergy.Charge(i_MinutesToAdd);
        }

        public void UpdateVehicleStatus(string i_LicensePlate, string i_NewStatus)
        {
            Garage.eVehicleStatus statusToUpdate = LogicUtils.EnumValidation<Garage.eVehicleStatus>(i_NewStatus, k_VehicleStatusKey);
            this.checkLicensePlate(i_LicensePlate);
            this.m_Vehicle[i_LicensePlate].Status = statusToUpdate;
        }

        public string DisplayVehicleFullDeatails(string i_LicensePlate)
        {
            StringBuilder vehicleInfo = new StringBuilder();
            Vehicle vehicleToCheck = this.m_Vehicle[i_LicensePlate].Vehicle;
            vehicleInfo.Append(string.Format(
@"{6}License Plate: {0}
Model Name: {1}
Owner Name: {2}
Status in garage: {3}
Air pressure in tires: {4}
Tiers manufacturer: {5}
",  
    vehicleToCheck.LicensePlate, 
    vehicleToCheck.ModelName,
    this.m_Vehicle[i_LicensePlate].OwnerName,
    this.m_Vehicle[i_LicensePlate].Status, 
    vehicleToCheck.TiresList[0].CurrentAirPressure,
    vehicleToCheck.TiresList[0].ManufacturerName,
    Environment.NewLine));

            if (vehicleToCheck.Energy is FuelEnergy)
            {
                vehicleInfo.Append(string.Format(
@"Fuel Type: {0}
Fuel liters remaining percentage: {1}", 
                ((FuelEnergy)vehicleToCheck.Energy).FuelType, 
                vehicleToCheck.Energy.RemainingEnergyPercentage.ToString("0.##\\%")));
            }
            else if (vehicleToCheck.Energy is ElectricEnergy)
            {
                vehicleInfo.Append(string.Format(
@"Battery remaining percentage: {0}", vehicleToCheck.Energy.RemainingEnergyPercentage.ToString("0.##\\%")));
            }

            vehicleInfo.Append(string.Format("{0}{1}", Environment.NewLine, vehicleToCheck.GetUniquePropertiesInfo()));
            return vehicleInfo.ToString();
        }

        public string DisplayVehiclesList(string i_FilterByStatus)
        {
            eVehicleStatus fillter = eVehicleStatus.InProcess;
            StringBuilder licensePlatesStr = new StringBuilder();

            if (i_FilterByStatus != this.k_NoStatusFilter)
            {
                fillter = LogicUtils.EnumValidation<Garage.eVehicleStatus>(i_FilterByStatus, Garage.k_VehicleStatusKey);
            }

            foreach (ClientVehicle client in this.m_Vehicle.Values)
            {
                if (i_FilterByStatus == this.k_NoStatusFilter || client.Status == fillter)
                {
                    licensePlatesStr.Append(string.Format("{0}{1}", client.Vehicle.LicensePlate, Environment.NewLine).ToString());
                }
            }

            return licensePlatesStr.ToString();
        }

        private void checkLicensePlate(string i_LicensePlate)
        {
            if (!this.isVehicleInGarage(i_LicensePlate))
            {
                throw new ArgumentException("License plate not found!");
            }
        }
    }
}
