using System.Net.Http;

namespace battery.app.Services
{
	/// <summary>
	/// Представляет базовый сервис.
	/// </summary>
	public abstract class BaseService
	{
		/// <summary>
		/// Инициализирует новый экземпляр <see cref="BaseService" />.
		/// </summary>
		/// <param name="httpClientHandler">Обработчик сообщения по умолчанию, для <see cref="HttpClient"/>.</param>
		public BaseService(HttpClientHandler httpClientHandler) => HttpClientHandler = httpClientHandler;

		/// <summary>
		/// Возвращает обработчик сообщения по умолчанию, для <see cref="HttpClient"/>.
		/// </summary>
		protected HttpClientHandler HttpClientHandler
		{
			get;
		}
	}
}
