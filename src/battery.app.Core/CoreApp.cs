using System.Linq;
using battery.app.Core.Models;
using battery.app.Core.Pages;
using battery.app.Core.Repositories;
using battery.app.Core.Services;
using battery.app.Core.ViewModels;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Realms;

namespace battery.app.Core
{
	public class CoreApp : MvxApplication
	{
		public override void Initialize()
		{
			CreatableTypes()
				.EndingWith("Service")
				.AsInterfaces()
				.RegisterAsLazySingleton();

			CreatableTypes()
				.InNamespace("battery.app.Core.Repositories")
				.EndingWith("Repository")
				.AsInterfaces()
				.RegisterAsDynamic();

			var userRepository = Mvx.IoCProvider.Resolve<IUserRepository>();

			User user = userRepository.GetUsers().SingleOrDefault();
			
			if (user?.AccessToken == null)
			{
				RegisterAppStart<AuthorizationViewModel>();
				return;
			}
			Mvx.IoCProvider.RegisterSingleton<IDealerService>(new DealerService(user.AccessToken));

			RegisterAppStart<MainViewModel>();
		}
	}
}
