using System.Collections.Generic;
using System.Diagnostics;
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
	/// <summary>
	/// Представляет 
	/// </summary>
	public class AuthService : IAuthService
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
		public AuthService()
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
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var requestBody = JsonConvert.SerializeObject(new AuthDto
				{
					Login = login,
					Password = password
				});

				Debug.WriteLine(requestBody);

				var response = await client.PostAsync(LoginUri, 
													  new StringContent(requestBody, Encoding.UTF8, "application/json"));

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
