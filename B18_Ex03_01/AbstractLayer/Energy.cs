namespace Ex03.GarageLogic
{
    public abstract class Energy
    {
        private float? m_CurrentEnergyAmount = null;
        private float? m_MaxEnergy = null;

        public Energy(float? i_MaxEnergy, float? i_CurrentEnergy)
        {
            m_CurrentEnergyAmount = i_CurrentEnergy;
            m_MaxEnergy = i_MaxEnergy;
        }

        public float? CurrentEnergyAmount
        {
            get
            {

                return m_CurrentEnergyAmount;
            }
        }

        public float? MaxEnergy
        {
            get
            {

                return m_MaxEnergy;
            }
        }

        internal void AddEnergy(float? i_EnergyToAdd)
        {

            if (m_CurrentEnergyAmount + i_EnergyToAdd > m_MaxEnergy)
            {
                throw new ValueOutOfRangeException
                    (0, m_MaxEnergy);
            }

            m_CurrentEnergyAmount += i_EnergyToAdd;
        }

        public abstract override string ToString();

    }
}
