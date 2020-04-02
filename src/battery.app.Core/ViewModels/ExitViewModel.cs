using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using battery.app.Core.Pages;
using battery.app.Core.Repositories;
using battery.app.Core.Services;
using MvvmCross;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace battery.app.Core.ViewModels
{
	/// <summary>
	/// Представляет модель представления для страницы выхода.
	/// </summary>
	public class ExitViewModel : MvxViewModel
	{
		private readonly IMvxNavigationService _navigationService;

		/// <summary>
		/// Команда для выхода.
		/// </summary>
		private ICommand _exitCommand;
		private readonly IAuthService _authService;

		public ExitViewModel(IMvxNavigationService navigationService, IAuthService authService)
		{
			_authService = authService;
			_navigationService = navigationService;
		}

		/// <summary>
		/// Возвращает команду для выхода.
		/// </summary>
		public ICommand ExitCommand
		{
			get
			{
				_exitCommand = _exitCommand ?? new MvxCommand(DoExit);
				return _exitCommand;
			}
		}

		/// <summary>
		/// Выполняет выход из приложения и открывает авторизацию.
		/// </summary>
		public async void DoExit()
		{
			if (await _authService.Logout(_authService.User))
			{
				await _navigationService.Navigate<AuthorizationViewModel>();
			}
		}
	}
}
