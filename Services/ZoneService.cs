using AspNetMonsters.Blazor.Geolocation;
using DontParkHere.Helpers;
using DontParkHere.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DontParkHere.Services
{
    public class ZoneService
    {
        private ICloudDeliveryService _cloudDeliveryService;
        private static List<Zone> _zones = null;

        public ZoneService(ICloudDeliveryService cloudDeliveryService)
        {
            _cloudDeliveryService = cloudDeliveryService;
        }

        public async Task<List<Zone>> GetAllZonesAsync()
        {
            if (_zones == null)
            {
                _zones = await _cloudDeliveryService.GetAllItems<Zone>();
            }
            return _zones;
        }

        public async Task<Zone> GetZoneByPoint(Location point)
        {
            var zones = await GetAllZonesAsync();
            return zones.FirstOrDefault(z => ZoneHelper.IsPointInside(point, z));
        }
    }
}
