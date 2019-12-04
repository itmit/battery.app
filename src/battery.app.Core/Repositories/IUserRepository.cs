using System.Collections.Generic;
using battery.app.Core.Models;

namespace battery.app.Core.Repositories
{
	public interface IUserRepository
	{
		void Add(User user);

		IEnumerable<User> GetUsers();

		void Remove(User user);

		void Update(User user);
	}
}
