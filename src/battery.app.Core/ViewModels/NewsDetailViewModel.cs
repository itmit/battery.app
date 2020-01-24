using battery.app.Core.Models;
using MvvmCross.ViewModels;

namespace battery.app.Core.ViewModels
{
	public class NewsDetailViewModel : MvxViewModel<News>
	{
		private News _news;

		public News News
		{
			get => _news;
			private set => SetProperty(ref _news, value);
		}

		public override void Prepare(News parameter)
		{
			News = parameter;
		}
	}
}
