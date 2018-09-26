using Ex03.GarageLogic.GrageVehicleProperties;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Ex03.GarageLogic
{
    public class TruckProperties : VehicleProperties
    {
        private const string k_MaximumCarryingCapacity = "MaximumCarryingCapacity";
        private const string k_IsCarryingDangerousSubstance = "IsCarryingDangerousSubstance";

        private float? m_MaximumCarryingCapacity;
        private bool? m_IsCarryingDangerousSubstance;
        private float? m_WheelMaxAirPressure;
        int m_NumOfWheels;
        Dictionary<string, string> m_TruckQuestionnaire;

        public TruckProperties()
        {
            m_MaximumCarryingCapacity = null;
            m_IsCarryingDangerousSubstance = null;
            m_WheelMaxAirPressure = 28.0f;
            m_NumOfWheels = 12;
            m_TruckQuestionnaire = new Dictionary<string, string>();
            m_TruckQuestionnaire.Add(k_MaximumCarryingCapacity, "What is your truck's maximum carrying capacity?");
            m_TruckQuestionnaire.Add(k_IsCarryingDangerousSubstance, "Is your truck carrying a dangerous substance?");
        }

        public override void SetResponse(string i_QuistionKey, string i_Response)
        {
            if (i_QuistionKey == k_MaximumCarryingCapacity)
            {
                if (!PropertiesValidation.IsNumeric(i_Response))
                {
                    throw new NotANumberException(i_Response);
                }
                m_MaximumCarryingCapacity = float.Parse(i_Response, CultureInfo.InvariantCulture.NumberFormat);
            }

            else if (i_QuistionKey == k_IsCarryingDangerousSubstance)
            {
                if (!(PropertiesValidation.IsYesOrNo(i_Response)))
                {
                    throw new FormatException();
                }

                else
                {
                    m_IsCarryingDangerousSubstance = PropertiesValidation.ConvertStringToBool(i_Response);
                }
            }

            else if (i_QuistionKey == k_WheelCurrentAirPressureQuestionKey)
            {
                if (!PropertiesValidation.IsNumeric(i_Response))
                {
                    throw new NotANumberException(i_Response);
                }

                float airPressure = float.Parse(i_Response);

                if (airPressure >= WheelMaxAirPressure)
                {
                    throw new NotSupportedByGarageExcrption(i_Response);
                }

                WheelCurrentAirPressure = airPressure;
            }

            else if (i_QuistionKey == k_CurrentEnergyStatusQuestionKey)
            {

                if (!PropertiesValidation.IsNumeric(i_Response))
                {
                    throw new NotANumberException(i_Response);
                }

                if (VehicleType == Garage.eSupportedVehicleTypes.FuelTruck && float.Parse(i_Response) >= 115)
                {
                    throw new NotSupportedByGarageExcrption(i_Response);
                }

                CurrentEnergyStatus = float.Parse(i_Response);
            }
        }

        public Dictionary<string, string> TruckQuestionnaire
        {
            get
            {

                return m_TruckQuestionnaire;
            }
        }

        //For future use 
        internal override void AddQuestion(string i_QuestionKey, string i_Question)
        {
            TruckQuestionnaire.Add(i_QuestionKey, i_Question);
        }

        public float? MaximumCarryingCapacity
        {
            get
            {

                return m_MaximumCarryingCapacity;
            }

            set
            {
                m_MaximumCarryingCapacity = value;
            }
        }

        public bool? IsCarryingDangerousSubstance
        {
            get
            {

                return m_IsCarryingDangerousSubstance;
            }

            set
            {
                m_IsCarryingDangerousSubstance = value;
            }
        }

        public int NumOfWheels
        {
            get
            {

                return m_NumOfWheels;
            }

            set
            {
                m_NumOfWheels = value;
            }
        }

        public float? WheelMaxAirPressure
        {
            get
            {

                return m_WheelMaxAirPressure;
            }

            set
            {
                m_WheelMaxAirPressure = value;
            }
        }
    }
}