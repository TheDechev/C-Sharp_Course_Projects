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

        public enum eVehicleStatus
        {
            InProcess,
            Repaired,
            Paid
        }

        //=========== TODO ========================================================


        public void insertVehicle(Vehicle i_VehicleToAdd, string i_ClientName, string i_ClientPhoneNumber)
        {

            if (isVehicleInGarage(i_VehicleToAdd.LicensePlate))
            {
                m_Vehicle[i_VehicleToAdd.LicensePlate].Status = eVehicleStatus.InProcess;
                throw new Exception("Vehicle already exists!");
            }

            m_Vehicle.Add(i_VehicleToAdd.LicensePlate, new ClientVehicle(i_VehicleToAdd,i_ClientName, i_ClientPhoneNumber)); //TODO: Need to get the client's name and phone number
        }


        public void DisplayVehiclesList()
        {   // fillter option by process status
            // 1. InProcess
            // 2. Repaired
            // 3. Paid
            // 4. no fillter
        }

        public void DisplayVehicleFullDeatails(string i_LicensePlate)
        {
            if (isVehicleInGarage(i_LicensePlate))
            {
                throw new Exception("Vehicle not in garage!");
            }
        }



       //=========== DONE FOR *NOW* ========================================================

        private bool isVehicleInGarage(string i_LicenseToCheck)
        {
            return this.m_Vehicle.ContainsKey(i_LicenseToCheck);
        }

        public void InflateTireToMax(string i_LicensePlate)
        {
            if (!isVehicleInGarage(i_LicensePlate))
            {
                throw new ArgumentException("License plate not found.");
            }

            m_Vehicle[i_LicensePlate].Vehicle.InflateTiersToMax();
        }

        public void RefuelFuelVehicle(string i_LicensePlate, FuelEnergy.eFuelType i_FuelType, float i_FuelToAdd)
        {
            if (!isVehicleInGarage(i_LicensePlate))
            {
                throw new ArgumentException("License plate not found.");
            }

            FuelEnergy currentEnergy = m_Vehicle[i_LicensePlate].Vehicle.Energy as FuelEnergy;

            currentEnergy.Refuel(i_FuelToAdd, i_FuelType);
        }

        public void RechargeElectricVehicle(string i_LicensePlate, float i_MinutesToAdd)
        {
            if (!isVehicleInGarage(i_LicensePlate))
            {
                throw new ArgumentException("License plate not found.");
            }

            ElectricEnergy currentEnergy = m_Vehicle[i_LicensePlate].Vehicle.Energy as ElectricEnergy;

            currentEnergy.Charge(i_MinutesToAdd);
        }

        public void UpdateVehicleStatus(string i_LicensePlate, eVehicleStatus i_NewStatus)
        {
            if (!isVehicleInGarage(i_LicensePlate))
            {
                throw new ArgumentException("License plate not found.");
            }

            m_Vehicle[i_LicensePlate].Status = i_NewStatus;
        }

    }
}
