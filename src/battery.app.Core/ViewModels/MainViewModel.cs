using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using battery.app.Core.Models;
using battery.app.Core.Pages;
using battery.app.Core.Repositories;
using battery.app.Core.ViewModels.ShipmentViewModels;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace battery.app.Core.ViewModels
{
	public class MainViewModel : MvxNavigationViewModel
	{
		public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, IUserRepository repository)
			: base(logProvider, navigationService)
		{
			ShowInitialViewModelsCommand = new MvxAsyncCommand(ShowInitialViewModels);
			_user = repository.GetUsers()
							  .Single();
		}

		public IMvxAsyncCommand ShowInitialViewModelsCommand { get; private set; }

		private IMvxViewModel _exitViewModel;

		private readonly User _user;

		private Task ShowInitialViewModels()
		{
			_exitViewModel = new ExitViewModel();
			var tasks = new List<Task>
			{
				NavigationService.Navigate<ScanningViewModel>()
			};

			if (_user.Role == UserRole.Stockman)
			{
				tasks.Add(NavigationService.Navigate<ShipmentViewModel>());
				tasks.Add(NavigationService.Navigate<ShippingListViewModel>());
			}
			tasks.Add(NavigationService.Navigate<NewsViewModel>());
			tasks.Add(NavigationService.Navigate(_exitViewModel));

			return Task.WhenAll(tasks);
		}
	}
}
