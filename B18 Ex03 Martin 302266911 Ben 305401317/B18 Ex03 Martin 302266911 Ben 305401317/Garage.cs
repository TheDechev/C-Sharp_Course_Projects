using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {

        public const string k_VehicleStatus = "Vehicle status";
        public const string k_LicenseNotFound = "License plate not found!";
        private Dictionary<string, ClientVehicle> m_Vehicle = new Dictionary<string, ClientVehicle>();

        public Garage()
        {
            //TODO: DELETE THESE TEST OBJECTS
            Vehicle testVehicle1 = VehicleFactory.CreateVehicle("FuelCar-Test", Vehicle.eVehicleType.FuelCar);
            Vehicle testVehicle2 = VehicleFactory.CreateVehicle("ElectricCar-Test", Vehicle.eVehicleType.ElectricCar);
            Vehicle testVehicle3 = VehicleFactory.CreateVehicle("FuelMotorcycle-Test", Vehicle.eVehicleType.FuelMotorcycle);
            Vehicle testVehicle4 = VehicleFactory.CreateVehicle("FuelTruck-Test", Vehicle.eVehicleType.FuelTruck);
            Vehicle testVehicle5 = VehicleFactory.CreateVehicle("ElectricMotorcycle-Test", Vehicle.eVehicleType.ElectricMotorcycle);
            m_Vehicle.Add("FuelCar-Test", new ClientVehicle(testVehicle1, "Martin", "050-0125456"));
            m_Vehicle.Add("ElectricCar-Test", new ClientVehicle(testVehicle2, "Ben", "050-7123456"));
            m_Vehicle.Add("FuelMotorcycle-Test", new ClientVehicle(testVehicle3, "Mrt-Ben", "050-0423456"));
            m_Vehicle.Add("FuelTruck-Test", new ClientVehicle(testVehicle4, "MBn", "050-0123456"));
            m_Vehicle.Add("ElectricMotorcycle-Test", new ClientVehicle(testVehicle5, "Be", "050-5123456"));
        }

        public enum eVehicleStatus
        {
            InProcess = 1,
            Repaired,
            Paid,
            None
        }

        public void insertVehicle(Vehicle i_VehicleToAdd, string i_ClientName, string i_ClientPhoneNumber)
        {
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
@"{6}License Plate: {0}
Model Name: {1}
Owner Name: {2}
Status in garage: {3}
Air pressure in tires: {4}
Tiers manufacturer: {5}
",  vehicleToCheck.LicensePlate, 
    vehicleToCheck.ModelName,
    m_Vehicle[i_LicensePlate].OwnerName,
    m_Vehicle[i_LicensePlate].Status, 
    vehicleToCheck.TiresList[0].CurrentAirPressure,
    vehicleToCheck.TiresList[0].ManufacturerName,Environment.NewLine)) ;

            if (vehicleToCheck.Energy is FuelEnergy)
            {
                vehicleInfo.Append(String.Format(
@"Fuel Type: {0}
Fuel liters remaining percentage: {1}", ((FuelEnergy)vehicleToCheck.Energy).FuelType, vehicleToCheck.Energy.RemainingEnergyPercentage));
            }
            else if (vehicleToCheck.Energy is ElectricEnergy)
            {
                vehicleInfo.Append(String.Format(
@"Battery ramining percentage: {0}", vehicleToCheck.Energy.RemainingEnergyPercentage));
            }

            vehicleInfo.Append(String.Format("{0}{1}",Environment.NewLine, vehicleToCheck.GetUniquePropertiesInfo()));

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
                throw new ArgumentException(k_LicenseNotFound);
            }
        }

    }
}
