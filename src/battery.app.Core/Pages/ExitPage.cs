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
	[MvxTabbedPagePresentation(Position = TabbedPosition.Tab, WrapInNavigationPage = false, Icon = "arrow_right")]
	public class ExitPage : MvxContentPage<ExitViewModel>
	{
		public ExitPage()
		{
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