using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        public enum eLicenseType
        {
            B2 = 1,
            B1 = 2,
            A1 = 3,
            A = 4
        }

        private Energy m_Engine;
        private eLicenseType? m_LicenseType;
        private int? m_EngineCapacity;

        public Motorcycle(VehicleProperties i_VehicleProperties, MotorcycleProperties i_MotorcycleProperties, Energy i_Engine) : base(i_VehicleProperties, i_Engine)
        {
            m_Engine = i_Engine;
            m_LicenseType = i_MotorcycleProperties.LicenseType;
            m_EngineCapacity = i_MotorcycleProperties.EngineCapacity;
            SetCarWheels(i_VehicleProperties.WheelManufacturerName, i_MotorcycleProperties.NumOfWheels,
                             i_MotorcycleProperties.WheelMaxAirPressure, i_VehicleProperties.WheelCurrentAirPressure);
        }

        public eLicenseType? LicenseType
        {
            get
            {

             return m_LicenseType;
            }
        }

        public int? EngineCapacity
        {
            get
            {

             return m_EngineCapacity;
            }
        }

        public override string GetVehicleData()
        {
            string motorcycleData = string.Format(@"{0}
{1}
Motorcycle licenseType: {2}
Motorcycle EngineCapacity: {3}", base.GetVehicleData(), m_Engine.ToString(), m_LicenseType, m_EngineCapacity);

            return motorcycleData;
        }
    }
}
