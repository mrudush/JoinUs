using JoinUs.Models;
using JoinUs.Models.Validator;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoinUs.UnitTests.Models
{
    [TestClass]
    public class ConfigurationValidatorTest
    {      

        [TestMethod]
        public void ConfigurationValidator_ValidateConfiguration_NullInputFilePath_ReturnFalse()
        {
            ConfigurationSettings configurationSettings = new ConfigurationSettings();
            configurationSettings.InputFilePath = "";
            Mock<ILogger<ConfigurationValidator>> mockLogger = new Mock<ILogger<ConfigurationValidator>>();

            ConfigurationValidator configurationValidator = new ConfigurationValidator(mockLogger.Object);

            var result = configurationValidator.ValidateConfiguration(configurationSettings);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ConfigurationValidator_ValidateConfiguration_NullOutputFilePath_ReturnFalse()
        {
            ConfigurationSettings configurationSettings = new ConfigurationSettings();
            configurationSettings.InputFilePath = "InputFilePath";
            configurationSettings.OutputFilePath = "";
            Mock<ILogger<ConfigurationValidator>> mockLogger = new Mock<ILogger<ConfigurationValidator>>();

            ConfigurationValidator configurationValidator = new ConfigurationValidator(mockLogger.Object);

            var result = configurationValidator.ValidateConfiguration(configurationSettings);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ConfigurationValidator_ValidateConfiguration_NullLongitude1_ReturnFalse()
        {
            ConfigurationSettings configurationSettings = new ConfigurationSettings();
            configurationSettings.InputFilePath = "InputFilePath";
            configurationSettings.OutputFilePath = "OutputFilePath";
            configurationSettings.Longitude1 = null;
            
            Mock<ILogger<ConfigurationValidator>> mockLogger = new Mock<ILogger<ConfigurationValidator>>();

            ConfigurationValidator configurationValidator = new ConfigurationValidator(mockLogger.Object);

            var result = configurationValidator.ValidateConfiguration(configurationSettings);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ConfigurationValidator_ValidateConfiguration_NullLatitude1_ReturnFalse()
        {
            ConfigurationSettings configurationSettings = new ConfigurationSettings();
            configurationSettings.InputFilePath = "InputFilePath";
            configurationSettings.OutputFilePath = "OutputFilePath";
            configurationSettings.Longitude1 = 0;
            configurationSettings.Latitude1 = null;

            Mock<ILogger<ConfigurationValidator>> mockLogger = new Mock<ILogger<ConfigurationValidator>>();

            ConfigurationValidator configurationValidator = new ConfigurationValidator(mockLogger.Object);

            var result = configurationValidator.ValidateConfiguration(configurationSettings);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ConfigurationValidator_ValidateConfiguration_NullWithInDistance_ReturnFalse()
        {
            ConfigurationSettings configurationSettings = new ConfigurationSettings();
            configurationSettings.InputFilePath = "InputFilePath";
            configurationSettings.OutputFilePath = "OutputFilePath";
            configurationSettings.Longitude1 = 0;
            configurationSettings.Latitude1 = 0;
            configurationSettings.WithInDistance = null;

            Mock<ILogger<ConfigurationValidator>> mockLogger = new Mock<ILogger<ConfigurationValidator>>();

            ConfigurationValidator configurationValidator = new ConfigurationValidator(mockLogger.Object);

            var result = configurationValidator.ValidateConfiguration(configurationSettings);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ConfigurationValidator_ValidateConfiguration_NotNullConfiguration_ReturnTrue()
        {
            ConfigurationSettings configurationSettings = new ConfigurationSettings();
            configurationSettings.InputFilePath = "InputFilePath";
            configurationSettings.OutputFilePath = "OutputFilePath";
            configurationSettings.Longitude1 = 0;
            configurationSettings.Latitude1 = 0;
            configurationSettings.WithInDistance = 0;

            Mock<ILogger<ConfigurationValidator>> mockLogger = new Mock<ILogger<ConfigurationValidator>>();

            ConfigurationValidator configurationValidator = new ConfigurationValidator(mockLogger.Object);

            var result = configurationValidator.ValidateConfiguration(configurationSettings);
            Assert.IsTrue(true);
        }
    }
}
