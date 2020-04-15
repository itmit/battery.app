using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using battery.app.Core.DTO;
using battery.app.Core.Models;
using Newtonsoft.Json;

namespace battery.app.Core.Services
{
	public class ShipmentService : IShipmentService
	{
		/// <summary>
		/// Маппер.
		/// </summary>
		private readonly Mapper _mapper;

		/// <summary>
		/// Сервис авторизации, для получения токена доступа пользователя.
		/// </summary>
		private readonly IAuthService _authService;

		private const string CheckBatteryUri = "http://battery.itmit-studio.ru/api/delivery/getBatteryByCode";
		private const string GetDeliveriesAndShipmentsUri = "http://battery.itmit-studio.ru/api/checkDeliveryAndShipment/listOfDeliveriesAndShipments";

		/// <summary>
		/// Адрес для получения дилеров.
		/// </summary>
		private const string StoreUri = "http://battery.itmit-studio.ru/api/shipment/store";

		/// <summary>
		/// Инициализирует новый экземпляр <see cref="DealerService" />.
		/// </summary>
		/// <param name="authService">Сервис авторизации, для получения токена доступа пользователя.</param>
		public ShipmentService(IAuthService authService)
		{
			_authService = authService;
			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.AllowNullCollections = false;
				cfg.CreateMap<Shipment, ShipmentDto>()
				   .ForMember(dto => dto.Serials, m => m.MapFrom(ship => ship.Goods.Select(i => i.SerialNumber)))
				   .ForMember(dto => dto.Guid, m => m.MapFrom(ship => ship.Dealer.Guid));


				cfg.CreateMap<ShipmentDto, Shipment>()
				   .ForMember(ship => ship.CreatedAt, m => m.MapFrom(dto => dto.CreatedAt ?? DateTime.MinValue));
				
				cfg.CreateMap<Delivery, DeliveryDto>()
				   .ForMember(dto => dto.Serials, m => m.MapFrom(ship => ship.Goods.Select(i => i.SerialNumber)));
				cfg.CreateMap<DeliveryDto, Delivery>()
				   .ForMember(ship => ship.CreatedAt, m => m.MapFrom(dto => dto.CreatedAt ?? DateTime.MinValue));

				cfg.CreateMap<GoodsDto, Battery>()
				   .ForMember(batter => batter.Delivery, m => m.MapFrom(dto => dto))
				   .ForMember(batter => batter.SerialNumber, m => m.MapFrom(dto => dto.Serial));

				cfg.CreateMap<GoodsDto, Delivery>();
			}));
		}

		public async Task<bool> Store(Shipment shipment)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(_authService.UserToken.ToString());
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var requestBody = JsonConvert.SerializeObject(_mapper.Map<ShipmentDto>(shipment));

				Debug.WriteLine(requestBody);

				var response = await client.PostAsync(StoreUri,
													  new StringContent(requestBody, Encoding.UTF8, "application/json"));

				var jsonString = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(jsonString);

				return response.IsSuccessStatusCode;
			}
		}

		public async Task<Battery> CheckGoods(string code)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(_authService.UserToken.ToString());
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.PostAsync(CheckBatteryUri,
								   new FormUrlEncodedContent(new Dictionary<string, string>
								   {
									   { "code", code }
								   }));

				var jsonString = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(jsonString);

				if (response.IsSuccessStatusCode)
				{
					if (!string.IsNullOrEmpty(jsonString))
					{
						var jsonData = JsonConvert.DeserializeObject<GeneralDto<GoodsDto>>(jsonString);
						return await Task.FromResult(_mapper.Map<Battery>(jsonData.Data));
					}
				}

				return null;
			}
		}

		public async Task<List<Shipment>> GetDeliveriesAndShipments()
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(_authService.UserToken.ToString());
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync(GetDeliveriesAndShipmentsUri);

				var jsonString = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(jsonString);

				if (response.IsSuccessStatusCode)
				{
					if (!string.IsNullOrEmpty(jsonString))
					{
						var jsonData = JsonConvert.DeserializeObject<GeneralDto<DeliveriesShipmentsDto>>(jsonString);
						var list = _mapper.Map<List<Shipment>>(jsonData.Data.Shipments);
						list.AddRange(_mapper.Map<List<Delivery>>(jsonData.Data.Deliveries));
						return list;
					}
				}

				return null;
			}
		}

		private const string GetBatteryInShipmentsUri = "http://battery.itmit-studio.ru/api/checkDeliveryAndShipment/getBatteriesFromShipment";
		public async Task<List<Battery>> GetBatteryInShipments(Shipment shipment)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(_authService.UserToken.ToString());
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.PostAsync(GetBatteryInShipmentsUri, new FormUrlEncodedContent(new Dictionary<string, string>
				{
					{"shipment_uuid", shipment.Guid.ToString() }
				}));

				var jsonString = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(jsonString);

				if (response.IsSuccessStatusCode)
				{
					if (!string.IsNullOrEmpty(jsonString))
					{
						var jsonData = JsonConvert.DeserializeObject<GeneralDto<List<GoodsDto>>>(jsonString);
						return await Task.FromResult(_mapper.Map<List<Battery>>(jsonData.Data));
					}
				}

				return null;
			}
		}

		private const string GetBatteryInDeliveryUri = "http://battery.itmit-studio.ru/api/checkDeliveryAndShipment/getBatteriesFromDelivery";
		public async Task<List<Battery>> GetBatteryInDelivery(Delivery delivery)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(_authService.UserToken.ToString());
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.PostAsync(GetDeliveriesAndShipmentsUri, new FormUrlEncodedContent(new Dictionary<string, string>
				{
					{"delivery_uuid", delivery.Guid.ToString() }
				}));

				var jsonString = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(jsonString);

				if (response.IsSuccessStatusCode)
				{
					if (!string.IsNullOrEmpty(jsonString))
					{
						var jsonData = JsonConvert.DeserializeObject<GeneralDto<List<GoodsDto>>>(jsonString);
						return await Task.FromResult(_mapper.Map<List<Battery>>(jsonData.Data));
					}
				}

				return null;
			}
		}


	}
}
