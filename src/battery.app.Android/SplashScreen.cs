using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.OS;
using battery.app.Core;
using MvvmCross.Forms.Platforms.Android.Views;
using MvvmCross.Platforms.Android.Views;

namespace battery.app.Droid
{
	[Activity(
		Label = "Батарейки"
		, MainLauncher = true
		, Icon = "@drawable/icon"
		, Theme = "@style/Theme.Splash"
		, NoHistory = true
		, ScreenOrientation = ScreenOrientation.Portrait)]
	public class SplashScreen : MvxFormsSplashScreenActivity<Setup, CoreApp, App>
	{
		public SplashScreen()
			: base(Resource.Layout.SplashScreen)
		{
		}

		protected override Task RunAppStartAsync(Bundle bundle)
		{
			StartActivity(typeof(FormsActivity));
			return base.RunAppStartAsync(bundle);
		}
	}
}
