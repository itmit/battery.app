﻿using System;

namespace battery.app.Core.Models
{
	/// <summary>
	/// Представляет token доступа к api.
	/// </summary>
	public class AccessToken
	{
		/// <summary>
		/// Возвращает или устанавливает тело token.
		/// </summary>
		public string Body
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или устанавливает тип token.
		/// </summary>
		public string Type
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает или устанавливает строковое представление даты, до которой действует токен.
		/// </summary>
		public DateTime ExpiresAt
		{
			get;
			set;
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => $"{Type} {Body}";
	}
}
