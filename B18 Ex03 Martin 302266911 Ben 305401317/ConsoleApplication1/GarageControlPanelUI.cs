using System;
using System.Collections.Generic;
using System.Threading;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageControlPanelUI
    {
        private const string k_UserChoiceKey = "User choice";
        private const string k_PhoneNumberMsg = "Client's phone number: ";
        private Garage m_Garage = new Garage();

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
                string plateNumber = string.Empty;
                this.printMenu();
                userChoice = this.getUserChoice();
                this.getLicensePlate(ref plateNumber, ref userChoice);

                switch (userChoice)
                {
                    case eUserChoice.InsertNewVehicle:
                        this.insertNewVehicle(plateNumber);
                        break;
                    case eUserChoice.DisplayVehicleList:
                        this.displayVehiclesLicensePlateList();
                        break;
                    case eUserChoice.UpdateVehicleStatus:
                        this.updateVehicleStatus(plateNumber);
                        break;
                    case eUserChoice.InflateTieres:
                        this.inflateTieresToMax(plateNumber);
                        break;
                    case eUserChoice.RefuelVehicle:
                        this.refuelVehicle(plateNumber);
                        break;
                    case eUserChoice.ChargeVehicle:
                        this.chargeVehicle(plateNumber);
                        break;
                    case eUserChoice.DisplayVehicleFullDetails:
                        this.displayVehicleFullDetails(plateNumber);
                        break;
                    case eUserChoice.ExitProgram:
                        exitProgram = true;
                        this.printExitPorgramMsg();
                        return;
                }

                Console.WriteLine("{0}< Press any key to return the main menu! >", Environment.NewLine);
                Console.ReadLine();
                Console.Clear();
            }
        }

        private void insertNewVehicle(string i_userPlateNum)
        {
            if (this.m_Garage.isVehicleInGarage(i_userPlateNum))
            {
                this.m_Garage.UpdateVehicleStatus(i_userPlateNum, ((int)Garage.eVehicleStatus.InProcess).ToString());
                Console.WriteLine(
@"This vehicle is already in the garage. 
vehicle's status was updated to: 'In Process'");
            }
            else
            {
                Vehicle newVehicle;
                newVehicle = this.createNewVehicle(i_userPlateNum);
                string clientName, clientPhone;
                Console.Write("Client's name: ");
                clientName = Console.ReadLine().Trim();
                clientPhone = this.getNumericInput(0, k_PhoneNumberMsg).ToString().Trim();
                this.m_Garage.insertVehicle(newVehicle, clientName, clientPhone);
                Console.WriteLine("{0}Vehicle added successfuly!", Environment.NewLine);
            }
        }

        private Vehicle createNewVehicle(string i_UserPlateNumber)
        {
            bool isVehicleTypeValid = false;
            Vehicle newVehicle;
            VehicleFactory.eVehicleType newVehicleType = VehicleFactory.eVehicleType.FuelCar;
            string userChoice;
            Console.WriteLine("{0}Please Enter the following information: {0}", Environment.NewLine);
            this.printMultiChoiceList(VehicleFactory.VehicleTypeKey, Enum.GetNames(typeof(VehicleFactory.eVehicleType)));
            this.printEnterChoiceMsg();
            do
            {
                try
                {
                    userChoice = Console.ReadLine();
                    newVehicleType = LogicUtils.EnumValidation<VehicleFactory.eVehicleType>(userChoice, VehicleFactory.VehicleTypeKey);
                    isVehicleTypeValid = true;
                }
                catch(Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            while (!isVehicleTypeValid);

            newVehicle = VehicleFactory.CreateVehicle(i_UserPlateNumber, newVehicleType);
            this.setVehicleInfo(newVehicle, newVehicleType);

            return newVehicle;
        }

        private void setVehicleInfo(Vehicle i_VehicleToUpdate, VehicleFactory.eVehicleType i_vehicleType)
        {
            float currentAirPressure;
            Console.Write("Model: ");
            i_VehicleToUpdate.ModelName = Console.ReadLine().Trim();
            i_VehicleToUpdate.Energy.CurrentEnergy = this.getNumericInput(i_VehicleToUpdate.Energy.MaxCapacity, string.Format("{0}: ", i_VehicleToUpdate.Energy.EnergyUnits()).ToString());
            currentAirPressure = this.getNumericInput(i_VehicleToUpdate.TiresList[0].MaxAirPressure, "Tires air pressure: ");
            Console.Write("Tiers manufacturer's name: ");
            i_VehicleToUpdate.UpdateTiresInfo(currentAirPressure, Console.ReadLine().Trim());
            this.setVehicleUniqueInfo(i_VehicleToUpdate, i_vehicleType);
        }

        private void setVehicleUniqueInfo(Vehicle i_VehicleToUpdate, VehicleFactory.eVehicleType i_vehicleType)
        {
            Dictionary<string, string[]> uniqueAttributesDictionary = i_VehicleToUpdate.GetUniqueAtttributesDictionary();
            List<string> userInputAttributes = new List<string>(uniqueAttributesDictionary.Count);
            int attributeValuesNum;

            foreach (string key in uniqueAttributesDictionary.Keys)
            {
                attributeValuesNum = uniqueAttributesDictionary[key].Length;

                if (attributeValuesNum > 1)
                {
                    this.printMultiChoiceList(key, uniqueAttributesDictionary[key]);
                    this.printEnterChoiceMsg();
                }
                else
                {
                    Console.Write("Enter {0}: ", key);
                }

                this.setVehicleUniquePropertyInput(i_VehicleToUpdate, key);
            }
        }

        private void setVehicleUniquePropertyInput(Vehicle i_Vehicle, string i_Key)
        {
            string userInput;
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
            }
            while (!isValid);
        }

        private void displayVehiclesLicensePlateList()
        {
            bool isStatusValid = false;
            string filterStr = string.Empty;
            string[] enumNames = Enum.GetNames(typeof(Garage.eVehicleStatus));
            this.printMultiChoiceList(Garage.VehicleStatusKey, enumNames);
            Console.WriteLine("< {0} > None", enumNames.Length + 1);
            do
            {
                try
                {
                    this.printEnterChoiceMsg();
                    filterStr = Console.ReadLine();
                    string displayVehicleList = this.m_Garage.DisplayVehiclesList(filterStr);
                    Console.WriteLine("{0}Vehicles in garage with the chosen status are: ", Environment.NewLine);
                    if (displayVehicleList == string.Empty)
                    {
                        displayVehicleList = "<No vehicles in garage with this status.>";
                    }

                    Console.WriteLine("{0}{1}", Environment.NewLine, displayVehicleList);
                    isStatusValid = true;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            while (!isStatusValid);
        }

        private void updateVehicleStatus(string i_LicensePlate)
        {
            bool isUpdateSuccessful = false;
            this.printMultiChoiceList(Garage.VehicleStatusKey, Enum.GetNames(typeof(Garage.eVehicleStatus)));

            do
            {
                try
                {
                    this.printEnterChoiceMsg();
                    string statusToUpdateStr = Console.ReadLine();
                    this.m_Garage.UpdateVehicleStatus(i_LicensePlate, statusToUpdateStr);
                    isUpdateSuccessful = true;
                    Console.WriteLine("The vehicle with license plate \"{0}\" status was updated successfuly", i_LicensePlate);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            while (!isUpdateSuccessful);
        }

        private void inflateTieresToMax(string i_PlateNumber)
        {
            this.m_Garage.InflateTireToMax(i_PlateNumber);
            Console.WriteLine("The vehicle's tires inflated to maximum air pressure");
        }

        private void refuelVehicle(string i_PlateNumber)
        {
            this.printMultiChoiceList(FuelEnergy.FuelTypeKey, Enum.GetNames(typeof(FuelEnergy.eFuelType)));
            this.printEnterChoiceMsg();
            string i_Choice = Console.ReadLine();

            try
            {
                FuelEnergy.eFuelType fuelTypeChosen = LogicUtils.EnumValidation<FuelEnergy.eFuelType>(i_Choice, FuelEnergy.FuelTypeKey);
                float amountToAdd = this.getAmountOfUnitsToAdd(FuelEnergy.FuelUnits);
                this.m_Garage.RefuelFuelVehicle(i_PlateNumber, fuelTypeChosen, amountToAdd);
                Console.WriteLine("The vehicle with license plate: {0} was refueled successfuly!", i_PlateNumber);
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void chargeVehicle(string i_PlateNumber)
        {
            float amountToAdd = this.getAmountOfUnitsToAdd(ElectricEnergy.ElectricUnits);

            try
            {
                this.m_Garage.RechargeElectricVehicle(i_PlateNumber, amountToAdd);
                Console.WriteLine("The vehicle with license plate: {0} was charged successfuly with {1} hours!", i_PlateNumber, amountToAdd);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private float getAmountOfUnitsToAdd(string i_UnitsToAddKey)
        {
            Console.Write("Enter {0} to add: ", i_UnitsToAddKey);
            float userInput;
            bool isSuccessful = float.TryParse(Console.ReadLine(), out userInput);

            while (!isSuccessful)
            {
                Console.WriteLine("Invalid input!");
                isSuccessful = float.TryParse(Console.ReadLine(), out userInput);
            }

            return userInput;
        }

        private void displayVehicleFullDetails(string i_PlateNumber)
        {
            Console.Clear();
            Console.WriteLine("Vehicle Information:");
            Console.WriteLine(this.m_Garage.DisplayVehicleFullDeatails(i_PlateNumber));
        }

        private float getNumericInput(float i_MaximumValue, string i_AskUserMsg)
        {
            string userInput;
            float numericInput = 0f;
            bool isValid = false;
            do
            {
                Console.Write(i_AskUserMsg);
                userInput = Console.ReadLine();
                try
                {
                    if(i_AskUserMsg == k_PhoneNumberMsg)
                    {
                        numericInput = LogicUtils.NumericValueValidation(userInput);
                    }
                    else
                    {
                        numericInput = LogicUtils.NumericValueValidation(userInput, i_MaximumValue);
                    }
                
                    isValid = true;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            while (!isValid);

            return numericInput;
        }

        private eUserChoice getUserChoice()
        {
            eUserChoice userChoice = eUserChoice.ExitProgram;
            string userChoiceStr;
            bool isValidInput = false;

            do
            {
                try
                {
                    this.printEnterChoiceMsg();
                    userChoiceStr = Console.ReadLine();
                    userChoice = LogicUtils.EnumValidation<eUserChoice>(userChoiceStr, k_UserChoiceKey);
                    isValidInput = true;
                }
                catch(Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            while (!isValidInput);

            return userChoice;
        }

        private void getLicensePlate(ref string io_PlateNumber, ref eUserChoice io_UserChoice)
        {
            if (Enum.IsDefined(typeof(eUserChoice), io_UserChoice) && io_UserChoice != eUserChoice.DisplayVehicleList && io_UserChoice != eUserChoice.ExitProgram)
            {
                Console.Write("Enter your registration plate number: ");
                io_PlateNumber = Console.ReadLine().Trim();
                bool isVehicleInGarage = this.m_Garage.isVehicleInGarage(io_PlateNumber);

                if (!isVehicleInGarage)
                {
                    if (io_UserChoice != eUserChoice.InsertNewVehicle)
                    {
                        Console.WriteLine("No matching vehicle with license plate \"{0}\" found in the garage.", io_PlateNumber);
                        io_UserChoice = 0;
                    }
                }
            }
        }

        private void printEnterChoiceMsg()
        {
            Console.Write("Enter your choice: ");
        }

        private void printMultiChoiceList(string i_ListKey, string[] i_List)
        {
            Console.WriteLine("Choose from the following {0} list: ", i_ListKey);
           
            for (int i = 0; i < i_List.Length; i++)
            {
                Console.WriteLine("< {0} > {1}", i + 1, i_List[i]);
            }
        }

        private void printMenu()
        {
            Console.WriteLine(
@"Hello! Welcome to the Garage Control pannel :)
---------------------------------------------------------------
Please choose an action to execut:
< 1 > Insert a new vehicle to the system.
< 2 > Display all the registration plates list of the vehicles.
< 3 > Update vehicle's status.
< 4 > Inflate vehicle's tires to maximum.
< 5 > Refuel a vehicle powered by fuel.
< 6 > Charge an electric vehicle.
< 7 > Display vehicle's full details.
< 8 > Exit
    ");
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
    }
}
