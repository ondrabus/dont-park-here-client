using System;
using System.Collections.Generic;
using System.Linq;
using Kentico.Kontent.Delivery;

namespace KenticoKontentModels
{
    public class CustomTypeProvider : ITypeProvider
    {
        private static readonly Dictionary<Type, string> _codenames = new Dictionary<Type, string>
        {
            // generated types
            {typeof(Area), "area"},
            {typeof(Duration), "duration"},
            {typeof(ParkingMachine), "parking_machine"},
            {typeof(PublicHoliday), "public_holiday"},
            {typeof(ResidentRestriction), "resident_restriction"},
            {typeof(VisitorRestriction), "visitor_restriction"}
        };

        public Type GetType(string contentType)
        {
            return _codenames.Keys.FirstOrDefault(type => GetCodename(type).Equals(contentType));
        }

        public string GetCodename(Type contentType)
        {
            return _codenames.TryGetValue(contentType, out var codename) ? codename : null;
        }
    }
}