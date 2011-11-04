using System.Collections.Generic;
using Color.Core.Domain.Entities;
using Color.Tests.Helpers.Builders;
using Color.Tests.Infrastructure;
using Machine.Specifications;

namespace Color.Tests.Unit.Domain
{
	[Subject("Domain, Player, Score")]
	public class when_asking_for_a_players_score : BaseSpec<Player>
	{
		private static Player _entity;

		Establish context;

		Because of = () =>
		{
			_entity = Player.CreateFrom(new PlayerCreateCommand { Name = "Player 1" });
			var gameRounds = new List<GameRound>();
			gameRounds.Add(new GameRoundBuilder(_entity).WithScore(101).Build());
			gameRounds.Add(new GameRoundBuilder(_entity).WithScore(200).Build());
			gameRounds.Add(new GameRoundBuilder(_entity).WithScore(300).Build());
			gameRounds.Add(new GameRoundBuilder(_entity).WithScore(400).Build());
			_entity.GameRounds = gameRounds;
		};

		It should_return_the_combined_score_from_all_the_games_that_the_player_has_played = () => _entity.CalculateScore().ShouldEqual(1001);
	}
}