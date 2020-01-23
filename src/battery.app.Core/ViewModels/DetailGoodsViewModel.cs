using battery.app.Core.Models;
using MvvmCross.ViewModels;

namespace battery.app.Core.ViewModels
{
	/// <summary>
	/// Представляет модель представления детальную страницу товара.
	/// </summary>
	public class DetailGoodsViewModel : MvxViewModel<Goods>
	{
		/// <summary>
		/// Код товара.
		/// </summary>
		private Goods _goods;

		/// <summary>
		/// Подготавливает параметры.
		/// </summary>
		/// <param name="parameter">Код товара.</param>
		public override void Prepare(Goods parameter)
		{
			_goods = parameter;
		}

		public Goods Goods => _goods;
	}
}
