﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using MvvmCross.Forms.Platforms.Android.Views;

namespace battery.app.Droid
{
	[Activity(Label = "Батарейки"
		, Icon = "@drawable/icon"
		, Theme = "@style/MainTheme"
		, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class FormsActivity : MvxFormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			ZXing.Net.Mobile.Forms.Android.Platform.Init();
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
		{
			global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
	}
}