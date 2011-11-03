using Color.Core.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Color.Core.Data.Mappings
{
	public class GameRoundChoiceMap : ClassMap<GameRoundChoice>
	{
		public GameRoundChoiceMap()
		{
			Table("GameRoundsChoices");
			Id(x => x.Id);
			Map(x => x.Choice).Not.Nullable();

			References(g => g.GameRound).Column("GameRoundId").Not.Nullable().ForeignKey("FK_GameRounds_GameRoundsChoices").LazyLoad();
		}
	}
}