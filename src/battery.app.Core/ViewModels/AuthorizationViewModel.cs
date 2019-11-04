using System;
using System.Net.Http;
using System.Threading.Tasks;
using battery.app.Core;
using battery.app.Core.Pages;
using battery.app.Core.Repositories;
using battery.app.Models;
using battery.app.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Realms;

namespace battery.app.Core.ViewModels
{
	/// <summary>
	/// Представляет модель представления для авторизации.
	/// </summary>
	public class AuthorizationViewModel : MvxViewModel
	{
		/// <summary>
		/// Репозиторий для работы с базой пользователей.
		/// </summary>
		private readonly UserRepository _userRepository = new UserRepository(RealmModel.RealmDefaultConfiguration);

		public override Task Initialize()
		{
			return base.Initialize();
		}

		/// <summary>
		/// Текущее приложение.
		/// </summary>
		private readonly App _app = App.Current;
		private string _password;
		private string _login;

		/// <summary>
		/// Представляет команду для авторизации.
		/// </summary>
		public IMvxCommand AuthCommand => new MvxCommand(Authorizate);

		private async void Authorizate()
		{
			if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
			{
				await _app.MainPage.DisplayAlert(Properties.Strings.Alert, Properties.Strings.EmptyFieldsMessage, Properties.Strings.Ok);
				return;
			}

			IAuthService authService = new AuthService(new HttpClientHandler());
			User user = await authService.Login(Login, Password);
			if (user == null)
			{
				await _app.MainPage.DisplayAlert(Properties.Strings.Alert, Properties.Strings.AuthorizationError, Properties.Strings.Ok);
				return;
			}

			_userRepository.Add(user);

			_app.MainPage = new MainPage();
		}

		/// <summary>
		/// Возвращает или устанавливает пароль для авторизации.
		/// </summary>
		public string Password
		{
			get => _password;
			set => SetProperty(ref _password, value);
		}

		/// <summary>
		/// Возвращает или устанавливает логин для авторизации.
		/// </summary>
		public string Login
		{
			get => _login;
			set => SetProperty(ref _login, value);
		}
	}
}
