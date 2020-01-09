using AspNetMonsters.Blazor.Geolocation;
using AutoMapper;
using Kentico.Kontent.Delivery;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace KenticoKontentModels.Mapping
{
	public class MapperConfig
	{
		public static MapperConfiguration GetConfig()
		{
			return new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<ContentItem, DontParkHere.Models.VisitorRestriction>()
					.ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.GetString(VisitorRestriction.DescriptionCodename)));

				cfg.CreateMap<ContentItem, DontParkHere.Models.Zone>()
					.ForMember(dst => dst.Points, opt => opt.ConvertUsing<ZoneConverter, string>(src => src.GetString(Area.AreaDataCodename)))
					.ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.System.Name))
					.ForMember(dst => dst.VisitorRestrictions, opt => opt.ConvertUsing<VisitorRestrictionsConverter, IEnumerable<ContentItem>>(src => src.GetLinkedItems(Area.RestrictionsCodename)))
					.AfterMap((area, zone) =>
					{
						zone.Points.ForEach(point =>
						{
							zone.LonMin = Math.Min(zone.LonMin, point.Longitude);
							zone.LonMax = Math.Max(zone.LonMax, point.Longitude);
							zone.LatMin = Math.Min(zone.LatMin, point.Latitude);
							zone.LatMax = Math.Max(zone.LatMax, point.Latitude);
						});
					});

				cfg.CreateMap<ReadOnlyCollection<Area>, ReadOnlyCollection<DontParkHere.Models.Zone>>();

				cfg.CreateMap<ContentItem, DontParkHere.Models.ParkingMachine>()
					.ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.System.Name))
					.ForMember(dst => dst.Location, opt => opt.ConvertUsing<LocationConverter, string>(src => src.GetString(ParkingMachine.LocationCodename)));

			});
		}
	}
}
