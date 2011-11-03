namespace tests.Infrastructure
{
	public interface IAutoMockingContainer<Target>
		where Target : class
	{
		Target Create();
		TMock GetMock<TMock>() where TMock : class;
		TStub GetStub<TStub>() where TStub : class;
	}
}