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
using PommaLabs.Thrower;

namespace battery.app.Core.Services
{
	public class ShipmentService : IShipmentService
	{
		/// <summary>
		/// Маппер.
		/// </summary>
		private readonly Mapper _mapper;

		/// <summary>
		/// Токен для доступа к api.
		/// </summary>
		private readonly AccessToken _accessToken;

		private const string CheckBatteryUri = "http://battery.itmit-studio.ru/api/delivery/checkBattery";

		/// <summary>
		/// Адрес для получения дилеров.
		/// </summary>
		private const string StoreUri = "http://battery.itmit-studio.ru/api/delivery/store";

		/// <summary>
		/// Инициализирует новый экземпляр <see cref="DealerService" />.
		/// </summary>
		/// <param name="accessToken">Токен для доступа к api.</param>
		public ShipmentService(AccessToken accessToken)
		{
			Raise.ArgumentNullException.IfIsNull(accessToken, nameof(accessToken));
			_accessToken = accessToken;
			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<Shipment, ShipmentDto>()
				   .ForMember(dto => dto.Dealer, m => m.MapFrom(ship => ship.Dealer.Guid))
				   .ForMember(dto => dto.GoodsCodes, m => m.MapFrom(ship => ship.Goods.Select(i => i.SerialNumber)));

				cfg.CreateMap<ShipmentDto, Shipment>();

				cfg.CreateMap<GoodsDto, Goods>()
				   .ForMember(g => g.ProductionDate, m => m.MapFrom(dto => dto.ProductionDate ?? DateTime.MinValue))
				   .ForMember(g => g.DeliveryDate, m => m.MapFrom(dto => dto.DeliveryDate ?? DateTime.MinValue));

			}));
		}

		public async Task<bool> Store(Shipment shipment)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{_accessToken.Type} {_accessToken.Body}");
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

		public async Task<Goods> CheckGoods(string code)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{_accessToken.Type} {_accessToken.Body}");
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.PostAsync(CheckBatteryUri,
								   new FormUrlEncodedContent(new Dictionary<string, string>
								   {
									   { "serial_number", code }
								   }));

				var jsonString = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(jsonString);

				if (response.IsSuccessStatusCode)
				{
					if (!string.IsNullOrEmpty(jsonString))
					{
						var jsonData = JsonConvert.DeserializeObject<GeneralDto<GoodsDto>>(jsonString);
						return await Task.FromResult(_mapper.Map<Goods>(jsonData.Data));
					}
				}

				return null;
			}
		}
	}
}
