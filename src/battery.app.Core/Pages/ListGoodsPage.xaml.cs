using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace battery.app.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListGoodsPage : ContentPage
    {
        public ListGoodsPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ShipmentPage());
        }
    }
}