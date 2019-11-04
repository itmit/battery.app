using System.Threading.Tasks;
using battery.app.Core.Repositories;
using MvvmCross;
using MvvmCross.ViewModels;

namespace battery.app.Core.ViewModels
{
	public class ExitViewModel : MvxViewModel
	{
		public override Task Initialize()
		{
			var foo = Mvx.IoCProvider.Create<IUserRepository>();

			return base.Initialize();
		}
	}
}
