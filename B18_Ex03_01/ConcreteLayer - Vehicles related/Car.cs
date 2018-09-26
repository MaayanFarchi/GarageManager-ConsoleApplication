using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        public enum eColorOptions
        {
            Gray = 1,
            Blue = 2,
            White = 3,
            Black = 4
        }

        public enum eNumberOfDoors
        {
            Two = 1,
            Three = 2,
            Four = 3,
            Five = 4
        }

        private Energy m_Engine;
        private eColorOptions? m_Color;
        private eNumberOfDoors? m_NumberOfDoors;

        public Car(VehicleProperties i_VehicleProperties, CarProperties i_CarProperties, Energy i_Engine) : base(i_VehicleProperties, i_Engine)
        {
            m_Engine = i_Engine;
            m_Color = i_CarProperties.Color;
            m_NumberOfDoors = i_CarProperties.NumOfDoors;
            SetCarWheels(i_VehicleProperties.WheelManufacturerName, i_CarProperties.NumOfWheels,
                i_CarProperties.WheelMaxAirPressure, i_CarProperties.WheelCurrentAirPressure);
        }

        public Energy Engine
        {
            get
            {

                return m_Engine;
            }
        }

        public eColorOptions? Color
        {
            get
            {

                return m_Color;
            }
        }

        public eNumberOfDoors? NumberOfDoors
        {
            get
            {

                return m_NumberOfDoors;
            }
        }

        public override string GetVehicleData()
        {
            string carData = string.Format(@"{0}
{1}
Car color: {2}
Car number of doors: {3}", base.GetVehicleData(), m_Engine.ToString(), this.m_Color, this.m_NumberOfDoors);
            return carData;
        }
    }
}
