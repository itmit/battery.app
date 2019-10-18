using System.Net.Http;
using battery.app.Models;
using battery.app.Repositories;
using battery.app.Services;
using FreshMvvm;
using Realms;

namespace battery.app.PageModels
{
	/// <summary>
	/// Представляет модель представления для авторизации.
	/// </summary>
	public class AuthorizationPageModel : FreshBasePageModel
	{
		private readonly UserRepository _userRepository = new UserRepository(RealmModel.RealmDefaultConfiguration);
		private readonly App _app = App.Current;

		/// <summary>
		/// Представляет команду для авторизации.
		/// </summary>
		public FreshAwaitCommand AuthCommand
			=> new FreshAwaitCommand(async (obj, tcs) =>
			{
				if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
				{
					tcs.SetResult(true);
					return;
				}

				IAuthService authService = new AuthService(new HttpClientHandler());
				User user = await authService.Login(Login, Password);
				if (user == null)
				{
					tcs.SetResult(true);
					return;
				}

				_userRepository.Add(user);

				_app.MainPage = _app.GetMainNavigationPage(user.Role);
				tcs.SetResult(false);
			});

		/// <summary>
		/// Возвращает или устанавливает пароль для авторизации.
		/// </summary>
		public string Password
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или устанавливает логин для авторизации.
		/// </summary>
		public string Login
		{
			get;
			set;
		}
	}
}
