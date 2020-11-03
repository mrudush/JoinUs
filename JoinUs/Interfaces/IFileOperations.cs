using JoinUs.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoinUs.Interfaces
{
    /// <summary>
    /// Interface for FileOperations
    /// </summary>
    public interface IFileOperations
    {
        /// <summary>
        /// This method read data from input file and
        /// map to details into Customer object.
        /// </summary>
        /// <param name="inputFilePath">Input file path</param>        
        /// <returns>collection of Customer object generated from input json data</returns>
        IList<Customer> Read(string inputFilePath);

        /// <summary>
        /// This method write customers detail into output file.
        /// </summary>
        /// <param name="outputFilePath">output file path</param>
        /// <param name="customers">Customer object collection</param>        
        void Write(string outputFilePath, IList<Customer> customers);
    }
}
