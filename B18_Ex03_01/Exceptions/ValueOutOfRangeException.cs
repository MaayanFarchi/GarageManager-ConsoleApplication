using System.Collections.Generic;
using System;

public class ValueOutOfRangeException : Exception
{
    private float? m_MinValue;
    private float? m_MaxValue;
    private const string m_OutOfRangeExceptionMessage = "The current value must be between {0} and {1}";

    public ValueOutOfRangeException(float? i_MinRangeValue, float? i_MaxRangeValue) : base(string.Format(m_OutOfRangeExceptionMessage, i_MinRangeValue, i_MaxRangeValue))
    {
        m_MinValue = i_MinRangeValue;
        m_MaxValue = i_MaxRangeValue;
    }
}

