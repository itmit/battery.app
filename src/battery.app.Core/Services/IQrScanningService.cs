using System.Threading.Tasks;

namespace battery.app.Core.Services
{
	public interface IQrScanningService
	{
		Task<string> ScanAsync();
	}
}
