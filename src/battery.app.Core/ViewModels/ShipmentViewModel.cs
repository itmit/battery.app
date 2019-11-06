using System.Windows.Input;
using battery.app.Core.Pages;
using battery.app.Core.Services;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace battery.app.Core.ViewModels
{
	public class ShipmentViewModel : MvxViewModel
	{
		private ICommand _addGoodsCommand;
		private IMvxNavigationService _navigationService;
		private ICommand _scanGoodsCommand;

		public ShipmentViewModel(IMvxNavigationService navigationService)
		{
			_navigationService = navigationService;
		}

		public ICommand ScanGoodsCommand
		{
			get
			{
				_scanGoodsCommand = _scanGoodsCommand ?? new MvxCommand(ScanGoods);
				return _scanGoodsCommand;
			}
		}

		public ICommand AddGoodsCommand
		{
			get
			{
				_addGoodsCommand = _addGoodsCommand ?? new MvxCommand<string>(AddGoods);
				return _addGoodsCommand;
			}
		}

		private void AddGoods(string obj)
		{
			if (obj is string code)
			{

				GoodCodes.Add(code);
			} 
		}

		public override void ViewAppeared()
		{
			base.ViewAppeared();
		}

		private void ScanGoods() 
		{ 
			var vm = new ScannerViewModel(Mvx.IoCProvider.Resolve<IMvxNavigationService>());
			_navigationService.Navigate(vm);
		}

		public MvxObservableCollection<string> GoodCodes
		{
			get;
			set;
		} = new MvxObservableCollection<string>();

	}
}
