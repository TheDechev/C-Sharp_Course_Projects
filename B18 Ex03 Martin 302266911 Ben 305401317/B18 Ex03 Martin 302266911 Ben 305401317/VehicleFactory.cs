using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class VehicleFactory
    {


        //Wheels number default values
        private const int k_CarNumberOfWheels = 4;
        private const int k_MotorcycleNumberOfWheels = 2;
        private const int k_TruckNumberOfWheels = 12;
        //Wheel pressure default values
        private const float k_MotorcycleMaxWheelPressure = 30f;
        private const float k_TruckMaxWheelPressure = 28f;
        private const float k_FuelCarMaxWheelPressure = 30f;
        private const float k_ElectricCarMaxWheelPressure = 32f;
        //Energy capacity default values
        private const float k_TruckMaxEnergyCapacity = 115f;
        private const float k_FuelCarMaxEnergyCapacity = 45f;
        private const float k_FuelMotorcycleMaxEnergyCapacity = 6f;
        private const float k_ElectricCarMaxEnergyCapacity = 3.2f;
        private const float k_ElectricMotorcycleMaxEnergyCapacity = 1.8f;

        //private List<Vehicle> m_SupportedVeihiclesInSystem;

        public Vehicle CreateVehicle(string i_LicensePlate, string i_ModelName,float i_StartingEnergy, e_VehicleType i_VechicleType)
        {
            Vehicle newVehicle = null;
            Energy energyType = null;

            if (i_VechicleType == Vehicle.e_VehicleType.FuelCar)
            {
                energyType = new FuelEnergy(FuelEnergy.eFuelType.Octan98, i_StartingEnergy, k_FuelCarMaxEnergyCapacity);
                newVehicle = new Car(i_LicensePlate, i_ModelName, k_CarNumberOfWheels, energyType.CurrentEnergy, energyType);
            }
            else if (i_VechicleType == Vehicle.e_VehicleType.ElectricCar)
            {
                energyType = new ElectricEnergy(i_StartingEnergy, k_ElectricCarMaxEnergyCapacity);
                newVehicle = new Car(i_LicensePlate, i_ModelName, k_CarNumberOfWheels, energyType.CurrentEnergy, energyType);
            }
            else if (i_VechicleType == Vehicle.e_VehicleType.ElectricMotorcycle)
            {
                energyType = new ElectricEnergy(i_StartingEnergy, k_ElectricMotorcycleMaxEnergyCapacity);
                newVehicle = new Motorcycle(i_LicensePlate, i_ModelName, k_MotorcycleNumberOfWheels, energyType.CurrentEnergy, energyType);
            }
            else if (i_VechicleType == Vehicle.e_VehicleType.FuelMotorcycle)
            {
                energyType = new FuelEnergy(FuelEnergy.eFuelType.Octan96, i_StartingEnergy, k_FuelMotorcycleMaxEnergyCapacity);
                newVehicle = new Motorcycle(i_LicensePlate, i_ModelName, k_MotorcycleNumberOfWheels, energyType.CurrentEnergy, energyType);
            }
            else if (i_VechicleType == Vehicle.e_VehicleType.FuelTruck)
            {
                energyType = new FuelEnergy(FuelEnergy.eFuelType.Soler, i_StartingEnergy, k_TruckMaxEnergyCapacity);
                newVehicle = new Truck(i_LicensePlate, i_ModelName, k_TruckNumberOfWheels, energyType.CurrentEnergy, energyType);
            }
            else
            {
                throw new ArgumentException("Invalid vehicle type inserted!"); 
            }

            return newVehicle;
        }
    }
}
