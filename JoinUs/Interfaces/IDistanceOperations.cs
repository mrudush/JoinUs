using System;
using System.Collections.Generic;
using System.Text;

namespace JoinUs.Interfaces
{
    /// <summary>
    /// Interface for DistanceOperations
    /// </summary>
    public interface IDistanceOperations
    {
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
        double CalculateDistance(double longitude1, double latitude1, double longitude2, double latitude2);

        /// <summary>
        /// This method checks wheather the longitude and latitude are valid.
        /// Latitude must be in [-90, 90] interval
        /// Longitude must be in [-180, 180] interval
        /// </summary>
        /// <param name="longitude">longitude of point</param>
        /// <param name="latitude">latitude of point</param>
        /// <returns>true if valid point else false</returns>
        bool ValidatePoint(double longitude, double latitude);
    }
}
