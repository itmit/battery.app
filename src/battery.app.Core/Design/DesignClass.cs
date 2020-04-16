using Xamarin.Forms;

namespace battery.app.Core.Design
{
	static class DesignClass
	{
		#region Fields
		#region Fields Margin
		public static Thickness LayoutThickness;
		public static Thickness Margin;
		#endregion

		#region Fields Fonts
		public static string FontRegular, FontLight, FontMedium, FontBold;
		#endregion

		#region Fields Other
		public static Color Color;
		public static bool HasShadow;
		public static double Number, Width;
		#endregion
		#endregion


		#region Ctor
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
				Width = 20;
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
				Width = 40;
			}
		}
		#endregion

	}
}
