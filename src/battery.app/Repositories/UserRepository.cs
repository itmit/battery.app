using System.Collections.Generic;
using AutoMapper;
using battery.app.Models;
using battery.app.RealmObjects;
using Realms;

namespace battery.app.Repositories
{
	public class UserRepository
	{
		#region Data
		#region Fields
		private readonly IMapper _mapper;
		private RealmConfiguration _config;
		#endregion
		#endregion

		#region .ctor
		public UserRepository(RealmConfiguration config)
		{
			_config = config;

			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<User, UserRealmObject>();
				cfg.CreateMap<UserRealmObject, User>();

				cfg.CreateMap<AccessToken, AccessTokenRealmObject>();
				cfg.CreateMap<AccessTokenRealmObject, AccessToken>();
			}));
		}
		#endregion

		#region Public
		public void Add(User user)
		{
			using (var realm = RealmModel.GetInstance(_config))
			{
				var userRealm = _mapper.Map<UserRealmObject>(user);
				using (var transaction = realm.BeginWrite())
				{
					realm.Add(userRealm, true);
					transaction.Commit();
				}
			}
		}

		public IEnumerable<User> All()
		{
			using (var realm = Realm.GetInstance(_config))
			{
				var users = realm.All<UserRealmObject>();
				var userList = new List<User>();
				foreach (var user in users)
				{
					userList.Add(_mapper.Map<User>(user));
				}

				return userList;
			}
		}

		public void Remove(User user)
		{
			using (var realm = Realm.GetInstance(_config))
			{
				using (var transaction = realm.BeginWrite())
				{
					var userRealm = realm.Find<UserRealmObject>(user.Guid.ToString());
					realm.Remove(userRealm);
					transaction.Commit();
				}
			}
		}

		public void Update(User user)
		{
			Remove(user);
			Add(user);
		}
		#endregion
	}
}
