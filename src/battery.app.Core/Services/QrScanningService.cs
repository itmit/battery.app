using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Mobile;

namespace battery.app.Core.Services
{
	public class QrScanningService : IQrScanningService
	{
		public async Task<string> ScanAsync()
		{
			var scanner = new MobileBarcodeScanner();

			var scanResult = await scanner.Scan();
			return scanResult.Text;
		}
	}
}
