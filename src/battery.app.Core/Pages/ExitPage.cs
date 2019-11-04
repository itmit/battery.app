using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using battery.app.Core.ViewModels;
using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace battery.app.Core.Pages
{
	public class ExitPage : MvxContentPage<ExitViewModel>
	{
		public ExitPage()
		{
			Content = new StackLayout
			{
				Children = {
					new Label { Text = "Welcome to Xamarin.Forms!" }
				}
			};
		}
	}
}