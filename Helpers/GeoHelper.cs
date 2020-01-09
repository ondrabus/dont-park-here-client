using AspNetMonsters.Blazor.Geolocation;
using GeoCoordinatePortable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DontParkHere.Helpers
{
    public static class GeoHelper
    {
        public static double GetDistanceTo(this Location source, Location target)
        {
            var sourceCoords = new GeoCoordinate(Convert.ToDouble(source.Latitude), Convert.ToDouble(source.Longitude));
            var targetCoords = new GeoCoordinate(Convert.ToDouble(target.Latitude), Convert.ToDouble(target.Longitude));

            return sourceCoords.GetDistanceTo(targetCoords);
        }
    }
}
