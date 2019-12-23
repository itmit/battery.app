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
		public Shipment(IEnumerable<string> goodsCodes, Dealer dealer)
		{
			GoodsCodes = new List<string>(goodsCodes);
			Dealer = dealer;
		}

		/// <summary>
		/// Возвращает коды товаров отгрузки.
		/// </summary>
		public List<string> GoodsCodes
		{
			get;
		}

		/// <summary>
		/// Возвращает дилера.
		/// </summary>
		public Dealer Dealer
		{
			get;
		}
	}
}
