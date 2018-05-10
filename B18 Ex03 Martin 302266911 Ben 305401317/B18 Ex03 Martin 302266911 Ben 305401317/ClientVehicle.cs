using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    class ClientVehicle
    {
        private Vehicle m_Vehicle;
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private Garage.eStatus m_Status = Garage.eStatus.InProcess;

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

        public Garage.eStatus Status
        {
            get
            {
                return this.m_Status;
            }
        }
    }
}
