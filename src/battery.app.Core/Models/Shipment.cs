﻿using System;
using System.Collections.Generic;

namespace battery.app.Core.Models
{
	/// <summary>
	/// Представляет модель отгрузки.
	/// </summary>
	public class Shipment
	{
		/// <summary>
		/// Инициализирует новый экземпляр <see cref="Shipment" />.
		/// </summary>
		/// <param name="goodsCodes">Коды товаров отгрузки.</param>
		/// <param name="dealer">Дилер.</param>
		public Shipment(IEnumerable<Battery> goodsCodes, Dealer dealer)
		{
			Goods = new List<Battery>(goodsCodes);
			Dealer = dealer;
		}

		public Shipment() { }

		public Guid Guid
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает коды товаров отгрузки.
		/// </summary>
		public List<Battery> Goods
		{
			get;
			set;
		}

		/// <summary>
		/// Возвращает дилера.
		/// </summary>
		public Dealer Dealer
		{
			get;
			set;
		}

		public User Storekeeper
		{
			get;
			set;
		}

		public DateTime CreatedAt
		{
			get;
			set;
		}
	}
}
