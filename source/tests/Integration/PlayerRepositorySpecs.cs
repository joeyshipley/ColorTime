using System;
using Color.Core.Application.Repositories;
using Color.Core.Data.Repositories;
using Color.Tests.Helpers.Builders;
using Color.Tests.Infrastructure;
using Machine.Specifications;

namespace Color.Tests.Integration
{
	[Subject("Data, Player Repository")]
	public class when_using_the_player_repository : BaseRepositorySpecs
	{
		private static IPlayerRepository _repository;
		private static Guid _newPlayerId;
		private static Guid _returnedPlayerId;

		Establish context = () => 
		{
			_repository = new PlayerRepository(SessionBuilder);
		};

		Because of = () => 
		{
			var player = new PlayerBuilder("Player Name").Build();
			using(var uow = UnitOfWorkFactory.BeginTransaction())
			{
				_repository.Save(player);
				uow.Commit();
			}
			_newPlayerId = player.Id;
			_returnedPlayerId = _repository.Get(_newPlayerId).Id;
		};

		It should_be_able_to_save_a_new_player = () => _newPlayerId.Equals(Guid.Empty).ShouldBeFalse();

		It should_be_able_to_fetch_an_existing_player = () => _returnedPlayerId.Equals(Guid.Empty).ShouldBeFalse();
	}
}