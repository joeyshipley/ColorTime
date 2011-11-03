using System.Linq;
using Color.Core.Domain.Entities;
using Color.Tests.Helpers.Builders;
using Color.Tests.Infrastructure;
using Machine.Specifications;

namespace Color.Tests.Unit.Domain
{
	[Subject("Domain, Game Round, Creation")]
	public class when_creating_a_game_round : BaseSpec<GameRound>
	{
		private static GameRound _entity;

		Establish context;

		Because of = () => 
		{
			var player = new PlayerBuilder("player 1").Build();
			_entity = GameRound.CreateNewGameRoundFor(player);
		};

		It should_provide_3_color_choices_to_choose_from = () => _entity.GetChoices().Count().ShouldEqual(3);

		It should_know_the_answer_should_be_in_the_list_of_choices = () => _entity.GetChoices().Any(c => c == _entity.Answer).ShouldBeTrue();
	}
}