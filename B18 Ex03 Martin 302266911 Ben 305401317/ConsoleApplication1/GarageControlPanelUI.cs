using System;
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

            while (!exitProgram)
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

        private void insertNewVehicle()
        {
            string userPlateNum = getRegistrationPlateNumber();
            if (m_garage.isVehicleInGarage(userPlateNum))
            {
                this.m_garage.UpdateVehicleStatus(userPlateNum, GarageLogic.Garage.eVehicleStatus.InProcess);
                Console.WriteLine(
@"This vehicle is already in the grage. 
vehicle's status was updated to: 'In Process'"); 
            }
            else
            {
                getInfoAndcreateNewVehicle();
                GarageLogic.Vehicle newVehicle = GarageLogic.VehicleFactory.CreateVehicle(userPlateNum,);
            }

         
            
        }

        private void getInfoAndcreateNewVehicle()
        {
            GarageLogic.Vehicle newVehicle;

            printVehicleTypeSubMenu();
            printEnterChoiceMsg();

            eColor carColor = (eColor)Enum.ToObject(typeof(eColor), i_FirstProperty);

            if (!Enum.IsDefined(typeof(eColor), carColor))
            {
                throw new ValueOutOfRangeException("Invalid license plate!");
            }

            GarageLogic.Vehicle.eVehicleType vehicleType = (GarageLogic.Vehicle.eVehicleType)Enum.ToObject(typeof(GarageLogic.Vehicle.eVehicleType), i_FirstProperty);

            newVehicle = GarageLogic.VehicleFactory.CreateVehicle();



        }

        public  T getEnumFromUser<T>()
        {
            string value = Console.ReadLine();

            T userInput = (T)Enum.ToObject(typeof(T), value);
            if (!Enum.IsDefined(typeof(T), userInput))
            {
                //ERROR
            }
            else
            {
                return userInput;
            }

            return userInput;
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
