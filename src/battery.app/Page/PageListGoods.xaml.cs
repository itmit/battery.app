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
    public partial class PageListGoods : ContentPage
    {
        public PageListGoods()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ShipmentPage());
        }
    }
}