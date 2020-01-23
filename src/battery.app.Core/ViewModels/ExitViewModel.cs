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
using Xamarin.Forms;

namespace battery.app.Core.ViewModels
{
	/// <summary>
	/// Представляет модель представления для страницы выхода.
	/// </summary>
	public class ExitViewModel : MvxViewModel
	{
		/// <summary>
		/// Команда для выхода.
		/// </summary>
		private ICommand _exitCommand;

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
		private void DoExit()
		{
			var userRepository = Mvx.IoCProvider.Create<IUserRepository>();
			userRepository.Remove(userRepository.GetAll().SingleOrDefault());
			Application.Current.MainPage = new AuthorizationPage();
		}
	}
}
