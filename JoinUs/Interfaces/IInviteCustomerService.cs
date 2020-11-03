using System;
using System.Collections.Generic;
using System.Text;

namespace JoinUs.Interfaces
{
    /// <summary>
    /// Interface for InviteCustomerService
    /// </summary>
    public interface IInviteCustomerService
    {
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
        bool Run();
    }
}
