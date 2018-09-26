using System;

public class CustomerExistsException : Exception
{

    private string m_LicensePlate;
    private const string m_CustomerExistsExceptionMessage = "Customer with LicensePlate {0} already existis in the system";
    public CustomerExistsException(string i_LicensePlate) : base(string.Format(m_CustomerExistsExceptionMessage, i_LicensePlate))
    {
        m_LicensePlate = i_LicensePlate;
    }
}

