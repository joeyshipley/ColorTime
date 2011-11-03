using Color.Core.Application.Infrastructure;
using Machine.Specifications;
using Rhino.Mocks;
using StructureMap.AutoMocking;

namespace Color.Tests.Infrastructure
{
	public abstract class BaseSpec<Target> 
		where Target : class
	{
		protected static AutoMockingContainer<Target> Mocks;
		
		Establish context = () => 
		{
			Mocks = new AutoMockingContainer<Target>(MockMode.AAA);
			Mocks.Get<IUnitOfWorkFactory>()
				.Stub(f => f.BeginTransaction())
				.Return(Mocks.Get<IUnitOfWork>());
		};

		protected static T GetStub<T>() 
			where T : class
		{
			return MockRepository.GenerateStub<T>();
		}
	}
}
