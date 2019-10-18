using System;
using battery.app.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace battery.app.Pages
{
	/// <summary>
	/// Представляет страницу для авторизации.
	/// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthorizationPage : ContentPage
    {
		/// <summary>
		/// Инициализирует новый экземпляр <see cref="AuthorizationPage" />.
		/// </summary>
		public AuthorizationPage()
        {
            InitializeComponent();
        }
    }
}