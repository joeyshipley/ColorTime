using System.Configuration;

namespace Color.Core.Application.Infrastructure
{
	public class ConfigManagerSettingsProvider : ISettingsProvider
	{
		public T Get<T>(string key)
		{
			return Get(key, default(T));
		}

		public T Get<T>(string key, T defaultValue)
		{
			var value = ConfigurationManager.AppSettings[key];
			return string.IsNullOrEmpty(value)
				? defaultValue
				: (T) System.Convert.ChangeType(value, typeof(T));
		}
	}
}