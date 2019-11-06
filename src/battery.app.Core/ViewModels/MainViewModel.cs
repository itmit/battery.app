using System.Collections.Generic;
using System.Threading.Tasks;
using battery.app.Core.Pages;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace battery.app.Core.ViewModels
{
	public class MainViewModel : MvxNavigationViewModel
	{
		public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService) => ShowInitialViewModelsCommand = new MvxAsyncCommand(ShowInitialViewModels);

		public IMvxAsyncCommand ShowInitialViewModelsCommand { get; private set; }

		private IMvxViewModel _exitViewModel;

		private Task ShowInitialViewModels()
		{
			_exitViewModel = new ExitViewModel();
			var tasks = new List<Task>
			{
				NavigationService.Navigate<ScanningViewModel>(),
				NavigationService.Navigate<ListGoodsViewModel>(),
				NavigationService.Navigate<ShippingListViewModel>(),
				NavigationService.Navigate<NewsViewModel>(),
				NavigationService.Navigate(_exitViewModel)
			};
			return Task.WhenAll(tasks);
		}
	}
}
