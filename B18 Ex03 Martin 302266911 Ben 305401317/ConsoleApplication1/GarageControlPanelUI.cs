using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    class GarageControlPanelUI
    {

        
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

            do
            {
                PrintMenu();
                printEnterChoiceMsg();
                userChoice = getUserChoice();

                switch (userChoice)
                {
                    case eUserChoice.InsertNewVehicle:
                        insertNewVehicle();
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
            while (!exitProgram);
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

        private void updateVehicleStatus()
        {
            throw new NotImplementedException();
        }

        private void displayVehiclesList()
        {
            throw new NotImplementedException();
        }

        private void insertNewVehicle()
        {

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
