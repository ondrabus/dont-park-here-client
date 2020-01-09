using AutoMapper;
using DontParkHere.Models;
using Kentico.Kontent.Delivery;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DontParkHere.Services
{
	public class KontentDeliveryService : ICloudDeliveryService
	{
		private IDeliveryClient _deliveryClient;
		private HttpClient _httpClient;
		private ITypeProvider _typeProvider;
		private IMapper _mapper;

		private Dictionary<Type, string> _typeMapper = new Dictionary<Type, string>
		{
			{ typeof(Zone), KenticoKontentModels.Area.Codename },
			{ typeof(ParkingMachine), KenticoKontentModels.ParkingMachine.Codename }
		};

		public KontentDeliveryService(HttpClient httpClient, ITypeProvider typeProvider)
		{
			_httpClient = httpClient;
			_typeProvider = typeProvider;
			_mapper = KenticoKontentModels.Mapping.MapperConfig.GetConfig().CreateMapper();
		}
		public IDeliveryClient DeliveryClient
		{
			get
			{
				if (_deliveryClient == null)
				{
					var projectId = "7018f14f-f1c3-0111-8a5b-f14953cccaa4";
					_deliveryClient = DeliveryClientBuilder.WithProjectId(projectId).WithHttpClient(_httpClient).WithTypeProvider(_typeProvider).Build();
				}

				return _deliveryClient;
			}
		}

		public async Task<List<T>> GetAllItems<T>()
		{
			var typeClassname = _typeMapper[typeof(T)];
			var data = await DeliveryClient.GetItemsAsync(new EqualsFilter("system.type", typeClassname), new DepthParameter(5));

			return _mapper.Map<List<T>>(data.Items);
		}
	}
}
