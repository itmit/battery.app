using System;
using battery.app.Core.ViewModels;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace battery.app.Core.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShipmentPage : MvxContentPage<ShipmentViewModel>
    {
        public ShipmentPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SalePage(), false);
        }
        private void Close_modal(object sender, EventArgs e)
        {
            Navigation.PopAsync(false);
        }
    }
}