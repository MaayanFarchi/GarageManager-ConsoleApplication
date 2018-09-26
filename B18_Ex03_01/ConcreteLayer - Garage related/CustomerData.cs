using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class CustomerData
    {
        public enum eVehicleGarageStatus
        {
            InRepair = 1,
            FixedAndUnpaid = 2,
            FixedAndPaid = 3,
        }

        private const string k_FullNameQuestionKey = "FullName";
        private const string k_PhoneNameQuestionKey = "PhoneNumber";

        private string m_FullName;
        private string m_PhoneNumber;
        private Vehicle m_CustomerVehicle;
        private eVehicleGarageStatus? m_CurrentVehicleStatus;
        private Dictionary<string, string> m_CustomerQuestionnaire;

        public CustomerData()
        {
            m_FullName = null;
            m_PhoneNumber = null;
            m_CustomerVehicle = null;
            m_CurrentVehicleStatus = eVehicleGarageStatus.InRepair;
            m_CustomerQuestionnaire = new Dictionary<string, string>
            {
                { k_FullNameQuestionKey, "Please enter your full name" },
                { k_PhoneNameQuestionKey, "Please enter your phone number" }
            };
        }

        public void SetResponse(string i_QuistionKey, string i_Response)
        {
            if(i_QuistionKey == k_FullNameQuestionKey)
            {
                FullName = i_Response;
            }

            else if(i_QuistionKey == k_PhoneNameQuestionKey)
            {
                PhoneNumber = i_Response;
            }
        }

        public string FullName
        {
            get
            {

                return m_FullName;
            }
            set
            {
                m_FullName = value;
            }
        }

        public string PhoneNumber
        {
            get
            {

                return m_PhoneNumber;
            }

            set
            {
                m_PhoneNumber = value;
            }
        }

        public Vehicle CustomerVehicle
        {
            get
            {
                return m_CustomerVehicle;
            }
            set
            {
                m_CustomerVehicle = value;
            }
        }

        public eVehicleGarageStatus? CuurentVehicleStatus
        {
            get
            {

                return m_CurrentVehicleStatus;
            }

            set
            {
                m_CurrentVehicleStatus = value;
            }
        }

        public Dictionary<string, string> CustomerQuestionnaire
        {
            get
            {
                return m_CustomerQuestionnaire;
            }
        }

        public override string ToString()
        {
            string customerData = string.Format(@"{0}
Owner's name: {1}
Vehical status: {2}{3}",
            m_CustomerVehicle.GetVehicleData(),
            m_FullName,
            m_CurrentVehicleStatus, Environment.NewLine);

            return customerData;
        }
    }
}
