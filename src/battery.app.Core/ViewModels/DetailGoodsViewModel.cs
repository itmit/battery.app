using battery.app.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
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
		private MvxCommand _closePageCommand;
		private IMvxNavigationService _mvxNavigationService;

		public DetailGoodsViewModel(IMvxNavigationService mvxNavigationService)
		{
			_mvxNavigationService = mvxNavigationService;
		}

		public IMvxCommand ClosePageCommand
		{
			get
			{
				_closePageCommand = _closePageCommand ??
										   new MvxCommand(() =>
										   {
											   _mvxNavigationService.Close(this);
										   });
				return _closePageCommand;
			}
		}

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
