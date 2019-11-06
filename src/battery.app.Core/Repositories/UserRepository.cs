﻿using System.Collections.Generic;
using AutoMapper;
using battery.app.Core.Models;
using battery.app.Core.RealmObjects;
using Realms;

namespace battery.app.Core.Repositories
{
	public class UserRepository : IUserRepository
	{
		#region Data
		#region Fields
		private readonly IMapper _mapper;
		#endregion
		#endregion

		#region .ctor
		public UserRepository()
		{
			_mapper = new Mapper(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<AccessToken, AccessTokenRealmObject>();
				cfg.CreateMap<AccessTokenRealmObject, AccessToken>();

				cfg.CreateMap<User, UserRealmObject>()
				   .ForPath(m => m.AccessToken.Body, o => o.MapFrom(q => q.AccessToken.Body))
				   .ForPath(m => m.AccessToken.Type, o => o.MapFrom(q => q.AccessToken.Type))
				   .ForPath(m => m.AccessToken.ExpiresAt, o => o.MapFrom(q => q.AccessToken.ExpiresAt));
				cfg.CreateMap<UserRealmObject, User>()
				   .ForPath(m => m.AccessToken.Body, o => o.MapFrom(q => q.AccessToken.Body))
				   .ForPath(m => m.AccessToken.Type, o => o.MapFrom(q => q.AccessToken.Type))
				   .ForPath(m => m.AccessToken.ExpiresAt, o => o.MapFrom(q => q.AccessToken.ExpiresAt.DateTime));

			}));
		}
		#endregion

		#region Public
		public void Add(User user)
		{
			using (var realm = RealmModel.GetInstance(RealmModel.Configuration))
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
			using (var realm = Realm.GetInstance(RealmModel.Configuration))
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
			using (var realm = Realm.GetInstance(RealmModel.Configuration))
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
