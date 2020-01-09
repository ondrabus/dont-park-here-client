using AspNetMonsters.Blazor.Geolocation;
using System.Collections.Generic;

namespace DontParkHere.Models
{
    public class Zone
    {
        public decimal LatMin { get; set; }
        public decimal LatMax { get; set; }
        public decimal LonMin { get; set; }
        public decimal LonMax { get; set; }
        public List<Location> Points { get; set; }
        public string Name { get; set; }
        public List<VisitorRestriction> VisitorRestrictions { get; set; }
    }
}
