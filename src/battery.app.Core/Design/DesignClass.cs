using battery.app.Core.Models;
using battery.app.Core.Services;
using MvvmCross;
using Xamarin.Forms;

namespace battery.app.Core.Design
{
	internal static class DesignClass
	{
		#region Data
		#region Static
		public static Color Color;
		public static readonly string FontBold;
		public static readonly string FontLight;
		public static readonly string FontMedium;
		public static readonly string FontRegular;
		public static readonly bool HasShadow;
		public static Thickness LayoutThickness;
		public static Thickness Margin;
		public static readonly double Width;
		#endregion
		#endregion

		#region .ctor
		static DesignClass()
		{
			if (Device.iOS == Device.RuntimePlatform)
			{
				LayoutThickness = new Thickness(0, 50, 0, 0);
				Margin = new Thickness(10, 50, 0, 10);
				Color = Color.LightGray;
				HasShadow = false;
				FontRegular = "roboto_regular";
				FontBold = "Roboto-Bold";
				FontLight = "roboto_light";
				FontMedium = "roboto_medium";
				Width = 15;
			}
			else if (Device.Android == Device.RuntimePlatform)
			{
				HasShadow = true;
				Color = Color.FromHex("#ebebeb");
				FontRegular = "roboto_regular.ttf#roboto_regular";
				FontBold = "Roboto-Bold.ttf#Roboto-Bold";
				FontLight = "roboto_light.ttf#roboto_light";
				FontMedium = "roboto_medium.ttf#roboto_medium";
				Margin = new Thickness(10, 30, 0, 10);
				Width = 20;
			}
		}
		#endregion

		public static string BackGroundImageSource
		{
			get
			{
				if (Mvx.IoCProvider.Resolve<IAuthService>().User.Role == UserRole.Dealer)
				{
					return "bg_red";
				}

				return "picture_600";
			}
		}
	}
}
