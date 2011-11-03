using System;
using System.Reflection;
using Color.Core.Domain.Infrastructure;

namespace Color.Tests.Helpers
{
	public static class ReflectionHelper
	{
		public static void SetId(this BaseEntity entity, Guid id)
		{
			entity.SetFieldValue("_id", id);
		}

		public static void SetFieldValue<T>(this object obj, string name, T value)
		{
			var fieldInfo = obj.GetType().GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);
			fieldInfo.SetValue(obj, value);
		}
	}
}