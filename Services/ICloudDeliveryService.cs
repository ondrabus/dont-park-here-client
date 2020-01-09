using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DontParkHere.Services
{
	public interface ICloudDeliveryService
	{
		Task<List<T>> GetAllItems<T>();
	}
}
