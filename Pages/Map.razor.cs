using AspNetMonsters.Blazor.Geolocation;
using DontParkHere.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DontParkHere.Pages
{
    public class MapBase : ComponentBase
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }
        [Inject]
        protected LocationService LocationService { get; set; }
        [Inject]
        protected MapService MapService { get; set; }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                MapService.WatchLocation(this.SetLocation);

                Console.WriteLine("Initializing map");
                await JSRuntime.InvokeAsync<object>("mapInit");

                var currentLocation = await LocationService.GetLocationAsync();
                await JSRuntime.InvokeAsync<object>("mapCenter", currentLocation.Latitude, currentLocation.Longitude);
            }
        }
        protected void SetLocation(Location location)
        {
            Console.WriteLine($"Check this position: {location.Latitude} {location.Longitude}");
            Latitude = location.Latitude;
            Longitude = location.Longitude;
            StateHasChanged();
        }
    }
}
