using battery.app.Core;
using battery.app.Core.Services;
using battery.app.Droid.Services;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace battery.app.Droid
{
	public class Setup : MvxFormsAndroidSetup<CoreApp, App>
	{
		protected override IMvxApplication CreateApp() => new CoreApp();

		protected override Application CreateFormsApplication() => new App();

		protected override IMvxIoCProvider InitializeIoC()
		{
			var provider = base.InitializeIoC();
			provider.RegisterSingleton<ISettingsHelper>(new SettingsHelperDroid());
			return provider;
		}
	}
}
