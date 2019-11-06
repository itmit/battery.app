using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace battery.app.Core.ViewModels
{
	/// <summary>
	/// Представляет модель представления для списка товаров.
	/// </summary>
	public class ListGoodsViewModel : MvxNavigationViewModel
	{
		private MvxCommand _openCreateGoodsCommand;

		public ListGoodsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService)
			: base(logProvider, navigationService)
		{
		}

		public ICommand OpenCreateGoodsCommand
		{
			get
			{
				_openCreateGoodsCommand = _openCreateGoodsCommand ?? new MvxCommand(OpenCreateGoods);
				return _openCreateGoodsCommand;
			}
		}

		private void OpenCreateGoods()
		{
			NavigationService.Navigate<ShipmentViewModel>();
		}
	}
}
