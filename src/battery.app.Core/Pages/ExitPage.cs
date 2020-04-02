using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using battery.app.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace battery.app.Core.Pages
{
	[MvxTabbedPagePresentation(Position = TabbedPosition.Tab, WrapInNavigationPage = false, Title = "Выход", Icon = "arrow_right")]
	public class ExitPage : MvxContentPage<ExitViewModel>
	{
		public ExitPage()
		{
			IconImageSource = "arrow_right";
			if (Device.iOS==Device.RuntimePlatform)
			{
				IconImageSource = "baseline_arrow_forward_black_24pt_1x.png";
			}

			var b = new Button
			{
				Text = "Welcome to Xamarin.Forms!"
			};
			b.SetBinding(Button.CommandProperty, new Binding("ExitCommand"));
			Content = new StackLayout
			{
				Children = {
					b
				}
			};
		}
	}
}