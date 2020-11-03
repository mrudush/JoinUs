using System;
using System.Collections.Generic;
using System.Text;

namespace JoinUs.Models.Validator
{
    /// <summary>
    /// Interface for ConfigurationValidator
    /// </summary>
    public interface IConfigurationValidator
    {
        /// <summary>
        /// Checks all configuration values are null or not
        /// </summary>
        /// <param name="configurationSettings">configuration settings object</param>
        /// <returns>true when all configuration data have value else false</returns>
        public bool ValidateConfiguration(ConfigurationSettings configurationSettings);
    }
}
