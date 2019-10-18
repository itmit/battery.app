﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using battery.app.DTO;
using battery.app.Models;
using Newtonsoft.Json;

namespace battery.app.Services
{
	/// <summary>
	/// Представляет 
	/// </summary>
	public class AuthService : BaseService, IAuthService
	{
		/// <summary>
		/// Адрес для авторизации.
		/// </summary>
		private const string LoginUri = "http://battery.itmit-studio.ru/api/login";

		/// <summary>
		/// <see cref="IMapper"/> для преобразования ДТО в модели.
		/// </summary>
		private readonly IMapper _mapper;

		/// <summary>
		/// Инициализирует новый экземпляр <see cref="AuthService" />.
		/// </summary>
		/// <param name="httpClientHandler">Обработчик сообщения по умолчанию, для <see cref="HttpClient"/>.</param>
		public AuthService(HttpClientHandler httpClientHandler)
			: base(httpClientHandler)
		{
			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<AccessToken, UserDto>();
				cfg.CreateMap<User, UserDto>()
				   .ForMember(m => m.Body, o => o.MapFrom(q => q.AccessToken.Body))
				   .ForMember(m => m.Type, o => o.MapFrom(q => q.AccessToken.Type))
				   .ForMember(m => m.ExpiresAt, o => o.MapFrom(q => q.AccessToken.ExpiresAt));

				cfg.CreateMap<UserDto, User>()
				   .ForPath(m => m.AccessToken.Body, o => o.MapFrom(q => q.Body))
				   .ForPath(m => m.AccessToken.Type, o => o.MapFrom(q => q.Type))
				   .ForPath(m => m.AccessToken.ExpiresAt, o => o.MapFrom(q => q.ExpiresAt));
			}));
		}

		/// <summary>
		/// Проводит авторизацию пользователя.
		/// </summary>
		/// <param name="login">Логин пользователя.</param>
		/// <param name="password">Пароль пользователя.</param>
		/// <returns>Авторизованный пользователь.</returns>
		public async Task<User> Login(string login, string password)
		{
			using (var client = new HttpClient(HttpClientHandler))
			{
				var encodedContent = new FormUrlEncodedContent(new Dictionary<string, string>
				{
					{
						"login", login
					},
					{
						"password", password
					}
				});

				var response = await client.PostAsync(LoginUri, encodedContent);

				var jsonString = await response.Content.ReadAsStringAsync();
				Debug.WriteLine(jsonString);

				if (response.IsSuccessStatusCode)
				{
					if (!string.IsNullOrEmpty(jsonString))
					{
						var jsonData = JsonConvert.DeserializeObject<GeneralDto<UserDto>>(jsonString);
						return await Task.FromResult(_mapper.Map<User>(jsonData.Data));
					}
				}

				return null;
			}
		}
	}
}