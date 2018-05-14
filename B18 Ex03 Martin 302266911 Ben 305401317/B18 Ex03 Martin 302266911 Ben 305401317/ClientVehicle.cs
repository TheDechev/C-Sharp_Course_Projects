using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class ClientVehicle
    {
        private Vehicle m_Vehicle;
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private Garage.eVehicleStatus m_Status = Garage.eVehicleStatus.InProcess;

        public Vehicle Vehicle
        {
            get
            {
                return this.m_Vehicle;
            }
        }

        public string OwnerName
        {
            get
            {
                return this.m_OwnerName;
            }
        }

        public string OwnerPhoneNumber
        {
            get
            {
                return this.m_OwnerPhoneNumber;
            }
        }

        public Garage.eVehicleStatus Status
        {
            get
            {
                return this.m_Status;
            }

            set
            {
                this.m_Status = value;
            }
        }

        public ClientVehicle(Vehicle i_Vehicle, string i_Name, string i_PhoneNumber)
        {
            this.m_Vehicle = i_Vehicle;
            this.m_OwnerName = i_Name;
            this.m_OwnerPhoneNumber = i_PhoneNumber;
        }
    }
}
