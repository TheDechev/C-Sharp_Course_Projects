using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected const int k_UniqueAttributesNum = 2;
        protected readonly string m_LicensePlate;
        protected readonly List<Wheel> m_WheelsList;
        protected string m_ModelName;
        protected Energy m_Energy;

        internal Vehicle(string i_LicensePlate, Energy i_EnergyType)
        {
            this.m_WheelsList = new List<Wheel>();
            this.m_LicensePlate = i_LicensePlate;
            this.m_Energy = i_EnergyType;
        }

        public string ModelName
        {
            get
            {
                return this.m_ModelName;
            }
            set
            {
                m_ModelName = value;
            }
        }

        public string LicensePlate
        {
            get
            {
                return this.m_LicensePlate;
            }
        }

        public Energy Energy
        {
            get
            {
               return this.m_Energy;
            }
        }

        public List<Wheel> TiresList
        {
            get
            {
                return this.m_WheelsList;
            }
        }

        public void InflateWheelsToMax()
        {
            foreach(Wheel wheel in m_WheelsList)
            {
                wheel.Inflate(wheel.MaxAirPressure - wheel.CurrentAirPressure);
            }
        }

        public abstract void UpdateUniqueProperties(string i_Key, string i_Value);

        public abstract string GetUniquePropertiesInfo();

        public void UpdateWheelsInfo(float i_CurrentPreasure, string i_ManufacturerName)
        {
            foreach (Wheel wheel in m_WheelsList)
            {
                wheel.CurrentAirPressure = i_CurrentPreasure;
                wheel.ManufacturerName = i_ManufacturerName;
            }
        }

        public abstract Dictionary<string, string[]> GetUniqueAtttributesDictionary();

        protected void AddNewWheels(int i_NumberOfWheels, float i_MaxPressure)
        {
            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                Wheel wheel = new Wheel();
                wheel.MaxAirPressure = i_MaxPressure;
                this.m_WheelsList.Add(wheel);
            }
        }

    }
}
