using System;
using battery.app.Core.Models;
using battery.app.Core.Pages;
using battery.app.Core.Properties;
using battery.app.Core.Repositories;
using battery.app.Core.Services;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using PommaLabs.Thrower;
using Xamarin.Forms;

namespace battery.app.Core.ViewModels
{
	/// <summary>
	/// Представляет модель представления для авторизации.
	/// </summary>
	public class AuthorizationViewModel : MvxViewModel
	{
		#region Data
		#region Fields
		/// <summary>
		/// Сервис для авторизации.
		/// </summary>
		private readonly IAuthService _authService;

		/// <summary>
		/// Введенный пользователем логин.
		/// </summary>
		private string _login;

		/// <summary>
		/// Введенный пользователем пароль.
		/// </summary>
		private string _password;
		/// <summary>
		/// Репозиторий для работы с базой пользователей.
		/// </summary>
		private readonly IUserRepository _userRepository;
		#endregion
		#endregion

		#region .ctor
		/// <summary>
		/// Инициализирует AuthorizationViewModel с параметрами.
		/// </summary>
		/// <param name="userRepository">Репозиторий для сохранения пользователей после входа.</param>
		/// <param name="authService">Сервис для авторизации пользователей.</param>
		/// <exception cref="ArgumentNullException">Вызывается, если переданные репозиторий или сервис являются <c>null</c>.</exception>
		public AuthorizationViewModel(IUserRepository userRepository, IAuthService authService)
		{
			Raise.ArgumentNullException.IfIsNull(authService, nameof(authService));
			Raise.ArgumentNullException.IfIsNull(userRepository, nameof(userRepository));
			_userRepository = userRepository;
			_authService = authService;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Представляет команду для авторизации.
		/// </summary>
		public IMvxCommand AuthCommand => new MvxCommand(SignIn);

		/// <summary>
		/// Возвращает или устанавливает логин для авторизации.
		/// </summary>
		public string Login
		{
			get => _login;
			set => SetProperty(ref _login, value);
		}

		/// <summary>
		/// Возвращает или устанавливает пароль для авторизации.
		/// </summary>
		public string Password
		{
			get => _password;
			set => SetProperty(ref _password, value);
		}
		#endregion

		#region Private
		/// <summary>
		/// Авторизует пользователя для входа в приложение.
		/// </summary>
		private async void SignIn()
		{
			if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
			{
				await Application.Current.MainPage.DisplayAlert(Strings.Alert, Strings.EmptyFieldsMessage, Strings.Ok);
				return;
			}

			User user;
			try
			{
				user = await _authService.Login(Login, Password);
			}
			catch (Exception e)
			{
				await Application.Current.MainPage.DisplayAlert(Strings.Alert, Strings.AuthorizationError, Strings.Ok);
				Console.WriteLine(e);
				return;
			}

			if (user?.AccessToken == null)
			{
				await Application.Current.MainPage.DisplayAlert(Strings.Alert, Strings.AuthorizationError, Strings.Ok);
				return;
			}

			Mvx.IoCProvider.RegisterSingleton<IDealerService>(new DealerService(user.AccessToken));

			_userRepository.Add(user);

			Application.Current.MainPage = new MainPage();
		}
		#endregion
	}
}
