using Color.Core.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Color.Core.Data.Mappings
{
	public class GameRoundMap : ClassMap<GameRound>
	{
		public GameRoundMap()
		{
			Id(x => x.Id);
			Map(x => x.Date).Not.Nullable();
			Map(x => x.Answer).Not.Nullable();
			Map(x => x.Score).Nullable();
			Map(x => x.FailedAttempts).Not.Nullable();

			References(g => g.Player).Column("PlayerId").ForeignKey().Not.Nullable().LazyLoad();
			HasMany(g => g.Choices).KeyColumns.Add("GameRoundId").AsBag().Inverse().Cascade.AllDeleteOrphan().ForeignKeyConstraintName("FK_GameRounds_GameRoundsChoices").LazyLoad();
		}
	}
}