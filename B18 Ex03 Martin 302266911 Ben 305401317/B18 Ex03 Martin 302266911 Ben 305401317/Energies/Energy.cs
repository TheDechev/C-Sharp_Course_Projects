
namespace Ex03.GarageLogic
{
    public abstract class Energy
    {
        protected float m_CurrentEnergy;
        protected float m_MaxCapacity;
        protected float m_RemainingEnergyPercentage;

        internal Energy(float i_MaxCapacity)
        {
            m_MaxCapacity = i_MaxCapacity;
        }

        public float CurrentEnergy
        {
            get
            {
                return this.m_CurrentEnergy;
            }
            set
            {
                if (value > m_MaxCapacity)
                {
                    throw new ValueOutOfRangeException(m_CurrentEnergy, m_MaxCapacity);
                }

                this.m_CurrentEnergy = value;
                m_RemainingEnergyPercentage = (m_CurrentEnergy / m_MaxCapacity) * 100;
            }
        }

        public float MaxCapacity
        {
            get
            {
                return this.m_MaxCapacity;
            }
        }

        public float RemainingEnergyPercentage
        {
            get
            {
                return m_RemainingEnergyPercentage;
            }
        }

        public abstract string EnergyUnits();
    }
}
