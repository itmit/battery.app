using System.Threading.Tasks;
using Realms;

namespace battery.app
{
	public static class RealmModel
	{
		public static RealmConfiguration RealmDefaultConfiguration
		{
			get
			{
				var configuration = RealmConfiguration.DefaultConfiguration;
				configuration.SchemaVersion = 1;
				return (RealmConfiguration)configuration;
			}
		}

		public static Realms.Realm GetInstance() => GetInstance("");

		public static Realms.Realm GetInstance(string databasePath) => GetInstance(new RealmConfiguration(databasePath));

		public static Realms.Realm GetInstance(RealmConfigurationBase config) => Realms.Realm.GetInstance(config);

		public static Task<Realms.Realm> GetInstanceAsync(RealmConfigurationBase config) => Realms.Realm.GetInstanceAsync(config);
	}
}
