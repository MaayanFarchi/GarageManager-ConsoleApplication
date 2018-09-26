using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleGenerator
    {
        public Vehicle GenerateVehicle(Garage.eSupportedVehicleTypes? i_VehicleType, VehicleProperties i_VehicleProperties, VehicleProperties i_SpecificVehicleProperties)
        {
            Vehicle newVehicle = null;

            switch (i_VehicleType)
            {
                case Garage.eSupportedVehicleTypes.FuelCar:
                    Energy carFuleEngine = new FuelEngine(FuelEngine.eFuelType.Octan98, 45, i_SpecificVehicleProperties.CurrentEnergyStatus);
                    newVehicle = new Car(i_VehicleProperties, (CarProperties)i_SpecificVehicleProperties, carFuleEngine);
                    break;
                case Garage.eSupportedVehicleTypes.ElectricCar:
                    Energy carElectricEngine = new ElectricEngine((3.2f * 60), i_SpecificVehicleProperties.CurrentEnergyStatus);
                    newVehicle = new Car(i_VehicleProperties, (CarProperties)i_SpecificVehicleProperties, carElectricEngine);
                    break;
                case Garage.eSupportedVehicleTypes.FuelMotorcycle:
                    Energy motorcycleElectricEngine = new FuelEngine(FuelEngine.eFuelType.Octan96, 6, i_SpecificVehicleProperties.CurrentEnergyStatus);
                    newVehicle = new Motorcycle(i_VehicleProperties, (MotorcycleProperties)i_SpecificVehicleProperties, motorcycleElectricEngine);
                    break;
                case Garage.eSupportedVehicleTypes.ElectricMotorcycle:
                    Energy motorcycleFuelEngine = new ElectricEngine((1.8f * 60), i_SpecificVehicleProperties.CurrentEnergyStatus);
                    newVehicle = new Motorcycle(i_VehicleProperties, (MotorcycleProperties)i_SpecificVehicleProperties, motorcycleFuelEngine);
                    break;
                case Garage.eSupportedVehicleTypes.FuelTruck:
                    Energy truckFuelEngine = new FuelEngine(FuelEngine.eFuelType.Octan96, 115, i_SpecificVehicleProperties.CurrentEnergyStatus);
                    newVehicle = new Truck(i_VehicleProperties, (TruckProperties)i_SpecificVehicleProperties, truckFuelEngine);
                    break;

                default:
                    break;
            }

            return newVehicle;
        }
    }
}
