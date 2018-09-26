using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_ManufacturerName;
        private float? m_CurrentAirPressure;
        private float? m_MaxAirPressure;

        public Wheel(string i_ManufacturerName, float? i_MaxAirPressure, float? i_CurrentAirPressure)
        {
            m_ManufacturerName = i_ManufacturerName;
            m_MaxAirPressure = i_MaxAirPressure;
            m_CurrentAirPressure = i_CurrentAirPressure;
        }

        public string ManufacturerName
        {
            get
            {

                return m_ManufacturerName;
            }
        }

        public float? MaxAirPressure
        {
            get
            {

                return m_MaxAirPressure;
            }
        }

        public float? CurrentAirPressure
        {
            get
            {

                return m_CurrentAirPressure;
            }
        }

        internal void InflateTires(float? i_AirToAddToWheel)
        {
            if (m_CurrentAirPressure + i_AirToAddToWheel <= m_MaxAirPressure)
            {
                m_CurrentAirPressure += i_AirToAddToWheel;
            }
            else
            {
                throw new ValueOutOfRangeException(0, m_MaxAirPressure);
            }
        }

        public override string ToString()
        {
            String wheelData = string.Format(@"Wheel manufacturer name: {0},
Max air pressure: {1}
Current air pressure: {2}", m_ManufacturerName, m_MaxAirPressure, m_CurrentAirPressure);

            return wheelData;
        }
    }
}
