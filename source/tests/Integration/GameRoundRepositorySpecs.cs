using System;
using Color.Core.Application.Repositories;
using Color.Core.Data.Repositories;
using Color.Core.Domain.Entities;
using Color.Tests.Helpers.Builders;
using Color.Tests.Infrastructure;
using Machine.Specifications;

namespace Color.Tests.Integration
{
	[Subject("Data, Player Repository")]
	public class when_using_the_game_round_repository : BaseRepositorySpecs
	{
		private static IPlayerRepository _playerRepository;
		private static IGameRoundRepository _gameRoundRepository;
		private static Guid _gameRoundId;

		Establish context = () => 
		{
			_playerRepository = new PlayerRepository(SessionBuilder);
			_gameRoundRepository = new GameRoundRepository(SessionBuilder);
		};

		Because of = () => 
		{
			GameRound gameRound;
			using(var uow = UnitOfWorkFactory.BeginTransaction())
			{
				var player = new PlayerBuilder("Player Name").Build();
				_playerRepository.Save(player);

				gameRound = new GameRoundBuilder(player).Build();
				_gameRoundRepository.Save(gameRound);

				uow.Commit();
			}
			_gameRoundId = gameRound.Id;
		};

		It should_be_able_to_save_a_game_round = () => _gameRoundId.Equals(Guid.Empty).ShouldBeFalse();
	}
}