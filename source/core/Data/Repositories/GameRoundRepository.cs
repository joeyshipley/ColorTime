using Color.Core.Application.Repositories;
using Color.Core.Data.Infrastructure;
using Color.Core.Domain.Entities;

namespace Color.Core.Data.Repositories
{
	public class GameRoundRepository : BaseRepository<GameRound>, IGameRoundRepository
	{
		public GameRoundRepository(ISessionBuilder sessionBuilder)
			: base(sessionBuilder)
		{}
	}
}