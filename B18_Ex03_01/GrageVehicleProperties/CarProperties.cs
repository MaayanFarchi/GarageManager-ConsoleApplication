using Ex03.GarageLogic.GrageVehicleProperties;
using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class CarProperties : VehicleProperties
    {
        private const string k_CarColorQuestionKey = "CarColor";
        private const string k_NumOfDoorsQuestionKey = "NumOfDoors";

        private Car.eColorOptions? m_Color;
        private Car.eNumberOfDoors? m_NumOfDoors;
        private float? m_WheelMaxAirPressure;
        private int m_NumOfWheels;
        private Dictionary<string, string> m_CarQuestionnaire;

        public CarProperties()
        {
            m_Color = null;
            m_NumOfDoors = null;
            m_WheelMaxAirPressure = 32.0f;
            m_NumOfWheels = 4;
            m_CarQuestionnaire = setCarQuestionnaire();
        }

        public override void SetResponse(string i_QuistionKey, string i_Response)
        {
            if (i_QuistionKey == k_CarColorQuestionKey)
            {
                if (!PropertiesValidation.IsNumeric(i_Response))
                {
                    throw new NotANumberException(i_Response);
                }

                int optionNumber = int.Parse(i_Response);

                if (!(PropertiesValidation.IsAEnumOption(Enum.GetValues(typeof(Car.eColorOptions)).Length, optionNumber)))
                {
                    throw new NotAPossbileOptionInEnumException(i_Response);
                }

                Color = (Car.eColorOptions)optionNumber;
            }

            else if (i_QuistionKey == k_NumOfDoorsQuestionKey)
            {
                if (!PropertiesValidation.IsNumeric(i_Response))
                {
                    throw new NotANumberException(i_Response);
                }

                int optionNumber = int.Parse(i_Response);

                if (!(PropertiesValidation.IsAEnumOption(Enum.GetValues(typeof(Car.eNumberOfDoors)).Length, optionNumber)))
                {
                    throw new NotAPossbileOptionInEnumException(i_Response);
                }

                NumOfDoors = (Car.eNumberOfDoors)optionNumber;
            }

            else if (i_QuistionKey == k_WheelCurrentAirPressureQuestionKey)
            {
                if (!PropertiesValidation.IsNumeric(i_Response))
                {
                    throw new NotANumberException(i_Response);
                }

                float airPressure = float.Parse(i_Response);

                if (airPressure > WheelMaxAirPressure)
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

                if ((VehicleType == Garage.eSupportedVehicleTypes.FuelCar) && (float.Parse(i_Response) >= 45))
                {
                    throw new NotSupportedByGarageExcrption(i_Response);
                }

                else if ((VehicleType == Garage.eSupportedVehicleTypes.ElectricCar) && (float.Parse(i_Response) >= 3.2))
                {
                    throw new NotSupportedByGarageExcrption(i_Response);
                }

                CurrentEnergyStatus = float.Parse(i_Response);
            }
        }

        private Dictionary<string, string> setCarQuestionnaire()
        {
            Dictionary<string, string> questionnaire = new Dictionary<string, string>();

            string colorQ = string.Format(@"What is the color of your car? 
            1.{0}
            2.{1}
            3.{2}
            4.{3}
            ", Car.eColorOptions.Gray, Car.eColorOptions.Blue, Car.eColorOptions.White, Car.eColorOptions.Black);


            string numOfDoorsQ = string.Format(@"How many doors dose your car have? 
            1.{0}
            2.{1}
            3.{2}
            4.{3}
            ", Car.eNumberOfDoors.Two, Car.eNumberOfDoors.Three, Car.eNumberOfDoors.Four, Car.eNumberOfDoors.Five);

            questionnaire.Add(k_CarColorQuestionKey, colorQ);
            questionnaire.Add(k_NumOfDoorsQuestionKey, numOfDoorsQ);

            return questionnaire;
        }

        public Dictionary<string, string> CarQuestionnaire
        {
            get
            {

                return m_CarQuestionnaire;
            }
        }

        //For future use 
        internal override void AddQuestion(string i_QuestionKey, string i_Question)
        {
            CarQuestionnaire.Add(i_QuestionKey, i_Question);
        }

        public Car.eColorOptions? Color
        {
            get
            {

                return m_Color;
            }

            set
            {
                m_Color = value;
            }
        }

        public Car.eNumberOfDoors? NumOfDoors
        {
            get
            {

                return m_NumOfDoors;
            }

            set
            {
                m_NumOfDoors = value;
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