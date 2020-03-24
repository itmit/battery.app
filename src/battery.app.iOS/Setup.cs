using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using battery.app.Core;
using battery.app.Core.Services;
using battery.app.iOS.Services;
using Foundation;
using MvvmCross.Forms.Platforms.Ios.Core;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using UIKit;

namespace battery.app.iOS
{
	public class Setup : MvxFormsIosSetup
	{
		protected override IMvxApplication CreateApp() => new CoreApp();

		protected override Xamarin.Forms.Application CreateFormsApplication() => new App();

		protected override IMvxIoCProvider InitializeIoC()
		{
			var provider = base.InitializeIoC();
			provider.RegisterSingleton<ISettingsHelper>(new SettingsHelperIos());
			return provider;
		}
	}
}