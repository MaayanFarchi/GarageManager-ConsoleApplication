using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.ConsoleUI
{
    class GarageInterface
    {
        Garage m_Garage;
        bool m_UserInGarage;

        public GarageInterface()
        {
            m_Garage = new Garage();
            m_UserInGarage = true;
        }

        public void StarStartUserInteraction()
        {
            Console.WriteLine("Welcome to our the garage!");
            int wantedServiceKey = 0;

            CustomerData currentCustomerData = new CustomerData();

            while (m_UserInGarage)
            {
                wantedServiceKey = GetWantedService();
                PreformeService(wantedServiceKey);
            }
        }

        public int GetWantedService()
        {
            Console.WriteLine("{0}Please enter the service you would like the garage to preforme", Environment.NewLine);

            foreach (KeyValuePair<int, string> services in m_Garage.OptionalServices)
            {
                Console.WriteLine("{0}) {1}", services.Key, services.Value);
            }

            string response = Console.ReadLine();

            int optionNumber = 0;

            bool validInput = (int.TryParse(response, out optionNumber)) &&
                (optionNumber > 0) && (optionNumber <= m_Garage.OptionalServices.Count);

            while (!validInput)
            {
                optionNumber = 0;
                Console.WriteLine("Please enter a option number between 1 to {0}", m_Garage.OptionalServices.Count);
                response = Console.ReadLine();
                validInput = (int.TryParse(response, out optionNumber)) &&
                (optionNumber > 0) && (optionNumber <= m_Garage.OptionalServices.Count);
            }

            return optionNumber;
        }

        public VehicleProperties GetQuestionnaireResponse(VehicleProperties i_Properties, Dictionary<string, string> i_Questionnaire, bool doGarageQuestionnaire)
        {
            string response = String.Empty;

            bool validInput = false;

            foreach (string QuestionKey in i_Questionnaire.Keys)
            {
                validInput = false;
                i_Questionnaire.TryGetValue(QuestionKey, out string question);
                Console.WriteLine(question);
                while (!validInput)
                {
                    response = Console.ReadLine();
                    while (string.IsNullOrEmpty(response))
                    {
                        Console.WriteLine("You have entered a empty response");
                        response = Console.ReadLine();
                    }

                    try
                    {
                        i_Properties.SetResponse(QuestionKey, response);
                        validInput = true;
                    }

                    catch (NotANumberException)
                    {
                        Console.WriteLine("Please enter a number");
                        validInput = false;
                    }

                    catch (NotAPossbileOptionInEnumException)
                    {
                        Console.WriteLine("Please enter a valid option-number");
                        validInput = false;
                    }

                    catch (FormatException)
                    {
                        Console.WriteLine("Please enter the response in the right format");
                        validInput = false;
                    }
                    catch (System.OverflowException)
                    {
                        Console.WriteLine("The number you have entered is too big, please try again");
                        validInput = false;
                    }

                }
            }

            if (doGarageQuestionnaire)
            {
                Dictionary<string, string> GarageQuestionnaire = i_Properties.SupportedGarageTypeQuestionaire();

                foreach (string QuestionKey in GarageQuestionnaire.Keys)
                {
                    validInput = false;
                    GarageQuestionnaire.TryGetValue(QuestionKey, out string question);
                    Console.WriteLine(question);

                    while (!validInput)
                    {

                        response = Console.ReadLine();

                        while (string.IsNullOrEmpty(response))
                        {
                            Console.WriteLine("You have entered a empty response");
                            response = Console.ReadLine();
                        }

                        try
                        {
                            i_Properties.SetResponse(QuestionKey, response);
                            validInput = true;
                        }

                        catch (NotSupportedByGarageExcrption)
                        {
                            Console.WriteLine("Sorry, Our garage dose not support this amount of {0}, please try again", QuestionKey);
                            validInput = false;
                        }

                        catch (NotANumberException)
                        {
                            Console.WriteLine("Please enter a number");
                            validInput = false;
                        }
                    }
                }
            }

            return i_Properties;
        }

        public VehicleProperties GetVehicleProperties()
        {
            VehicleProperties vehicleProperties = new VehicleProperties();
            Dictionary<string, string> vehicleQuestionnaire = vehicleProperties.VehicleQuestionnaire;

            return GetQuestionnaireResponse(vehicleProperties, vehicleQuestionnaire, false);
        }

        private VehicleProperties GetSpecificVehicleProperties(VehicleProperties vehicleProperties)
        {
            if (vehicleProperties.VehicleType == Garage.eSupportedVehicleTypes.FuelCar
            || vehicleProperties.VehicleType == Garage.eSupportedVehicleTypes.ElectricCar)
            {
                CarProperties carProperties = new CarProperties();

                Dictionary<string, string> carQuestionnaire = carProperties.CarQuestionnaire;

                return GetQuestionnaireResponse(carProperties, carQuestionnaire, true);
            }

            else if (vehicleProperties.VehicleType == Garage.eSupportedVehicleTypes.FuelMotorcycle
            || vehicleProperties.VehicleType == Garage.eSupportedVehicleTypes.ElectricMotorcycle)
            {
                MotorcycleProperties motorcycleProperties = new MotorcycleProperties();

                Dictionary<string, string> motorcycleQuestionnaire = motorcycleProperties.MotorcycleQuestionnaire;

                return GetQuestionnaireResponse(motorcycleProperties, motorcycleQuestionnaire, true);
            }

            else if (vehicleProperties.VehicleType == Garage.eSupportedVehicleTypes.FuelTruck)
            {
                TruckProperties truckProperties = new TruckProperties();

                Dictionary<string, string> truckQuestionnaire = truckProperties.TruckQuestionnaire;

                return GetQuestionnaireResponse(truckProperties, truckQuestionnaire, true);
            }

            return new VehicleProperties();
        }

        public CustomerData GetCustomerData()
        {
            CustomerData customerData = new CustomerData();

            Dictionary<string, string> customerQuestionnaire = customerData.CustomerQuestionnaire;

            string response = String.Empty;

            foreach (string QuestionKey in customerQuestionnaire.Keys)
            {
                customerQuestionnaire.TryGetValue(QuestionKey, out string question);
                Console.WriteLine(question);
                response = Console.ReadLine();
                customerData.SetResponse(QuestionKey, response);
            }

            return customerData;
        }

        private CustomerData GetCustomerByLicensePlate()
        {
            Console.WriteLine("Please enter your {0} number", VehicleProperties.k_LicensePlateQuestionKey);

            string response = String.Empty;

            CustomerData customer = new CustomerData();

            bool isValid = false;

            while (!isValid)
            {
                response = Console.ReadLine();
                try
                {
                    customer = m_Garage.GetCustomer(response);
                    isValid = true;
                }

                catch (CustomerDoseNotExistsException)
                {
                    Console.WriteLine("Sorry, you are not in our system, please add your vehicle to garage");
                    customer = null;
                    isValid = true;
                }
            }

            return customer;
        }

        public void PreformeService(int i_NumberOServiceToPerform)
        {
            CustomerData customerData = null; 

            if (i_NumberOServiceToPerform < 7)
            {
                customerData = GetCustomerByLicensePlate();
                if (customerData == null) return;
            }

            switch (i_NumberOServiceToPerform)
            {
                case 1:
                    PreformeRefuelService(1, customerData);
                    break;
                case 2:
                    PreformeRechrageService(2, customerData);
                    break;
                case 3:
                    PreformeChangeStatusService(3, customerData);
                    break;
                case 4:
                    PreformeFilterLicnesePlatesByVehicleGarageStatusService(4);
                    break;
                case 5:
                    PreformeInflateTiresToMax(customerData);
                    break;
                case 6:
                    PreformeDisplayAllReleventData(customerData);
                    break;
                case 7:
                    PreformeAddVehicleToGarageService();
                    break;
                case 8:
                    ExitGrarage();
                    break;
            }
        }

        public void PreformeAddVehicleToGarageService()
        {
            CustomerData customerData = GetCustomerData();

            VehicleProperties vehicleProperties = GetVehicleProperties();

            VehicleProperties specificvehicleProperties = GetSpecificVehicleProperties(vehicleProperties);

            try
            {
                customerData.CustomerVehicle = m_Garage.InitializeVehicle(vehicleProperties.VehicleType, vehicleProperties, specificvehicleProperties);
                m_Garage.AddVehicleToGarage(customerData);
                Console.WriteLine(@"Your car has been added to our garage!");
            }

            catch (CustomerExistsException)
            {
                Console.WriteLine(@"You are already in our system, we have put your vehicile in repair!");
            }
        }

        private void ExitGrarage()
        {
            m_UserInGarage = false;
            Console.WriteLine("We hope you enjoyed our service, Good Bye!");
        }

        private void PreformeDisplayAllReleventData(CustomerData i_Customer)
        {

            Console.Write(m_Garage.DisplayAllReleventData(i_Customer.CustomerVehicle.LicensePlate));
        }

        private void PreformeInflateTiresToMax(CustomerData i_Customer)
        {
            try
            {
                m_Garage.InflateTiresToMax(i_Customer.CustomerVehicle.LicensePlate);
                Console.WriteLine(@"We have inflated your tires to max!");
            }
            catch(ValueOutOfRangeException)
            {
                Console.WriteLine("Your tires are already inflated to max");
            }

        }

        public void PreformeFilterLicnesePlatesByVehicleGarageStatusService(int i_ServiceNumber)
        {

            Console.WriteLine(m_Garage.GetServiceQuestionnaireByNumber(i_ServiceNumber));
            string response = String.Empty;

            bool validInput = false;

            while (!validInput)
            {
                response = Console.ReadLine();
                try
                {
                    List<string> filteredList = m_Garage.FilterLicnesePlatesByVehicleGarageStatus(response);

                    Console.WriteLine("License plate list by your given status");
                    foreach (string licnesePlate in filteredList)
                    {
                        Console.WriteLine("License-plate number: {0}{1}", licnesePlate, Environment.NewLine);
                    }
                    validInput = true;
                }

                catch (NotAPossbileOptionInEnumException)
                {
                    Console.Write("Please chose a valid option-number");
                    validInput = false;
                }
            }
        }

        public void PreformeRefuelService(int i_ServiceNumber, CustomerData i_Customer)
        {
            Console.WriteLine(m_Garage.GetServiceQuestionnaireByNumber(i_ServiceNumber));

            bool validInput = false;

            string response = string.Empty;

            while (!validInput)
            {
                response = Console.ReadLine();
                try
                {
                    m_Garage.CheckCustomerHasAFuelBased(i_Customer);

                    Energy tmpEnergy = i_Customer.CustomerVehicle.Energy;
                    FuelEngine.eFuelType fuelType = ((FuelEngine)tmpEnergy).FuleType;

                    m_Garage.RefuelCar(i_Customer.CustomerVehicle.LicensePlate, fuelType, response);
                    Console.WriteLine(@"We have refueld your vehicle!");
                    validInput = true;
                }

                catch (NotANumberException)
                {
                    Console.WriteLine("Please enter a number");
                    validInput = false;
                }

                catch (ValueOutOfRangeException)
                {
                    Console.WriteLine("Fuel addition is above max, please enter less fuel");
                    validInput = false;
                }

                catch (CustomerDoseNotExistsException)
                {
                    Console.WriteLine(@"Sorry, we can't find your LicensePlate in our system, 
                        please chose the Add vehicile to garage service");
                    validInput = true;
                }

                catch (CanNotAddEnergyToThisEngineTypeExeption)
                {
                    Console.WriteLine("Sorry, you can't add fuel to a electric based car");
                    validInput = true;
                }
            }
        }

        public void PreformeRechrageService(int i_ServiceNumber, CustomerData i_Customer)
        {

            Console.WriteLine(m_Garage.GetServiceQuestionnaireByNumber(i_ServiceNumber));
            string response = String.Empty;

            bool validInput = false;

            while (!validInput)
            {
                response = Console.ReadLine();

                try
                {
                    m_Garage.RechargeCar(i_Customer.CustomerVehicle.LicensePlate, response);
                    Console.WriteLine(@"We have recharged your vehicle!");
                    validInput = true;
                }

                catch (NotANumberException)
                {
                    Console.WriteLine("Please enter a number");
                    validInput = false;
                }

                catch (ValueOutOfRangeException)
                {
                    Console.WriteLine("Minuets addition is above max, please enter less minuets");
                    validInput = false;
                }

                catch (CustomerDoseNotExistsException)
                {
                    Console.WriteLine(@"Sorry, we can't find your LicensePlate in our system, 
                        please chose the Add vehicile to garage service");
                    validInput = true;
                }

                catch (CanNotAddEnergyToThisEngineTypeExeption)
                {
                    Console.WriteLine("Sorry, you can't add battery-time to a fuel based car");
                    validInput = true;
                }
            }
        }

        public void PreformeChangeStatusService(int i_ServiceNumber, CustomerData i_Customer)
        {
            Console.WriteLine(m_Garage.GetServiceQuestionnaireByNumber(i_ServiceNumber));
            string response = Console.ReadLine();

            bool validInput = false;

            while (!validInput)
            {
                try
                {
                    m_Garage.ChangeVehicleGarageStatus(i_Customer.CustomerVehicle.LicensePlate, response);
                    Console.WriteLine(@"Your vehicle status has been updated!");
                    validInput = true;
                }

                catch (NotAPossbileOptionInEnumException)
                {
                    Console.Write("Please chose a valid option-number");
                    validInput = false;
                }
                catch (CustomerDoseNotExistsException)
                {
                    Console.WriteLine(@"Sorry, we can't find your LicensePlate in our system, 
                        please chose the Add vehicile to garage service");
                    validInput = true;
                }
            }
        }
    }
}

