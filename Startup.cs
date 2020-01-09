using AspNetMonsters.Blazor.Geolocation;
using DontParkHere.Services;
using Kentico.Kontent.Delivery;
using KenticoKontentModels;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DontParkHere
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<LocationService>();
            services.AddSingleton<MapService>();
            services.AddSingleton<ZoneService>();
            services.AddSingleton<ICloudDeliveryService, KontentDeliveryService>();
            services.AddSingleton<ITypeProvider, CustomTypeProvider>();
            services.AddSingleton<ParkingMachineService>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
