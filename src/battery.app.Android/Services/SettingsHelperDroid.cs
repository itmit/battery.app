using Android;
using Android.App;
using Android.Content;
using battery.app.Core.Services;

namespace battery.app.Droid.Services
{
	public class SettingsHelperDroid : ISettingsHelper
	{
		private const string PackageName = "itmit.battery.app";

		public void OpenAppSettings()
		{
			var intent = new Intent(Android.Provider.Settings.ActionApplicationDetailsSettings);
			intent.AddFlags(ActivityFlags.NewTask);
			var uri = Android.Net.Uri.FromParts("package", PackageName, null);
			intent.SetData(uri);
			Application.Context.StartActivity(intent);
		}
	}
}
