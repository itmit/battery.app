﻿using System;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Forms.Presenters;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace battery.app.Core
{
	public class CustomMvxFormsPagePresenter : MvxFormsPagePresenter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MvvmCross.Forms.Views.MvxFormsPagePresenter" /> class.
		/// </summary>
		/// <param name="platformPresenter">The native platform presenter from where the MvxFormsPagePresenter is created</param>
		public CustomMvxFormsPagePresenter(IMvxFormsViewPresenter platformPresenter)
			: base(platformPresenter)
		{
		}

		public override TPage GetPageOfType<TPage>(Xamarin.Forms.Page rootPage = null)
		{
			if (rootPage == null)
			{
				rootPage = FormsApplication.MainPage;
			}

			if (rootPage is TPage page)
			{
				return page;
			}

			if (rootPage is TabbedPage tabbedPage)
			{
				var currentPage = GetPageOfType<TPage>(tabbedPage.CurrentPage);
				return currentPage;
			}

			if (rootPage is NavigationPage navigationRootPage)
			{
				if (navigationRootPage.RootPage is TabbedPage tabbedNestedPage)
				{
					return GetPageOfType<TPage>(tabbedNestedPage);
				}
			}

			return base.GetPageOfType<TPage>(rootPage);
		}

		public override Task<bool> CloseContentPage(IMvxViewModel viewModel, MvxContentPagePresentationAttribute attribute) 
		{
			if ((FormsApplication.MainPage as TabbedPage)?.CurrentPage is NavigationPage root)
			{
				return FindAndCloseViewFromViewModel(viewModel, root, attribute);
			}
			return base.CloseContentPage(viewModel, attribute);
		}

		public override NavigationPage TopNavigationPage(Xamarin.Forms.Page rootPage = null)
		{
			if (rootPage == null)
			{
				rootPage = FormsApplication.MainPage;
			}

			if (rootPage is TabbedPage tabbedPage)
			{
				if (tabbedPage.CurrentPage != null)
				{
					var navTabbedPage = TopNavigationPage(tabbedPage.CurrentPage);
					if (navTabbedPage != null)
					{
						return navTabbedPage;
					}
				}
			}

			return base.TopNavigationPage(rootPage);
		}

		public override async Task<bool> ShowTabbedPage(Type view, MvxTabbedPagePresentationAttribute attribute, MvxViewModelRequest request)
		{
			if (attribute.Position == TabbedPosition.Tab)
			{
				var page = await CloseAndCreatePage(view, request, attribute);
				var tabHost = GetPageOfType<TabbedPage>();

				if (tabHost == null)
				{
					tabHost = new TabbedPage();
					await PushOrReplacePage(FormsApplication.MainPage, tabHost, attribute);
				}

				if (tabHost.Children.Any(p => p.GetType() == page.GetType() || (p as NavigationPage)?.RootPage.GetType() == page.GetType()))
				{
					return true;
				}

				if (attribute.WrapInNavigationPage)
				{
					tabHost.Children.Add(new NavigationPage(page)
					{
						Title = page.Title,
						IconImageSource = page.IconImageSource
					});
					return true;
				}

				tabHost.Children.Add(page);
				return true;
			}

			return await base.ShowTabbedPage(view, attribute, request);
		}
	}
}
