using System;
using battery.app.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace battery.app.Core.Pages
{
	[MvxTabbedPagePresentation(WrapInNavigationPage = false)]
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListGoodsPage : MvxContentPage<ListGoodsViewModel>
    {
        public ListGoodsPage()
        {
            InitializeComponent();
        }
    }
}