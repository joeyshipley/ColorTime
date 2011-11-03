using Color.Core.Application.Services;
using Color.Core.Domain.Entities;
using Color.Core.Domain.Infrastructure;
using Color.Tests.Helpers.Builders;
using Color.Tests.Infrastructure;
using Machine.Specifications;

namespace Color.Tests.Unit.Application
{
	[Subject("Application, Game Round Scorer, Determining the Score")]
	public class when_a_correct_answer_has_been_choosen_with_no_failed_attempts : BaseSpec<GameRoundScorerSpecification>
	{
		private static int _score;
		private static Player _player;

		Establish context = () => _player = new PlayerBuilder("player 1").Build();

		Because of = () => 
		{
			var gameRound = new GameRoundBuilder(_player).Build();
			_score = Mocks.ClassUnderTest.DetermineScoreFromGameRound(gameRound);
		};

		It should_return_the_maximum_amount_of_points_earnable_for_a_round = () => _score.Equals(Constants.GameRoundMaximumPoints).ShouldBeTrue();
	}

	[Subject("Application, Game Round Scorer, Determining the Score")]
	public class when_a_correct_answer_has_been_choosen_with_some_failed_attempts : BaseSpec<GameRoundScorerSpecification>
	{
		private static int _score;
		private static Player _player;

		Establish context = () => _player = new PlayerBuilder("player 1").Build();

		Because of = () => 
		{
			var gameRound = new GameRoundBuilder(_player).WithFailedAttempts(1).Build();
			_score = Mocks.ClassUnderTest.DetermineScoreFromGameRound(gameRound);
		};

		It should_return_a_reduced_points_value_than_the_maximum_amount_earnable = () => (_score < Constants.GameRoundMaximumPoints).ShouldBeTrue();
	}

	[Subject("Application, Game Round Scorer, Determining the Score")]
	public class when_a_correct_answer_has_been_choosen_with_a_whole_lot_of_failed_attempts : BaseSpec<GameRoundScorerSpecification>
	{
		private static int _score;
		private static Player _player;

		Establish context = () => _player = new PlayerBuilder("player 1").Build();

		Because of = () => 
		{
			var gameRound = new GameRoundBuilder(_player).WithFailedAttempts(10).Build();
			_score = Mocks.ClassUnderTest.DetermineScoreFromGameRound(gameRound);
		};

		It should_return_some_score_points_instead_of_no_points = () => _score.Equals(Constants.GameRoundMinimumPoints).ShouldBeTrue();
	}
}