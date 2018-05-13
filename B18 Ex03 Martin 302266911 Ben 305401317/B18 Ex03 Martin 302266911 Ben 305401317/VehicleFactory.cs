using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public static class VehicleFactory
    {

        //Wheel pressure default values
        //Energy capacity default values
        private const float k_TruckMaxEnergyCapacity = 115f;
        private const float k_FuelCarMaxEnergyCapacity = 45f;
        private const float k_FuelMotorcycleMaxEnergyCapacity = 6f;
        private const float k_ElectricCarMaxEnergyCapacity = 3.2f;
        private const float k_ElectricMotorcycleMaxEnergyCapacity = 1.8f;

        //private List<Vehicle> m_SupportedVeihiclesInSystem;

        public static Vehicle CreateVehicle(string i_LicensePlate , Vehicle.eVehicleType i_VehicleType)
        {
            Vehicle newVehicle = null;
            Energy energyType = null;

            if (i_VehicleType == Vehicle.eVehicleType.FuelCar)
            {
                energyType = new FuelEnergy(FuelEnergy.eFuelType.Octan98, k_FuelCarMaxEnergyCapacity);
                newVehicle = new Car(i_LicensePlate, energyType);
            }
            else if (i_VehicleType == Vehicle.eVehicleType.ElectricCar)
            {
                energyType = new ElectricEnergy(k_ElectricCarMaxEnergyCapacity);
                newVehicle = new Car(i_LicensePlate, energyType);
            }
            else if (i_VehicleType == Vehicle.eVehicleType.ElectricMotorcycle)
            {
                energyType = new ElectricEnergy(k_ElectricMotorcycleMaxEnergyCapacity);
                newVehicle = new Motorcycle(i_LicensePlate,energyType);
            }
            else if (i_VehicleType == Vehicle.eVehicleType.FuelMotorcycle)
            {
                energyType = new FuelEnergy(FuelEnergy.eFuelType.Octan96, k_FuelMotorcycleMaxEnergyCapacity);
                newVehicle = new Motorcycle(i_LicensePlate, energyType);
            }
            else if (i_VehicleType == Vehicle.eVehicleType.FuelTruck)
            {
                energyType = new FuelEnergy(FuelEnergy.eFuelType.Soler, k_TruckMaxEnergyCapacity);
                newVehicle = new Truck(i_LicensePlate, energyType);
            }
            else
            {
                throw new ArgumentException("Invalid vehicle type inserted!"); 
            }

            return newVehicle;
        }
    }
}
