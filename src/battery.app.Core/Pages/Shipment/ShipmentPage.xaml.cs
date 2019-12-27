﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using battery.app.Core.ViewModels.ShipmentViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace battery.app.Core.Pages.Shipment
{
	[MvxTabbedPagePresentation(WrapInNavigationPage = false)]
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShipmentPage : MvxContentPage<ShipmentViewModel>
	{
		public ShipmentPage()
		{
			InitializeComponent();
		}
	}
}