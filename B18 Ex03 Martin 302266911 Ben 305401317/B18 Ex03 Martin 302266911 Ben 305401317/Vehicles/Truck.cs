using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const int k_TruckNumberOfWheels = 12;
        private const float k_TruckMaxWheelPressure = 28f;
        private const string k_IsTrunkCooledKey = "Is trunk cooled";
        private const string k_TrunkCapacityKey = "Trunk capacity";


        private bool m_IsTrunkCooled;
        private float m_TrunkCapacity;


        public Truck(string i_LicensePlate, Energy i_EnergyType): base(i_LicensePlate, i_EnergyType, k_TruckNumberOfWheels)
        {
        }

        public bool isTrunkCooled
        {
            get
            {
                return this.m_IsTrunkCooled;
            }
        }

        public float TrunkCapacity
        {
            get
            {
                return this.m_TrunkCapacity;
            }
        }


        public override void UpdateUniqueProperties(string i_Key, string i_Value)
        {
            if (i_Key == k_IsTrunkCooledKey)
            {
                this.m_IsTrunkCooled = bool.Parse(i_Value);
            }
            else if (i_Key == k_TrunkCapacityKey)
            {
                this.m_TrunkCapacity = LogicUtils.NumericValueValidation(i_Value, float.MaxValue);
            }
            else
            {
                throw new ArgumentException("Invalid key");
            }
        }

        public override string GetUniquePropertiesInfo()
        {
            return String.Format(
@"Trunk capacity: {0}
Cooled trunk: {1}", m_LicensePlate, m_IsTrunkCooled);
        }

        public override void UpdateWheelsInfo(float i_CurrentPreasure, string i_ManufacturerName)
        {
            foreach (Wheel wheel in m_WheelsList)
            {
                wheel.MaxManufacturerAirPressure = k_TruckMaxWheelPressure;
                wheel.CurrentAirPressure = i_CurrentPreasure;
                wheel.ManufacturerName = i_ManufacturerName;
            }
        }

        public override Dictionary<string, string[]> GetUniqueAtttributesDictionary()
        {
            Dictionary<string, string[]> stringAttributes = new Dictionary<string, string[]>();

            string[] isCooled = {bool.TrueString, bool.FalseString};

            stringAttributes.Add("Trunk cooled", isCooled);
            stringAttributes.Add("Trunk capacity", new string[]{});

            return stringAttributes;
        }


    }
}
