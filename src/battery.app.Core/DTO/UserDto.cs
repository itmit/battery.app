using System;
using battery.app.Models;
using Newtonsoft.Json;

namespace battery.app.DTO
{
	/// <summary>
	/// Представляет ДТО для сущности пользователя, с token.
	/// </summary>
	public class UserDto
	{

		/// <summary>
		/// Возвращает или устанавливает ид пользователя.
		/// </summary>
		[JsonProperty("uid")]
		public Guid Guid
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или устанавливает тело token.
		/// </summary>
		[JsonProperty("access_token")]
		public string Body
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или устанавливает логин пользователя.
		/// </summary>
		public string Login
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или устанавливает роль пользователя.
		/// </summary>
		public UserRole Role
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или устанавливает тип token.
		/// </summary>
		[JsonProperty("token_type")]
		public string Type
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или устанавливает строковое представление даты, до которой действует токен.
		/// </summary>
		[JsonProperty("expires_at")]
		public DateTime ExpiresAt
		{
			get;
			set;
		}
	}
}
