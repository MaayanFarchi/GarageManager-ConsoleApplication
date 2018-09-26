using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {

        private Energy m_Energy;
        private string m_ModelName;
        private string m_LicensePlate;
        private float? m_EnergyPercentageStatus;
        private Wheel[] m_Wheels;

        public Vehicle()
        {
            m_Energy = null;
            m_ModelName = null;
            m_LicensePlate = null;
            m_EnergyPercentageStatus = null;
            m_Wheels = null;
        }

        public Vehicle(VehicleProperties i_VehicleProperties, Energy i_Energy)
        {
            m_Energy = i_Energy;
            m_ModelName = i_VehicleProperties.ModelName;
            m_LicensePlate = i_VehicleProperties.LicensePlate;
            m_EnergyPercentageStatus = getEnergyPrcentage(i_VehicleProperties.CurrentEnergyStatus);
            m_Wheels = null;
        }

        public Energy Energy
        {
            get
            {

                return m_Energy;
            }
        }

        public String ModelName
        {
            get
            {

                return m_ModelName;
            }
        }

        public String LicensePlate
        {
            get
            {

                return m_LicensePlate;
            }
        }

        public Wheel[] Wheels
        {
            set
            {

                m_Wheels = value;
            }

            get
            {

                return m_Wheels;
            }
        }

        public virtual string GetVehicleData()
        {
            string vehicleData = string.Format(@"Vehicle model name: {0}
License plate: {1}
Energy Precentage Status: {2}

{3}", m_ModelName, m_LicensePlate, m_EnergyPercentageStatus, displayWheelsData());

            return vehicleData;
        }

        private float? getEnergyPrcentage(float? currentEnergyStatus)
        {
            return (m_Energy.CurrentEnergyAmount / m_Energy.MaxEnergy);
        }

        private StringBuilder displayWheelsData()
        {
            StringBuilder wheelsData = new StringBuilder();

            string data = String.Empty;

            for (int i = 1; i <= m_Wheels.Length; i++)
            {
                data = string.Format("Wheel number {0} information: {1}{2}", i, m_Wheels[i - 1].ToString(), Environment.NewLine);
                wheelsData.Append(data);
            }

            return wheelsData;
        }

        internal void SetCarWheels(string i_ManufacturerName, int i_NumOfWheels, float? i_MaxAirPressure, float? i_CurrentAirPressure)
        {
            m_Wheels = new Wheel[i_NumOfWheels];

            for (int i = 0; i < i_NumOfWheels; i++)
            {
                m_Wheels[i] = new Wheel(i_ManufacturerName, i_MaxAirPressure, i_CurrentAirPressure);
            }
        }

    }
}
