﻿using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Cryptography;
using battery.app.Core.Controls;
using battery.app.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using TabbedPage = Xamarin.Forms.TabbedPage;

namespace battery.app.Core.Pages
{
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible(false)]
	[MvxTabbedPagePresentation(TabbedPosition.Root, NoHistory = true, WrapInNavigationPage = true)]
    public partial class MainPage : MvxTabbedPage<MainViewModel>
	{
        public MainPage()
        {
			InitializeComponent();
			Effects.Add(new NoShiftEffect());
			On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
			On<Android>()
				.DisableSwipePaging();
		}

		/// <summary>Raises the <see cref="E:Xamarin.Forms.MultiPage`1.CurrentPageChanged" /> event.</summary>
		/// <remarks>To be added.</remarks>
		protected override void OnCurrentPageChanged()
		{
			base.OnCurrentPageChanged();
			if (CurrentPage is ExitPage exitPage)
			{
				exitPage.ViewModel.DoExit();
			}
		}
	}
}