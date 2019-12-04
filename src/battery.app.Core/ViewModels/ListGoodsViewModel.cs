using System.Threading.Tasks;
using System.Windows.Input;
using battery.app.Core.Models;
using battery.app.Core.Services;
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
		private IDealerService _dealerService;
		private MvxObservableCollection<Dealer> _dealers;

		public ListGoodsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IDealerService dealerService)
			: base(logProvider, navigationService)
		{
			_dealerService = dealerService;
		}

		public override async Task Initialize()
		{
			await base.Initialize();
			Dealers = new MvxObservableCollection<Dealer>(await _dealerService.GetDealers());
		}

		public ICommand OpenCreateGoodsCommand
		{
			get
			{
				_openCreateGoodsCommand = _openCreateGoodsCommand ?? new MvxCommand(OpenCreateGoods);
				return _openCreateGoodsCommand;
			}
		}

		public MvxObservableCollection<Dealer> Dealers
		{
			get => _dealers;
			set => SetProperty(ref _dealers, value);
		}

		private void OpenCreateGoods()
		{
			_ = Initialize();

			NavigationService.Navigate<ShipmentViewModel>();
		}
	}
}
