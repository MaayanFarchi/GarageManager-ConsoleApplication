using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic.GrageVehicleProperties;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        public enum eSupportedVehicleTypes
        {
            FuelCar = 1,
            ElectricCar = 2,
            FuelMotorcycle = 3,
            ElectricMotorcycle = 4,
            FuelTruck = 5
        }

        private Dictionary<string, CustomerData> m_CustomerListByLicensePlate = null;
        private VehicleGenerator m_VehicleGenerator;
        private Dictionary<int, string> m_OptionalServices = null;

        public Garage()
        {

            m_CustomerListByLicensePlate = new Dictionary<string, CustomerData>();
            m_VehicleGenerator = new VehicleGenerator();
            m_OptionalServices = new Dictionary<int, string>
            {
                { 1, "Refuel a vehicle" },
                { 2, "Recharge a vehicle" },
                { 3, "Update a vehicle status" },
                { 4, "Display the license plates of the vehiclels that are in the garage by a given status" },
                { 5, "Inflate air to maximum capacity in vehicle" },
                { 6, "Display vehicle information" },
                { 7, "Add a new vehicle to the garage" },
                { 8, "Leave garage" }
            };

        }

        public CustomerData GetCustomer(string i_LicensePlate)
        {
            bool customerExists = m_CustomerListByLicensePlate.TryGetValue(i_LicensePlate, out CustomerData customer);

            if (!customerExists)
            {
                throw new CustomerDoseNotExistsException(i_LicensePlate);
            }

            return customer;
        }

        public Dictionary<int, string> OptionalServices
        {
            get
            {

                return m_OptionalServices;
            }
        }

        //For future use, in case the garage want to add a service
        internal void AddService(string i_Service)
        {
            OptionalServices.Add(m_OptionalServices.Count + 1, i_Service);
        }

        public Dictionary<string, CustomerData> CustomerListByLicensePlate
        {
            get
            {

                return m_CustomerListByLicensePlate;
            }
        }

        public Vehicle InitializeVehicle(eSupportedVehicleTypes? i_VehicleType, VehicleProperties i_VehicleProperties, VehicleProperties i_SpecificVehicleProperties)
        {
            bool customerExists = m_CustomerListByLicensePlate.TryGetValue(i_VehicleProperties.LicensePlate, out CustomerData customer);

            if (customerExists)
            {
                customer.CuurentVehicleStatus = CustomerData.eVehicleGarageStatus.InRepair;
                throw new CustomerExistsException(i_VehicleProperties.LicensePlate);
            }

            return m_VehicleGenerator.GenerateVehicle(i_VehicleType, i_VehicleProperties, i_SpecificVehicleProperties);
        }

        public void AddVehicleToGarage(CustomerData i_Client)
        {
            m_CustomerListByLicensePlate.Add(i_Client.CustomerVehicle.LicensePlate, i_Client);
        }

        public List<string> FilterLicnesePlatesByVehicleGarageStatus(string i_GivenStatus)
        {
            int optionNumber = int.Parse(i_GivenStatus);

            if (!(PropertiesValidation.IsAEnumOption(Enum.GetValues(typeof(CustomerData.eVehicleGarageStatus)).Length, optionNumber)))
            {
                throw new NotAPossbileOptionInEnumException(i_GivenStatus);
            }

            CustomerData.eVehicleGarageStatus newStatus = (CustomerData.eVehicleGarageStatus)optionNumber;

            List<string> filteredLicensePlatesList = new List<string>();

            foreach (CustomerData customer in m_CustomerListByLicensePlate.Values)
            {
                if (customer.CuurentVehicleStatus == newStatus)
                {
                    filteredLicensePlatesList.Add(customer.CustomerVehicle.LicensePlate);
                }
            }

            return filteredLicensePlatesList;
        }

        public void ChangeVehicleGarageStatus(string i_LicensePlate, string i_GivenStatus)
        {
            int optionNumber = int.Parse(i_GivenStatus);

            if (!(PropertiesValidation.IsAEnumOption(Enum.GetValues(typeof(CustomerData.eVehicleGarageStatus)).Length, optionNumber)))
            {
                throw new NotAPossbileOptionInEnumException(i_GivenStatus);
            }

            m_CustomerListByLicensePlate.TryGetValue(i_LicensePlate, out CustomerData customer);
            CustomerData.eVehicleGarageStatus newStatus = (CustomerData.eVehicleGarageStatus)optionNumber;
            customer.CuurentVehicleStatus = newStatus;
        }

        public void InflateTiresToMax(string i_LicensePlate)
        {
            m_CustomerListByLicensePlate.TryGetValue(i_LicensePlate, out CustomerData customer);
            foreach (Wheel vehicleWeel in customer.CustomerVehicle.Wheels)
            {
                vehicleWeel.InflateTires(vehicleWeel.MaxAirPressure - vehicleWeel.CurrentAirPressure);
            }
        }

        public void CheckCustomerHasAFuelBased(CustomerData i_Customer)
        {
            if (!(i_Customer.CustomerVehicle.Energy is FuelEngine))
            {
                throw new CanNotAddEnergyToThisEngineTypeExeption();
            }
        }

        public void RefuelCar(string i_LicensePlate, FuelEngine.eFuelType i_FuelType, string i_FuelToAdd)
        {
            if (!(PropertiesValidation.IsNumeric(i_FuelToAdd)))
            {
                throw new NotANumberException(i_FuelToAdd);
            }
            m_CustomerListByLicensePlate.TryGetValue(i_LicensePlate, out CustomerData customer);
            ((FuelEngine)(customer.CustomerVehicle.Energy)).AddFuel(i_FuelType, float.Parse(i_FuelToAdd));
        }

        public void RechargeCar(string i_LicensePlate, string i_MinuetsToAdd)
        {
            if (!(PropertiesValidation.IsNumeric(i_MinuetsToAdd)))
            {
                throw new NotANumberException(i_MinuetsToAdd);
            }

            m_CustomerListByLicensePlate.TryGetValue(i_LicensePlate, out CustomerData customer);
            if (customer.CustomerVehicle.Energy is ElectricEngine)
            {
                ((ElectricEngine)(customer.CustomerVehicle.Energy)).Recharge(float.Parse(i_MinuetsToAdd));

            }
            else
            {
                throw new CanNotAddEnergyToThisEngineTypeExeption();
            }
        }

        public string DisplayAllReleventData(string i_LicensePlate)
        {
            m_CustomerListByLicensePlate.TryGetValue(i_LicensePlate, out CustomerData customer);

            return customer.ToString();
        }

        public string GetServiceQuestionnaireByNumber(int i_ServiceNumber)
        {
            string serviceQuestionnaire = String.Empty;

            if (i_ServiceNumber == 1)
            {
                serviceQuestionnaire = string.Format("How much fuel would you like to add (in liters)?");
            }

            else if (i_ServiceNumber == 2)
            {
                serviceQuestionnaire = "How much battery-time would you like to add? (in minuets)";
            }

            else if (i_ServiceNumber == 3 || i_ServiceNumber == 4)
            {
                serviceQuestionnaire = string.Format(@"Status options are:
                1) {0}
                2) {1}
                3) {2}", CustomerData.eVehicleGarageStatus.InRepair, CustomerData.eVehicleGarageStatus.FixedAndPaid,
                CustomerData.eVehicleGarageStatus.FixedAndUnpaid);
            }

            else if (i_ServiceNumber == 3 || i_ServiceNumber == 4)
            {
                serviceQuestionnaire = string.Format(@"Status options are:
                1) {0}
                2) {1}
                3) {2}", CustomerData.eVehicleGarageStatus.InRepair, CustomerData.eVehicleGarageStatus.FixedAndPaid,
                CustomerData.eVehicleGarageStatus.FixedAndUnpaid);
            }

            return serviceQuestionnaire;
        }
    }
}
