using System.Collections.Generic;
using System.Threading.Tasks;
using battery.app.Core.Models;

namespace battery.app.Core.Services
{
	/// <summary>
	/// Представляет сервис для получения дилеров.
	/// </summary>
	public interface IDealerService
	{
		/// <summary>
		/// Получает список дилеров.
		/// </summary>
		/// <returns>Список дилеров.</returns>
		Task<List<Dealer>> GetAll();
	}
}
