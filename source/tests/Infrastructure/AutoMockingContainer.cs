using System;
using System.Collections.Generic;
using Color.Core.Application.Infrastructure;
using StructureMap.AutoMocking;

namespace Color.Tests.Infrastructure
{
	public class AutoMockingContainer<Target> : RhinoAutoMocker<Target>, IDependencyProvider
		where Target : class
	{
		public AutoMockingContainer(MockMode mode)
			: base(mode)
		{}

		T IDependencyProvider.Get<T>()
		{
			return Get<T>();
		}

		object IDependencyProvider.Get(Type type)
		{
			return null;
		}

		IList<T> IDependencyProvider.GetAll<T>()
		{
			return null;
		}
	}
}