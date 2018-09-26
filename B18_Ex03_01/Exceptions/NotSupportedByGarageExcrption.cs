using System;
using System.Runtime.Serialization;

namespace Ex03.GarageLogic
{
    public class NotSupportedByGarageExcrption : Exception
    {
        private string m_Property;
        private const string k_CustomerExistsExceptionMessage = "The given property ({0}) is not supported by this garage";

        public NotSupportedByGarageExcrption(string i_Property) : base(string.Format(k_CustomerExistsExceptionMessage, i_Property))
        {
            m_Property = i_Property;
        }
    }
}