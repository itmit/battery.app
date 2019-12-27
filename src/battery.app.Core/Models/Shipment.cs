﻿using System.Collections.Generic;

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
		public Shipment(IEnumerable<Goods> goodsCodes, Dealer dealer)
		{
			Goods = new List<Goods>(goodsCodes);
			Dealer = dealer;
		}

		/// <summary>
		/// Возвращает коды товаров отгрузки.
		/// </summary>
		public List<Goods> Goods
		{
			get;
		}

		/// <summary>
		/// Возвращает дилера.
		/// </summary>
		public Dealer Dealer
		{
			get;
			set;
		}
	}
}