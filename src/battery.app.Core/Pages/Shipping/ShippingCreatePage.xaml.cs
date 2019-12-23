﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using battery.app.Core.ViewModels.Shipping;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace battery.app.Core.Pages.Shipping
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShippingCreatePage : MvxContentPage<ShippingCreateViewModel>
	{
		public ShippingCreatePage()
		{
			InitializeComponent();
		}
	}
}