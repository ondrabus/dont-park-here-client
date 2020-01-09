using AspNetMonsters.Blazor.Geolocation;
using DontParkHere.Helpers;
using DontParkHere.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DontParkHere.Services
{
	public class ParkingMachineService
	{
		private ICloudDeliveryService _cloudDeliveryService;
		private List<ParkingMachine> _parkingMachines = null;

		public ParkingMachineService(ICloudDeliveryService cloudDeliveryService)
		{
			_cloudDeliveryService = cloudDeliveryService;
		}
		public async Task<List<ParkingMachine>> GetAllParkingMachinesAsync()
		{
			if (_parkingMachines == null)
			{
				_parkingMachines = await _cloudDeliveryService.GetAllItems<ParkingMachine>();
			}
			return _parkingMachines;
		}
		public async Task<List<ParkingMachine>> GetNearestParkingMachines(Location point)
		{
			var parkingMachines = await GetAllParkingMachinesAsync();
			return parkingMachines.OrderBy(p => p.Location.GetDistanceTo(point)).Take(2).ToList();
		}
	}
}
