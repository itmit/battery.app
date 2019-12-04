using System;
using Newtonsoft.Json;

namespace battery.app.Core.DTO
{
	/// <summary>
	/// Представляет DTO для сущности дилера.
	/// </summary>
	public class DealerDto
	{
		/// <summary>
		/// Возвращает или устанавливает логин дилера.
		/// </summary>
		public string Login
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращают или устанавливают ид.
		/// </summary>
		[JsonProperty("uid")]
		public Guid Guid
		{
			get;
			set;
		}
	}
}
