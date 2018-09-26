using System;
using System.Collections.Generic;
using System.Text;

public class NotAPossbileOptionInEnumException : Exception
{
    string m_Response;
    private const string m_NotAPossbileOptionInEnumExceptionMessage = "{0} Is not a possbile option";
    public NotAPossbileOptionInEnumException(string i_Response) : base(string.Format(m_NotAPossbileOptionInEnumExceptionMessage, i_Response))
    {
        m_Response = i_Response;
    }
}

