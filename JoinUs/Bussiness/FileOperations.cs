using JoinUs.Interfaces;
using JoinUs.Models;
using Microsoft.Extensions.Logging;
using Namotion.Reflection;
using Newtonsoft.Json;
using NJsonSchema.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace JoinUs.Bussiness
{
    /// <summary>
    /// Class for File operations
    /// </summary>
    public class FileOperations : IFileOperations
    {
        #region Global members
        private readonly ILogger<FileOperations> _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor, dependency injection happening here
        /// </summary>
        /// <param name="logger">Injects Logger instance</param>
        public FileOperations(ILogger<FileOperations> logger)
        {
            _logger = logger;
        }
        #endregion

        #region Read from input file
        /// <summary>
        /// This method read data from input file and
        /// map to details into Customer object.
        /// </summary>
        /// <param name="inputFilePath">Input file path</param>
        /// <returns>collection of Customer object generated from input json data</returns>
        public IList<Customer> Read(string inputFilePath)
        {
            if (string.IsNullOrEmpty(inputFilePath))
            {
                _logger.LogError("Input File Path is empty");
                return new List<Customer>();
            }

            if(! File.Exists(inputFilePath))
            {
                _logger.LogError("Input File Path not exists, please check configuration file; Path: "+ inputFilePath);
                return new List<Customer>();
            }

            string jsonString = File.ReadAllText(inputFilePath);
            string base64EncodedExternalAccount = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonString));
            var jsonStream = new MemoryStream(Convert.FromBase64String(base64EncodedExternalAccount));

            return MapJsonStreamToCustomerObject(jsonStream);

        }

        #region Map Json Stream To Customer Object
        /// <summary>
        /// This method helps to map JsonStream into Customer Object collection.
        /// </summary>
        /// <param name="stream">Json stream</param>
        /// <returns>empty collection when json stream has nothing, else 
        /// return collection of Customer object generated from input json data</returns>
        private IList<Customer> MapJsonStreamToCustomerObject(Stream stream)
        {
            if (stream.Length == 0)
            {
                return new List<Customer>();
            }

            IList<Customer> customers = new List<Customer>();
            using (var reader = new StreamReader(stream))
            {
                string line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        customers.Add(JsonConvert.DeserializeObject<Customer>(line));
                    }
                }
            }
            return customers;
        }
        #endregion
        #endregion        

        #region Write into output file
        /// <summary>
        /// This method write customers detail into output file.
        /// </summary>
        /// <param name="outputFilePath">output file path</param>
        /// <param name="customers">Customer object collection</param>        
        public void Write(string outputFilePath, IList<Customer> customers)
        {
            if (customers.Count == 0)
            {
                _logger.LogError("There is no customers to write");
            }

            string jsonString = MapCustomerObjectToJsonString(customers);

            using (StreamWriter sw = new StreamWriter(outputFilePath))
            {
                sw.WriteLine(jsonString);
            }
        }

        #region Map Customer Object To String
        /// <summary>
        /// This method helps to map Customer object collection into to Json String.
        /// </summary>
        /// <param name="customers">customer object collection</param>
        /// <returns>Json string</returns>
        private static string MapCustomerObjectToJsonString(IList<Customer> customers)
        {
            JsonSerializerSettings serializerSettings = IgnoreProperties();

            StringBuilder stringBuilder = new StringBuilder();
            foreach (Customer customer in customers)
            {
                stringBuilder.Append(JsonConvert.SerializeObject(customer, serializerSettings));
            }

            return stringBuilder.ToString();
        }
        #endregion

        #region Ignore Properties
        /// <summary>
        /// Thsi method helps to ignore selected properties from Json string.
        /// </summary>
        /// <returns>JsonSerializerSettings</returns>
        private static JsonSerializerSettings IgnoreProperties()
        {
            var jsonResolver = new PropertyRenameAndIgnoreSerializerContractResolver();
            jsonResolver.IgnoreProperty(typeof(Customer), "latitude");
            jsonResolver.IgnoreProperty(typeof(Customer), "longitude");

            JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = jsonResolver;

            return serializerSettings;
        }
        #endregion
        #endregion

    }
}
