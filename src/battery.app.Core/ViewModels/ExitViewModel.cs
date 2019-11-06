using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using battery.app.Core.Pages;
using battery.app.Core.Repositories;
using MvvmCross;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace battery.app.Core.ViewModels
{
	public class ExitViewModel : MvxViewModel
	{
		private ICommand _exitCommand;

		public ICommand ExitCommand
		{
			get
			{
				_exitCommand = _exitCommand ?? new MvxCommand(DoExit);
				return _exitCommand;
			}
		}

		private void DoExit()
		{
			var userRepository = Mvx.IoCProvider.Create<IUserRepository>();
			userRepository.Remove(userRepository.All().SingleOrDefault());
			App.Current.MainPage = new AuthorizationPage();
		}
	}
}
