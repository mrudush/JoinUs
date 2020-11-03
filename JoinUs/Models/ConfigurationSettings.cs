using System;
using System.Collections.Generic;
using System.Text;

namespace JoinUs.Models
{
    /// <summary>
    /// This model maps Appsettings configuration file details
    /// </summary>
    public class ConfigurationSettings
    {
        /// <summary>
        /// Input file for this application, the value configured in appsettings file
        /// </summary>
        public string InputFilePath { get; set; }

        /// <summary>
        /// Output file for this application, the value configured in appsettings file
        /// </summary>
        public string OutputFilePath { get; set; }

        /// <summary>
        /// Company longitude, the value configured in appsettings file
        /// </summary>
        public double? Longitude1 { get; set; }

        /// <summary>
        /// Company latitude, the value configured in appsettings file
        /// </summary>
        public double? Latitude1 { get; set; }

        /// <summary>
        /// Invitation send within this distance, the value configured in appsettings file
        /// Note: Distance in km
        /// </summary>
        public double? WithInDistance { get; set; }

    }
}
