using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using battery.app.Core.Models;
using battery.app.Core.Pages;
using battery.app.Core.Repositories;
using battery.app.Core.Services;
using battery.app.Core.ViewModels.Battery;
using battery.app.Core.ViewModels.ShipmentViewModels;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace battery.app.Core.ViewModels
{
	public class MainViewModel : MvxNavigationViewModel
	{
		public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IAuthService authService)
			: base(logProvider, navigationService)
		{
			_authService = authService;
		}


		private IMvxViewModel _exitViewModel;
		private readonly IAuthService _authService;

		public override void ViewAppearing()
		{
			base.ViewAppearing();
			
			_exitViewModel = new ExitViewModel(Mvx.IoCProvider.GetSingleton<IMvxNavigationService>(), Mvx.IoCProvider.GetSingleton<IAuthService>());
			NavigationService.Navigate<ScanningViewModel>();

			if (_authService.User.Role == UserRole.Stockman)
			{
				NavigationService.Navigate<ShipmentViewModel>();
				NavigationService.Navigate<ShippingListViewModel>();
			}
			if (_authService.User.Role == UserRole.Dealer)
			{
				NavigationService.Navigate<ShippingListViewModel>();
			}
			NavigationService.Navigate<NewsViewModel>();
			NavigationService.Navigate(_exitViewModel);
		
		}
	}
}
