using Color.Core.Domain.Entities;
using Color.Core.Domain.Infrastructure;

namespace Color.Core.Application.Services
{
	public class GameRoundScorerSpecification : IGameRoundScorerSpecification
	{
		public int DetermineScoreFromGameRound(GameRound gameRound)
		{
			var penalty = gameRound.FailedAttempts * Constants.GameRoundFailedAttemptPenalty;
			var subTotal = Constants.GameRoundMaximumPoints - penalty;
			return subTotal > Constants.GameRoundMinimumPoints 
				? subTotal 
				: Constants.GameRoundMinimumPoints;
		}
	}
}