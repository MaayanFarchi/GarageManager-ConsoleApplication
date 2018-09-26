using System;
using System.Collections.Generic;
using System.Text;

public class NotANumberException : Exception
{
    string m_Response;
    private const string m_NotANumberExceptionMessage = "{0} Is not a number";
    public NotANumberException(string i_Response) : base(string.Format(m_NotANumberExceptionMessage, i_Response))
    {
        m_Response = i_Response;
    }
}

