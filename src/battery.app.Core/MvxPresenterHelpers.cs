using System;
using System.Linq;
using System.Reflection;
using MvvmCross;
using MvvmCross.Exceptions;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace battery.app.Core
{
	public static class MvxPresenterHelpers
	{
		public static IMvxViewModel LoadViewModel(MvxViewModelRequest request)
		{
			IMvxViewModelLoader viewModelLoader = Mvx.IoCProvider.Resolve<IMvxViewModelLoader>();
			var viewModel = viewModelLoader.LoadViewModel(request, null);
			return viewModel;
		}

		public static Page CreatePage(MvxViewModelRequest request)
		{
			var viewModelName = request.ViewModelType.Name;
			var pageName = viewModelName.Replace("ViewModel", "Page");
			var pageType = request.ViewModelType.GetTypeInfo().Assembly.CreatableTypes()
								  .FirstOrDefault(t => t.Name == pageName);

			if (pageType == null)
			{
				
				//Mvx.Trace("Page not found for {0}", pageName);
				return null;
			}

			var page = Activator.CreateInstance(pageType);
			var page1 = page as Page;
			if (page1 == null)
			{
				throw new MvxException("Failed to create Page {0}", pageName); 
			}
			return page1;
		}
	}
}
