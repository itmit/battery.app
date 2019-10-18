﻿using System;
using battery.app.Models;
using Realms;

namespace battery.app.RealmObjects
{
	public class UserRealmObject : RealmObject
	{
		[PrimaryKey]
		public string Guid
		{
			get;
			set;
		} = System.Guid.NewGuid()
				.ToString();

		public string Login
		{
			get;
			set;
		}

		public string Role
		{
			get;
			set;
		}

		public int Dealer
		{
			get;
			set;
		}

		public AccessTokenRealmObject AccessToken
		{
			get;
			set;
		}
	}
}
