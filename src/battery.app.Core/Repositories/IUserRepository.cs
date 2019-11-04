using System.Collections.Generic;
using battery.app.Models;
using battery.app.RealmObjects;

namespace battery.app.Core.Repositories
{
	public interface IUserRepository
	{
		void Add(User user);

		IEnumerable<User> All();

		void Remove(User user);

		void Update(User user);
	}
}
