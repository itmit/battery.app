using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace battery.app.Core.ViewModels.Battery
{
	/// <summary>
	/// Представляет модель представления детальную страницу товара.
	/// </summary>
	public class BatteryDetailViewModel : MvxViewModel<Models.Battery>
	{
		/// <summary>
		/// Код товара.
		/// </summary>
		private Models.Battery _battary;
		private MvxCommand _closePageCommand;
		private IMvxNavigationService _mvxNavigationService;

		public BatteryDetailViewModel(IMvxNavigationService mvxNavigationService)
		{
			_mvxNavigationService = mvxNavigationService;
		}

		public IMvxCommand ClosePageCommand
		{
			get
			{
				_closePageCommand = _closePageCommand ??
										   new MvxCommand(() =>
										   {
											   _mvxNavigationService.Close(this);
										   });
				return _closePageCommand;
			}
		}

		/// <summary>
		/// Подготавливает параметры.
		/// </summary>
		/// <param name="parameter">Код товара.</param>
		public override void Prepare(Models.Battery parameter)
		{
			_battary = parameter;
		}

		public Models.Battery Battery => _battary;
	}
}
