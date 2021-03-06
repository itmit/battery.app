﻿using System.Threading.Tasks;
using battery.app.Core.Models;

namespace battery.app.Core.Services
{
	/// <summary>
	/// Представляет интерфейс для авторизации.
	/// </summary>
	public interface IAuthService
	{
		/// <summary>
		/// Проводит авторизацию пользователя.
		/// </summary>
		/// <param name="login">Логин пользователя.</param>
		/// <param name="password">Пароль пользователя.</param>
		/// <returns>Авторизованный пользователь.</returns>
		Task<User> Login(string login, string password);


		User User
		{
			get;
		}

		AccessToken UserToken
		{
			get;
		}

		Task<bool> Logout(User user);
	}
}
