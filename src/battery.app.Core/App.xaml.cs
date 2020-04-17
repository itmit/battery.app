using System.Linq;
using battery.app.Core.Pages;
using battery.app.Core.Repositories;
using battery.app.Core.ViewModels;
using MvvmCross.Forms.Core;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Application = Xamarin.Forms.Application;

namespace battery.app.Core
{
	/// <summary>
	/// Представляет общее приложения для разных платформ.
	/// </summary>
    public partial class App : MvxFormsApplication
	{
		/// <summary>
		/// Инициализирует новый экземпляр <see cref="App" />.
		/// </summary>
		public App()
        {
            InitializeComponent();

			On<Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
		}
    }
}
