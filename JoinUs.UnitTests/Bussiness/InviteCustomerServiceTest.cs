using Castle.Core.Logging;
using JoinUs.Bussiness;
using JoinUs.Interfaces;
using JoinUs.Models;
using JoinUs.Models.Validator;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoinUs.UnitTests.Bussiness
{
    [TestClass]
    public class InviteCustomerServiceTest
    {

        [TestMethod]
        public void InviteCustomerService_Run_InvalidConfiguration_ReturnFalse()
        {

            var someOptions = Options.Create(new ConfigurationSettings());

            Mock<IConfigurationValidator> mockConfigurationValidator = new Mock<IConfigurationValidator>();
            mockConfigurationValidator.Setup(x => x.ValidateConfiguration(someOptions.Value))
                .Returns(false);

            InviteCustomerService inviteCustomerService = new InviteCustomerService
                (null, null, someOptions, mockConfigurationValidator.Object, null);

            bool result = inviteCustomerService.Run();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void InviteCustomerService_Run_InValidatePoint1_Returnfalse()
        {
            var someOptions = Options.Create(new ConfigurationSettings());
            someOptions.Value.InputFilePath = "";
            someOptions.Value.OutputFilePath = "";
            someOptions.Value.Latitude1 = 1;
            someOptions.Value.Longitude1 = 1;
            someOptions.Value.WithInDistance = 100;

            Mock<IConfigurationValidator> mockConfigurationValidator = new Mock<IConfigurationValidator>();
            mockConfigurationValidator.Setup(x => x.ValidateConfiguration(someOptions.Value))
                .Returns(true);

            Mock<IDistanceOperations> mockDistanceOperations = new Mock<IDistanceOperations>();
            mockDistanceOperations.Setup(x => x.ValidatePoint(1, 1)).Returns(false);

            Mock<ILogger<InviteCustomerService>> mockLogger = new Mock<ILogger<InviteCustomerService>>();
            InviteCustomerService inviteCustomerService = new InviteCustomerService
                (null, mockDistanceOperations.Object, someOptions, mockConfigurationValidator.Object, mockLogger.Object);

            bool result = inviteCustomerService.Run();
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void InviteCustomerService_Run_NoCustomersFoundForSearch_ReturnFalse()
        {
            var someOptions = Options.Create(new ConfigurationSettings());
            someOptions.Value.InputFilePath = "";
            someOptions.Value.OutputFilePath = "";
            someOptions.Value.Latitude1 = 1;
            someOptions.Value.Longitude1 = 1;
            someOptions.Value.WithInDistance = 100;

            Mock<IConfigurationValidator> mockConfigurationValidator = new Mock<IConfigurationValidator>();
            mockConfigurationValidator.Setup(x => x.ValidateConfiguration(someOptions.Value))
                .Returns(true);

            Mock<IDistanceOperations> mockDistanceOperations = new Mock<IDistanceOperations>();
            mockDistanceOperations.Setup(x => x.ValidatePoint(1, 1)).Returns(true);

            List<Customer> customers = new List<Customer>();
            Mock<IFileOperations> mockFileOperations = new Mock<IFileOperations>();
            mockFileOperations.Setup(x => x.Read(""))
                .Returns(customers);

            Mock<ILogger<InviteCustomerService>> mockLogger = new Mock<ILogger<InviteCustomerService>>();

            InviteCustomerService inviteCustomerService = new InviteCustomerService
                (mockFileOperations.Object, mockDistanceOperations.Object, someOptions,
                mockConfigurationValidator.Object, mockLogger.Object);

            bool result = inviteCustomerService.Run();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void InviteCustomerService_Run_NoCustomerWithIn100_ReturnEligibleCustomersWithOutValue()
        {
            var someOptions = Options.Create(new ConfigurationSettings());
            someOptions.Value.InputFilePath = "";
            someOptions.Value.OutputFilePath = "";
            someOptions.Value.Latitude1 = 52.986375;
            someOptions.Value.Longitude1 = -6.043701;
            someOptions.Value.WithInDistance = 100;

            Mock<IConfigurationValidator> mockConfigurationValidator = new Mock<IConfigurationValidator>();
            mockConfigurationValidator.Setup(x => x.ValidateConfiguration(someOptions.Value))
                .Returns(true);

            Mock<IDistanceOperations> mockDistanceOperations = new Mock<IDistanceOperations>();
            mockDistanceOperations.Setup(x => x.ValidatePoint(-6.043701, 52.986375)).Returns(true);

            List<Customer> customers = new List<Customer>();
            customers.Add(new Customer
            {
                Latitude = 52.986375,
                Longitude = -6.043701,
                Name = "Christina McArdle",
                UserId = 12
            });

            Mock<IFileOperations> mockFileOperations = new Mock<IFileOperations>();
            mockFileOperations.Setup(x => x.Read(""))
                .Returns(customers);

            Mock<ILogger<InviteCustomerService>> mockLogger = new Mock<ILogger<InviteCustomerService>>();

            mockDistanceOperations.Setup(x => x.CalculateDistance(-6.043701, 52.986375, -6.043701, 52.986375)).Returns(101);

            InviteCustomerService inviteCustomerService = new InviteCustomerService
                (mockFileOperations.Object, mockDistanceOperations.Object, someOptions,
                mockConfigurationValidator.Object, mockLogger.Object);

            bool result = inviteCustomerService.Run();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void InviteCustomerService_Run_FoundEligibleCustomersWithIn100_ReturnTrue()
        {
            var someOptions = Options.Create(new ConfigurationSettings());
            someOptions.Value.InputFilePath = "";
            someOptions.Value.OutputFilePath = "";
            someOptions.Value.Latitude1 = 52.986375;
            someOptions.Value.Longitude1 = -6.043701;
            someOptions.Value.WithInDistance = 100;

            Mock<IConfigurationValidator> mockConfigurationValidator = new Mock<IConfigurationValidator>();
            mockConfigurationValidator.Setup(x => x.ValidateConfiguration(someOptions.Value))
                .Returns(true);

            Mock<IDistanceOperations> mockDistanceOperations = new Mock<IDistanceOperations>();
            mockDistanceOperations.Setup(x => x.ValidatePoint(-6.043701, 52.986375)).Returns(true);

            List<Customer> customers = new List<Customer>();
            customers.Add(new Customer
            {
                Latitude = 52.986375,
                Longitude = -6.043701,
                Name = "Christina McArdle",
                UserId = 12
            });

            Mock<IFileOperations> mockFileOperations = new Mock<IFileOperations>();
            mockFileOperations.Setup(x => x.Read(""))
                .Returns(customers);

            Mock<ILogger<InviteCustomerService>> mockLogger = new Mock<ILogger<InviteCustomerService>>();

            mockDistanceOperations.Setup(x => x.CalculateDistance(-6.043701, 52.986375, -6.043701, 52.986375)).Returns(99);

            InviteCustomerService inviteCustomerService = new InviteCustomerService
                (mockFileOperations.Object, mockDistanceOperations.Object, someOptions,
                mockConfigurationValidator.Object, mockLogger.Object);

            bool result = inviteCustomerService.Run();

            Assert.IsTrue(result);
        }

    }
}
