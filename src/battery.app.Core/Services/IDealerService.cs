using System.Collections.Generic;
using System.Threading.Tasks;
using battery.app.Core.Models;

namespace battery.app.Core.Services
{
	public interface IDealerService
	{
		Task<IEnumerable<Dealer>> GetDealers();
	}
}
