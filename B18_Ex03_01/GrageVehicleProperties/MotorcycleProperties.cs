using Ex03.GarageLogic.GrageVehicleProperties;
using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class MotorcycleProperties : VehicleProperties
    {
        private const string k_LicenseTypeQuestionKey = "LicenseType";
        private const string k_EngineCapacityQuestionKey = "EngineCapacity";

        private Motorcycle.eLicenseType? m_LicenseType;
        private int? m_EngineCapacity;
        float? m_WheelMaxAirPressure;
        int m_NumOfWheels;
        Dictionary<string, string> m_MotorcycleQuestionnaire;

        public MotorcycleProperties()
        {
            m_LicenseType = null;
            m_EngineCapacity = null;
            m_WheelMaxAirPressure = 32.0f;
            m_NumOfWheels = 2;
            m_MotorcycleQuestionnaire = new Dictionary<string, string>();
            string LicenseTypeQ = string.Format(@"What is your motorcycle license type? 
            1.{0}
            2.{1}
            3.{2}
            4.{3}
            ", Motorcycle.eLicenseType.B2, Motorcycle.eLicenseType.B1, Motorcycle.eLicenseType.A1, Motorcycle.eLicenseType.A);

            m_MotorcycleQuestionnaire.Add(k_LicenseTypeQuestionKey, LicenseTypeQ);
            m_MotorcycleQuestionnaire.Add(k_EngineCapacityQuestionKey, "What is your motorcycle's engine capacity?");
        }

        public override void SetResponse(string i_QuistionKey, string i_Response)
        {
            if (i_QuistionKey == k_LicenseTypeQuestionKey)
            {
                if (!PropertiesValidation.IsNumeric(i_Response))
                {
                    throw new NotANumberException(i_Response);
                }

                int optionNumber = int.Parse(i_Response);

                if (!(PropertiesValidation.IsAEnumOption(Enum.GetValues(typeof(Motorcycle.eLicenseType)).Length, optionNumber)))
                {
                    throw new NotAPossbileOptionInEnumException(i_Response);
                }

                LicenseType = (Motorcycle.eLicenseType)optionNumber;
            }

            else if (i_QuistionKey == k_EngineCapacityQuestionKey)
            {
                if (!(int.TryParse(i_Response, out int engineCapacityInput)))
                {
                    throw new NotANumberException(i_Response);
                }

                EngineCapacity = engineCapacityInput;
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

                if (VehicleType == Garage.eSupportedVehicleTypes.FuelMotorcycle && float.Parse(i_Response) >= 6)
                {
                    throw new NotSupportedByGarageExcrption(i_Response);
                }

                else if (VehicleType == Garage.eSupportedVehicleTypes.ElectricMotorcycle && float.Parse(i_Response) >= 1.8)
                {
                    throw new NotSupportedByGarageExcrption(i_Response);
                }

                CurrentEnergyStatus = float.Parse(i_Response);
            }
        }

        public Dictionary<string, string> MotorcycleQuestionnaire
        {
            get
            {

                return m_MotorcycleQuestionnaire;
            }
        }

        //For future use 
        internal override void AddQuestion(string i_QuestionKey, string i_Question)
        {
            MotorcycleQuestionnaire.Add(i_QuestionKey, i_Question);
        }

        public Motorcycle.eLicenseType? LicenseType
        {
            get
            {

                return m_LicenseType;
            }

            set
            {
                m_LicenseType = value;
            }
        }

        public int? EngineCapacity
        {
            get
            {

                return m_EngineCapacity;
            }

            set
            {
                m_EngineCapacity = value;
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