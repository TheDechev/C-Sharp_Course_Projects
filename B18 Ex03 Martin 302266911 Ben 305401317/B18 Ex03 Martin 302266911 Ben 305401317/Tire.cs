namespace Ex03.GarageLogic
{
    public class Tire
    {
        private string m_ManufacturerName;
        private float m_MaxAirPressure;
        private float m_CurrentAirPressure;

        public string ManufacturerName
        {
            get
            {
                return this.m_ManufacturerName;
            }

            set
            {
                this.m_ManufacturerName = value;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return this.m_CurrentAirPressure;
            }

            set
            {
                if(value > this.m_MaxAirPressure)
                {
                    throw new ValueOutOfRangeException("Air preassure exceeding the maximum value!");
                }

                this.m_CurrentAirPressure = value;
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return this.m_MaxAirPressure;
            }

            set
            {
                this.m_MaxAirPressure = value;
            }
        }

        public void Inflate(float i_AmountToAdd)
        {
           if(this.m_CurrentAirPressure + i_AmountToAdd > this.m_MaxAirPressure)
            {
                throw new ValueOutOfRangeException(this.m_CurrentAirPressure, this.m_MaxAirPressure);
            }

            this.m_CurrentAirPressure += i_AmountToAdd;
        }
    }
}
