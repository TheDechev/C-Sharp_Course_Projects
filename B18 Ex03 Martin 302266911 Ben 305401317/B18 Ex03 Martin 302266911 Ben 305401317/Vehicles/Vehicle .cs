using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected const int k_UniqueAttributesNum = 2;
        protected readonly string m_LicensePlate;
        protected readonly List<Tire> m_TiresList;
        protected string m_ModelName;
        protected Energy m_Energy;

        internal Vehicle(string i_LicensePlate, Energy i_EnergyType)
        {
            this.m_TiresList = new List<Tire>();
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
                this.m_ModelName = value;
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

        public List<Tire> TiresList
        {
            get
            {
                return this.m_TiresList;
            }
        }

        public abstract void UpdateUniqueProperties(string i_Key, string i_Value);

        public abstract string GetUniquePropertiesInfo();

        public void UpdateTiresInfo(float i_CurrentPreasure, string i_ManufacturerName)
        {
            foreach (Tire tire in this.m_TiresList)
            {
                tire.CurrentAirPressure = i_CurrentPreasure;
                tire.ManufacturerName = i_ManufacturerName;
            }
        }

        public abstract Dictionary<string, string[]> GetUniqueAtttributesDictionary();

        public void InflateTiresToMax()
        {
            foreach (Tire tire in this.m_TiresList)
            {
                tire.Inflate(tire.MaxAirPressure - tire.CurrentAirPressure);
            }
        }

        protected void AddNewTires(int i_NumberOfTires, float i_MaxPressure)
        {
            for (int i = 0; i < i_NumberOfTires; i++)
            {
                Tire tire = new Tire();
                tire.MaxAirPressure = i_MaxPressure;
                this.m_TiresList.Add(tire);
            }
        }
    }
}
