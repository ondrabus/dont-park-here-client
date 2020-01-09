using AspNetMonsters.Blazor.Geolocation;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace KenticoKontentModels
{
	public class ZoneConverter : IValueConverter<string, List<Location>>
	{
		public List<Location> Convert(string sourceMember, ResolutionContext context)
		{
			if (string.IsNullOrEmpty(sourceMember))
			{
				return new List<Location>();
			}

			var areaData = sourceMember.Replace('"', ' ');
			var data = JsonSerializer.Deserialize<List<List<double>>>(areaData);
			return data.Select(c => new Location { Latitude = System.Convert.ToDecimal(c[0]), Longitude = System.Convert.ToDecimal(c[1]), Accuracy = 1 }).ToList();
		}
	}
}
