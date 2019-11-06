using System.Threading.Tasks;
using battery.app.Core.Services;
using ZXing.Mobile;

namespace battery.app.Droid.Services
{
	public class QrScanningService : IQrScanningService
	{
		public async Task<string> ScanAsync()
		{
			var optionsDefault = new MobileBarcodeScanningOptions();
			var optionsCustom = new MobileBarcodeScanningOptions();

			var scanner = new MobileBarcodeScanner()
			{
				TopText = "Scan the QR Code",
				BottomText = "Please Wait",
			};

			var scanResult = await scanner.Scan(optionsCustom);
			return scanResult.Text;
		}
	}
}
