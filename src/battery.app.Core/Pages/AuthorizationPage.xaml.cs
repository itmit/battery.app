using System;
using System.Diagnostics;
using battery.app.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace battery.app.Core.Pages
{
	/// <summary>
	/// Представляет страницу для авторизации.
	/// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
	[MvxContentPagePresentation(NoHistory = true)]
    public partial class AuthorizationPage : MvxContentPage<AuthorizationViewModel>
	{
		/// <summary>
		/// Инициализирует новый экземпляр <see cref="AuthorizationPage" />.
		/// </summary>
		public AuthorizationPage()
        {
			try
			{
				InitializeComponent();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
        }
    }
}