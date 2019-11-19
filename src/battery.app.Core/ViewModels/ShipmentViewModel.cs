using System.Windows.Input;
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
		private MvxObservableCollection<string> _goodsCodes = new MvxObservableCollection<string>();
		private App _app = App.Current;

		public ShipmentViewModel(IMvxNavigationService navigationService) => _navigationService = navigationService;

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
				GoodsCodes.Add(code);
			} 
		}

		private void ScanGoods()
		{
			var page = new ZXingScannerPage();
			page.OnScanResult += PageOnOnScanResult;
			
			_app.MainPage.Navigation.PushModalAsync(page);
		}

		private void PageOnOnScanResult(Result result)
		{
			if (result == null || string.IsNullOrEmpty(result.Text))
			{
				return;
			}

			var newCollection = new MvxObservableCollection<string>();
			foreach (var goodsCode in GoodsCodes)
			{
				newCollection.Add(goodsCode);
			}
			newCollection.Add(result.Text);
			GoodsCodes = newCollection;

			_app.MainPage.Navigation.PopModalAsync();
		}

		public MvxObservableCollection<string> GoodsCodes
		{
			get => _goodsCodes;
			set => SetProperty(ref _goodsCodes, value);
		}
	}
}
