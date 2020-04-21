using battery.app.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace battery.app.Core.ViewModels
{
	public class NewsDetailViewModel : MvxViewModel<Models.News>
	{
		private Models.News _news;
		private MvxCommand _closePageCommand;
		private readonly IMvxNavigationService _navigationService;

		public NewsDetailViewModel(IMvxNavigationService navigationService)
		{
			_navigationService = navigationService;
		}

		public Models.News News
		{
			get => _news;
			private set => SetProperty(ref _news, value);
		}

		public MvxCommand ClosePageCommand
		{
			get
			{
				_closePageCommand = _closePageCommand ?? new MvxCommand(() =>
				{
					_navigationService.Close(this);
				});
				return _closePageCommand;
			}
		}

		public override void Prepare(Models.News parameter)
		{
			News = parameter;
		}
	}
}
