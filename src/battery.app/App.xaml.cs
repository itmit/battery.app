using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using battery.app.Views;
using battery.app.Page;

namespace battery.app
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MainPage = new Authorization();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
