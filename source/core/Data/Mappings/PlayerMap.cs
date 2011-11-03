using Color.Core.Domain.Entities;
using FluentNHibernate.Mapping;

namespace Color.Core.Data.Mappings
{
	public class PlayerMap : ClassMap<Player>
	{
		public PlayerMap()
		{
			Id(x => x.Id);
			Map(x => x.Name).Length(30).Not.Nullable();
		}
	}
}