using System;
using battery.app.Controls;
using battery.app.Models;
using Xamarin.Forms;
using battery.app.PageModels;
using battery.app.Pages;
using battery.app.Views;
using FreshMvvm;
using Realms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Application = Xamarin.Forms.Application;
using TabBar = Xamarin.Forms.PlatformConfiguration;

namespace battery.app
{
	/// <summary>
	/// Представляет общее приложения для разных платформ.
	/// </summary>
    public partial class App : Application
    {
		/// <summary>
		/// Возвращает текущее приложение.
		/// </summary>
		public new static App Current => Application.Current as App;

		/// <summary>
		/// Возвращает название контейнера навигации, для авторизации.
		/// </summary>
		private const string AuthenticationContainer = "AuthenticationContainer";

		/// <summary>
		/// Возвращает название контейнера навигации, для авторизации.
		/// </summary>
		private const string MainContainer = "AuthenticationContainer";

		/// <summary>
		/// Инициализирует новый экземпляр <see cref="App" />.
		/// </summary>
		public App()
        {
            InitializeComponent();

			var loginContainer = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<AuthorizationPageModel>(), AuthenticationContainer);


			MainPage = loginContainer;
        }

		public Page GetMainNavigationPage(UserRole role)
		{
			var tabbedNavigation = new MainNavigationPage(MainContainer);

			tabbedNavigation.AddTab<ScanningPageModel>(null, "qrcode");
			if (role == UserRole.Stockman)
			{
				tabbedNavigation.AddTab<ListGoodsPageModel>(null, "plus");
				tabbedNavigation.AddTab<ShippingListPageModel>(null, "list_ul");
			}
			tabbedNavigation.AddTab<NewsPageModel>(null, "newspaper");
			tabbedNavigation.AddTab<NewsPageModel>(null, "arrow_right");

			tabbedNavigation.Effects.Add(new NoShiftEffect());
			tabbedNavigation.On<TabBar.Android>()
							.SetToolbarPlacement(ToolbarPlacement.Bottom);
			tabbedNavigation.On<TabBar.Android>()
							.SetIsSwipePagingEnabled(false);
			tabbedNavigation.BarBackgroundColor = Color.Transparent;
			tabbedNavigation.SelectedTabColor = Color.Red;
			tabbedNavigation.UnselectedTabColor = Color.Gray;

			return tabbedNavigation;
		}

		/// <summary>
		/// Вызывается при запуске приложения.
		/// </summary>
		protected override void OnStart()
        {
            // Handle when your app starts
        }

		/// <summary>
		/// Вызывается при сворачивании приложения.
		/// </summary>
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

		/// <summary>
		/// Вызывается при возобновлении работы приложения.
		/// </summary>
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
