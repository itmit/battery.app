using System;
using battery.app.Models;
using Newtonsoft.Json;

namespace battery.app.DTO
{
	public class UserDto
	{

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

		public string Login
		{
			get;
			set;
		}

		public UserRole Role
		{
			get;
			set;
		}

		public int Dealer
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
