using AspNetMonsters.Blazor.Geolocation;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DontParkHere.Services
{
    public class MapService
    {
        static Action<Location> _callback;

        public void WatchLocation(Action<Location> watchCallback)
        {
            _callback = watchCallback;
        }

        [JSInvokable]
        public static void SetLocation(string latitude, string longitude)
        {
            var location = new Location
            {
                Latitude = Convert.ToDecimal(latitude),
                Longitude = Convert.ToDecimal(longitude),
                Accuracy = 1
            };

            _callback.Invoke(location);
        }

        public static void SetLocation(Location location)
        {
        }
    }
}
