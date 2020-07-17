using System;
using System.Collections.Generic;
using battery.app.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace battery.app.Core.Pages
{
	[MvxModalPresentation(Animated = true, WrapInNavigationPage = false)]
	public class ScannerPage : ZXingScannerPage, IMvxPage<ScannerViewModel>
	{
		public ScannerPage()
			: base(new MobileBarcodeScanningOptions
			{
				CameraResolutionSelector = SelectLowestResolutionMatchingDisplayAspectRatio
			})
		{
		}

		private static CameraResolution SelectLowestResolutionMatchingDisplayAspectRatio(List<CameraResolution> availableResolutions)
		{
			CameraResolution result = null;
			//a tolerance of 0.1 should not be recognizable for users
			double aspectTolerance = 0.2;
			//calculating our targetRatio
			var targetRatio = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Width;
			var targetHeight = DeviceDisplay.MainDisplayInfo.Height;
			var minDiff = double.MaxValue;
			//camera API lists all available resolutions from highest to lowest, perfect for us
			//making use of this sorting, following code runs some comparisons to select the lowest resolution that matches the screen aspect ratio
			//selecting the lowest makes QR detection actual faster most of the time
			foreach (var r in availableResolutions)
			{
				//if current ratio is bigger than our tolerance, move on
				//camera resolution is provided landscape ...
				var a = Math.Abs((double) r.Width / r.Height - targetRatio);
				if (a > aspectTolerance)
				{
					continue;
				}

				if (Math.Abs(r.Height - targetHeight) < minDiff)
				{
					minDiff = Math.Abs(r.Height - targetHeight);
				}

				result = r;
			}
			return result;
		}
		/// <summary>Application developers can override this method to provide behavior when the back button is pressed.</summary>
		/// <returns>To be added.</returns>
		/// <remarks>To be added.</remarks>
		protected override bool OnBackButtonPressed()
		{
			Navigation.PopModalAsync();
			return true;
		}

		/// <summary>When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.</summary>
		/// <remarks>To be added.</remarks>
		protected override void OnAppearing()
		{
			base.OnAppearing();

			ViewModel?.ViewAppearing();
			ViewModel?.ViewAppeared();

			NavigationPage.SetHasNavigationBar(this, false);
			if (ViewModel != null)
			{
				OnScanResult += ViewModel.OnScanResult;
			}
		}

		private static void ViewModelChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			if (newvalue == null)
			{
				return;
			}

			if (bindable is IMvxElement element)
			{
				element.DataContext = newvalue;
			}
			else
			{
				bindable.BindingContext = newvalue;
			}
		}

		public static readonly BindableProperty ViewModelProperty = BindableProperty.Create(nameof(ViewModel), typeof(IMvxViewModel), typeof(IMvxElement), default(MvxViewModel), BindingMode.Default, null, ViewModelChanged);

		public object DataContext
		{
			get => BindingContext.DataContext;
			set
			{
				if (value != null && !(BindingContext != null && ReferenceEquals(DataContext, value)))
				{
					BindingContext = new MvxBindingContext(value);
				}
			}
		}

		IMvxViewModel IMvxView.ViewModel
		{
			get => ViewModel;
			set => ViewModel = value as ScannerViewModel;
		}

		public new IMvxBindingContext BindingContext
		{
			get;
			set;
		}

		public ScannerViewModel ViewModel
		{
			get;
			set;
		}
	}
}
