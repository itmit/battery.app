using System.Linq;
using battery.app.Core.Pages;
using battery.app.Core.Repositories;
using battery.app.Core.ViewModels;
using battery.app.Models;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

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

			UserRepository userRepository = new UserRepository(RealmModel.RealmDefaultConfiguration);

			User user = userRepository.All().SingleOrDefault();

			if (user == null)
			{
				RegisterAppStart<AuthorizationViewModel>();
				return;
			}

			RegisterAppStart<MainViewModel>();
		}
	}
}
