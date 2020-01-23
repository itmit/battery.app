using System.Threading.Tasks;
using battery.app.Core.Models;
using battery.app.Core.Services;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace battery.app.Core.ViewModels
{
	/// <summary>
	/// Представляет модель представления для страницы новостей.
	/// </summary>
	public class NewsViewModel : MvxNavigationViewModel
	{
		private INewsService _newsService;
		private MvxObservableCollection<News> _news;
		private News _selectedNews;

		public NewsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, INewsService newsService)
			: base(logProvider, navigationService)
		{
			_newsService = newsService;
		}

		public MvxObservableCollection<News> News
		{
			get => _news;
			private set => SetProperty(ref _news, value);
		}

		public News SelectedNews
		{
			get => _selectedNews;
			set
			{
				if (value != null && SetProperty(ref _selectedNews, value))
				{
					// NavigationService.Navigate()
				}
			}
		}

		public override async Task Initialize()
		{
			await base.Initialize();

			News = new MvxObservableCollection<News>(await _newsService.GetNews());
		}
	}
}
