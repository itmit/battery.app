using System;
using System.Threading.Tasks;
using battery.app.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace battery.app.Core.ViewModels.News
{
	/// <summary>
	/// Представляет модель представления для страницы новостей.
	/// </summary>
	public class NewsViewModel : MvxNavigationViewModel
	{
		private INewsService _newsService;
		private MvxObservableCollection<Models.News> _news;
		private Models.News _selectedNews;
		private MvxCommand _refreshCommand;
		private bool _isRefreshing;

		public NewsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, INewsService newsService)
			: base(logProvider, navigationService)
		{
			_newsService = newsService;
		}

		public MvxObservableCollection<Models.News> News
		{
			get => _news;
			private set => SetProperty(ref _news, value);
		}

		public Models.News SelectedNews
		{
			get => _selectedNews;
			set
			{
				if (value == null)
				{
					return;

				}

				SetProperty(ref _selectedNews, value);

				NavigationService.Navigate<NewsDetailViewModel, Models.News>(value);
			}
		}

		public MvxCommand RefreshCommand
		{
			get
			{
				_refreshCommand = _refreshCommand ?? new MvxCommand(async () =>
				{
					IsRefreshing = true;
					try
					{
						News = new MvxObservableCollection<Models.News>(await _newsService.GetNews());

					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}
					IsRefreshing = false;
				});
				return _refreshCommand;
			}
		}

		public bool IsRefreshing
		{
			get => _isRefreshing;
			private set => SetProperty(ref _isRefreshing, value);
		}

		public override async Task Initialize()
		{
			await base.Initialize();
			try
			{
				News = new MvxObservableCollection<Models.News>(await _newsService.GetNews());

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}
