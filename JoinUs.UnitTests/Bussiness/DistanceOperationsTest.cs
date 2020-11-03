using JoinUs.Bussiness;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoinUs.UnitTests.Bussiness
{
    [TestClass]
    public class DistanceOperationsTest
    {
        [TestMethod]
        public void DistanceOperations_CalculateDistance_PassPoints_RetrunDistance()
        {
            double longitude1 = -6.257664;
            double latitude1 = 53.339428;
            double longitude2 = -10.27699;
            double latitude2 = 51.92893;

            DistanceOperations distanceOperations = new DistanceOperations();
            var result = distanceOperations.CalculateDistance(longitude1, latitude1, longitude2, latitude2);
            Assert.AreEqual(313.26, Convert.ToDouble(string.Format("{0:N2}", result)));
        }
        

        [TestMethod]
        public void DistancePointValidator_ValidatePoint_PointLatitudeLessThan_Negactive90_ThrowsArgumentOutOfRangeException()
        {
            double Latitude = -91;
            double Longitude = 180;

            DistanceOperations sistanceOperations = new DistanceOperations();
            var result = sistanceOperations.ValidatePoint(Longitude, Latitude);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DistancePointValidator_ValidatePoint_PointLatitudeGreaterThan_Positive90_ThrowsArgumentOutOfRangeException()
        {            

            double Latitude = 91;
            double Longitude = 180;

            DistanceOperations sistanceOperations = new DistanceOperations();
            var result = sistanceOperations.ValidatePoint(Longitude, Latitude);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DistancePointValidator_ValidatePoint_PointLongitudeLessThan_Negactive180_ThrowsArgumentOutOfRangeException()
        {            

            double Latitude = 90;
            double Longitude = -181;

            DistanceOperations sistanceOperations = new DistanceOperations();
            var result = sistanceOperations.ValidatePoint(Longitude, Latitude);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DistancePointValidator_ValidatePoint_PointLongitudeGreaterThan_Positive180_ThrowsArgumentOutOfRangeException()
        {
            double Latitude = 90;
            double Longitude = 181;

            DistanceOperations sistanceOperations = new DistanceOperations();
            var result = sistanceOperations.ValidatePoint(Longitude, Latitude);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DistancePointValidator_ValidatePoint_ValidLatitudeAndLogitude_ReturnTrue()
        {          
            double Latitude = 90;
            double Longitude = 180;

            DistanceOperations sistanceOperations = new DistanceOperations();
            var result = sistanceOperations.ValidatePoint(Longitude, Latitude);
            Assert.IsTrue(result);
        }

    }
}
