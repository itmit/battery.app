using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace battery.app.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailGoods : ContentPage
    {
        public DetailGoods()
        {
            InitializeComponent();
        }

        private void Close_modal(object sender, EventArgs e)
        {
            Navigation.PopModalAsync(false);
        }
    }
}