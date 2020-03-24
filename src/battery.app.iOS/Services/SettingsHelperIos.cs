using battery.app.Core.Services;
using Foundation;
using UIKit;

namespace battery.app.iOS.Services
{
	public class SettingsHelperIos : ISettingsHelper
	{
		public void OpenAppSettings()
		{
			UIApplication.SharedApplication.OpenUrl(new NSUrl("app-settings:"));
		}
	}
}
