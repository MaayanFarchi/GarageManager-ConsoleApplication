using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic.GrageVehicleProperties
{
    public class PropertiesValidation
    {
        public static bool IsNumeric(string i_Response)
        {
            foreach (char c in i_Response)
            {
                if (!char.IsDigit(c) && c != '.')
                {

                    return false;
                }
            }

            return true;
        }

        public static bool ConvertStringToBool(string i_Response)
        {

            return i_Response.ToLower() == "yes" ? true : false;
        }

        public static bool IsAEnumOption(int i_EnumNumberOfOptions, int i_Response)
        {

            return (i_Response <= i_EnumNumberOfOptions && i_Response > 0);
        }

        public static bool IsYesOrNo(String i_Response)
        {
            bool isYesOrNo = false;

            if (i_Response.ToLower() == "yes" || i_Response.ToLower() == "no")
            {
                isYesOrNo = true;
            }

            return isYesOrNo;
        }
    }
}

