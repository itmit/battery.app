using System;
using Newtonsoft.Json;
using PropertyChanged;

namespace battery.app.Models
{
	/// <summary>
	/// Представляет пользователя.
	/// </summary>
	[AddINotifyPropertyChangedInterface]
	public class User
	{
		/// <summary>
		/// Возвращает или устанавливает id пользователя.
		/// </summary>
		public Guid Guid
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
		/// Возвращает или устанавливает token доступа пользователя.
		/// </summary>
		public AccessToken AccessToken
		{
			get;
			set;
		}
	}
}
