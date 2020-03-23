using Xamarin.Forms;

namespace battery.app.Core.Design
{
	static class DesignClass
	{
		public static Thickness LayoutThickness;
		public static Color Color;
		public static bool HasShadow;
		public static double Number;

		#region Fields Font
		public static string FontRegular, FontLight, FontMedium, FontBold;
		#endregion



		static DesignClass()
		{
			if (Device.iOS == Device.RuntimePlatform)
			{
				LayoutThickness = new Thickness(0, 50, 0, 0);
				Color = Color.LightGray;
				HasShadow = false;
				FontRegular = "roboto_regular";
				FontBold = "Roboto-Bold";
				FontLight = "roboto_light";
				FontMedium = "roboto_medium";
			}
			else if (Device.Android == Device.RuntimePlatform)
			{
				HasShadow = true;
				Color = Color.FromHex("#ebebeb");
				FontRegular = "roboto_regular.ttf#roboto_regular";
				FontBold = "Roboto-Bold.ttf#Roboto-Bold";
				FontLight = "roboto_light.ttf#roboto_light";
				FontMedium = "roboto_medium.ttf#roboto_medium";
			}
		}
	}
}
