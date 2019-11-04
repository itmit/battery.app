using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using battery.app.Core;
using battery.app.Core.Pages;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Platforms.Android.Core;
using MvvmCross.Platforms.Android.Presenters;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace battery.app.Droid
{
	public class Setup : MvxFormsAndroidSetup<CoreApp, App>
	{
		protected override IMvxApplication CreateApp()
		{
			return new CoreApp();
		}

		protected override Application CreateFormsApplication()
		{
			return new App();
		}
	}
}
