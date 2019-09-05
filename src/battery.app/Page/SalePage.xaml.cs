using battery.app.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace battery.app.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SalePage : ContentPage
    {
        public SalePage()
        {
            InitializeComponent();
        }
        private void Close_modal(object sender, EventArgs e)
        {
            Navigation.PopAsync(false);
        }
        private void Retry_goods(object sender, EventArgs e)
        {
            Navigation.PopAsync(false);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DetailGoods(), false);
        }
    }
}