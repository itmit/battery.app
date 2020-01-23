using System.Collections.Generic;
using System.Threading.Tasks;
using battery.app.Core.Models;

namespace battery.app.Core.Services
{
	public interface INewsService
	{
		Task<List<News>> GetNews(int limit = 100, int offset = 0);
	}
}
