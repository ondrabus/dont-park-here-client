using AspNetMonsters.Blazor.Geolocation;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace KenticoKontentModels
{
	public class LocationConverter : IValueConverter<string, Location>
	{
		public Location Convert(string sourceMember, ResolutionContext context)
		{
			if (string.IsNullOrEmpty(sourceMember))
			{
				return new Location();
			}

			var data = JsonSerializer.Deserialize<List<double>>(sourceMember);
			return new Location
			{
				Latitude = System.Convert.ToDecimal(data[0]),
				Longitude = System.Convert.ToDecimal(data[1])
			};
		}
	}
}
