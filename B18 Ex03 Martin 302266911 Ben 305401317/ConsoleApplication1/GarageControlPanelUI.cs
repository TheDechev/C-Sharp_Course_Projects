using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    class GarageControlPanelUI
    {
        private const string k_VehicleStatusKey = "Vehicle status";

        private Garage  m_garage = new Garage();
        
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
            string plateNumber = string.Empty;

            while (!exitProgram)
            { 
                PrintMenu();
                printEnterChoiceMsg();
                userChoice = getUserChoice();

                if (isUserMenuChoiceValid(userChoice) && userChoice != eUserChoice.DisplayVehicleList && userChoice != eUserChoice.ExitProgram)
                {
                    plateNumber = getRegistrationPlateNumber();
                }
                
                switch (userChoice)
                {
                    case eUserChoice.InsertNewVehicle:
                        insertNewVehicle(plateNumber);
                        break;
                    case eUserChoice.DisplayVehicleList:
                        displayVehiclesList();
                        break;
                    case eUserChoice.UpdateVehicleStatus:
                        updateVehicleStatus(plateNumber);
                        break;
                    case eUserChoice.InflateTieres:
                        inflateTieres(plateNumber);
                        break;
                    case eUserChoice.RefuelVehicle:
                        refuelVehicle(plateNumber);
                        break;
                    case eUserChoice.ChargeVehicle:
                        chargeVehicle(plateNumber);
                        break;
                    case eUserChoice.DisplayVehicleFullDetails:
                        displayVehicleFullDetails(plateNumber);
                        break;
                    case eUserChoice.ExitProgram:
                        exitProgram = true;
                        printExitPorgramMsg();
                        break;
                    default:
                        printInvalidInputMsg();
                        break;
                }

                Console.WriteLine("Press 'Enter' to continue..");
                Console.ReadLine();
                Console.Clear();
            }
           
    }

        private void printExitPorgramMsg()
        {
            Console.Write("Exiting program ");
            for (int i = 0; i < 10; i++)
            {
                Console.Write(" . ");
                Thread.Sleep(100);
            }
            Console.WriteLine();
        }

        private void inflateTieres(string i_PlateNumber)
        {
            throw new NotImplementedException();
        }

        private void refuelVehicle(string i_PlateNumber)
        {
            throw new NotImplementedException();
        }

        private void chargeVehicle(string i_PlateNumber)
        {
            throw new NotImplementedException();
        }

        private void displayVehicleFullDetails(string i_PlateNumber)
        {
            
            if (m_garage.isVehicleInGarage(i_PlateNumber))
            {
                Console.WriteLine("Vehicle Information:");
                Console.WriteLine(m_garage.DisplayVehicleFullDeatails(i_PlateNumber));
            }
            else
            {
                Console.WriteLine("This vehicle is not in the garage");
            }
        }

        private bool isUserMenuChoiceValid(eUserChoice i_UserChoice)
        {
            return Enum.IsDefined(typeof(eUserChoice), i_UserChoice);
        }

        private void printEnterChoiceMsg()
        {
            Console.Write("Enter your choice: ");
        }

        private void printInvalidInputMsg()
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

        private void updateVehicleStatus(string i_LicensePlate)
        {
            string statusToUpdateStr;
            bool isPlateExist = true;

            Garage.eVehicleStatus statusToUpdate;
            printUpdateStatusSubMenu();

            do
            {
                
                printEnterChoiceMsg();
                statusToUpdateStr = Console.ReadLine();
                statusToUpdate = LogicUtils.EnumValidation<Garage.eVehicleStatus>(statusToUpdateStr, k_VehicleStatusKey);
                try
                {
                    this.m_garage.UpdateVehicleStatus(i_LicensePlate, statusToUpdate);
                    isPlateExist = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (isPlateExist); 
        }

        private void printUpdateStatusSubMenu()
        {        
            Console.WriteLine(
@"Choose the desire status to update:
< 1 > In process
< 2 > Repaired
< 3 > Paid
");
    }

        private void displayVehiclesList()
        {
            string userChoice;
            Garage.eVehicleStatus fillter;

            Console.Clear();
            printVehicleListFillterSubMenu();
            printEnterChoiceMsg();
            userChoice = Console.ReadLine();
            fillter = LogicUtils.EnumValidation< Garage.eVehicleStatus>(userChoice, Garage.k_VehicleStatus);
            m_garage.DisplayVehiclesList(fillter);          
        }

        private void insertNewVehicle(string i_userPlateNum)
        {
            if (m_garage.isVehicleInGarage(i_userPlateNum))
            {
                this.m_garage.UpdateVehicleStatus(i_userPlateNum, Garage.eVehicleStatus.InProcess);
                Console.WriteLine(
@"This vehicle is already in the grage. 
vehicle's status was updated to: 'In Process'"); 
            }
            else
            {
                Vehicle newVehicle;
                newVehicle = CreateNewVehicle(i_userPlateNum);
            }
        }

        private Vehicle CreateNewVehicle(string i_UserPlateNumber)
        {
            Vehicle newVehicle;
            Vehicle.eVehicleType newVehicleType;
            string userChoice;

            printVehicleTypeSubMenu();
            printEnterChoiceMsg();
            userChoice = Console.ReadLine();
            
            newVehicleType = LogicUtils.EnumValidation<Vehicle.eVehicleType>(userChoice,Vehicle.k_VehicleTypeKey);
            newVehicle = VehicleFactory.CreateVehicle(i_UserPlateNumber, newVehicleType);

            SetVehicleInfo(newVehicle,newVehicleType);

            return newVehicle;

        }

        private void SetVehicleInfo(Vehicle i_VehicleToUpdate, Vehicle.eVehicleType i_vehicleType)
        {
            float currentAirPressure;
            Console.WriteLine("Please Enter the following information: {0}", Environment.NewLine);
            Console.Write("Model: ");
            i_VehicleToUpdate.ModelName = Console.ReadLine();
            Console.Write("{0}: ",i_VehicleToUpdate.Energy.EnergyUnits());
            i_VehicleToUpdate.Energy.CurrentEnergy = getNumericInput(i_VehicleToUpdate.Energy.MaxCapacity);
            Console.Write("Tires air pressure: ");
            currentAirPressure = getNumericInput(i_VehicleToUpdate.TiresList[0].MaxManufacturerAirPressure);
            Console.Write("Tiers manufacturer's name: ");
            i_VehicleToUpdate.UpdateWheelsInfo(currentAirPressure , Console.ReadLine());
            SetVehicleExraInfo(i_VehicleToUpdate, i_vehicleType);
        }

        private void SetVehicleExraInfo(Vehicle i_VehicleToUpdate, Vehicle.eVehicleType i_vehicleType)
        {
            Dictionary<string, string[]> uniqueAttributesDictionary = i_VehicleToUpdate.GetUniqueAtttributesDictionary();
            List<string> userInputAttributes = new List<string>(uniqueAttributesDictionary.Count);
            int attributeValuesNum;

            foreach (string key in uniqueAttributesDictionary.Keys)
            {
                attributeValuesNum = uniqueAttributesDictionary[key].Length;
                Console.WriteLine("Enter {0}: ", uniqueAttributesDictionary[key]);
                if (attributeValuesNum > 1)
                {
                    Console.WriteLine("Choose the following list {0}: ", key);
                    for (int i = 0; i < uniqueAttributesDictionary[key].Length; i++)
                    {
                        Console.WriteLine("< {0} > {1}", i + 1, uniqueAttributesDictionary[key][i]);
                    }
                }

                getUniquePropertyInput(i_VehicleToUpdate, key);
            }


        }

        private float getNumericInput(float i_MaximumValue)
        {
            string userInput;
            float numericInput = 0f;
            bool isValid = false;
            do
            {
                userInput = Console.ReadLine();
                try
                {
                    numericInput = LogicUtils.NumericValueValidation(userInput, i_MaximumValue);
                    isValid = true;
                }
                catch (ValueOutOfRangeException exception)
                {
                    Console.WriteLine(exception.Message);
                }

            } while (!isValid);

            return numericInput;
        }

        private void getUniquePropertyInput(Vehicle i_Vehicle, string i_Key)
        {
            string userInput;
            //float numericInput = 0f;
            bool isValid = false;
            do
            {
                userInput = Console.ReadLine();
                try
                {
                    i_Vehicle.UpdateUniqueProperties(i_Key, userInput);
                    isValid = true;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }

            } while (!isValid);

        }

        private void printVehicleTypeSubMenu()
        {
            Console.WriteLine(
@"Enter yours vehicle type:
< 1 > Electric car
< 2 > Fuel car
< 3 > Electric motorcycle
< 4 > Fuel motorcycle
< 5 > Fuel truck
        ");
        }

        private string getRegistrationPlateNumber()
        {
            Console.Write("Enter your registration plate number: ");
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
---------------------------------------------------------------
Please choose an action to execut:
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
