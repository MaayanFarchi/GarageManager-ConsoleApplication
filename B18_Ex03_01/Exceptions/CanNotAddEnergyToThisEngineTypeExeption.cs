using System;
using System.Runtime.Serialization;

namespace Ex03.GarageLogic
{
    public class CanNotAddEnergyToThisEngineTypeExeption : Exception
    {
        const string m_CanNotAddEnergyToThisEngineTypeMessage = "Can't add energy to this type of engine";
        public CanNotAddEnergyToThisEngineTypeExeption() : base(m_CanNotAddEnergyToThisEngineTypeMessage)
        {
        }
    }
}