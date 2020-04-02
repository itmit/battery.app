using battery.app.Core;
using battery.app.Core.Services;
using battery.app.Droid.Services;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Presenters;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace battery.app.Droid
{
	public class Setup : MvxFormsAndroidSetup<CoreApp, App>
	{
		protected override IMvxApplication CreateApp() => new CoreApp();

		protected override Application CreateFormsApplication() => new App();

		protected override IMvxFormsPagePresenter CreateFormsPagePresenter(IMvxFormsViewPresenter viewPresenter)
		{
			var formsPagePresenter = new CustomMvxFormsPagePresenter(viewPresenter);
			Mvx.IoCProvider.RegisterSingleton<IMvxFormsPagePresenter>(formsPagePresenter);
			Mvx.IoCProvider.RegisterSingleton<ISettingsHelper>(new SettingsHelperDroid());
			return formsPagePresenter;
		}
	}
}
