using Ex03.GarageLogic.GrageVehicleProperties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleProperties
    {

        private const string k_VehicleTypeQuestionKey = "VehicleType";
        private const string k_ModelNameQuestionKey = "ModelName";
        private const string k_WheelManufacturerNameQuestionKey = "WheelManufacturerName";
        internal const string k_WheelCurrentAirPressureQuestionKey = "Wheel current air pressure";
        internal const string k_CurrentEnergyStatusQuestionKey = "Current energy status";
        public const string k_LicensePlateQuestionKey = "License plate";

        private Garage.eSupportedVehicleTypes? m_VehicleType;
        private string m_ModelName;
        private string m_LicensePlate;
        private string m_WheelManufacturerName;
        private float? m_WheelCurrentAirPressure;
        private float? m_CurrentEnergyStatus;
        private Dictionary<string, string> m_VehicleQuestionnaire;

        public VehicleProperties()
        {
            m_VehicleType = null;
            m_ModelName = null;
            m_LicensePlate = null;
            m_WheelManufacturerName = null;
            m_WheelCurrentAirPressure = null;
            m_CurrentEnergyStatus = null;
            m_VehicleQuestionnaire = setVehicleQuestionaire();
        }

        public virtual void SetResponse(string i_QuistionKey, string i_Response)
        {
            if (i_QuistionKey == k_VehicleTypeQuestionKey)
            {
                if (!PropertiesValidation.IsNumeric(i_Response))
                {
                    throw new NotANumberException(i_Response);
                }

                int optionNumber = int.Parse(i_Response);

                if (!(PropertiesValidation.IsAEnumOption(Enum.GetValues(typeof(Garage.eSupportedVehicleTypes)).Length, optionNumber)))
                {
                    throw new NotAPossbileOptionInEnumException(i_Response);
                }

                VehicleType = (Garage.eSupportedVehicleTypes)optionNumber;
            }

            else if (i_QuistionKey == k_ModelNameQuestionKey)
            {
                ModelName = i_Response;
            }

            else if (i_QuistionKey == k_LicensePlateQuestionKey)
            {
                LicensePlate = i_Response;
            }

            else if (i_QuistionKey == k_WheelManufacturerNameQuestionKey)
            {
                WheelManufacturerName = i_Response;
            }
        }

        public Garage.eSupportedVehicleTypes? VehicleType
        {
            get
            {
                return m_VehicleType;
            }

            set
            {
                m_VehicleType = value;
            }
        }

        private Dictionary<string, string> setVehicleQuestionaire()
        {
            Dictionary<string, string> vehicleQuestionnaire = new Dictionary<string, string>();
            string VehicleTypeQ = string.Format(@"What is the type of your vehicle? 
            1.{0}
            2.{1}
            3.{2} 
            4.{3} 
            5.{4}", Garage.eSupportedVehicleTypes.FuelCar, Garage.eSupportedVehicleTypes.ElectricCar, Garage.eSupportedVehicleTypes.FuelMotorcycle,
            Garage.eSupportedVehicleTypes.ElectricMotorcycle, Garage.eSupportedVehicleTypes.FuelTruck);

            vehicleQuestionnaire.Add(k_VehicleTypeQuestionKey, VehicleTypeQ);
            vehicleQuestionnaire.Add(k_ModelNameQuestionKey, "What is your vehicle model name?");
            vehicleQuestionnaire.Add(k_LicensePlateQuestionKey, "What is your vehicle License Plate number?");
            vehicleQuestionnaire.Add(k_WheelManufacturerNameQuestionKey, "What is your Vehicle wheel's manufacturer name?");

            return vehicleQuestionnaire;
        }

        public Dictionary<string, string> SupportedGarageTypeQuestionaire()
        {
            Dictionary<string, string> vehicleQuestionnaire = new Dictionary<string, string>
            {
                { k_WheelCurrentAirPressureQuestionKey, "What is your Vehicle wheel's current air pressure?" },
                { k_CurrentEnergyStatusQuestionKey, "What is your Vehicle energy status?" }
            };
            return vehicleQuestionnaire;
        }

        public Dictionary<string, string> VehicleQuestionnaire
        {
            get
            {

                return m_VehicleQuestionnaire;
            }
        }

        //For future use 
        internal virtual void AddQuestion(string i_QuestionKey, string i_Question)
        {
            VehicleQuestionnaire.Add(i_QuestionKey, i_Question);
        }

        public string ModelName
        {
            get
            {

                return m_ModelName;
            }
            set
            {
                m_ModelName = value;
            }
        }

        public string LicensePlate
        {
            get
            {

                return m_LicensePlate;
            }
            set
            {
                m_LicensePlate = value;
            }
        }

        public string WheelManufacturerName
        {
            get
            {

                return m_WheelManufacturerName;
            }
            set
            {
                m_WheelManufacturerName = value;
            }
        }

        public float? WheelCurrentAirPressure
        {
            get
            {

                return m_WheelCurrentAirPressure;
            }

            set
            {
                m_WheelCurrentAirPressure = value;
            }
        }

        public float? CurrentEnergyStatus
        {
            get
            {

                return m_CurrentEnergyStatus;
            }

            set
            {
                m_CurrentEnergyStatus = value;
            }
        }
    }
}
