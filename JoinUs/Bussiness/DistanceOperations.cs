using JoinUs.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JoinUs.Bussiness
{
    /// <summary>
    /// Class for distance operations
    /// </summary>
    public class DistanceOperations : IDistanceOperations
    {
        #region Global members
        const double radious = 6371;
        #endregion

        #region Calculate distance
        /// <summary>
        /// The method CalculateDistance finding distance between two points.
        /// We converted all longitude and latitude degrees into radians.
        /// Note: Distance calculated in km
        /// </summary>
        /// <param name="longitude1">Longitude of point 1</param>
        /// <param name="latitude1">Latitude of point 1</param>
        /// <param name="longitude2">Longitude of point 2</param>
        /// <param name="latitude2">Latitude of point 2/param>
        /// <returns>distance between two points</returns>
        public double CalculateDistance(double longitude1, double latitude1, double longitude2, double latitude2)
        {            
            double LogitudeDifference = longitude2 - longitude1;            

            double p = Math.Sin(DegreesToRadians(latitude1)) * Math.Sin(DegreesToRadians(latitude2)) +
                       Math.Cos(DegreesToRadians(latitude1)) * Math.Cos(DegreesToRadians(latitude2)) *
                       Math.Cos(DegreesToRadians(LogitudeDifference));

            double distance = Math.Acos(p) * radious;
            return distance;
        }
        #endregion

        #region Degrees To Radians
        /// <summary>
        /// This method convert degrees To radians
        /// </summary>
        /// <param name="degree">degree of positions</param>
        /// <returns>Radian value of degree</returns>
        private static double DegreesToRadians(double degree)
        {
            return degree * Math.PI / 180;
        }
        #endregion

        #region Validate Point
        /// <summary>
        /// This method checks wheather the longitude and latitude are valid.
        /// Latitude must be in [-90, 90] interval
        /// Longitude must be in [-180, 180] interval
        /// </summary>
        /// <param name="longitude">longitude of point</param>
        /// <param name="latitude">latitude of point</param>
        /// <returns>true if valid point else false</returns>
        public bool ValidatePoint(double longitude, double latitude)
        {
            if (latitude < -90 || latitude > 90)
            {
                return false;
            }
            if (longitude < -180 || longitude > 180)
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
