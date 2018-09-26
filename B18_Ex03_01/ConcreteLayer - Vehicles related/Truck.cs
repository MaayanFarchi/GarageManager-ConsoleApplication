using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {

        private Energy m_Engine;
        private float? m_MaximumCarryingCapacity;
        private bool? m_IsCarryingDangerousSubstance;

        public Truck(VehicleProperties i_VehicleProperties, TruckProperties i_TruckProperties, Energy i_Engine) : base(i_VehicleProperties, i_Engine)
        {
            m_Engine = i_Engine;
            m_MaximumCarryingCapacity = i_TruckProperties.MaximumCarryingCapacity;
            m_IsCarryingDangerousSubstance = i_TruckProperties.IsCarryingDangerousSubstance;
            SetCarWheels(i_VehicleProperties.WheelManufacturerName, i_TruckProperties.NumOfWheels,
                 i_TruckProperties.WheelMaxAirPressure, i_VehicleProperties.WheelCurrentAirPressure);
        }

        public Energy Engine
        {
            get
            {

                return m_Engine;
            }
        }

        public float? MaximumCarryingCapacity
        {
            get
            {

                return m_MaximumCarryingCapacity;
            }
        }

        public bool? IsCarryingDangerousSubstance
        {
            get
            {

                return m_IsCarryingDangerousSubstance;
            }
        }

        public override string GetVehicleData()
        {
            string truckData = string.Format(@"{0}
{1}
Truck maximum carrying capacity: {2}
Truck is carrying dangerous substance? {3}", base.GetVehicleData(), m_Engine.ToString(), m_MaximumCarryingCapacity, m_IsCarryingDangerousSubstance);
            return truckData;
        }
    }
}
