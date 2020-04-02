using System;
using battery.app.Core.ViewModels;
using battery.app.Core.Views;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;

namespace battery.app.Core.Pages
{

	[XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxTabbedPagePresentation(Position = TabbedPosition.Tab, WrapInNavigationPage = false, Title = "СКАНИРОВАНИЕ")]
    public partial class ScanningPage : MvxContentPage<ScanningViewModel>
    {
        public ScanningPage()
        {
            InitializeComponent();
        }
	}
}