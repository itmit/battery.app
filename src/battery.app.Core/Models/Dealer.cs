using System;

namespace battery.app.Core.Models
{
	public class Dealer
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
		public Guid Guid
		{
			get;
			set;
		}
	}
}
