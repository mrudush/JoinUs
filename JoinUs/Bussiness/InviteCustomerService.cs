using JoinUs.Interfaces;
using JoinUs.Models;
using JoinUs.Models.Validator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JoinUs.Bussiness
{
    /// <summary>
    /// Class for Invite Customer
    /// </summary>
    public class InviteCustomerService : IInviteCustomerService
    {
        #region Global Data members
        private readonly IFileOperations _fileOperations;
        private readonly IDistanceOperations _distanceOperations;
        private readonly IOptions<ConfigurationSettings> _configuration;
        private readonly IConfigurationValidator _configurationValidator;
        private readonly ILogger<InviteCustomerService> _logger;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor, dependency injection happening here
        /// </summary>
        /// <param name="fileOperations">Injects FileOperations instance</param>
        /// <param name="distanceOperations">Injects DistanceOperations instance</param>
        /// <param name="configuration">Injects ConfigurationSettings instance</param>
        /// <param name="configurationValidator">Injects ConfigurationValidator instance</param>
        /// <param name="logger">Injects Logger instance</param>
        public InviteCustomerService(IFileOperations fileOperations, IDistanceOperations distanceOperations,
            IOptions<ConfigurationSettings> configuration, IConfigurationValidator configurationValidator,
            ILogger<InviteCustomerService> logger)
        {
            _fileOperations = fileOperations;
            _distanceOperations = distanceOperations;
            _configuration = configuration;
            _configurationValidator = configurationValidator;
            _logger = logger;
        }
        #endregion

        #region Run 
        /// <summary>
        /// This method follows below steps
        /// 1. Validate Configuration details we have in Appsettings.json file.
        /// 2. return false if any data missing from configuration file.
        /// 3. If True, validate the point 1 Company Longitude and Latitude valid or not.
        /// 4. If not valid, return false else Read customer details from Input file and map to Customer object.
        /// 5. Return false if Customer object is empty, else Find Distance and check the distance within 100km.
        /// 6. Generate an new Customer onbject which will have only customers who are eligible for invitation.
        /// 7. Write eligible customer into out put file in Ascending order of User_Id.
        /// 8. If there is no eligible customer return false
        /// </summary>
        /// <returns>True if we have eligible customer else return false</returns>
        public bool Run()
        {
            if (_configurationValidator.ValidateConfiguration(_configuration.Value))
            {
                string inputFilePath = _configuration.Value.InputFilePath;
                string outputFilePath = _configuration.Value.OutputFilePath;
                double longitude1 =(double) _configuration.Value.Longitude1;
                double latitude1 = (double)_configuration.Value.Latitude1;
                double withInDistance = (double)_configuration.Value.WithInDistance;
                IList<Customer> eligibleCustomers = new List<Customer>();

                if (_distanceOperations.ValidatePoint(longitude1, latitude1))
                {
                    IList<Customer> customers = _fileOperations.Read(inputFilePath);

                    if (customers.Count > 0)
                    {
                        _logger.LogInformation("Searching customers who are living within..." 
                            + withInDistance+ "km");

                        foreach (Customer customer in customers)
                        {
                            if (_distanceOperations.ValidatePoint(customer.Longitude, customer.Latitude))
                            {
                                double distance = _distanceOperations.CalculateDistance(longitude1, latitude1,
                                                                        customer.Longitude, customer.Latitude);

                                if (distance < withInDistance)
                                {
                                    eligibleCustomers.Add(customer);
                                }
                            }
                            else{
                                _logger.LogWarning("Customer Longitude/Latitude is in-valid, " +
                                    "Note: Latitude Must be in [-90, 90] interval " +
                                    "and Longitude Must be in [-180, 180] interval;customer details, " +
                                    "username:{0}, user_id:{1}", customer.Name, customer.UserId);
                            }
                        }

                        if (eligibleCustomers.Count > 0)
                        {                            
                            _fileOperations.Write(outputFilePath, eligibleCustomers.OrderBy(x => x.UserId).ToList());
                        }
                        else{
                            _logger.LogInformation("There is no Customers living within " + withInDistance + "km");
                            return false;
                        }
                    }
                    else{
                        _logger.LogError("We couldnt read Json data from input file");
                        return false;
                    }
                }
                else{
                    _logger.LogError("Company Longitude/Latitude is in-valid, please correct configuration file");
                    return false;
                }
            }
            else{
                return false;
            }
            _logger.LogInformation("We found some customers who are eligible for our invitation. " +
                    "Please find the output file from following location {0}", _configuration.Value.OutputFilePath);
            return true;
        }
        #endregion
    }
}
