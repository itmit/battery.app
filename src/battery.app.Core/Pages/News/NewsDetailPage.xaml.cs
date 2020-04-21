using battery.app.Core.ViewModels;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace battery.app.Core.Pages.News
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewsDetailPage : MvxContentPage<NewsDetailViewModel>
	{
		public NewsDetailPage()
		{
			InitializeComponent();
		}
	}
}