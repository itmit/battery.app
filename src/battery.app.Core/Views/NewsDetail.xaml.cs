using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace battery.app.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsDetail : ContentPage
    {
        public NewsDetail()
        {
            InitializeComponent();
        }

        private void Close_modal(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}