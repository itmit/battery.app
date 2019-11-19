using MvvmCross.ViewModels;

namespace battery.app.Core.ViewModels
{
	public class DetailGoodsViewModel : MvxViewModel<string>
	{
		private string _goodsCode;

		public override void Prepare(string parameter)
		{
			_goodsCode = parameter;
		}
	}
}
