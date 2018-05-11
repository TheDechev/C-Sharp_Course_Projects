﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class VehicleFactory
    {
        public enum e_VehicleType
        {
            ElectricCar,
            FuelCar,
            ElectricMotorcycle,
            FuelMotorcycle,
            FuelTruck
        }

        private List<Vehicle> m_SupportedVeihiclesInSystem;

        public Vehicle CreateVehicle()
        {
            Vehicle newVehicle = new Car("ModelName","1994",Car.eColor.White, Car.eDoorsNumber.Five);

            return newVehicle;
        }
    }
}
