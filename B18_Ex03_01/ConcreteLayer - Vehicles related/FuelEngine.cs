using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Energy
    {
        public enum eFuelType
        {
            Soler,
            Octan95,
            Octan96,
            Octan98
        }

        private eFuelType m_FuelType;

        public FuelEngine(eFuelType i_FuelType, float? i_MaximumFuelCapacity, float? i_CurrentEnergyAmount) : base(i_MaximumFuelCapacity, i_CurrentEnergyAmount)
        {
            m_FuelType = i_FuelType;
        }

        public eFuelType FuleType
        {
            get
            {

                return m_FuelType;
            }
        }

        public void AddFuel(eFuelType i_Fueltype, float? i_FuelToAdd)
        {
            if (i_Fueltype != m_FuelType)
            {
                throw new ArgumentException();
            }

            AddEnergy(i_FuelToAdd);
        }

        public override string ToString()
        {
            return string.Format("This is a fuel engine, The fuel type is: {0} and the remaining amount of fuel is: {1}", m_FuelType, CurrentEnergyAmount);
        }
    }
}
