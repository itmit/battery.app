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
		#region Data
		#region Consts
		private const string CheckBatteryUri = "http://battery.itmit-studio.ru/api/delivery/getBatteryByCode";
		private const string GetBatteryInShipmentsUri = "http://battery.itmit-studio.ru/api/shipment/{0}";
		private const string GetShipmentsUri = "http://battery.itmit-studio.ru/api/shipment";

		/// <summary>
		/// Адрес для получения дилеров.
		/// </summary>
		private const string StoreUri = "http://battery.itmit-studio.ru/api/shipment";
		#endregion

		#region Fields
		/// <summary>
		/// Сервис авторизации, для получения токена доступа пользователя.
		/// </summary>
		private readonly IAuthService _authService;
		/// <summary>
		/// Маппер.
		/// </summary>
		private readonly Mapper _mapper;
		#endregion
		#endregion

		#region .ctor
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
				   .ForMember(dto => dto.WhomUuid, m => m.MapFrom(ship => ship.Dealer.Guid))
				   .ForMember(dto => dto.FromUuid, m => m.MapFrom(ship => ship.Storekeeper.Guid));

				cfg.CreateMap<ShipmentDto, Shipment>()
				   .ForMember(ship => ship.CreatedAt, m => m.MapFrom(dto => dto.CreatedAt ?? DateTime.MinValue))
				   .ForPath(ship => ship.Dealer.Guid, m => m.MapFrom(dto => dto.WhomUuid ?? Guid.Empty))
				   .ForPath(ship => ship.Dealer.Login, m => m.MapFrom(dto => dto.WhomClientName))
				   .ForPath(ship => ship.Storekeeper.Guid, m => m.MapFrom(dto => dto.FromUuid ?? Guid.Empty))
				   .ForPath(ship => ship.Storekeeper.Login, m => m.MapFrom(dto => dto.FromClientName));

				cfg.CreateMap<GoodsDto, Battery>()
				   .ForMember(batter => batter.Delivery, m => m.MapFrom(dto => dto))
				   .ForMember(batter => batter.SerialNumber, m => m.MapFrom(dto => dto.Serial));

				cfg.CreateMap<GoodsDto, Delivery>();
			}));
		}
		#endregion

		#region IShipmentService members
		public async Task<Battery> CheckGoods(string code)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(_authService.UserToken.ToString());
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.PostAsync(CheckBatteryUri,
													  new FormUrlEncodedContent(new Dictionary<string, string>
													  {
														  {
															  "code", code
														  }
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

		public async Task<List<Battery>> GetBatteryInShipments(int id)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(_authService.UserToken.ToString());
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync(string.Format(GetBatteryInShipmentsUri, id));

				var jsonString = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(jsonString);

				if (response.IsSuccessStatusCode)
				{
					if (!string.IsNullOrEmpty(jsonString))
					{
						var jsonData = JsonConvert.DeserializeObject<GeneralDto<List<GoodsDto>>>(jsonString);
						var list = _mapper.Map<List<Battery>>(jsonData.Data);
						return list;
					}
				}

				return null;
			}
		}

		public async Task<List<Shipment>> GetShipments()
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(_authService.UserToken.ToString());
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync(GetShipmentsUri);

				var jsonString = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(jsonString);

				if (response.IsSuccessStatusCode)
				{
					if (!string.IsNullOrEmpty(jsonString))
					{
						var jsonData = JsonConvert.DeserializeObject<GeneralDto<List<ShipmentDto>>>(jsonString);
						var list = _mapper.Map<List<Shipment>>(jsonData.Data);
						return list;
					}
				}

				return null;
			}
		}

		public async Task<bool> Store(Shipment shipment)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(_authService.UserToken.ToString());
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var requestBody = JsonConvert.SerializeObject(_mapper.Map<ShipmentDto>(shipment));

				Debug.WriteLine(requestBody);

				var response = await client.PostAsync(StoreUri, new StringContent(requestBody, Encoding.UTF8, "application/json"));
#if (DEBUG)
				var jsonString = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(jsonString);
#endif
				return response.IsSuccessStatusCode;
			}
		}
		#endregion
	}
}
