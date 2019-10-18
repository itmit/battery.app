﻿using System;
using battery.app.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace battery.app.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsPage : ContentPage
    {
        public NewsPage()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewsDetail());
        }
    }
}