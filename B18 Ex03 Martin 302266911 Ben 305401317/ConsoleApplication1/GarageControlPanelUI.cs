﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    class GarageControlPanelUI
    {
        private GarageLogic.Garage  m_garage = new GarageLogic.Garage();
        
        private enum eUserChoice
        {
            InsertNewVehicle = 1,
            DisplayVehicleList,
            UpdateVehicleStatus,
            InflateTieres,
            RefuelVehicle,
            ChargeVehicle,
            DisplayVehicleFullDetails,
            ExitProgram
        }


        public void Run()
        {
            bool exitProgram = false;
            eUserChoice userChoice;
            string plateNumber;

            while (!exitProgram)
            { 
                PrintMenu();
                printEnterChoiceMsg();
                userChoice = getUserChoice();

                /// IF USER CHOICE IS NOT ALL LIST AND VALID
                plateNumber = getRegistrationPlateNumber();

                switch (userChoice)
                {
                    case eUserChoice.InsertNewVehicle:
                        insertNewVehicle(plateNumber);
                        break;
                    case eUserChoice.DisplayVehicleList:
                        displayVehiclesList();
                        break;
                    case eUserChoice.UpdateVehicleStatus:
                        updateVehicleStatus();
                        break;
                    case eUserChoice.InflateTieres:
                        inflateTieres();
                        break;
                    case eUserChoice.RefuelVehicle:
                        refuelVehicle();
                        break;
                    case eUserChoice.ChargeVehicle:
                        chargeVehicle();
                        break;
                    case eUserChoice.DisplayVehicleFullDetails:
                        displayVehicleFullDetails();
                        break;
                    case eUserChoice.ExitProgram:
                        exitProgram = true;
                        break;
                    default:
                        printInvalidInputMsg();
                        break;
                }
            }
           
    }

        private void printEnterChoiceMsg()
        {
            Console.Write("Please Enter your choice: ");
        }

        private void printInvalidInputMsg()
        {
            throw new NotImplementedException();
        }

        private void displayVehicleFullDetails()
        {
            throw new NotImplementedException();
        }

        private void chargeVehicle()
        {
            throw new NotImplementedException();
        }

        private void refuelVehicle()
        {
            throw new NotImplementedException();
        }

        private void inflateTieres()
        {
            throw new NotImplementedException();
        }

        private void updateVehicleStatus(GarageLogic.Garage.eVehicleStatus vehicleStatus)
        {
            this.m_garage.UpdateVehicleStatus();
        }

        private void displayVehiclesList()
        {
            Console.Clear();
            printVehicleListFillterSubMenu();
            printEnterChoiceMsg();
                        
            
        }

        private void insertNewVehicle(string i_userPlateNum)
        {
            if (m_garage.isVehicleInGarage(i_userPlateNum))
            {
                this.m_garage.UpdateVehicleStatus(i_userPlateNum, GarageLogic.Garage.eVehicleStatus.InProcess);
                Console.WriteLine(
@"This vehicle is already in the grage. 
vehicle's status was updated to: 'In Process'"); 
            }
            else
            {
                GarageLogic.Vehicle newVehicle;
                newVehicle = CreateNewVehicle(i_userPlateNum);
                SetBasicVehicleInfo();
            }
        }

        private GarageLogic.Vehicle CreateNewVehicle(string i_UserPlateNumber)
        {
            GarageLogic.Vehicle newVehicle;
            GarageLogic.Vehicle.eVehicleType newVehicleType;
            string userChoice;

            printVehicleTypeSubMenu();
            printEnterChoiceMsg();
            userChoice = Console.ReadLine();
            
            newVehicleType = GarageLogic.LogicUtils.EnumParse<GarageLogic.Vehicle.eVehicleType>(userChoice);
            newVehicle = GarageLogic.VehicleFactory.CreateVehicle(i_UserPlateNumber, newVehicleType);

            return newVehicle;

        }

        private void printVehicleTypeSubMenu()
        {
            Console.WriteLine(
@"Please Enter yours vehicle type:
        < 1 > Electric car
        < 2 > Fuel car
        < 3 > Electric motorcycle
        < 4 > Fuel motorcycle
        < 5 > Fuel truck
            ");
        }

        private string getRegistrationPlateNumber()
        {
            Console.Write("Please Enter your registration plate number: ");
            string userPlateNum = Console.ReadLine();

            return userPlateNum;
        }

        private eUserChoice getUserChoice()
        {
            eUserChoice userChoice;
            string userChoiceStr;

            userChoiceStr = Console.ReadLine();
            userChoice = (eUserChoice)Enum.Parse(typeof(eUserChoice), userChoiceStr);

            return userChoice;
        }

        private void PrintMenu()
        {
            Console.WriteLine(
      @"Hello! Welcome to the Garage Control pannel :)
        Please Enter XXXXXX execut:
        < 1 > Insert a new vehicle to the system.
        < 2 > Display all the registration plates list of the vehicles.
        < 3 > Update vehicle's status.
        < 4 > Inflate vehicle's wheels to maximum.
        < 5 > Refuel a vehicle powered by fuel.
        < 6 > Charge an electric vehicle.
        < 7 > Display vehicle's full details.
        < 8 > Exit
            ");
        }

        private void printVehicleListFillterSubMenu()
        {
            Console.WriteLine(
      @"Please Enter the fillter method
        < 1 > In process
        < 2 > Repaired
        < 3 > Paid
        < 4 > Without fillter
            ");
        }
    }
}
