using AspNetMonsters.Blazor.Geolocation;
using DontParkHere.Models;
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
        [Inject]
        protected ZoneService ZoneService { get; set; }
        [Inject]
        protected ParkingMachineService ParkingMachineService { get; set; }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public Zone CurrentZone { get; set; }
        public List<ParkingMachine> ParkingMachines { get; set; }

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
        protected async void SetLocation(Location location)
        {
            Latitude = location.Latitude;
            Longitude = location.Longitude;
            CurrentZone = await ZoneService.GetZoneByPoint(location);
            var parkingMachines = await ParkingMachineService.GetNearestParkingMachines(location);
            if (parkingMachines.Count > 1)
            {
                await JSRuntime.InvokeAsync<object>("mapSetParkingMachines", parkingMachines[0].Location, parkingMachines[1].Location);
            }
            StateHasChanged();
        }
    }
}
