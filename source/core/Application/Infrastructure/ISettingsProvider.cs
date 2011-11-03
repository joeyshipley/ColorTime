namespace Color.Core.Application.Infrastructure
{
	public interface ISettingsProvider
	{
		T Get<T>(string key);
		T Get<T>(string key, T defaultValue);
	}
}