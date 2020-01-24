using System;
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
	public class NewsService : INewsService
	{
		private AccessToken _accessToken;
		private Mapper _mapper;
		public const string StorageUri = "http://battery.itmit-studio.ru/storage/";

		/// <summary>
		/// Инициализирует новый экземпляр <see cref="DealerService" />.
		/// </summary>
		/// <param name="accessToken">Токен для доступа к api.</param>
		public NewsService(AccessToken accessToken)
		{
			Raise.ArgumentNullException.IfIsNull(accessToken, nameof(accessToken));
			_accessToken = accessToken;
			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<NewsDto, News>()
				   .ForMember(news => news.CreatedAt, m => m.MapFrom(dto => dto.CreatedAt ?? DateTime.MinValue))
				   .ForMember(news => news.ImageSource, m => m.MapFrom(dto => dto.Picture == null ? "about:blank" : StorageUri + dto.Picture));
			}));
		}

		private const string GetAllNewsUri = "http://battery.itmit-studio.ru/api/news/index/";
		public async Task<List<News>> GetNews(int limit = 100, int offset = 0)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"{_accessToken.Type} {_accessToken.Body}");
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				var uri = GetAllNewsUri + $"{limit}/{offset}";
				Debug.WriteLine(uri);
				var response = await client.GetAsync(uri);

				var jsonString = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(jsonString);

				if (response.IsSuccessStatusCode)
				{
					if (!string.IsNullOrEmpty(jsonString))
					{
						var jsonData = JsonConvert.DeserializeObject<GeneralDto<List<NewsDto>>>(jsonString);
						return await Task.FromResult(_mapper.Map<List<News>>(jsonData.Data));
					}
				}

				return null;
			}
		}
	}
}
