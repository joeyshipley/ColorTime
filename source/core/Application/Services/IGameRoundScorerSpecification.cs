using Color.Core.Domain.Entities;

namespace Color.Core.Application.Services
{
	public interface IGameRoundScorerSpecification
	{
		int DetermineScoreFromGameRound(GameRound gameRound);
	}
}