using AspNetMonsters.Blazor.Geolocation;
using DontParkHere.Models;

namespace DontParkHere.Helpers
{
    public static class ZoneHelper
    {
        public static bool IsPointInside(Location point, Zone zone)
        {
            // pre-check using a minimum bounding box
            if (point.Latitude < zone.LatMin || point.Latitude > zone.LatMax || point.Longitude < zone.LonMin || point.Longitude > zone.LonMax)
            {
                return false;
            }

            var polygon = zone.Points;
            int i, j;
            bool c = false;
            for (i = 0, j = polygon.Count - 1; i < polygon.Count; j = i++)
            {
                if ((((polygon[i].Latitude <= point.Latitude) && (point.Latitude < polygon[j].Latitude)) || ((polygon[j].Latitude <= point.Latitude) && (point.Latitude < polygon[i].Latitude))) && (point.Longitude < (polygon[j].Longitude - polygon[i].Longitude) * (point.Latitude - polygon[i].Latitude) / (polygon[j].Latitude - polygon[i].Latitude) + polygon[i].Longitude))
                {
                    c = !c;
                }
            }

            return c;
        }
    }
}
