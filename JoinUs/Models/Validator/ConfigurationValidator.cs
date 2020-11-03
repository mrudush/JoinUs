using JoinUs.Bussiness;
using System;
using System.Collections.Generic;
using System.Text;
using Serilog;
using Microsoft.Extensions.Logging;

namespace JoinUs.Models.Validator
{
    /// <summary>
    /// Class for validate configuration
    /// </summary>
    public class ConfigurationValidator : IConfigurationValidator
    {
        #region Global Data members
        private readonly ILogger<ConfigurationValidator> _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor, dependency injection happening here
        /// </summary>
        /// <param name="logger">Injects Logger instance</param>
        public ConfigurationValidator(ILogger<ConfigurationValidator> logger)
        {
            _logger = logger;
        }
        #endregion

        #region Validate Configuration
        /// <summary>
        /// Checks all configuration values are null or not
        /// </summary>
        /// <param name="configurationSettings">configuration settings object</param>
        /// <returns>true when all configuration data have value else false</returns>
        public bool ValidateConfiguration(ConfigurationSettings configurationSettings)
        {
            if (string.IsNullOrEmpty(configurationSettings.InputFilePath))
            {
                _logger.LogError("Data missing from Configuration file, Missing data: InputFilePath");                
                return false;
            }

            if (string.IsNullOrEmpty(configurationSettings.OutputFilePath))
            {
                _logger.LogError("Data missing from Configuration file, Missing data: OutputFilePath");
                return false;
            }

            if (string.IsNullOrEmpty(Convert.ToString(configurationSettings.Longitude1)))
            {
                _logger.LogError("Data missing from Configuration file, Missing data: Longitude 1");
                return false;
            }

            if (string.IsNullOrEmpty(Convert.ToString(configurationSettings.Latitude1)))
            {
                _logger.LogError("Data missing from Configuration file, Missing data: Latitude 1");
                return false;
            }

            if (string.IsNullOrEmpty(Convert.ToString(configurationSettings.WithInDistance)))
            {
                _logger.LogError("Data missing from Configuration file, Missing data: WithInDistance");
                return false;
            }
            return true;
        }
        #endregion
    }
}
