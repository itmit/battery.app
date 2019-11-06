
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.Presenters.Hints;
using MvvmCross.ViewModels;

namespace battery.app.Core.ViewModels
{
	public class ScanningViewModel : MvxNavigationViewModel
	{
		public ScanningViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
		{
		}
		public override async Task Initialize()
		{
			await Task.Delay(3000);
		}

		public override void ViewAppeared()
		{
			base.ViewAppeared();
		}

	}
}
