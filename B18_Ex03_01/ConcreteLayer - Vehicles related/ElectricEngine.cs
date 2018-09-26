using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Energy
    {
        public ElectricEngine(float? i_MaximumBatteryTime, float? i_CurrentBatteryTime) : base(i_MaximumBatteryTime, i_CurrentBatteryTime)
        {
        }

        public void Recharge(float? i_MinuetsOfCharge)
        {
            AddEnergy(i_MinuetsOfCharge);
        }

        public override string ToString()
        {
            return string.Format("This is an electric engine: the remaining amount of battery-time is {0}", CurrentEnergyAmount);
        }
    }
}
