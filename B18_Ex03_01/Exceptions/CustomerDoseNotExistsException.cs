using System;
using System.Collections.Generic;
using System.Text;

public class CustomerDoseNotExistsException : Exception
{
    private string m_LicensePlate;
    private const string m_CustomerExistsExceptionMessage = "Customer with LicensePlate {0} dose not existis in the system";
    public CustomerDoseNotExistsException(string i_LicensePlate) : base(string.Format(m_CustomerExistsExceptionMessage, i_LicensePlate))
    {
        m_LicensePlate = i_LicensePlate;
    }
}
