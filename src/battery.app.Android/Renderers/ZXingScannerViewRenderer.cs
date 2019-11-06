using System;
using System.ComponentModel;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using battery.app.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ZXing.Mobile;
using ZXing.Net.Mobile.Android;
using ZXing.Net.Mobile.Forms;

[assembly: ExportRenderer(typeof(ZXingScannerView), typeof(ZXingScannerViewRenderer))]
namespace battery.app.Droid.Renderers
{
	[Preserve(AllMembers = true)]
	public class ZXingScannerViewRenderer : ViewRenderer<ZXingScannerView, ZXingSurfaceView>
	{
		#region Data
		#region Fields
		protected ZXingScannerView FormsView
		{
			get;
			set;
		}

		protected ZXingSurfaceView ZxingSurface
		{
			get;
			set;
		}
		private readonly Context _appContext;
		#endregion
		#endregion

		#region .ctor
		public ZXingScannerViewRenderer(Context context)
			: base(context) =>
			_appContext = context;
		#endregion


		#region Overrided
		public override bool OnTouchEvent(MotionEvent e)
		{
			var x = e.GetX();
			var y = e.GetY();
			if (ZxingSurface != null)
			{
				ZxingSurface.AutoFocus((int) x, (int) y);
				System.Diagnostics.Debug.WriteLine("Touch: x={0}, y={1}", x, y);
			}

			return base.OnTouchEvent(e);
		}

		protected override async void OnElementChanged(ElementChangedEventArgs<ZXingScannerView> e)
		{
			base.OnElementChanged(e);
			FormsView = Element;
			if (ZxingSurface == null)
			{
				// Process requests for autofocus
				FormsView.AutoFocusRequested += (x, y) =>
				{
					if (ZxingSurface != null)
					{
						if (x < 0 && y < 0)
						{
							ZxingSurface.AutoFocus();
						}
						else
						{
							ZxingSurface.AutoFocus(x, y);
						}
					}
				};

				if (_appContext is Activity activity)
				{
					await PermissionsHandler.RequestPermissionsAsync(activity);
				}

				ZxingSurface = new ZXingSurfaceView(_appContext, FormsView.Options)
				{
					LayoutParameters = new LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent)
				};

				SetNativeControl(ZxingSurface);

				if (FormsView.IsScanning)
				{
					ZxingSurface.StartScanning(FormsView.RaiseScanResult, FormsView.Options);
				}

				if (!FormsView.IsAnalyzing)
				{
					ZxingSurface.PauseAnalysis();
				}

				if (FormsView.IsTorchOn)
				{
					ZxingSurface.Torch(true);
				}
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			if (ZxingSurface == null)
			{
				return;
			}

			switch (e.PropertyName)
			{
				case nameof(ZXingScannerView.IsTorchOn):
					ZxingSurface.Torch(FormsView.IsTorchOn);
					break;
				case nameof(ZXingScannerView.IsScanning):
					if (FormsView.IsScanning)
					{
						ZxingSurface.StartScanning(FormsView.RaiseScanResult, FormsView.Options);
					}
					else
					{
						ZxingSurface.StopScanning();
					}

					break;
				case nameof(ZXingScannerView.IsAnalyzing):
					if (FormsView.IsAnalyzing)
					{
						ZxingSurface.ResumeAnalysis();
					}
					else
					{
						ZxingSurface.PauseAnalysis();
					}

					break;
			}
		}
		#endregion
	}
}
