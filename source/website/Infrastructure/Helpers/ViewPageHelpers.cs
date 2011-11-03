using System.Web.Script.Serialization;

namespace Color.Website.Infrastructure.Helpers
{
	public static class ViewPageHelpers
	{
		public static string ToJavascriptEncode(this bool original)
		{
			return new JavaScriptSerializer().Serialize(original);
		}

		public static string ToJavascriptEncode(this string original)
		{
			return new JavaScriptSerializer().Serialize(original ?? string.Empty);
		}
	}
}