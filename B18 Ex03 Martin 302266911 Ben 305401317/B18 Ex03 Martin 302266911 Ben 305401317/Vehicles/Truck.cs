using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const int k_TruckNumberOfTires = 12;
        private const float k_TruckMaxTirePressure = 28f;
        private const string k_IsTrunkCooledKey = "Is trunk cooled";
        private const string k_TrunkCapacityKey = "Trunk capacity";

        private bool m_IsTrunkCooled;
        private float m_TrunkCapacity;

        internal Truck(string i_LicensePlate, Energy i_EnergyType) : base(i_LicensePlate, i_EnergyType)
        {
            this.AddNewTires(k_TruckNumberOfTires, k_TruckMaxTirePressure);
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
            const int trueValue = 1;
            const int falseValue = 2;
            int trunkCool;
            if (i_Key == k_IsTrunkCooledKey)
            {
                int.TryParse(i_Value, out trunkCool);

                if(trunkCool > falseValue || trunkCool < trueValue)
                {
                    throw new ValueOutOfRangeException(0, 1);
                }

                this.m_IsTrunkCooled = trunkCool == trueValue;
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
            return string.Format(
@"Trunk capacity: {0}
Cooled trunk: {1}", 
            this.m_LicensePlate, 
            this.m_IsTrunkCooled);
        }

        public override Dictionary<string, string[]> GetUniqueAtttributesDictionary()
        {
            Dictionary<string, string[]> stringAttributes = new Dictionary<string, string[]>();
            string[] isCooled = { bool.TrueString, bool.FalseString };
            stringAttributes.Add(k_IsTrunkCooledKey, isCooled);
            stringAttributes.Add(k_TrunkCapacityKey, new string[] { });

            return stringAttributes;
        }
    }
}
