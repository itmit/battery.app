using System;
using battery.app.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace battery.app.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanningPage : ContentPage
    {
        public ScanningPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DetailGoods(), false);
        }
    }
}