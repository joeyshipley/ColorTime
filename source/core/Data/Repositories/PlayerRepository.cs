using Color.Core.Application.Repositories;
using Color.Core.Data.Infrastructure;
using Color.Core.Domain.Entities;

namespace Color.Core.Data.Repositories
{
	public class PlayerRepository : BaseRepository<Player>, IPlayerRepository
	{
		public PlayerRepository(ISessionBuilder sessionBuilder)
			: base(sessionBuilder)
		{}
	}
}