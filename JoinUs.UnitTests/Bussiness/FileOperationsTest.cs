using JoinUs.Bussiness;
using JoinUs.Models;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JoinUs.UnitTests.Bussiness
{
    [TestClass]
    public class FileOperationsTest
    {

        [TestMethod]        
        public void FileOperations_Read_ValidInputFilePath_ReturnNonEmptyCustomerCollection()
        {
            string inputFilePath = "C:/Users/Dell/source/repos/JoinUs/Files/Input/customers.txt";
            Mock<ILogger<FileOperations>> mockLogger = new Mock<ILogger<FileOperations>>();
            FileOperations fileOperations = new FileOperations(mockLogger.Object);

            var result= fileOperations.Read(inputFilePath);
            Assert.IsInstanceOfType(result, typeof(IList<Customer>));
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void FileOperations_Read_InValidInputFilePath_ReturnNullCustomerCollection()
        {
            string inputFilePath = "C:/Users/Dell/source/JoinUs/Files/Input/customers.txt";

            Mock<ILogger<FileOperations>> mockLogger = new Mock<ILogger<FileOperations>>();
            FileOperations fileOperations = new FileOperations(mockLogger.Object);

            var result = fileOperations.Read(inputFilePath);
            Assert.IsInstanceOfType(result, typeof(IList<Customer>));
            Assert.AreEqual(0,result.Count);
        }

        [TestMethod]       
        public void FileOperations_Read_EmptyInputFilePath_ReturnNullCustomerCollection()
        {           
            
            Mock<ILogger<FileOperations>> mockLogger = new Mock<ILogger<FileOperations>>();
            FileOperations fileOperations = new FileOperations(mockLogger.Object);

            var result = fileOperations.Read("");

            Assert.IsInstanceOfType(result, typeof(IList<Customer>));
            Assert.AreEqual(0, result.Count);
        }        

    }
}
