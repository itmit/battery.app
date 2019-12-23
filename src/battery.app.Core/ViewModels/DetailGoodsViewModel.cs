using MvvmCross.ViewModels;

namespace battery.app.Core.ViewModels
{
	/// <summary>
	/// Представляет модель представления детальную страницу товара.
	/// </summary>
	public class DetailGoodsViewModel : MvxViewModel<string>
	{
		/// <summary>
		/// Код товара.
		/// </summary>
		private string _goodsCode;

		/// <summary>
		/// Подготавливает параметры.
		/// </summary>
		/// <param name="parameter">Код товара.</param>
		public override void Prepare(string parameter)
		{
			_goodsCode = parameter;
		}
	}
}
