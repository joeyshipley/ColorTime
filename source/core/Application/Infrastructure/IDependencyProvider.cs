using System;
using System.Collections.Generic;

namespace Color.Core.Application.Infrastructure
{
	public interface IDependencyProvider
	{
		object Get(Type type);
		T Get<T>() where T : class;
		IList<T> GetAll<T>() where T : class;
	}
}