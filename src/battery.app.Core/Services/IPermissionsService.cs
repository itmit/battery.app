using System.Threading.Tasks;
using Plugin.Permissions.Abstractions;

namespace battery.app.Core.Services
{
	public interface IPermissionsService
	{
		Task<bool> CheckPermission(Permission permission, string  message);
	}
}
