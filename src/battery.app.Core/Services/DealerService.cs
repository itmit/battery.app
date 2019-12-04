using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using battery.app.Core.DTO;
using battery.app.Core.Models;
using Newtonsoft.Json;
using PommaLabs.Thrower;

namespace battery.app.Core.Services
{
	public class DealerService : IDealerService
	{
		private readonly Mapper _mapper;
		private AccessToken _accessToken;

		private const string GetDealersUri = "http://battery.itmit-studio.ru/api/delivery/listOfDealers";

		public DealerService(AccessToken accessToken)
		{
			Raise.ArgumentNullException.IfIsNull(accessToken, nameof(accessToken));
			_accessToken = accessToken;
			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<Dealer, DealerDto>();

				cfg.CreateMap<DealerDto, Dealer>();
			}));
		}

		public async Task<IEnumerable<Dealer>> GetDealers()
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{_accessToken.Type} {_accessToken.Body}");
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync(GetDealersUri);

				var jsonString = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(jsonString);

				if (response.IsSuccessStatusCode)
				{
					if (!string.IsNullOrEmpty(jsonString))
					{
						var jsonData = JsonConvert.DeserializeObject<GeneralDto<List<DealerDto>>>(jsonString);
						return await Task.FromResult(_mapper.Map<IEnumerable<Dealer>>(jsonData.Data));
					}
				}

				return await Task.FromResult(new List<Dealer>());
			}
		}
	}
}
